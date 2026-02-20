import logging
import csv
import os

from global_state import global_state

logger = logging.getLogger("mkdocs")


def define_env(env):
    @env.macro
    def global_nav():
        logger.log(logging.INFO, "Rendering global navigation tree...")
        nav_tree = global_state.get("nav_tree")
        if not nav_tree:
            logger.log(logging.ERROR, "No navigation tree in global state.")
            return "Unable to render navigation tree."

        result = "\n".join(render(env.conf, nav_tree))
        logger.log(logging.INFO, result)
        return result

    @env.macro
    def vocabulary_table(category):
        filename = f"{category}.csv"
        path = os.path.join(env.conf['docs_dir'], 'assets', 'vocabulary', filename)
        if not os.path.exists(path):
            logger.log(logging.ERROR, f"Vocabulary file not found: {path}")
            return f"**Error: Vocabulary file '{filename}' not found.**"

        try:
            with open(path, mode='r', encoding='utf-8') as f:
                reader = csv.DictReader(f)
                rows = list(reader)

            if not rows:
                return "_No words available._"

            headers = reader.fieldnames
            header_row = "| " + " | ".join(headers) + " |"
            separator_row = "| " + " | ".join(["---"] * len(headers)) + " |"
            
            md_rows = []
            for row in rows:
                md_rows.append("| " + " | ".join(row.get(h, "") for h in headers) + " |")

            return "\n".join([header_row, separator_row] + md_rows)
        except Exception as e:
            logger.log(logging.ERROR, f"Error rendering vocabulary table: {e}")
            return f"**Error rendering vocabulary table: {e}**"


def render(conf, items, indent=0):
    lines = []
    for item in items:
        if item.is_section:
            lines.append("    " * indent + f"- {item.title}")

            # Recursively render children.
            if item.children:
                lines.extend(render(conf, item.children, indent + 1))
        elif item.is_page:
            if not item.is_homepage:
                # Read the source for the page so we can get the title.
                item.read_source(conf)
                lines.append("    " * indent + f"- [{item.title}]({item.file.src_uri})")
        else:
            raise Exception(f"Unexpected item type: {type(item)}")

    return lines
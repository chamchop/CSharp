﻿using System.Text;

namespace DesignPatterns.Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        HtmlElement() {}

        HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException("name");
            Text = text ?? throw new ArgumentNullException("text");
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var element in Elements)
            {
                sb.Append(element.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"{i}<{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }

        public class HtmlBuilder
        {
            private readonly string rootName;
            HtmlElement root = new HtmlElement();

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }

            public HtmlBuilder AddChild(string childName, string childText)
            {
                var element = new HtmlElement(childName, childText);
                root.Elements.Add(element);
                return this;
            }
            public void Clear()
            {
                root = new HtmlElement { Name = rootName };
            }
        }

        public class Execution
        {
            public static void Exe()
            {
                var hello = "hello";
                var sb = new StringBuilder();
                sb.Append("<p>");
                sb.Append(hello);
                sb.Append("/<p>");
                Console.WriteLine(sb);

                var words = new[] { "hello", "world" };
                sb.Clear();
                sb.Append("<ul>");

                foreach (var word in words)
                {
                    sb.AppendFormat("<li>{0}</li>", word);
                }

                sb.Append("</ul>");
                Console.WriteLine(sb);

                var builder = new HtmlBuilder("ul");
                builder.AddChild("li", "hello").AddChild("li", "world");
                Console.WriteLine(builder.ToString());
            }
        }
    }
}

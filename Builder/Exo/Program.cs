using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Exo
{
    public class CodeElement
    {
        public string ParamName, ParamType;
        public List<CodeElement> Elements = new List<CodeElement>();
        public const int IndentSize = 2;
        public const string Visibility = "private";

        public CodeElement()
        {

        }

        public CodeElement(string paramName, string paramType)
        {
            ParamName = paramName ?? throw new ArgumentNullException(nameof(paramName));
            ParamType = paramType ?? throw new ArgumentNullException(nameof(paramType));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);

            sb.Append(string.IsNullOrEmpty(ParamType)
                ? $"{i}{Visibility} class {ParamName} \n{i}{{\n"
                : $"{i}{Visibility} {ParamType} {ParamName};\n");

            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }

            if (string.IsNullOrEmpty(ParamType))
                sb.AppendLine($"{i}}}");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class CodeBuilder
    {
        private readonly string _className;
        CodeElement code = new CodeElement();

        public CodeBuilder(string className)
        {
            this._className = className;
            code.ParamName = className;

        }

        public CodeBuilder AddField(string paramName, string paramType)
        {
            var c = new CodeElement(paramName, paramType);
            code.Elements.Add(c);
            return this;
        }

        public override string ToString()
        {
            return code.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            WriteLine(cb.ToString());
            Read();
        }
    }
}

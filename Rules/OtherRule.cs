using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JavaToCSharp.Rules
{
    class NPOINamespaceRule:EquivalentRule
    {
        public override string RuleName
        {
            get
            {
                return "namespace";
            }
        }

        public override string Pattern
        {
            get { return @"org\.apache\.poi\.([a-zA-Z]+)\."; }
        }

        public override string Replacement
        {
            get { return "namespace"; }
        }
        protected override string ReplaceString(System.Text.RegularExpressions.Match match)
        {
            return "NPOI." + match.Groups[1].Value.ToUpper() + ".";
        }
    }

    public class EquivalentRule1 : EquivalentRule
    {
        public override string RuleName
        {
            get
            {
                return "new Double";
            }
        }

        public override string Replacement
        {
            get { return "new Double"; }
        }

        public override string Pattern
        {
            get { return @"new Double\((.+)\)"; }
        }

        protected override string ReplaceString(Match match)
        {
            return match.Groups[1].Value;
        }
    }
    public class EquivalentRule2 : EquivalentRule
    {
        public override string Replacement
        {
            get { return ".charAt(i)"; }
        }

        public override string Pattern
        {
            get { return @"\.charAt\((\w+)\)"; }
        }

        protected override string ReplaceString(Match match)
        {
            return string.Format("[{0}]", match.Groups[1].Value);
        }
    }


    public class EquivalentRule3 : EquivalentRule
    {
        public override string Replacement
        {
            get { return "read"; }
        }

        public override string Pattern
        {
            get { return @"(\s|\.)(read\S)"; }
        }

        protected override string ReplaceString(Match match)
        {
            string strTemp = match.Groups[2].Value;
            return match.Groups[1].Value + char.ToUpper(strTemp[0]) + strTemp.Substring(1, strTemp.Length - 1);
        }
    }

    internal class GetterRule : EquivalentRule
    {
        private List<string> exclude = new List<string>();

        public GetterRule()
        {
            string getter = ConfigurationManager.AppSettings["getter"];
            exclude.AddRange(getter.Split(",".ToCharArray()));
        }

        public override string RuleName
        {
            get { return "Getter"; }
        }

        public override string Pattern
        {
            get { return @"\.get([a-zA-Z0-9]+)\(\)"; }
        }

        public override string ReplaceString(Match match)
        {
            if (exclude.Contains(match.Groups[1].Value))
                return ".Get" + match.Groups[1].Value + "()";
            return "." + match.Groups[1].Value;
        }
    }

    internal class SetterRule : EquivalentRule
    {
        private List<string> exclude = new List<string>();

        public SetterRule()
        {
            string setter = ConfigurationManager.AppSettings["setter"];
            exclude.AddRange(setter.Split(",".ToCharArray()));
        }

        public override string RuleName
        {
            get { return "setter"; }
        }

        public override string Pattern
        {
            get { return @"\.set([a-zA-Z0-9]+)\((.*?)\)"; }
        }

        public override string ReplaceString(Match match)
        {
            if (exclude.Contains(match.Groups[1].Value))
                return ".Set" + match.Groups[1].Value + "(" + match.Groups[2].Value + ")";
            return "." + match.Groups[1].Value + "=(/*setter*/" + match.Groups[2].Value + ")";
        }
    }

    internal class TypeOfRule : EquivalentRule
    {
        public override string RuleName
        {
            get { return "XXXX.class"; }
        }

        public override string Pattern
        {
            get { return @"\(([a-zA-Z0-9]+)\.class"; }
        }

        public override string ReplaceString(Match match)
        {
            return "(typeof(" + match.Groups[1].Value + ")";
        }
    }

    internal class ImportRule : EquivalentRule
    {
        public override string RuleName
        {
            get { return "import"; }
        }

        public override string Pattern
        {
            get { return @"import\s(.*?)\.([a-zA-Z0-9]+|\*);"; }
        }

        public override string ReplaceString(Match match)
        {
            if (match.Groups[1].Value.StartsWith("junit."))
            {
                return "using Microsoft.VisualStudio.TestTools.UnitTesting;";
            }
            else
            {
                return "using " + match.Groups[1].Value + ";";
            }
        }
    }

    internal class PackageRule : EquivalentRule
    {
        public override string RuleName
        {
            get { return "package"; }
        }

        public override string Pattern
        {
            get { return @"package\s(.*?);"; }
        }

        public override string ReplaceString(Match match)
        {
            return "namespace " + match.Groups[1].Value + "\r\n{\r\n    using System;";
        }
    }

    internal class AssertAreEqualRule : EquivalentRule
    {
        public override string RuleName
        {
            get { return "Assert.AreEqual"; }
        }

        public override string Pattern
        {
            get { return @"Assert.AreEqual\(("".*?"".*?),(.*?),(.*?)\);"; }
        }

        public override string ReplaceString(Match match)
        {
            return "Assert.AreEqual(" + match.Groups[2].Value + "," + match.Groups[3].Value + "," + match.Groups[1].Value + ");";
        }
    }

    internal class TestClassRule : EquivalentRule
    {
        public override string RuleName
        {
            get { return "TestFixture Attribute"; }
        }

        public override string Pattern
        {
            get { return @"(.*?)\sclass\s(.*?)Test(.*?)\s{"; }
        }

        public override string ReplaceString(Match match)
        {
            return "    [TestFixture]\r\n    " + match.Groups[1].Value + " class " + match.Groups[2].Value + "Test" + match.Groups[3].Value + "\r\n{";
        }
    }

    internal class TestMethodRule : EquivalentRule
    {
        public override string RuleName
        {
            get { return "TestMethod Attribute"; }
        }

        public override string Pattern
        {
            get { return @"(public.*?)(t|T)est([a-zA-Z0-9_]+)\(\)\s(.*){"; }
        }

        public override string ReplaceString(Match match)
        {
            return "    [Test]\r\n    public void Test" + match.Groups[3].Value + "(){";
        }
    }

    internal class InterfaceVariantRule : EquivalentRule
    {
        public override string RuleName
        {
            get { return "IXXXX xxx = "; }
        }

        public override string Pattern
        {
            get { return @"\s(HSSF)?(Workbook|Sheet|Cell|Row|Name|Header|Footer|CellStyle|CellType)(\s+|\.)(.*?)="; }
        }

        public override string ReplaceString(Match match)
        {
            return " I" + match.Groups[2].Value + match.Groups[3].Value + match.Groups[4].Value + "=";
        }
    }

    internal class KeyVariantRule : EquivalentRule
    {
        public override string RuleName
        {
            get
            {
                return "in_out_ref";
            }
        }

        public override string Pattern
        {
            get
            {
                return @"(\(|\s)(in|out|ref|is)(\s=|\)|\.)";
            }
        }

        public override string ReplaceString(Match match)
        {
            return match.Groups[1].Value + match.Groups[2].Value + "1" + match.Groups[3].Value;
        }
    }

    internal class CapitalizeRule2 : EquivalentRule
    {
        public override string RuleName
        {
            get { return "CapitalizeRule2"; }
        }

        public override string Pattern
        {
            get { return @"(\w)\.([a-z])([a-zA-Z_0-9]+?)\("; }
        }

        public override string ReplaceString(Match match)
        {
            return match.Groups[1].Value + "." + match.Groups[2].Value.ToUpper() + match.Groups[3].Value + "(";
        }
    }
}

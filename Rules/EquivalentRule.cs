using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JavaToCSharp.Rules
{
    public class EquivalentRule : Rule
    {
        public EquivalentRule()
        { 
            
        }
        public EquivalentRule(string name)
        {
            this._name = name;
        }

        string _name;
        public override string RuleName
        {
            get { return _name; }
        }

        protected virtual string Pattern
        {
            get;
            set;
        }

        public string PatternValue
        {
            set { this.Pattern = value; }
        }
        
        protected virtual string Replacement
        {
            get;
            set;
        }

        public string ReplacementValue
        {
            set { this.Replacement = value; }
        }

        protected virtual string ReplaceString(Match match)
        {
            return this.Replacement;
        }

        public override sealed bool Execute(string strOrigin, out string strOutput, int iRowNumber)
        {
            Regex regex = new Regex(this.Pattern);
            string result=strOrigin;
            bool changedFlag = false;

            if (regex.IsMatch(strOrigin))
            {
                result= regex.Replace(strOrigin, new MatchEvaluator(ReplaceString));
                changedFlag = true;
            }
            strOutput = result;
            return changedFlag;
        }
        public override string ToString()
        {
            return string.Format("Equivalent Rule '{0}'", this.RuleName);
        }
    }
}

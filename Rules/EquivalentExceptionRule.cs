using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JavaToCSharp.Rules
{
    public abstract class EquivalentExceptionRule : EquivalentRule
    {
        protected override string Replacement
        {
            get { return this.Pattern; }
        }
    }


    public class EquivalentRuleException1 : EquivalentExceptionRule
    {

        protected override string Pattern
        {
            get { return @"RuntimeException"; }
        }

        protected override string ReplaceString(Match match)
        {
            return "Exception";
        }
    }

    public class EquivalentRuleException2 : EquivalentExceptionRule
    {

        protected override string Pattern
        {
            get { return @"ClassCastException"; }
        }

        protected override string ReplaceString(Match match)
        {
            return "InvalidCastException";
        }
    }

    public class EquivalentRuleException3 : EquivalentExceptionRule
    {

        protected override string Pattern
        {
            get { return @"UnsupportedEncodingException"; }
        }

        protected override string ReplaceString(Match match)
        {
            return "EncoderFallbackException";
        }
    }

    public class EquivalentRuleException4 : EquivalentExceptionRule
    {

        protected override string Pattern
        {
            get { return @"AssertionFailedError"; }
        }

        protected override string ReplaceString(Match match)
        {
            return "AssertFailedException";
        }
    }

    public class EquivalentRuleException5 : EquivalentExceptionRule
    {

        protected override string Pattern
        {
            get { return @"IllegalArgumentException"; }
        }

        protected override string ReplaceString(Match match)
        {
            return "ArgumentException";
        }
    }
    public class EquivalentRuleException6 : EquivalentExceptionRule
    {

        protected override string Pattern
        {
            get { return @"NumberFormatException"; }
        }

        protected override string ReplaceString(Match match)
        {
            return "FormatException";
        }
    }
    public class EquivalentRuleException7 : EquivalentExceptionRule
    {

        protected override string Pattern
        {
            get { return @"IllegalAccessError|IllegalStateException"; }
        }

        protected override string ReplaceString(Match match)
        {
            return "InvalidOperationException";
        }
    }
    
}

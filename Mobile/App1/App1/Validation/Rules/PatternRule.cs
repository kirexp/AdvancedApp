using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace App1.Validation.Rules
{
    public class PatternRule: IValidationRule<string>
    {
        public string ValidationMessage { get; set; }
        public string RegexPattern { get; set; }
        public PatternRule(string pattern,string validationMessage)
        {
            ValidationMessage = validationMessage;
            this.RegexPattern = pattern;
        }
        public bool Check(string value) {
            var regex = new Regex(this.RegexPattern);
            if (regex.IsMatch(value))
                return true;
                return false;

        }
    }
}

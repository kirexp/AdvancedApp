using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Validation.Rules
{
    public class MinLengthRule<T>: IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public MinLengthRule(string validationMessage)
        {
            ValidationMessage = validationMessage;
        }
        public bool Check(T value) {
            if ((value as string).Length < 10) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}

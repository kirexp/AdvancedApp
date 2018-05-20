

using App1.Helpers;

namespace App1.Validation.Rules
{
    public class IsNotNullReferenceItemRule<T> : IValidationRule<ReferenceItem> {
        public string ValidationMessage { get; set; }

        public bool Check(ReferenceItem value) {
            if (value == null) {
                return false;
            }
            return true;
        }

        public IsNotNullReferenceItemRule(string validationMessage) {
            ValidationMessage = validationMessage;
        }

        public IsNotNullReferenceItemRule() : this("Поле обязательно для ввода") {

        }
    }
}
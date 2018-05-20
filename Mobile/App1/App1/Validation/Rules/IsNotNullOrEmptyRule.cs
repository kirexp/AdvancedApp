using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Validation.Rules {
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T> {
        public string ValidationMessage { get; set; }

        public bool Check(T value) {
            if (value == null) {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }

        public IsNotNullOrEmptyRule(string validationMessage) {
            ValidationMessage = validationMessage;
        }

        public IsNotNullOrEmptyRule() : this("Поле обязательно для ввода") {

        }
    }
}

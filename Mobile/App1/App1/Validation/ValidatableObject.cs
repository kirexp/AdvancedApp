using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Helpers;

namespace App1.Validation
{
    public class ValidatableObject<T> : ObservableObject, IValidity {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid;
        private bool _isNotValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors {
            get {
                return _errors;
            }
            set { SetProperty(ref _errors, value); }
        }

        public T Value {
            get {
                return _value;
            }
            set { SetProperty(ref _value, value); }
        }

        public bool IsValid {
            get {
                return _isValid;
            }
            set {
                SetProperty(ref _isValid, value);
                IsNotValid = !value;
            }
        }

        public bool IsNotValid {
            get { return _isNotValid; }
            set { SetProperty(ref _isNotValid, value); }
        }

        public ValidatableObject() {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public ValidatableObject(T defaultValue) : this() {
            Value = defaultValue;
        }

        public bool Validate() {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}


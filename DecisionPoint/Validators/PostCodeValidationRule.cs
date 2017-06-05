using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Globalization;


namespace DecisionPoint.Validators
{
    public class PostalCodeValidationRule : ValidationRule
    {
        private string errorMessage = "PostalCode is invalid ";

        public PostalCodeValidationRule() { }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputString = (value == null) ? string.Empty : value.ToString();

            inputString = inputString.TrimStart();
            inputString = inputString.TrimEnd();
            inputString = inputString.ToLower();

            bool isValid = (inputString.Contains("o") || inputString.Contains("i")) ? false : true;

            if (Regex.IsMatch(inputString, @"^[abceghjklmnprstvxy]\d[a-z]\d[a-z]\d$") && isValid)
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, this.ErrorMessage);
            }
        }
    }
}

using System;
using System.Net.Mail;
using System.Windows.Controls;

namespace LapinCretinsFormes
{
    class EmailValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool result = Validate(value);

            if (result)
                return new ValidationResult(true, null);
            return new ValidationResult(false, string.Format("\"{0}\" n'est pas une adresse mail valide.", value as string));

        }

        public static bool Validate(object value)
        {
            string emailAddress = (value as string);

            if (string.IsNullOrEmpty(emailAddress))
                return true;

            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

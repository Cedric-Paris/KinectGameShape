using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LapinCretinsFormes
{
    class EmailValidationRules : ValidationRule
    {
        public EmailValidationRules() { }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool result = Validate(value);

            if (result)
                return new ValidationResult(true, null);
            return new ValidationResult(false, $"\"{value as string}\" n'est pas une adresse mail valide.");

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
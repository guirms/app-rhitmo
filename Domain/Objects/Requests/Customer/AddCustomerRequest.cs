using Domain.Models.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Objects.Requests.Usuario;

public class AddCustomerRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Cpf { get; set; }
    public required string Address { get; set; }
    public required string State { get; set; }
    public required string Cep { get; set; }
    public required string City { get; set; }
    public EPaymentMethod PaymentMethod { get; set; }
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? SecurityCode { get; set; }

    public class SaveCustomerRequestValidator : AbstractValidator<AddCustomerRequest>
    {
        public SaveCustomerRequestValidator()
        {
            RuleFor(s => s.Email)
                .NotEmpty()
                .Must(HasValidEmail)
                .WithMessage("E-mail inválido");

            RuleFor(s => s.Cpf)
                .NotEmpty()
                .Must(HaveValidCpf)
                .WithMessage("CPF inválido");

            RuleFor(s => s.Cep)
                .NotEmpty()
                .Must(HaveValidCep)
                .WithMessage("CEP inválido");

            RuleFor(s => s)
                .NotEmpty()
                .Must(HaveValidCreditCard)
                .WithMessage("Cartão de crédito inválido");
        }

        private bool HaveValidCreditCard(AddCustomerRequest request)
        {
            if (request.PaymentMethod == EPaymentMethod.BankSlip)
                return true;

            var cardNumber = request.CardNumber ?? string.Empty;
            var expirationDate = request.ExpirationDate?.ToString("MM/yyyy") ?? string.Empty;
            var cvv = request.SecurityCode ?? string.Empty;

            if (!Regex.IsMatch(cardNumber, @"^\d{16}$"))
                return false;

            if (!Regex.IsMatch(cvv, @"^\d{3}$"))
                return false;

            if (!Regex.IsMatch(expirationDate, @"^(0[1-9]|1[0-2])/\d{4}$"))
                return false;

            var parts = expirationDate.Split('/');
            var expirationMonth = int.Parse(parts[0]);
            var expirationYear = int.Parse(parts[1]);

            var currentDate = DateTime.Now;
            var expiration = new DateTime(expirationYear, expirationMonth, DateTime.DaysInMonth(expirationYear, expirationMonth));
            if (expiration <= currentDate)
                return false;

            return true;
        }

        private static bool HasValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
                return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private static bool HaveValidCpf(string cpf)
        {
            var multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cleanCpf = cpf;

            cleanCpf.Trim();
            cleanCpf = cpf.Replace(".", "").Replace("-", "");

            if (cleanCpf.Length != cpf.Length)
                return false;

            if (cleanCpf.Length != 11)
                return false;

            string? tempCpf = cleanCpf.Substring(0, 9);
            var sum = 0;

            for (var i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            }

            var remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            string? digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;
            for (var i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            }

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit += remainder.ToString();

            return cleanCpf.EndsWith(digit);
        }

        public static bool HaveValidCep(string cep)
        {
            var cleanCEP = Regex.Replace(cep, @"[^\d]", "");

            if (cleanCEP.Length != cep.Length)
                return false;

            if (cleanCEP.Length != 8)
                return false;

            if (cleanCEP.All(c => c == cleanCEP[0]))
                return false;

            return true;
        }
    }
}
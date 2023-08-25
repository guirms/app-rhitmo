using Domain.Models.Enums;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Application.Objects.Requests.Usuario;

public class SaveCustomerRequest
{
    public required string Name { get; set; }
    [EmailAddress(ErrorMessage = "Email em formato inválido")]
    public required string Email { get; set; }
    public required string Cpf { get; set; }
    public required string Address { get; set; }
    public required string State { get; set; }
    public required string Cep { get; set; }
    public required string City { get; set; }
    public EPaymentMethod PaymentMethod { get; set; }
    public string? CardName { get; set; }
    [CreditCard(ErrorMessage = "Cartão de crédito inválido")]
    public string? CardNumber { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? SecurityCode { get; set; }

    public class SaveCustomerRequestValidator : AbstractValidator<SaveCustomerRequest>
    {
        public SaveCustomerRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(HaveValidCpf)
                .WithMessage("CPF inválido");
        }

        private static bool HaveValidCpf(string cpf)
        {
            var multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            string? tempCpf = cpf.Substring(0, 9);
            var sum = 0;

            for (var i = 0; i < 9; i++) { 
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
            for (var i = 0; i < 10; i++) { 
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            }

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit += remainder.ToString();

            return cpf.EndsWith(digit);
        }

    }
}
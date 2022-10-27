using FluentValidation.Results;

namespace Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();

        public BadRequestException(string prop, string message) : this()
        {
            Errors.Add(new ValidationFailure(prop, message));
        }

        public BadRequestException(ValidationResult validationResult) : this()
        {
            Errors = validationResult.Errors;
        }

        public BadRequestException() : base("Requisição inválida por favor verifique os dados e tente novamente.")
        {
        }
    }
}
using FluentValidation.Results;

namespace Application.Interfaces
{
    public interface IRequestValidator<TModel>
    {
        Task<ValidationResult> ValidateAsync(TModel model, CancellationToken token = default);
    }
}
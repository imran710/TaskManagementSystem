using Library.Results;
using Shared.Library.Results;

namespace TaskManagement.Api.Presentation.Exeption;

public class DomainException(Error error) : Exception
{
    public Error Error { get; init; } = error;

    public static DomainException Create(ValidationError validationError) => new(Error.Validation(validationError));
    public static DomainException Create(Error error) => new(error);
}

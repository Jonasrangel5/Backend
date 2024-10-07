using FluentValidation.Results;

namespace Fisoterapia.Models.ResponseError
{
    public class BadRequestException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        // Constructor por defecto que establece un mensaje general
        public BadRequestException()
            : base("Se han producido uno o más errores de validación.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        // Constructor que permite pasar un mensaje personalizado
        public BadRequestException(string message)
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }

        // Constructor que toma una colección de errores de validación y los almacena
        public BadRequestException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(
                    failureGroup => failureGroup.Key,
                    failureGroup => failureGroup.ToArray()
                );
        }
    }
}

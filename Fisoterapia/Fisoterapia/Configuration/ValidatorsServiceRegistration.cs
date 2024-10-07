using FluentValidation.AspNetCore;
using FluentValidation;
using Fisoterapia.Request.TipoUsuarios;

namespace Fisoterapia.Configuration
{
    public static class ValidatorsServiceRegistration
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<TipoUsuarioEditarValidator>();
            services.AddValidatorsFromAssemblyContaining<TipoUsuarioCreateValidator>();
            services.AddFluentValidationClientsideAdapters();

        }
    }
}

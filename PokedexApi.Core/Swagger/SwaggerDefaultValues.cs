using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace PokedexApi.Core.Swagger
{
    /// <summary>
    ///     Operation filter to set default swagger values
    /// </summary>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        ///     Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The <see cref="OpenApiOperation">operation</see> to apply the filter to.</param>
        /// <param name="context">The current operation filter <see cref="OperationFilterContext">context</see>.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null) return;

            var attributes = context.ApiDescription.CustomAttributes().ToArray();
            if (attributes.OfType<AllowAnonymousAttribute>().Any())
                // Controller / action allows anonymous calls
                return;

            var authorizeAttributes = attributes.OfType<AuthorizeAttribute>().ToArray();
            if (authorizeAttributes.Length == 0) return;
        }
    }
}

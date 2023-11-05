using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NotesApi.Filters;

public class SessionTokenHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        if (!context.ApiDescription.RelativePath.Contains("users/login", StringComparison.OrdinalIgnoreCase))
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "x-session-id",
                In = ParameterLocation.Header,
                Description = "SessionToken",
                Required = false,
                Schema = new OpenApiSchema { Type = "String" }
            });
        }
    }
}

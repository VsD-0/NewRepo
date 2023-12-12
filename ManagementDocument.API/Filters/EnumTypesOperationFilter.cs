using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ManagementDocument.API.Filters
{
    public class EnumTypesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var item in operation.Parameters)
            {
                if (item.Name == "sort")
                {
                    item.Style = ParameterStyle.Simple;
                    item.Explode = false;
                }
            }
        }
    }
}

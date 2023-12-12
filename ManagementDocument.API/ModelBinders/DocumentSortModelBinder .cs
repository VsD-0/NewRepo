using ManagementDocument.API.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ManagementDocument.API.ModelBinders
{
    public class DocumentSortModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            string[] parsedValues = value.Split(',');

            var enumValues = parsedValues.Select(parsedValue =>
            {
                if (Enum.TryParse<DocumentSortField>(parsedValue, ignoreCase: true, out var enumValue))
                {
                    return enumValue;
                }
                return (DocumentSortField?)null;
            }).Where(v => v != null).Cast<DocumentSortField>().ToArray();

            bindingContext.Result = ModelBindingResult.Success(enumValues);

            return Task.CompletedTask;
        }
    }
}

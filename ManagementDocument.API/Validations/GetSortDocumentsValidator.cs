using FluentValidation;
using ManagementDocument.API.Commands;
using ManagementDocument.API.Enums;
using System.Reflection.Metadata;

namespace ManagementDocument.API.Validations
{
    public class GetSortDocumentsValidator : AbstractValidator<GetSortDocumentsCommand>
    {
        public GetSortDocumentsValidator()
        {
            RuleFor(x => x.Params)
               .Must(IsExistParam)
               .WithMessage("Один или несколько полей сортировки недопустимы.");
        }

        private bool IsExistParam(DocumentSortField[] sorts)
        {
            // Все свойства типа Document
            List<string> docParams = new();
            foreach (var param in typeof(Document).GetProperties()) docParams.Add(param.Name);

            // Список параметров сортировки, которые есть в docParams
            var validSorts = sorts.Where(sort => docParams.Contains(sort.ToString()[0] == '_' ? sort.ToString().Remove(0, 1) : sort.ToString())).ToList();

            return validSorts.Count < sorts.Length;
        }
    }
}

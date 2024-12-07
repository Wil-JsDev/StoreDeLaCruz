using FluentValidation;
using StoreDeLaCruz.Core.Aplication.DTOs.Folder;

namespace StoreDeLaCruz.Validation
{
    public class ValidationFolderDTos : AbstractValidator<FolderInsertDTos>
    {
        public ValidationFolderDTos()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Es obligatorio llenar el campo");
            RuleFor(x => x.Name).MinimumLength(1).WithMessage("Debe de ser mayor a uno");

        }
    }
}

using FluentValidation;
using StoreDeLaCruz.Core.Aplication.DTOs.Folder;

namespace StoreDeLaCruz.Validation
{
    public class ValidationFolderUpdate : AbstractValidator<FolderUpdate>
    {
        public ValidationFolderUpdate()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Es obligatorio");
            RuleFor(x => x.Name).MinimumLength(1).WithMessage("Debe de ser mayor a uno");
        }
    }
}

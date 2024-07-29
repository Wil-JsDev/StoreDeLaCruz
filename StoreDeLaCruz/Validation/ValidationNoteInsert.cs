using FluentValidation;
using StoreDeLaCruz.Core.Aplication.DTOs.Nota;

namespace StoreDeLaCruz.Validation
{
    public class ValidationNoteInsert : AbstractValidator<NotaInsertDTos>
    {
        public ValidationNoteInsert()
        {
            RuleFor(x => x.FolderId).NotEmpty().WithMessage("No se puede dejar este campo vacio");
            RuleFor(x => x.Titulo).NotEmpty().MinimumLength(10).WithMessage("Mas de 10 caracteres");
            RuleFor(x => x.Contenido).NotEmpty().WithMessage("Es obligatorio asignar un contenido");
            RuleFor(x => x.PrioridadTarea).NotEmpty().WithMessage("Debemos de saber tu prioridad");
        }
    }
}

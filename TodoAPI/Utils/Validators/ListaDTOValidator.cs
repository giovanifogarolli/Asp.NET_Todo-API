using FluentValidation;
using TodoAPI.DTOs.ListaDTO;

namespace TodoAPI.Utils.Validators;

internal sealed class ListaDTOValidator : AbstractValidator<CreateListaDTO>
{

    public ListaDTOValidator()
    {
        RuleFor(l => l.titulo)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("Titulo é obrigatório")
            .Length(5, 50).WithMessage("TItulo deve ter de 5 a 50 caracteres.");
    }
}

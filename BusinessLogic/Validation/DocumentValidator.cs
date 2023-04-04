
using CuentasPorCobrar.Shared;
using FluentValidation;

namespace CuentasPorCobrar.Shared;
public class DocumentValidator: AbstractValidator<Document>
{

    public DocumentValidator()
    {
        RuleFor(d => d.Description)
            .NotEmpty()
            .WithMessage("The document description cannot be empty");


        RuleFor(d => d.LedgerAccount)
            .NotEmpty()
            .WithMessage("It cannot be empty"); 


    }



}


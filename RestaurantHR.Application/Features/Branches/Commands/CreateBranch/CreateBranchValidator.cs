using FluentValidation;

namespace RestaurantHR.Application.Features.Branches.Commands.CreateBranch;

public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Branch name is required")
            .MaximumLength(100);

        RuleFor(r => r.Code)
            .NotEmpty().WithMessage("Branch code is required")
            .MaximumLength(20);

        RuleFor(r => r.Address)
            .MaximumLength(500);

        RuleFor(r => r.Phone)
            .MaximumLength(20);
    }
}

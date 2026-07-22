using FluentAssertions;
using FluentValidation.TestHelper;
using RestaurantHR.Application.Features.Branches.Commands.CreateBranch;

namespace RestaurantHR.Tests.Application;

public class CreateBranchValidatorTests
{
    private readonly CreateBranchValidator _validator = new();

    [Fact]
    public void ValidCommand_PassesValidation()
    {
        var command = new CreateBranchCommand
        {
            Name = "Test Branch",
            Code = "TST-01",
            Address = "Test Address",
            Phone = "02112345678"
        };

        var result = _validator.TestValidate(command);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void EmptyName_FailsValidation()
    {
        var command = new CreateBranchCommand { Name = "", Code = "C1" };
        var result = _validator.TestValidate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void EmptyCode_FailsValidation()
    {
        var command = new CreateBranchCommand { Name = "Test", Code = "" };
        var result = _validator.TestValidate(command);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Code");
    }

    [Fact]
    public void LongName_FailsMaximumLength()
    {
        var command = new CreateBranchCommand
        {
            Name = new string('x', 101),
            Code = "C1"
        };
        var result = _validator.TestValidate(command);
        result.IsValid.Should().BeFalse();
    }
}

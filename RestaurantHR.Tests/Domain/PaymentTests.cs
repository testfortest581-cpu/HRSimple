using FluentAssertions;
using RestaurantHR.Domain.Entities;
using RestaurantHR.Domain.Enums;

namespace RestaurantHR.Tests.Domain;

public class PaymentTests
{
    [Fact]
    public void CreatePayment_SetsProperties()
    {
        var payment = new Payment
        {
            EmployeeId = Guid.NewGuid(),
            Amount = 5000000,
            PaymentDate = new DateTime(2025, 2, 1),
            PaymentType = PaymentType.BankTransfer,
            Description = "Feb 2025 Salary"
        };

        payment.Amount.Should().Be(5000000);
        payment.PaymentType.Should().Be(PaymentType.BankTransfer);
        payment.SalaryId.Should().BeNull();
    }
}

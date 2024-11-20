namespace Cloudio.NetCore.App.Test.Models.Units;

using Cloudio.Core.Models;

public class ElementTests
{
    [Fact]
    internal void Elements_of_different_types_should_not_be_equal()
    {
        // setup
        Address address = new()
        {
            City = "Shiraz",
            State = "Fars",
            PostalCode = "12345"
        };
        Price price = new()
        {
            Currency = "Rial",
            Value = decimal.Zero
        };

        // verify
        (address == price).Should().BeFalse();
        (address != price).Should().BeTrue();

        address.Equals(price).Should().BeFalse();
        price.Equals(address).Should().BeFalse();

        (address.GetHashCode() == price.GetHashCode()).Should().BeFalse();
    }

    [Fact]
    internal void Elements_of_same_types_should_be_equal_when_values_match()
    {
        // setup
        Address addressA = new()
        {
            City = "Shiraz",
            State = "Fars",
            PostalCode = "12345"
        };
        Address addressB = new()
        {
            City = "Shiraz",
            State = "Fars",
            PostalCode = "12345"
        };

        // verify
        (addressA == addressB).Should().BeTrue();
        (addressA != addressB).Should().BeFalse();

        addressA.Equals(addressB).Should().BeTrue();
        addressB.Equals(addressA).Should().BeTrue();

        (addressA.GetHashCode() == addressB.GetHashCode()).Should().BeTrue();
    }

    [Fact]
    internal void Elements_of_same_types_should_not_be_equal_when_values_not_match()
    {
        // setup
        Address addressA = new()
        {
            City = "Saqqez",
            State = "Kurdistan",
            PostalCode = "12345"
        };
        Address addressB = new()
        {
            City = "Shiraz",
            State = "Fars",
            PostalCode = "12345"
        };

        // verify
        (addressA == addressB).Should().BeFalse();
        (addressA != addressB).Should().BeTrue();

        addressA.Equals(addressB).Should().BeFalse();
        addressB.Equals(addressA).Should().BeFalse();

        (addressA.GetHashCode() == addressB.GetHashCode()).Should().BeFalse();
    }

    #region Configs

    class Address : Element
    {
        public required string City { get; init; }
        public required string State { get; init; }
        public required string PostalCode { get; init; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return State;
            yield return PostalCode;
        }
    }
    class Price : Element
    {
        public required string Currency { get; init; }
        public required decimal Value { get; init; }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return Value;
        }
    }

    #endregion
}

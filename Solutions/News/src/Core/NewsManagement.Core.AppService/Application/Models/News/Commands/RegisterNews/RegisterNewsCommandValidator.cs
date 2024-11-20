namespace NewsManagement.Core.News.AppServices;

using FluentValidation;
using Cloudio.Web.Core.AppService;
using NewsManagement.Core.News.Contracts;

public sealed class RegisterNewsCommandValidator : Validator<RegisterNewsCommand>
{
    protected override void Initialize()
    {
        ValidateTitle();
        ValidateDescription();
        ValidateBody();
        ValidateKeywordsCodes();
    }

    #region Methods

    private void ValidateTitle()
    {
        var property = "Title";
        var minChar = 3;
        var maxChar = 250;

        RuleFor(e => e.Title)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    private void ValidateDescription()
    {
        var property = "Description";
        var minChar = 3;
        var maxChar = 500;

        RuleFor(e => e.Description)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    private void ValidateBody()
    {
        var property = "Body";

        RuleFor(e => e.Body)
        .NotEmpty().WithMessage($"{property} is required!");
    }

    private void ValidateKeywordsCodes()
    {
        var property = "KeywordsCodes";

        RuleFor(e => e.KeywordsCodes)
        .NotNull().WithMessage($"{property} is required!")
        .Must(e => e.Any()).WithMessage($"It is mandatory to select at least one keyword for {property}.");
    }

    #endregion
}
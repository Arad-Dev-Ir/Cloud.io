namespace NewsManagement.Core.News.AppServices;

using FluentValidation;
using Cloud.Web.Core.AppService;
using Contracts;
using Models;

public sealed class RegisterNewsCommandValidator : Validator<RegisterNews>
{
    protected override void Initialize()
    {
        ValidateTitle();
        ValidateDescription();
        ValidateBody();
    }

    #region Methods

    private void ValidateTitle()
    {
        var property = nameof(NewsTitle);
        var minChar = 3;
        var maxChar = 250;

        RuleFor(e => e.Title)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    private void ValidateDescription()
    {
        var property = nameof(NewsDescription);
        var minChar = 0;
        var maxChar = 500;

        RuleFor(e => e.Description)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    private void ValidateBody()
    {
        var property = nameof(NewsBody);
        //var minChar = 0;
        //var maxChar = 500;

        RuleFor(e => e.Body)
        .NotEmpty().WithMessage($"{property} is required!");
        //.MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        //.MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    #endregion
}
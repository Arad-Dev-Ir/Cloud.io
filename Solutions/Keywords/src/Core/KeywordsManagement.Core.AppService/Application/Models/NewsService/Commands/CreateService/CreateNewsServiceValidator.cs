namespace KeywordsManagement.Core.NewsService.AppServices;

using FluentValidation;
using Cloudio.Web.Core.AppService;
using KeywordsManagement.Core.NewsService.Contracts;
using KeywordsManagement.Core.NewsService.Models;

public sealed class CreateNewsServiceValidator : Validator<CreateNewsServiceCommand>
{
    protected override void Initialize()
    {
        ValidateTitle();
        ValidateName();
    }

    #region Methods

    private void ValidateTitle()
    {
        var property = nameof(NewsServiceTitle);
        var minChar = 3;
        var maxChar = 50;

        RuleFor(e => e.Title)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    private void ValidateName()
    {
        var property = nameof(NewsServiceName);
        var minChar = 3;
        var maxChar = 50;

        RuleFor(e => e.Name)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    #endregion
}
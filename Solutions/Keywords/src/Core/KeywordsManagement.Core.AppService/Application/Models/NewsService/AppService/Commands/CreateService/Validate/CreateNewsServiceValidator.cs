namespace KeywordsManagement.Core.NewsService.AppServices;

using FluentValidation;
using Cloud.Web.Core.AppService;
using Contracts;
using Title = Models.Title;
using Name = Models.Name;

public class CreateNewsServiceValidator : Validator<CreateNewsService>
{
    protected override void Initialize()
    {
        TitleValidation();
        NameValidation();
    }

    #region Methods

    private void TitleValidation()
    {
        var property = nameof(Title);
        var minChar = 3;
        var maxChar = 50;

        RuleFor(e => e.Title)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    private void NameValidation()
    {
        var property = nameof(Name);
        var minChar = 3;
        var maxChar = 50;

        RuleFor(e => e.Name)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    #endregion
}
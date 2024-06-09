namespace KeywordsManagement.Core.Keyword.AppServices;

using FluentValidation;
using Cloud.Web.Core.AppService;
using Contracts;
using Models;

public class CreateKeywordValidator : Validator<CreateKeyword>
{
    protected override void Initialize()
    => ValidateTitle();

    #region Methods

    private void ValidateTitle()
    {
        var property = nameof(KeywordTitle);
        var minChar = 3;
        var maxChar = 50;

        RuleFor(e => e.Title)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    #endregion
}

namespace NewsManagement.Core.News.AppServices;

using FluentValidation;
using Cloud.Web.Core.AppService;
using Contracts;
using Title = Models.Title;
using Description = Models.Description;
using Body = Models.Body;

public class RegisterNewsCommandValidator : Validator<RegisterNews>
{
    protected override void Initialize()
    {
        TitleValidation();
        DescriptionValidation();
        BodyValidation();
    }

    #region Methods

    private void TitleValidation()
    {
        var property = nameof(Title);
        var minChar = 3;
        var maxChar = 250;

        RuleFor(e => e.Title)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    private void DescriptionValidation()
    {
        var property = nameof(Description);
        var minChar = 0;
        var maxChar = 500;

        RuleFor(e => e.Description)
        .NotEmpty().WithMessage($"{property} is required!")
        .MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        .MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    private void BodyValidation()
    {
        var property = nameof(Body);
        //var minChar = 0;
        //var maxChar = 500;

        RuleFor(e => e.Body)
        .NotEmpty().WithMessage($"{property} is required!");
        //.MinimumLength(minChar).WithMessage($"The minimum length for {property} can be {minChar} character(s).")
        //.MaximumLength(maxChar).WithMessage($"The maximum length for {property} can be {maxChar} character(s).");
    }

    #endregion
}
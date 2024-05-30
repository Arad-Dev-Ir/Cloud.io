namespace Cloud.Web.Core.AppService;

using FluentValidation;
using Web.Core.Contract;

public abstract class Validator<T> : AbstractValidator<T>/*, IValidator<T>*/ where T : IRequest
{
    public Validator()
    => Initialize();

    protected abstract void Initialize();
}
namespace Cloudio.Web.Core.AppService;

using FluentValidation;
using Cloudio.Web.Core.Contract;

public abstract class Validator<T> : AbstractValidator<T> where T : IRequest
{
    public Validator()
    => Initialize();

    protected abstract void Initialize();
}
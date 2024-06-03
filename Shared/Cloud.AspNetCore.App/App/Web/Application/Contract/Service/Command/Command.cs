namespace Cloud.Web.Core.Contract;

using Cloud.Core.Models;

public interface ICommand : IRequest
{ }
public record Command : TransferModel, ICommand
{ }


public interface ICommand<D> : ICommand
{ }
public record Command<D> : TransferModel, ICommand<D>
{ }
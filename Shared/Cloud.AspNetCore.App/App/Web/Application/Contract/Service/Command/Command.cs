namespace Cloud.Web.Core.Contract;

using Cloud.Core.Models;

public interface ICommand : IRequest
{ }
public class Command : Model, ICommand
{ }


public interface ICommand<D> : ICommand
{ }
public class Command<D> : Model, ICommand<D>
{ }
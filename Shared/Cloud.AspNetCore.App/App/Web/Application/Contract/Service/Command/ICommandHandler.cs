namespace Cloud.Web.Core.Contract;

public interface ICommandHandler<C> where C : ICommand
{
    Task<CommandResponse> ExecuteAsync(C command);
}

public interface ICommandHandler<C, D> where C : ICommand<D>
{
    Task<CommandResponse<D>> ExecuteAsync(C command);
}
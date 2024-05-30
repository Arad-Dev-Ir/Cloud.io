namespace Cloud.Web.Core.AppService;

using System;
using Web.Core.Contract;

public abstract class CommandPipeline(IServiceProvider serviceProvider) : Procedure<CommandPipeline>(serviceProvider)
{
    public abstract Task<CommandResponse> ExecuteAsync<C>(C command) where C : ICommand;
    public abstract Task<CommandResponse<T>> ExecuteAsync<C, T>(C command) where C : ICommand<T>;
}
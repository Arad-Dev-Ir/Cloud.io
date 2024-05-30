namespace Cloud.Web.Core.Contract;

public interface IQuery : IRequest
{ }

public interface IQuery<D> : IQuery
{ }
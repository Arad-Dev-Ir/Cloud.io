namespace Cloud.Web.Core.Contract;

public interface IResponse<D>
{
    D Data { get; set; }
}
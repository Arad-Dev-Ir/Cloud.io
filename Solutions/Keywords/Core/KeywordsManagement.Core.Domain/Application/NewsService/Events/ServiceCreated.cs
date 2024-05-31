namespace BasicInformation.Core.NewsService.Models;

using Cloud.Core.Models;

public class ServiceCreated : Event
{
    public Guid Code { get; private set; }
    public string Title { get; private set; } = Empty;
    public string Name { get; private set; } = Empty;

    #region Initialize

    public ServiceCreated(Code code, Title title, Name name)
    => Initialize(code, title, name);

    private void Initialize(Code code, Title title, Name name, Action? act = default)
    {
        Code = code.Value;
        Title = title.Value;
        Name = name.Value;

        act?.Invoke();
    }

    public static ServiceCreated Instance(Code code, Title title, Name name)
    => new(code, title, name);
    public static ServiceCreated Instance(string code, string title, string name)
    => new(code, title, name);

    #endregion

    #region Methods



    #endregion
}

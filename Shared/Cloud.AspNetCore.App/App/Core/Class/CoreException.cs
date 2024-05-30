namespace Cloud.Core;

public abstract class CoreException(string message, params string[] parameters) : Exception(message)
{
    public List<string> Parameters { get; private set; } = parameters.Length > 0 ? [.. parameters] : [];

    #region Methods

    public override string ToString()
    {
        var result = Message;
        var count = Parameters.Count;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                var item = $"{{{i}}}";
                result = result.Replace(item, Parameters[i]);
            }
        }
        return result;
    }

    #endregion
}
namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLConnectionCredentialsProvider
    {
        ISQLConnectionCredentials ConnectionCredentials { get; }
    }
}

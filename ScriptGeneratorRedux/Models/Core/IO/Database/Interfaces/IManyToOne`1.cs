namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface IManyToOne<T>
    {
        T Onwer { get; }
    }
}

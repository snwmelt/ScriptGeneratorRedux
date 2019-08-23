using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces
{
    internal interface ICP4SecurityServer : ISQLServer
    {
        ISQLDatabase SecurityDB { get; }
    }
}

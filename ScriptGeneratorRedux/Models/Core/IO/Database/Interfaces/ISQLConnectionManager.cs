using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLConnectionManager
    {
        SqlConnection CreateConnection( ISQLConnectionCredentials ConnectionCredentials );
        IEnumerable<ISQLServer> GetLocalServers( );
        SqlConnection OpenConnection( ISQLConnectionCredentials ConnectionCredentials );
    }
}

using ScriptGeneratorRedux.Models.Core.IO.Database.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces
{
    internal interface ISQLConnectionManager
    {
        SqlConnection CreateConnection( ISQLConnectionCredentials ConnectionCredentials );
        IEnumerable<ISQLServer> GetLocalServers( );
        dynamic GetSchema( ISQLConnectionCredentials ConnectionCredentials, Enum SchemaType, ISchemaRestrictions SchemaRestrictions = null );
        SqlConnection OpenConnection( ISQLConnectionCredentials ConnectionCredentials );
    }
}

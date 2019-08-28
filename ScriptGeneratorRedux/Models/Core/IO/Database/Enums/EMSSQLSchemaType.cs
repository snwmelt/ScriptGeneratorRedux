using System.ComponentModel;

namespace ScriptGeneratorRedux.Models.Core.IO.Database.Enums
{
    internal enum EMSSQLSchemaType
    {
        /*Server Level Schema*/
        [Description( "Databases" )]
        Databases = 1,
        [Description( "Security" )]
        Security = 2,
        [Description( "Server Objects" )]
        ServerObjects = 3,
        [Description( "Replication" )]
        Replication = 4,
        [Description( "Management" )]
        Managment = 5,

        /*Database Level Schema*/
        [Description( "Database Diagrams" )]
        DBDatabaseDiagrams = 10,
        [Description( "Tables" )]
        DBTables = 11,
        [Description( "Views" )]
        DBViews = 12,
        [Description( "Synonyms" )]
        DBSynonyms = 13,
        [Description( "Programmability" )]
        DBProgrammability = 14,
        [Description( "Service Broker" )]
        DBServiceBroker = 15,
        [Description( "Storage" )]
        DBStorage = 16,
        [Description( "Security" )]
        DBSecurity = 17,

        /*Table Level Schema*/
        [Description( "Columns" )]
        TColumns = 110,
        [Description( "Keys" )]
        TKeys = 111,
        [Description( "Constraints" )]
        TConstraints = 112,
        [Description( "Triggers" )]
        TTriggers = 113,
        [Description( "Indexes" )]
        TIndexes = 114,
        [Description( "Statistics" )]
        TStatistics = 115
    }
}

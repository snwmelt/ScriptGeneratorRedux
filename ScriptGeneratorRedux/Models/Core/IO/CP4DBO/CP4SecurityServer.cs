using ScriptGeneratorRedux.Models.Core.IO.CP4DBO.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Database;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.Models.Core.IO.CP4DBO
{
    internal sealed class CP4SecurityServer : AIEnumerableDataSource<ISQLDatabase>, ICP4SecurityServer
    {
        public CP4SecurityServer( ISQLConnectionCredentials SQLConnectionCredentials, String Name, String SecurityDBName )
        {
            if( SQLConnectionCredentials == null )
                throw new ArgumentNullException( "SQLConnectionCredentials Cannot Be Null." );

            if( String.IsNullOrWhiteSpace( Name ) )
                throw new ArgumentOutOfRangeException( "Server Name Cannot Be Null Or Whitespace." );

            this.ConnectionCredentials = SQLConnectionCredentials;
            this.Name                     = Name;
            
            SecurityDB = new SQLDatabase( SecurityDBName, this );
        }

        public ISQLDatabase SecurityDB
        {
            get;
        }

        public String Name
        {
            get;
        }

        public ISQLConnectionCredentials ConnectionCredentials
        {
            get;
        }

        public override IEnumerator<ISQLDatabase> GetEnumerator( )
        {
            throw new NotImplementedException( );
        }

        public override void LoadData( )
        {
            throw new NotImplementedException( );
        }
    }
}

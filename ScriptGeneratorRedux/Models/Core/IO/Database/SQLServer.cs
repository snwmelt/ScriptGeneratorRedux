using System;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class SQLServer : AIEnumerableDataSource<ISQLDatabase>, ISQLServer
    {
        #region Private Variables
        private HashSet<ISQLDatabase> _Data;
        #endregion

        public SQLServer( String Name, ISQLConnectionCredentials SQLConnectionCredentials )
        {
            this.Name                  = Name;
            this.ConnectionCredentials = SQLConnectionCredentials;
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
            return _Data.GetEnumerator( );
        }

        public override void LoadData( )
        {
            try
            {
                _Data = new HashSet<ISQLDatabase>( Core.CP4DatabaseService.GetData( this ) );

                Status = ( _Data?.Count > 0 ) ? EIOState.Populated
                                              : EIOState.Empty;

                InvokeDataLoaded( ELoadingState.Completed );
            }
            catch( Exception Ex )
            {
                Status = ( _Data?.Count > 0 ) ? EIOState.Fallback
                                              : EIOState.Empty;

                InvokeDataLoaded( ELoadingState.Failed, Ex );
            }
        }
    }
}

using System;
using System.Collections.Generic;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System.Data.SqlClient;
using System.Data;
using ScriptGeneratorRedux.Models.Extensions;
using ScriptGeneratorRedux.Models.Core.IO.Database.Enums;
using ScriptGeneratorRedux.Models.Core.IO.Database;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class MSSQLServerInterop : ISQLServerInterop
    {
        public MSSQLServerInterop( )
        {
            ConnectionManager = new MSSQLConnectionManager( );
        }

        public ISQLConnectionManager ConnectionManager
        {
            get;
        }

        public IEnumerable<String[ ]> ExecuteSQL( ISQLConnectionCredentials ConnectionCredentials, String Query )
        {
            if( Query == null )
                yield break;

            using( SqlConnection  _SqlConnection  = ConnectionManager.OpenConnection( ConnectionCredentials ) )
            using( SqlCommand     _SqlCommand     = new SqlCommand( Query, _SqlConnection ) )
            using( SqlDataAdapter _SqlDataAdapter = new SqlDataAdapter( _SqlCommand ) )
            {
                DataSet _DataSet = new DataSet( );

                _SqlDataAdapter.Fill( _DataSet );

                for( int i = 0; i < _DataSet.Tables.Count; i++ )
                {
                    foreach( String[ ] _DataSetTableRow in _DataSet.Tables[ i ].GetDataRowValues( ) )
                    {
                        yield return _DataSetTableRow;
                    }
                }
            }
        }

        public ISQLDatabase GetDatabaseData( ISQLConnectionCredentials ServerConnectionCredentials, String DatabaseName )
        {
            ISQLDatabase Reuslt = new SQLDatabase( DatabaseName, new SQLServer( null, ServerConnectionCredentials ) );

            Reuslt.LoadData( );

            return Reuslt;
        }

        public IEnumerable<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>> GetTableData( ISQLTable SQLTable )
        {
            var       Result = new List<KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>>( );
            DataTable Schema = ConnectionManager.GetSchema( SQLTable.ConnectionCredentials, 
                                                            EMSSQLSchemaType.TColumns, 
                                                            new ColumnSchemaRestrictions( ) { Table = SQLTable.Name } );


            foreach( String ColumnName in Schema.GetColumnNameValues( "COLUMN_NAME" ) )
            {
                ISQLTableColumnKey ISQLTableColumnKey = new SQLTableColumnKey( ColumnName );

                Result.Add( new KeyValuePair<ISQLTableColumnKey, ISQLTableColumnValues>( ISQLTableColumnKey, new SQLTableColumnValues( ISQLTableColumnKey ) ) );
            }
            
            foreach( String[ ] SQLResultRow in ExecuteSQL( SQLTable.ConnectionCredentials, $"SELECT * FROM {SQLTable.Name}" ) )
            {
                for( int i = 0; i < SQLResultRow.Length; i++ )
                {
                    Result[ i ].Value.AddValue( SQLResultRow[ i ] );
                }
            }

            return Result.AsReadOnly( );
            
        }

        public IEnumerable<String> GetTableNames( ISQLDatabase SQLDatabase )
        {
            DataTable Schema = ConnectionManager.GetSchema( SQLDatabase.ConnectionCredentials, EMSSQLSchemaType.DBTables );

            foreach( String TableName in Schema.GetColumnNameValues( "TABLE_NAME" ) )
                yield return TableName;
        }
    }
}

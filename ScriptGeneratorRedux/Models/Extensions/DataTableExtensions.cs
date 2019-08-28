using System;
using System.Collections.Generic;
using System.Data;

namespace ScriptGeneratorRedux.Models.Extensions
{
    internal static class DataTableExtensions
    {
        public static IEnumerable<String> GetColumnNameValues( this DataTable _DataTable, string ColumnName )
        {
            DataRowCollection    _DataRowCollection    = _DataTable.Rows;
            DataColumnCollection _DataColumnCollection = _DataTable.Columns;

            for( int i = 0; i < _DataRowCollection.Count; i++ )
            {
                for( int j = 0; j < _DataColumnCollection.Count; j++ )
                {
                    if( _DataColumnCollection[ j ].ColumnName == ColumnName )
                    {
                        yield return ( _DataRowCollection[ i ] )[ _DataColumnCollection[ j ] ].ToString( );
                    }
                }
            }
        }

        public static IEnumerable<String[ ]> GetDataRowValues( this DataTable _DataTable )
        {
            DataRowCollection _DataRowCollection = _DataTable.Rows;

            for( int i = 0; i < _DataRowCollection.Count; i++ )
            {
                String[ ] _ItemArrayVales = new String[ _DataRowCollection[ i ].ItemArray.Length ];


                for( int j = 0; j < _DataRowCollection[ i ].ItemArray.Length; j++ )
                {
                    _ItemArrayVales[ j ] = _DataRowCollection[ i ].ItemArray[ j ].ToString( );
                }

                yield return _ItemArrayVales;
            }
        }
    }
}

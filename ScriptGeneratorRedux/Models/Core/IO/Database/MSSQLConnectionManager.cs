using System;
using System.Data.SqlClient;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using ScriptGeneratorRedux.Models.Extensions;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal sealed class MSSQLConnectionManager : ISQLConnectionManager
    {
        #region Private Variables
        private static readonly String    _SQLHiveKeyDefaultName = "MSSQLSERVER";
        private static readonly String    _SQLHiveKeyName        = @"Software\Microsoft\Microsoft SQL Server";
        private static readonly String    _SQLHiveKeyValueName   = "InstalledInstances";
        private static          Random    _RandomWaitTime        = new Random( );
        private static readonly Object    _ThreadLock            = new Object( );
        public static readonly Int64[ ]   _SQLExceptionExeptions = new Int64[] 
                                                                       {
                                                                           -2,
                                                                           67
                                                                       };
        private static readonly String[ ] _DefaultRestictions    = new String[ ]
                                                                       {
                                                                           "model",
                                                                           "ASPState",
                                                                           "tempdb",
                                                                           "master",
                                                                           "msdb",
                                                                           "CodebreakQueue",
                                                                           "CP4CAT",
                                                                           "_sbCP4EventsInitiator"
                                                                       };
        #endregion

        private IEnumerable<String> _GetLocalServerNames( RegistryKey _BaseHive )
        {
            using( RegistryKey _SubHive = _BaseHive.OpenSubKey( _SQLHiveKeyName, false ) )
            {
                if( _SubHive != null )
                {
                    String[ ] _SQLHiveKeyValues = _SubHive.GetValue( _SQLHiveKeyValueName ) as String[ ];


                    if( _SQLHiveKeyValues != null )
                    {
                        for( int index = 0; index < _SQLHiveKeyValues.Length; index++ )
                        {
                            if( string.Equals( _SQLHiveKeyValues[ index ], _SQLHiveKeyDefaultName, StringComparison.OrdinalIgnoreCase ) )
                            {
                                _SQLHiveKeyValues[ index ] = ".";
                            }
                            else
                            {
                                _SQLHiveKeyValues[ index ] = @".\" + _SQLHiveKeyValues[ index ];
                            }
                        }

                        return _SQLHiveKeyValues;
                    }
                }

                return Enumerable.Empty<String>( );
            }
        }

        private dynamic _GetSchema( ISQLConnectionCredentials _ConnectionCredentials, Enum _SchemaType, ISchemaRestrictions _SchemaRestrictions )
        {
            using( SqlConnection _SqlConnection = new SqlConnection( _ConnectionCredentials.ToString( ) ) )
            {
                _OpenSQLConnection( _SqlConnection );

                //System.Diagnostics.Debug.WriteLine( $"{_SchemaType?.GetDescription( )} _ { String.Join(", ", _SchemaRestrictions?.ToArray( ) ) }" );

                return ( _SchemaRestrictions == null ) ? _SqlConnection.GetSchema( _SchemaType.GetDescription( ) )
                                                       : _SqlConnection.GetSchema( _SchemaType.GetDescription( ), _SchemaRestrictions.ToArray( ) );
            }
        }

        private static void _OpenSQLConnection( SqlConnection _SqlConnection )
        {
            try
            {
                _SqlConnection.Open( );
            }
            catch( SqlException SEx )
            {
                if( !_SQLExceptionExeptions.Contains( SEx.Number ) )
                    throw SEx;

                int _Attempts;
                int _WaitTime;

                lock( _ThreadLock )
                {
                    _Attempts = 0;
                    _WaitTime = _RandomWaitTime.Next( 2000, 7500 );
                }

                while( _Attempts <= Environment.ProcessorCount )
                {
                    Thread.Sleep( _WaitTime );

                    try
                    {
                        _SqlConnection.Open( );
                    }
                    catch( SqlException SExInner )
                    {
                        if( SEx.Number != -2 )
                            throw SExInner;

                        _Attempts++;
                    }
                }

                Debug.WriteLine( $"[ FAILURE ] \tAttempting To Open SQL Connection: {_SqlConnection.ConnectionString} \n\n{SEx}" );
                throw SEx;
            }
        }

        public SqlConnection CreateConnection( ISQLConnectionCredentials ConnectionCredentials )
        {
            return new SqlConnection( ConnectionCredentials.ToString( ) );
        }


        public IEnumerable<ISQLServer> GetLocalServers( )
        {
            Int64 YieldIndex = 0;

            if( Environment.Is64BitOperatingSystem )
            {
                using( RegistryKey _BaseHiveX64 = RegistryKey.OpenBaseKey( RegistryHive.LocalMachine, RegistryView.Registry64 ) )
                using( RegistryKey _BaseHiveX86 = RegistryKey.OpenBaseKey( RegistryHive.LocalMachine, RegistryView.Registry32 ) )
                {
                    foreach( String _InstanceName in _GetLocalServerNames( _BaseHiveX64 ) )
                    {
                        yield return new SQLServer( $"X64_{YieldIndex}_{_InstanceName}", new SQLConnectionCredentials( $"Server={_InstanceName}" ) );
                        YieldIndex++;
                    }

                    foreach( String _InstanceName in _GetLocalServerNames( _BaseHiveX86 ) )
                    {
                        yield return new SQLServer( $"X86_{YieldIndex}_{_InstanceName}", new SQLConnectionCredentials( $"Server={_InstanceName}" ) );
                        YieldIndex++;
                    }
                }
            }
            else
            {
                foreach( String _InstanceName in _GetLocalServerNames( Registry.LocalMachine ) )
                {
                    yield return new SQLServer( $"X86_{YieldIndex}_{_InstanceName}", new SQLConnectionCredentials( $"Server={_InstanceName}" ) );
                    YieldIndex++;
                }
            }
        }

        public dynamic GetSchema( ISQLConnectionCredentials ConnectionCredentials, Enum SchemaType, ISchemaRestrictions SchemaRestrictions = null )
        {
            return _GetSchema( ConnectionCredentials, SchemaType, SchemaRestrictions );
        }

        public SqlConnection OpenConnection( ISQLConnectionCredentials ConnectionCredentials )
        {
            SqlConnection SqlConnection = new SqlConnection( ConnectionCredentials.ToString( ) );
            _OpenSQLConnection( SqlConnection );

            return SqlConnection;
        }
    }
}

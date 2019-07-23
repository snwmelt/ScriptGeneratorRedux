using SGRModules.Factories.Events.Interfaces;
using SGRModules.Factories.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace SGRModules.Factories
{
    public sealed class ModuleFactory<T> : IModuleFactory<T>
    {
        public void LoadModules( String DLLPath )
        {
            if ( !File.Exists( DLLPath ) || Path.GetExtension( DLLPath ) != ".dll" )
                throw new InvalidOperationException( $"Provided Path {DLLPath} Is Not A .dll File" );

            foreach ( Type ObjectType in Assembly.LoadFrom( DLLPath ).GetTypes( ) )
            {
                if ( !ObjectType.IsAbstract  &&
                     !ObjectType.IsInterface &&
                     typeof( T ).IsAssignableFrom( ObjectType ) )
                {
                    RaiseModuleCreatedEvent( DLLPath );
                }
            }
        }

        public event EventHandler<IModuleInstantiatedEvent<T>> OnModuleInstantiatedEvent;

        private void RaiseModuleCreatedEvent( String SourceDLL )
        {
            OnModuleInstantiatedEvent?.Invoke( this, new ModuleInstantiatedEventArgs<T>( ( T )Activator.CreateInstance( typeof( T ) ),
                                                                                         SourceDLL ) );
        }
    }
}

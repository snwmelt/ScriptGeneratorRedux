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
                    RaiseModuleCreatedEvent( DLLPath, ObjectType );
                }
            }
        }

        public event EventHandler<IModuleInstantiatedEvent<T>> OnModuleInstantiatedEvent;

        private void RaiseModuleCreatedEvent( String SourceDLL, Type ObjectType )
        {
            IModuleInstantiatedEvent<T> _IModuleInstantiatedEvent = null;

            try
            {
                _IModuleInstantiatedEvent = new ModuleInstantiatedEventArgs<T>( ( T )Activator.CreateInstance( ObjectType ),
                                                                                SourceDLL );
            }
            catch ( Exception Ex )
            {
                _IModuleInstantiatedEvent = new ModuleInstantiatedEventArgs<T>( default( T ), SourceDLL );

                _IModuleInstantiatedEvent.AddException( Ex );
            }

            OnModuleInstantiatedEvent?.Invoke( this, _IModuleInstantiatedEvent );
        }
    }
}

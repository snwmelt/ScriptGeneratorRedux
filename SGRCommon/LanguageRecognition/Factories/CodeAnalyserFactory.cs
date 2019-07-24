using SGRModules.Factories;
using SGRModules.Factories.Events.Interfaces;
using SGRModules.LanguageRecognition.CodeAnalysis.Interfaces;
using SGRModules.LanguageRecognition.Enums;
using SGRModules.LanguageRecognition.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SGRCommon.LanguageRecognition.Factories
{
    public sealed class CodeAnalyserFactory
    {
        #region Singleton Instantiation

        private static readonly CodeAnalyserFactory _Instance = new CodeAnalyserFactory( );

        private CodeAnalyserFactory( )
        {
            _TargetDirectory = Assembly.GetExecutingAssembly( ).Location;
            _ModuleFactory   = new ModuleFactory<ICodeAnalyser>( );
            _ModulesLoaded   = false;

            _ModuleFactory.OnModuleInstantiatedEvent += ModuleFactory_OnModuleInstantiatedEvent;
        }

        #endregion


        private void LoadCodeAnalysers( )
        {
            if( _CodeAnalysersList == null )
                _CodeAnalysersList = new List<ICodeAnalyser>( );

            if( _CodeAnalysersList.Count > 0 )
                _CodeAnalysersList.Clear( );

            foreach( String DLLPath in Directory.GetFiles( Path.GetDirectoryName( _TargetDirectory ), "*.dll" ) )
            {
                _ModuleFactory.LoadModules( DLLPath );
            }

            _ModulesLoaded = true;
        }

        private void ModuleFactory_OnModuleInstantiatedEvent( object sender, IModuleInstantiatedEvent<ICodeAnalyser> e )
        {
            _CodeAnalysers.Add( e.Instance );
        }


        #region Private Properties

        private ICollection<ICodeAnalyser> _CodeAnalysers
        {
            get
            {
                if( !_ModulesLoaded )
                    LoadCodeAnalysers( );

                return _CodeAnalysersList;
            }
        }

        private IList<ICodeAnalyser>         _CodeAnalysersList;
        private ModuleFactory<ICodeAnalyser> _ModuleFactory;
        private Boolean                      _ModulesLoaded;
        private String                       _TargetDirectory;

        #endregion


        public static ICodeAnalyser GetCodeAnalyser( ILanguage Language )
        {
            return _Instance._CodeAnalysers.FirstOrDefault( CodeAnalyser => CodeAnalyser.TargetLanguage == Language );
        }

        public static ICodeAnalyser GetCodeAnalyser( ELanguage Name )
        {
            return _Instance._CodeAnalysers.FirstOrDefault( CodeAnalyser => CodeAnalyser.TargetLanguage.Name == Name );
        }

        public static ICodeAnalyser GetCodeAnalyser( ELanguageFileExtension FileExtension )
        {
            return _Instance._CodeAnalysers.FirstOrDefault( CodeAnalyser => CodeAnalyser.TargetLanguage.FileExtension == FileExtension );
        }

        public String TargetDirectory
        {
            get
            {
                return _TargetDirectory;
            }

            set
            {
                _ModulesLoaded   = false;
                _TargetDirectory = value;
            }
        }
    }
}
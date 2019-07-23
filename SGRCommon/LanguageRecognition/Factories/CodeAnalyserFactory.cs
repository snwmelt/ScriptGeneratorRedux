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
        
        private CodeAnalyserFactory()
        {
            LoadCodeAnalysers( );
        }

        #endregion

        private void LoadCodeAnalysers( )
        {
            _ModuleFactory = new ModuleFactory<ICodeAnalyser>( );
            _CodeAnalysers = new List<ICodeAnalyser>( );

            _ModuleFactory.OnModuleInstantiatedEvent += ModuleFactory_OnModuleInstantiatedEvent;

            foreach ( String DLLPath in Directory.GetFiles( Path.GetDirectoryName( Assembly.GetExecutingAssembly( ).Location ) ) )
            {
                _ModuleFactory.LoadModules( DLLPath );
            }
        }

        private void ModuleFactory_OnModuleInstantiatedEvent( object sender, IModuleInstantiatedEvent<ICodeAnalyser> e )
        {
            _CodeAnalysers.Add( e.Instance );
        }


        #region Private Properties

        private ICollection<ICodeAnalyser> _CodeAnalysers
        {
            get;
            set;
        }

        private ModuleFactory<ICodeAnalyser> _ModuleFactory
        {
            get;
            set;
        }

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
    }
}
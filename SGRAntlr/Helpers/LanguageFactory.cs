using SGRModules.LanguageRecognition.Enums;
using SGRModules.LanguageRecognition.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGRAntlrl.Helpers
{
    internal sealed class LanguageFactory 
    {
        private sealed class Language : ILanguage
        {
            public ELanguage Name
            {
                get;
            }

            public ELanguageFileExtension FileExtension
            {
                get;
            }

            public Language( ELanguage Name, ELanguageFileExtension FileExtension )
            {
                this.Name          = Name;
                this.FileExtension = FileExtension;
            }
        }

        private static readonly IEnumerable<ILanguage> Languages = new List<ILanguage>( )
        {
            new Language( ELanguage.TOML, ELanguageFileExtension.toml )
        };

        internal static ILanguage GetLanguageInfo( String Target )
        {
            return Languages.FirstOrDefault( Language => ( Language.Name.ToString( ) == Target ) || 
                                                         ( Language.FileExtension.ToString( ) == Target ) );
        }
    }
}
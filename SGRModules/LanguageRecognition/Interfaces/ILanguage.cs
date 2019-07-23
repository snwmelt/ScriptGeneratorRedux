using SGRModules.LanguageRecognition.Enums;

namespace SGRModules.LanguageRecognition.Interfaces
{
    public interface ILanguage
    {
        ELanguage Name { get; }
        ELanguageFileExtension FileExtension { get; }
    }
}
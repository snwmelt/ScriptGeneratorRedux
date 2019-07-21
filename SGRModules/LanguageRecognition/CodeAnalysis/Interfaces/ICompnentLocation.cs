using System;

namespace SGRModules.LanguageRecognition.CodeAnalysis.Interfaces
{
    public interface ICompnentLocation
    {
        Int64 Column { get; }
        Int64 Length { get; }
        Int64 Row { get; }
    }
}
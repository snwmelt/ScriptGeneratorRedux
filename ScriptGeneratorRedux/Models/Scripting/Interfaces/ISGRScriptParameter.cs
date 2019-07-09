using System;
using System.Text.RegularExpressions;

namespace ScriptGeneratorRedux.Models.Scripting.Interfaces
{
    interface ISGRScriptParameter
    {
        Boolean IsNullable { get; }
        Boolean IsRequired { get; }
        Regex MatchAgainst { get; }
        String Name { get; }
        Type Type { get; }
        Regex Validator { get; }
        Object Value { get; }
    }
}

using Antlr4.Runtime;
using SGRModules.LanguageRecognition.CodeAnalysis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGRAntlrl.Helpers.SGRModules.CodeAnalysis
{
    internal sealed class ComponentLocation : IComponentLocation
    {
        public Int64 Column
        {
            get;
            internal set;
        }

        public Int64 Length
        {
            get;
            internal set;
        }

        public Int64 Row
        {
            get;
            internal set;
        }

        public Int64 RowSpan
        {
            get;
            internal set;
        }

        internal static IComponentLocation GetLocation( ParserRuleContext ParserRuleContext )
        {
            return new ComponentLocation( )
            {
                Column  = ParserRuleContext.Start.Column,
                Length  = ParserRuleContext.GetText( ).Length,
                Row     = ParserRuleContext.Start.Line,
                RowSpan = ParserRuleContext.Stop.Line - ParserRuleContext.Start.Line
            };
        }

        internal static IComponentLocation GetLocation( IEnumerable<ParserRuleContext> ParserRuleContextCollection )
        {
            return new ComponentLocation( )
            {
                Column  = ParserRuleContextCollection.First( ).Start.Column,
                Length  = ParserRuleContextCollection.Select( x => x.GetText( ).Length ).Sum( ),
                Row     = ParserRuleContextCollection.First( ).Start.Line,
                RowSpan = ParserRuleContextCollection.Last( ).Stop.Line - ParserRuleContextCollection.First( ).Start.Line
            };
        }
    }
}

using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    public abstract class ASchemaRestrictions : IReadOnlyList<String>, ISchemaRestrictions
    {
        protected String[ ] _Restrictions;

        public String this[ Int32 index ]
        {
            get
            {
                return _Restrictions[ index ];
            }
        }

        public Int32 Count
        {
            get
            {
                return _Restrictions.Length;
            }
        }

        public abstract Enum[ ] ApplicableTo
        {
            get;
        }

        public IEnumerator<String> GetEnumerator( )
        {
            foreach( String _Restriction in _Restrictions )
            {
                yield return _Restriction;
            }
        }

        IEnumerator IEnumerable.GetEnumerator( )
        {
            return this.GetEnumerator( );
        }

        public String[ ] ToArray( )
        {
            return _Restrictions;
        }
    }
}

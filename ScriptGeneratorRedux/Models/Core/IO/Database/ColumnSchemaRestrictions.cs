using System;
using ScriptGeneratorRedux.Models.Core.IO.Database.Enums;

namespace ScriptGeneratorRedux.Models.Core.IO.Database
{
    internal class ColumnSchemaRestrictions : ASchemaRestrictions
    {
        public ColumnSchemaRestrictions( )
        {
            _Restrictions = new String[ 4 ];
        }

        public override Enum[ ] ApplicableTo
        {
            get
            {
                return new Enum[ ] { EMSSQLSchemaType.TColumns };
            }
        }

        public String Catalog
        {
            get
            {
                return _Restrictions[ 0 ];
            }

            set
            {
                _Restrictions[ 0 ] = value;
            }

        }

        public String Column
        {
            get
            {
                return _Restrictions[ 3 ];
            }

            set
            {
                _Restrictions[ 3 ] = value;
            }
        }

        public String Schema
        {
            get
            {
                return _Restrictions[ 1 ];
            }

            set
            {
                _Restrictions[ 1 ] = value;
            }
        }

        public String Table
        {
            get
            {
                return _Restrictions[ 2 ];
            }

            set
            {
                _Restrictions[ 2 ] = value;
            }
        }
    }
}
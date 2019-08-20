using ScriptGeneratorRedux.Models.Core;
using ScriptGeneratorRedux.Models.Core.IO.Database;
using ScriptGeneratorRedux.Models.Core.IO.Database.Interfaces;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;
using System.Collections.Generic;

namespace ScriptGeneratorRedux.ViewModels
{
    internal sealed class AddServerUserControlViewModel : IDatabaseServerProvider
    {
        public AddServerUserControlViewModel( )
        {
            UseWindowsAuthentication = true;
            Core.DataContext.RegisterServerDetailsProvider( this );
        }

        public String Password
        {
            get;
            set;
        }

        public String SecurityDBName
        {
            get;
            set;
        }

        public String SecurityServer
        {
            get;
            set;
        }

        public String TargetServer
        {
            get;
            set;
        }

        public String Username
        {
            get;
            set;
        }

        public Boolean UseWindowsAuthentication
        {
            get;
            set;
        }
    }
}

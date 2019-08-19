using ScriptGeneratorRedux.Models.Core;
using ScriptGeneratorRedux.Models.Core.IO.Interfaces;
using System;

namespace ScriptGeneratorRedux.ViewModels
{
    internal sealed class AddServerUserControlViewModel : IServerDetailsProvider
    {
        public AddServerUserControlViewModel( )
        {
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

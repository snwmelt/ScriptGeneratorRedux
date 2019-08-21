﻿using ScriptGeneratorRedux.Models.Core;
using System.Windows;
using ScriptGeneratorRedux.Models.Core.Events.Enums;

namespace ScriptGeneratorRedux
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            Core.DataContext.OnInitialised += ( se, ev ) =>
            {
                switch( ev.State )
                {
                    case ELoadingState.Completed:
                        base.OnStartup( e );
                        break;

                    case ELoadingState.Failed:
                        throw ev.Exception;
                }
            };

            Core.DataContext.Initialise( );
        }
    }
}

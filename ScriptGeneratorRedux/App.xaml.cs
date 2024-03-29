﻿using ScriptGeneratorRedux.Models.Core;
using ScriptGeneratorRedux.Models.Core.IO.Events.Enums;
using System.Windows;

namespace ScriptGeneratorRedux
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            // TODO FIX
            Core.DataContext.OnDataLoaded += ( se, ev ) =>
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

            Core.DataContext.LoadData( );
        }
    }
}
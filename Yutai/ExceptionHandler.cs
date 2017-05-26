﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using Yutai.Forms;
using Yutai.Shared;

namespace Yutai
{
    public static class ExceptionHandler
    {
        public static void Attach()
        {
            if (Debugger.IsAttached) return;

            // main UI thread only
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;

            // in delegates called by Invoke or BeginInvoke.
            Dispatcher.CurrentDispatcher.UnhandledException += CurrentDispatcher_UnhandledException;

            // last resort, the app will be terminated anyway
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logger.Current.Error("Application_ThreadException", e.Exception);

            using (var form = new ErrorView(e.Exception, false))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private static void CurrentDispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Current.Error("CurrentDispatcher_UnhandledException", e.Exception);

            using (var form = new ErrorView(e.Exception, false))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Logger.Current.Error("CurrentDomain_UnhandledException", ex);

            using (var form = new ErrorView(ex, true))
            {
                form.ShowDialog();

                Application.Exit();
            }
        }
    }
}

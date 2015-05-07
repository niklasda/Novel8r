using System;
using System.Threading;
using System.Windows.Forms;
using Novel8r.Logic.Helpers;
using Novel8r.WinForms.Factories;
using Novel8r.WinForms.Presenters;
using Novel8r.Logic.Factories;

namespace Novel8r.App
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var app = new Sql8rApp();
            app.Run(args);
        }
    }

    public class Sql8rApp
    {
        private void openSplashScreen()
        {
            var splash = new BgSplash();
            splash.Start();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                PresentException(ex);
            }
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            PresentException(ex);
        }

        private void PresentException(Exception ex)
        {
            DialogHelper.Instance.ShowExceptionDialog(ex);
        }

        public void Run(string[] args)
        {
            openSplashScreen();

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;


            MainPresenter presenter = PresenterFactory.Instance.GetMainPresenter(args);
            Application.Run(presenter.View);

        }
    }
}
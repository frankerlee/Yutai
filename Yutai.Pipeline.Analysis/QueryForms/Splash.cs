using System;
using System.Threading;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public class Splash
    {
        private static frmPromptQuerying MySplashForm;

        private static Thread MySplashThread;

        public static string Status
        {
            get
            {
                if (Splash.MySplashForm == null)
                {
                    throw new InvalidOperationException("Splash Form not on screen");
                }
                return Splash.MySplashForm.StatusInfo;
            }
            set
            {
                if (Splash.MySplashForm != null)
                {
                    Splash.MySplashForm.StatusInfo = value;
                }
            }
        }

        private static void ShowThread()
        {
            Splash.MySplashForm = new frmPromptQuerying(1);
            Application.Run(Splash.MySplashForm);
        }

        public static void Show()
        {
            if (Splash.MySplashThread == null)
            {
                Splash.MySplashThread = new Thread(new ThreadStart(Splash.ShowThread));
                Splash.MySplashThread.IsBackground = true;
                Splash.MySplashThread.ApartmentState = ApartmentState.STA;
                Splash.MySplashThread.Start();
            }
        }

        public static void Close()
        {
            if (Splash.MySplashThread != null)
            {
                if (Splash.MySplashForm != null)
                {
                    try
                    {
                        Splash.MySplashForm.Invoke(new MethodInvoker(Splash.MySplashForm.Close));
                    }
                    catch (Exception)
                    {
                    }
                    Splash.MySplashThread = null;
                    Splash.MySplashForm = null;
                }
            }
        }
    }
}
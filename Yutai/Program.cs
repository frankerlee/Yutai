using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
using Yutai.DI.Castle;
using Yutai.Forms;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;
using Yutai.Services.Concrete;
using Yutai.Shared;
using Yutai.Views;

namespace Yutai
{
    static class Program
    {
        public static Stopwatch Timer = new Stopwatch();
        private static LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();

        private static IApplicationContainer CreateContainer()
        {
            // Switch the class here and change the using directive above to use another one
            // Also switch references.

            // LightInjectContainer
            // NinjectContainer
            // UnityApplicationContainer
            // return  new NinjectContainer();
            return new WindsorCastleContainer();
        }

        private static void LoadConfig(IApplicationContainer container)
        {
            Logger.Current.Trace("Start LoadConfig");
            // MapInitializer.InitMapConfig();

            Logger.Current.Trace("Before container.GetSingleton");
            var configService = container.GetSingleton<IConfigService>();
            Logger.Current.Trace("After container.GetSingleton");

            configService.LoadAll();
            Logger.Current.Trace("End LoadConfig");
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //{
            //    if (!RuntimeManager.Bind(ProductCode.Desktop))
            //    {
            //        MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
            //        return;
            //    }
            //}
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Form1());
            ExceptionHandler.Attach();

            //DumpFormats();

            var logger = new LoggingService();
            logger.Info("APPLICATION STARTUP");

            ShowSplashScreen();

            Timer.Start();
            SplashView.Instance.ShowStatus("正在检查许可...");
            m_AOLicenseInitializer.InitializeApplication(
                new esriLicenseProductCode[]
                {
                    esriLicenseProductCode.esriLicenseProductCodeEngine,
                    esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB
                },
                new esriLicenseExtensionCode[]
                {
                    esriLicenseExtensionCode.esriLicenseExtensionCode3DAnalyst,
                    esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork,
                    esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst,
                    esriLicenseExtensionCode.esriLicenseExtensionCodeSchematics,
                    esriLicenseExtensionCode.esriLicenseExtensionCodeMLE,
                    esriLicenseExtensionCode.esriLicenseExtensionCodeTracking
                });


            var container = CreateContainer();
            CompositionRoot.Compose(container);

            SplashView.Instance.ShowStatus("引导配置...");
            LoadConfig(container);

            SplashView.Instance.ShowStatus("启动应用程序...");
            container.Run<NewMainPresenter>();
        }

        private static void ShowSplashScreen()
        {
            var splashScreen = SplashView.Instance;
            splashScreen.ShowStatus("Composing DI container");
            splashScreen.Show();
            Application.DoEvents();
        }
    }
}
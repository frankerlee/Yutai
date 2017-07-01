using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;

namespace Yutai.ArcGIS.Controls.Historical
{
    internal class BaseClass
    {
        public static void CreateHistoricalMarker(IWorkspace pSDEWorkspace, DateTime dTimeStamp, string strName)
        {
            (pSDEWorkspace as IHistoricalWorkspace).AddHistoricalMarker(strName, dTimeStamp);
        }

        private static void ReturnMessages(ESRI.ArcGIS.Geoprocessor.Geoprocessor gp)
        {
            if (gp.MessageCount > 0)
            {
                for (int i = 0; i <= (gp.MessageCount - 1); i++)
                {
                    Console.WriteLine(gp.GetMessage(i));
                }
            }
        }

        public static bool RunTool(ESRI.ArcGIS.Geoprocessor.Geoprocessor geoprocessor, IGPProcess process,
            ITrackCancel TC)
        {
            geoprocessor.OverwriteOutput = true;
            geoprocessor.ClearMessages();
            try
            {
                object obj2 = geoprocessor.Execute(process, TC);
                ReturnMessages(geoprocessor);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                ReturnMessages(geoprocessor);
                return false;
            }
        }
    }
}
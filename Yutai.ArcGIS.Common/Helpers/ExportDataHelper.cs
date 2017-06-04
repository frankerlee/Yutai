using System.Collections;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class ExportDataHelper
    {
        // Fields
        private static int m_count;

        // Methods
        static ExportDataHelper()
        {
            old_acctor_mc();
        }

        public static void CopyRow(IRow irow_0, IRow irow_1)
        {
            IFields fields = irow_0.Fields;
            IFields fields2 = irow_1.Fields;
            for (int i = 0; i < fields2.FieldCount; i++)
            {
                IField field = fields2.get_Field(i);
                if (((field.Type != esriFieldType.esriFieldTypeOID) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && field.Editable)
                {
                    try
                    {
                        int index = fields.FindField(field.Name);
                        irow_1.set_Value(i, irow_0.get_Value(index));
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void ExportActiveView2(IActiveView iactiveView_0, string string_0)
        {
            IExport export = null;
            tagRECT grect;
            switch (System.IO.Path.GetExtension(string_0).ToLower())
            {
                case ".emf":
                    export = new ExportEMF() as IExport;
                    break;

                case ".eps":
                    export = new ExportPS() as IExport;
                    break;

                case ".ai":
                    export = new ExportAI() as IExport;
                    break;

                case ".pdf":
                    export = new ExportPDF() as IExport;
                    break;

                case ".svg":
                    export = new ExportSVG() as IExport;
                    break;

                case ".bmp":
                    export = new ExportBMP() as IExport;
                    break;

                case ".jpg":
                    export = new ExportJPEG() as IExport;
                    break;

                case ".png":
                    export = new ExportPNG() as IExport;
                    break;

                case ".tif":
                    export = new ExportTIFF() as IExport;
                    break;

                case ".gif":
                    export = new ExportGIF() as IExport;
                    break;

                default:
                    return;
            }
            export.ExportFileName = string_0;
            int num2 = 0x60;
            int num3 = 300;
            try
            {
                export.Resolution = num3;
                if (export is ISettingsInRegistry)
                {
                    (export as ISettingsInRegistry).RestoreDefault();
                }
            }
            catch
            {
            }
            grect.left = 0;
            grect.top = 0;
            grect.right = iactiveView_0.ExportFrame.right * (num3 / num2);
            grect.bottom = iactiveView_0.ExportFrame.bottom * (num3 / num2);
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords((double)grect.left, (double)grect.top, (double)grect.right, (double)grect.bottom);
            export.PixelBounds = envelope;
            int hDC = export.StartExporting();
            iactiveView_0.Output(hDC, (int)export.Resolution, ref grect, null, null);
            export.FinishExporting();
            export.Cleanup();
        }

        public static void ExportActiveView2(IMap imap_0, ILayer ilayer_0, string string_0)
        {
            if (ilayer_0 != null)
            {
                int num;
                IList list = new ArrayList();
                IEnvelope extent = (imap_0 as IActiveView).Extent;
                for (num = 0; num < imap_0.LayerCount; num++)
                {
                    ILayer layer = imap_0.get_Layer(num);
                    if (layer.Visible)
                    {
                        list.Add(true);
                        layer.Visible = false;
                    }
                    else
                    {
                        list.Add(false);
                    }
                }
                ilayer_0.Visible = true;
                ExportActiveView2(imap_0 as IActiveView, string_0);
                (imap_0 as IActiveView).Extent = extent;
                for (num = 0; num < imap_0.LayerCount; num++)
                {
                    imap_0.get_Layer(num).Visible = (bool)list[num];
                }
            }
        }

        public static void ExportData(IFeatureCursor ifeatureCursor_0, IFeatureClass ifeatureClass_0)
        {
            ProcessAssist assist = new ProcessAssist();
            assist.InitProgress();
            assist.SetMaxValue(m_count);
            assist.SetMessage("开始导出数据...");
            assist.Start();
            try
            {
                IFeature feature = ifeatureCursor_0.NextFeature();
                ISpatialReference spatialReference = (ifeatureClass_0 as IGeoDataset).SpatialReference;
                int num = 1;
                while (feature != null)
                {
                    string str = string.Format("导出第{0}条，共{1}条", num++, m_count);
                    assist.Increment(1);
                    assist.SetMessage(str);
                    IFeature feature2 = ifeatureClass_0.CreateFeature();
                    CopyRow(feature, feature2);
                    IGeometry shape = feature.Shape;
                    shape.Project(spatialReference);
                    feature2.Shape = shape;
                    feature2.Store();
                    feature = ifeatureCursor_0.NextFeature();
                }
                MessageBox.Show("导出成功!");
            }
            catch
            {
                MessageBox.Show("无法导出选择的数据源!");
            }
            assist.End();
        }

        public static void ExportData(IFeatureLayer ifeatureLayer_0, IFeatureClass ifeatureClass_0, bool bool_0)
        {
            IFeatureCursor cursor2;
            if (bool_0)
            {
                ICursor cursor;
                m_count = (ifeatureLayer_0.FeatureClass as IFeatureSelection).SelectionSet.Count;
                (ifeatureLayer_0.FeatureClass as IFeatureSelection).SelectionSet.Search(null, false, out cursor);
                cursor2 = cursor as IFeatureCursor;
            }
            else
            {
                m_count = ifeatureLayer_0.FeatureClass.FeatureCount(null);
                cursor2 = ifeatureLayer_0.FeatureClass.Search(null, false);
            }
            ExportData(cursor2, ifeatureClass_0);
            ComReleaser.ReleaseCOMObject(cursor2);
        }

        private static void old_acctor_mc()
        {
            m_count = 0;
        }
    }


}

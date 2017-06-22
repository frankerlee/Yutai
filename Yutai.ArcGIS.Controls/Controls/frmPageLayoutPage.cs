using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmPageLayoutPage : DockContent
    {
        public AxPageLayoutControl axPageLayoutControl1;
        private IMap m_OldMap = null;

        public frmPageLayoutPage()
        {
            this.InitializeComponent();
        }

        public void CopyMapToPageLayOutDataFrameforPrint(IMap MapControlOfMap)
        {
            this.m_OldMap = MapControlOfMap;
            IGraphicsContainerSelect graphicsContainer = this.axPageLayoutControl1.ActiveView.GraphicsContainer as IGraphicsContainerSelect;
            IElement element = null;
            IMapFrame frame = null;
            if (graphicsContainer.ElementSelectionCount <= 0)
            {
                this.CopyMapToPageLayOutforPrint(MapControlOfMap);
            }
            else
            {
                for (int i = 0; i < graphicsContainer.ElementSelectionCount; i++)
                {
                    element = graphicsContainer.SelectedElement(i);
                    if (element is IMapFrame)
                    {
                        frame = element as IMapFrame;
                        break;
                    }
                }
                if (frame != null)
                {
                    SimpleLineSymbolClass class2 = new SimpleLineSymbolClass();
                    IRgbColor color = new RgbColorClass {
                        Red = 255,
                        Green = 255,
                        Blue = 255
                    };
                    class2.Color = color;
                    ISymbolBorder border = new SymbolBorderClass {
                        LineSymbol = class2
                    };
                    frame.Border = border;
                    IObjectCopy copy = new ObjectCopyClass();
                    object pInObject = MapControlOfMap;
                    object obj3 = copy.Copy(pInObject);
                    object map = frame.Map;
                    copy.Overwrite(obj3, ref map);
                    this.fullPageLayerOut(this.axPageLayoutControl1.ActiveView.FocusMap, (MapControlOfMap as IActiveView).Extent);
                    this.axPageLayoutControl1.ActiveView.Refresh();
                }
                else
                {
                    this.CopyMapToPageLayOutforPrint(MapControlOfMap);
                }
            }
        }

        public void CopyMapToPageLayOutforPrint(IMap MapControlOfMap)
        {
            try
            {
                this.m_OldMap = MapControlOfMap;
                this.axPageLayoutControl1.GraphicsContainer.DeleteAllElements();
                IObjectCopy copy = new ObjectCopyClass();
                object pInObject = MapControlOfMap;
                object obj3 = copy.Copy(pInObject);
                object focusMap = this.axPageLayoutControl1.ActiveView.FocusMap;
                copy.Overwrite(obj3, ref focusMap);
                this.fullPageLayerOut(this.axPageLayoutControl1.ActiveView.FocusMap, (MapControlOfMap as IActiveView).Extent);
                SimpleLineSymbolClass class2 = new SimpleLineSymbolClass();
                IRgbColor color = new RgbColorClass {
                    Red = 255,
                    Green = 255,
                    Blue = 255
                };
                class2.Color = color;
                ISymbolBorder border = new SymbolBorderClass {
                    LineSymbol = class2
                };
                IElement element = this.axPageLayoutControl1.ActiveView.GraphicsContainer.Next();
                if ((element != null) && (element is IMapFrame))
                {
                    (element as IFrameElement).Border = border;
                }
                this.axPageLayoutControl1.ActiveView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "提示");
            }
        }

 private void frmPageLayoutPage_Load(object sender, EventArgs e)
        {
            this.CopyMapToPageLayOutDataFrameforPrint(this.FocusMap);
            this.axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale;
        }

        private void fullPageLayerOut(IMap PageLayOutOfMap, IEnvelope MapControlMapOfExtend)
        {
            try
            {
                IActiveView view = (IActiveView) PageLayOutOfMap;
                view.ScreenDisplay.DisplayTransformation.VisibleBounds = MapControlMapOfExtend;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }

 public IMap FocusMap { get; set; }
    }
}


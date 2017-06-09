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
    public class frmPageLayoutPage : DockContent
    {
        public AxPageLayoutControl axPageLayoutControl1;
        private IContainer components = null;
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
                        Red = 0xff,
                        Green = 0xff,
                        Blue = 0xff
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
                    Red = 0xff,
                    Green = 0xff,
                    Blue = 0xff
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPageLayoutPage));
            this.axPageLayoutControl1 = new AxPageLayoutControl();
            this.axPageLayoutControl1.BeginInit();
            base.SuspendLayout();
            this.axPageLayoutControl1.Dock = DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = (AxHost.State) resources.GetObject("axPageLayoutControl1.OcxState");
            this.axPageLayoutControl1.Size = new Size(0x124, 0x10a);
            this.axPageLayoutControl1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x10a);
            base.Controls.Add(this.axPageLayoutControl1);
            base.Name = "frmPageLayoutPage";
            base.TabText = "打印视图";
            this.Text = "打印视图";
            base.Load += new EventHandler(this.frmPageLayoutPage_Load);
            this.axPageLayoutControl1.EndInit();
            base.ResumeLayout(false);
        }

        public IMap FocusMap { get; set; }
    }
}


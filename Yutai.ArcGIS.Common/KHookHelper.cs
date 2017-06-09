using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Common
{
    [Guid("E0F9FD8D-81DA-4cee-B2D7-E2CC2C00A2F3")]
    public class KHookHelper : IKHookHelper, IHookHelper
    {
        private IApplication iapplication_0 = null;
        private IMap imap_0 = null;
        private IMapControl2 imapControl2_0 = null;
        private IPageLayoutControl2 ipageLayoutControl2_0 = null;
        private object object_0 = null;
        private object object_1 = null;

        public void DockWindows(object object_2, Bitmap bitmap_0)
        {
            if (this.iapplication_0 != null)
            {
                this.iapplication_0.DockWindows(object_2, bitmap_0);
            }
            else if (object_2 is Form)
            {
                (object_2 as Form).Show();
            }
            else if (object_2 is Control)
            {
                Form form = new Form {
                    Size = (object_2 as Control).Size
                };
                (object_2 as Control).Dock = DockStyle.Fill;
                form.Controls.Add(object_2 as Control);
                form.ShowDialog();
            }
        }

        public void HideDockWindow(object object_2)
        {
            if (this.iapplication_0 != null)
            {
                this.iapplication_0.HideDockWindow(object_2);
            }
            else if (object_2 is Form)
            {
                (object_2 as Form).Hide();
            }
        }

       
        public void MapDocumentChanged()
        {
            if (this.iapplication_0 != null)
            {
                this.iapplication_0.MapDocumentChanged();
            }
        }

        public void ResetCurrentTool()
        {
            this.CurrentTool = null;
        }

        public void SetStatus(string string_0)
        {
            if (this.iapplication_0 != null)
            {
                this.iapplication_0.SetStatus(string_0);
            }
        }

        public void SetStatus(int int_0, string string_0)
        {
            if (this.iapplication_0 != null)
            {
                this.iapplication_0.SetStatus(int_0, string_0);
            }
        }

        public bool ShowCommandString(string string_0, CommandTipsType commandTipsType_0)
        {
            return ((this.iapplication_0 != null) && this.iapplication_0.ShowCommandString(string_0, commandTipsType_0));
        }

        public void UpdateUI()
        {
            if (this.iapplication_0 != null)
            {
                this.iapplication_0.UpdateUI();
            }
        }

        public IActiveView ActiveView
        {
            get
            {
                if (ApplicationBase.IsPrintForm)
                {
                    if (this.object_1 is IMapControl2)
                    {
                        return ((IMapControl2) this.object_1).ActiveView;
                    }
                    if (this.object_1 is IPageLayoutControl2)
                    {
                        return ((IPageLayoutControl2) this.object_1).ActiveView;
                    }
                    if (this.object_1 is IApplication)
                    {
                        return ((IApplication) this.object_1).ActiveView;
                    }
                    if (this.object_1 is IMap)
                    {
                        return (this.object_1 as IActiveView);
                    }
                    if (this.object_1 is MapAndPageLayoutControls)
                    {
                        if ((this.object_1 as MapAndPageLayoutControls).ActiveControl is IPageLayoutControl2)
                        {
                            return ((this.object_1 as MapAndPageLayoutControls).ActiveControl as IPageLayoutControl2).ActiveView;
                        }
                        return ((this.object_1 as MapAndPageLayoutControls).ActiveControl as IMapControl2).ActiveView;
                    }
                    if (this.object_1 is MapAndPageLayoutControlsold)
                    {
                        return (this.object_1 as MapAndPageLayoutControlsold).ActiveView;
                    }
                }
                else
                {
                    if (this.imapControl2_0 != null)
                    {
                        return this.imapControl2_0.ActiveView;
                    }
                    if (this.ipageLayoutControl2_0 != null)
                    {
                        return this.ipageLayoutControl2_0.ActiveView;
                    }
                    if (this.iapplication_0 != null)
                    {
                        return this.iapplication_0.ActiveView;
                    }
                    if (this.imap_0 != null)
                    {
                        return (this.imap_0 as IActiveView);
                    }
                    if (this.object_0 is MapAndPageLayoutControls)
                    {
                        if ((this.object_0 as MapAndPageLayoutControls).ActiveControl is IPageLayoutControl2)
                        {
                            return ((this.object_0 as MapAndPageLayoutControls).ActiveControl as IPageLayoutControl2).ActiveView;
                        }
                        return ((this.object_0 as MapAndPageLayoutControls).ActiveControl as IMapControl2).ActiveView;
                    }
                    if (this.object_1 is MapAndPageLayoutControlsold)
                    {
                        return (this.object_0 as MapAndPageLayoutControlsold).ActiveView;
                    }
                }
                return null;
            }
        }

        public ILayer CurrentLayer
        {
            get
            {
                if (this.iapplication_0 != null)
                {
                    return this.iapplication_0.CurrentLayer;
                }
                return null;
            }
            set
            {
                if (this.iapplication_0 != null)
                {
                    this.iapplication_0.CurrentLayer = value;
                }
            }
        }

        public ITool CurrentTool
        {
            get
            {
                if (ApplicationBase.IsPrintForm)
                {
                    if (this.object_1 is IMapControl2)
                    {
                        return ((IMapControl2) this.object_1).CurrentTool;
                    }
                    if (this.object_1 is IPageLayoutControl2)
                    {
                        return ((IPageLayoutControl2) this.object_1).CurrentTool;
                    }
                    if (this.object_1 is IApplication)
                    {
                        return (this.object_1 as IApplication).CurrentTool;
                    }
                }
                else
                {
                    if (this.iapplication_0 != null)
                    {
                        return this.iapplication_0.CurrentTool;
                    }
                    if (this.imapControl2_0 != null)
                    {
                        return this.imapControl2_0.CurrentTool;
                    }
                    if (this.ipageLayoutControl2_0 != null)
                    {
                        return this.ipageLayoutControl2_0.CurrentTool;
                    }
                }
                return null;
            }
            set
            {
                if (ApplicationBase.IsPrintForm)
                {
                    if (this.object_1 is IMapControl2)
                    {
                        ((IMapControl2) this.object_1).CurrentTool = value;
                    }
                    else if (this.object_1 is IPageLayoutControl2)
                    {
                        ((IPageLayoutControl2) this.object_1).CurrentTool = value;
                    }
                    else if (this.object_1 is IApplication)
                    {
                        (this.object_1 as IApplication).CurrentTool = value;
                    }
                }
                else if (this.iapplication_0 != null)
                {
                    this.iapplication_0.CurrentTool = value;
                }
                else if (this.imapControl2_0 != null)
                {
                    this.imapControl2_0.CurrentTool = value;
                }
                else if (this.ipageLayoutControl2_0 != null)
                {
                    this.ipageLayoutControl2_0.CurrentTool = value;
                }
            }
        }

        public IMap FocusMap
        {
            get
            {
                try
                {
                    if (ApplicationBase.IsPrintForm)
                    {
                        if (this.object_1 is IMapControl2)
                        {
                            return ((IMapControl2) this.object_1).Map;
                        }
                        if (this.object_1 is IPageLayoutControl2)
                        {
                            return ((IPageLayoutControl2) this.object_1).ActiveView.FocusMap;
                        }
                        if (this.object_1 is IApplication)
                        {
                            return ((IApplication) this.object_1).FocusMap;
                        }
                        if (this.object_1 is IMap)
                        {
                            return (this.object_1 as IMap);
                        }
                        if (this.object_1 is MapAndPageLayoutControls)
                        {
                            if ((this.object_1 as MapAndPageLayoutControls).ActiveControl is IPageLayoutControl2)
                            {
                                return ((this.object_1 as MapAndPageLayoutControls).ActiveControl as IPageLayoutControl2).ActiveView.FocusMap;
                            }
                            return ((this.object_1 as MapAndPageLayoutControls).ActiveControl as IMapControl2).Map;
                        }
                        if (this.object_1 is MapAndPageLayoutControlsold)
                        {
                            return (this.object_1 as MapAndPageLayoutControlsold).ActiveView.FocusMap;
                        }
                    }
                    else
                    {
                        if (this.imapControl2_0 != null)
                        {
                            return this.imapControl2_0.Map;
                        }
                        if (this.ipageLayoutControl2_0 != null)
                        {
                            return this.ipageLayoutControl2_0.ActiveView.FocusMap;
                        }
                        if (this.iapplication_0 != null)
                        {
                            return this.iapplication_0.FocusMap;
                        }
                        if (this.imap_0 != null)
                        {
                            return this.imap_0;
                        }
                        if (this.object_0 is ISceneControlDefault)
                        {
                            return ((this.object_0 as ISceneControlDefault).Scene as IMap);
                        }
                        if (this.object_0 is IGlobeControlDefault)
                        {
                            return ((this.object_0 as IGlobeControlDefault).Globe as IMap);
                        }
                        if (this.object_0 is MapAndPageLayoutControls)
                        {
                            if ((this.object_0 as MapAndPageLayoutControls).ActiveControl is IPageLayoutControl2)
                            {
                                return ((this.object_0 as MapAndPageLayoutControls).ActiveControl as IPageLayoutControl2).ActiveView.FocusMap;
                            }
                            return ((this.object_0 as MapAndPageLayoutControls).ActiveControl as IMapControl2).Map;
                        }
                        if (this.object_0 is MapAndPageLayoutControlsold)
                        {
                            return (this.object_0 as MapAndPageLayoutControlsold).ActiveView.FocusMap;
                        }
                    }
                }
                catch
                {
                }
                return null;
            }
        }

        public object Hook
        {
            get
            {
                if (ApplicationBase.IsPrintForm)
                {
                    return this.object_1;
                }
                return this.object_0;
            }
            set
            {
                if (ApplicationBase.IsPrintForm)
                {
                    this.object_1 = value;
                }
                else
                {
                    if (value is IMapControl2)
                    {
                        this.imapControl2_0 = (IMapControl2) value;
                    }
                    else if (value is IPageLayoutControl2)
                    {
                        this.ipageLayoutControl2_0 = (IPageLayoutControl2) value;
                    }
                    else if (value is IApplication)
                    {
                        this.iapplication_0 = (IApplication) value;
                    }
                    else if (value is IMap)
                    {
                        this.imap_0 = value as IMap;
                    }
                    this.object_0 = value;
                }
            }
        }

        public string MapDocName
        {
            get
            {
                if (this.iapplication_0 != null)
                {
                    return this.iapplication_0.MapDocName;
                }
                return "";
            }
            set
            {
                if (this.iapplication_0 != null)
                {
                    this.iapplication_0.MapDocName = value;
                }
            }
        }

        public IMapDocument MapDocument
        {
            get
            {
                if (this.iapplication_0 != null)
                {
                    return this.iapplication_0.MapDocument;
                }
                return null;
            }
            set
            {
                if (this.iapplication_0 != null)
                {
                    this.iapplication_0.MapDocument = value;
                }
            }
        }

        public IOperationStack OperationStack
        {
            get
            {
                return ApplicationBase.m_pOperationStack;
            }
        }

        public IPageLayout PageLayout
        {
            get
            {
                if (ApplicationBase.IsPrintForm)
                {
                    if (this.object_1 is IPageLayoutControl2)
                    {
                        return ((IPageLayoutControl2) this.object_1).PageLayout;
                    }
                    if (this.object_1 is IApplication)
                    {
                        return ((IApplication) this.object_1).PageLayout;
                    }
                    if ((this.object_1 is MapAndPageLayoutControls) && ((this.object_1 as MapAndPageLayoutControls).ActiveControl is IPageLayoutControl2))
                    {
                        return ((this.object_1 as MapAndPageLayoutControls).ActiveControl as IPageLayoutControl2).PageLayout;
                    }
                }
                else
                {
                    if (this.ipageLayoutControl2_0 != null)
                    {
                        return this.ipageLayoutControl2_0.PageLayout;
                    }
                    if (this.iapplication_0 != null)
                    {
                        return this.iapplication_0.PageLayout;
                    }
                }
                return null;
            }
        }

        public IEngineSnapEnvironment SnapEnvironment
        {
            get
            {
                if (this.iapplication_0 != null)
                {
                    if (this.iapplication_0.SnapEnvironment != null)
                    {
                        return this.iapplication_0.SnapEnvironment;
                    }
                    return this.iapplication_0.EngineSnapEnvironment;
                }
                return null;
            }
        }

        public double SnapTolerance
        {
            get
            {
                if (this.iapplication_0 != null)
                {
                    return this.iapplication_0.SnapTolerance;
                }
                return 0.1;
            }
            set
            {
                if (this.iapplication_0 != null)
                {
                    this.iapplication_0.SnapTolerance = value;
                }
            }
        }

        public IStyleGallery StyleGallery
        {
            get
            {
                return ApplicationBase.StyleGallery;
            }
        }

        public double Tolerance
        {
            get
            {
                if (this.iapplication_0 != null)
                {
                    return this.iapplication_0.Tolerance;
                }
                return 0.0001;
            }
            set
            {
                if (this.iapplication_0 != null)
                {
                    this.iapplication_0.Tolerance = value;
                }
            }
        }
    }
}


using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Helpers.Docking;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;
using DockPanelState = Yutai.Plugins.Enums.DockPanelState;

namespace Yutai.UI.Docking
{
    internal class DockPanel : IDockPanel
    {
        private readonly DevExpress.XtraBars.Docking.DockManager  _dockingManager;
        private readonly Control _control;
        private readonly Control _parent;

        internal DockPanel(DevExpress.XtraBars.Docking.DockManager dockingManager, Control control, Control parent)
        {
            if (dockingManager == null) throw new ArgumentNullException("dockingManager");
            if (control == null) throw new ArgumentNullException("control");
            if (parent == null) throw new ArgumentNullException("parent");

            _dockingManager = dockingManager;
            _control = control;
            _parent = parent;
        }

        public Control Control
        {
            get { return _control; }
        }

       
       
        public DockPanelState DockState
        {
            get
            {

                DevExpress.XtraBars.Docking.DockingStyle  style = _dockingManager.Panels[((IDockPanelView) _control).DockName].Dock;
                
                return DockHelper.DevExpressToMapWindow(style);
            }
        }

    
        public bool Visible
        {
            get { return _dockingManager.Panels[((IDockPanelView)_control).DockName].Visible; }
            set { _dockingManager.Panels[((IDockPanelView)_control).DockName].Visible=true; }
        }

        public void DockTo(string parentName, DockPanelState state, int size)
        {
            DevExpress.XtraBars.Docking.DockPanel parentPanel ;
            if (string.IsNullOrEmpty(parentName))
                parentPanel = null;
                else
            {
                parentPanel= _dockingManager.Panels[parentName];

                if (parentPanel != null && parentPanel.Visibility == DockVisibility.Hidden)
                {
                    parentPanel = null;
                }
            }
            DevExpress.XtraBars.Docking.DockingStyle style = DockHelper.MapWindowToDevExpress(((IDockPanelView) _control).DefaultDock);
            if(style == DockingStyle.Fill) style= DockingStyle.Float;

            DevExpress.XtraBars.Docking.DockPanel panel = null;
            if (parentPanel == null)
                panel = _dockingManager.AddPanel(style);
            else
                panel = parentPanel.AddPanel();
            panel.Name = ((IDockPanelView)_control).DockName;
            panel.Header = ((IDockPanelView)_control).Caption;
            panel.Image = ((IDockPanelView)_control).Image;
            //panel.Dock = DockHelper.MapWindowToDevExpress(((IDockPanelView)_control).DefaultDock);
            panel.FloatSize = ((IDockPanelView)_control).DefaultSize;
            _control.Dock = DockStyle.Fill;
            panel.TabText = ((IDockPanelView)_control).Caption;
            panel.Text = ((IDockPanelView)_control).Caption;
            panel.Controls.Add(_control);

            
            //    if (parentPanel != null)
            //    {
            //        parentPanel.Controls.Add(panel);
            //    }
           
            //else
            //{
            //    _dockingManager.AddPanel(panel.Dock, panel);
            //}
        }

        public void DockTo(IDockPanel parent, DockPanelState state, int size)
        {
            if (parent != null && !parent.Visible)
            {
                return;     // no need to throw exception if it not visible
            }

            var ctrl = parent != null ? parent.Control : _parent;
            
            //首先先创建一个DockPanel，然后将控件加上去
            DevExpress.XtraBars.Docking.DockPanel panel=new DevExpress.XtraBars.Docking.DockPanel();
            panel.Name = ((IDockPanelView) _control).DockName;
            panel.Header = ((IDockPanelView)_control).Caption;
            panel.Image = ((IDockPanelView)_control).Image;
            panel.Dock = DockHelper.MapWindowToDevExpress(((IDockPanelView) _control).DefaultDock);
            panel.FloatSize = ((IDockPanelView)_control).DefaultSize;
            _control.Dock= DockStyle.Fill;
            panel.Controls.Add(_control);

            if (parent != null)
            {
                DevExpress.XtraBars.Docking.DockPanel parentPanel = _dockingManager.Panels[parent.Name];
                if (parentPanel != null)
                {
                    parentPanel.Controls.Add(panel);
                }
            }
            else
            {
                _dockingManager.AddPanel(panel.Dock, panel);
            }
        }

        public void DockTo(DockPanelState state, int size)
        {
            DockTo("", state, size);
        }

        public string Caption
        {
            get { return _dockingManager.Panels[((IDockPanelView)_control).DockName].Header; }
            set { _dockingManager.Panels[((IDockPanelView)_control).DockName].Header=value; }
        }

        public Size Size
        {
            get { return _dockingManager.Panels[((IDockPanelView)_control).DockName].Size; }
            set { _dockingManager.Panels[((IDockPanelView)_control).DockName].Size=value; }
        }

        public bool FloatOnly
        {
            get { return !_dockingManager.Panels[((IDockPanelView)_control).DockName].Options.AllowDockAsTabbedDocument; }
            set { _dockingManager.Panels[((IDockPanelView)_control).DockName].Options.AllowDockAsTabbedDocument = !value; }
        }

        public bool AllowFloating
        {
            get { return _dockingManager.Panels[((IDockPanelView)_control).DockName].Options.AllowFloating; }
            set { _dockingManager.Panels[((IDockPanelView)_control).DockName].Options.AllowFloating=value; }
        }

        public Image GetIcon()
        {
            return  _dockingManager.Panels[((IDockPanelView)_control).DockName].Image;
        }

        public void SetIcon(Icon icon)
        {
            _dockingManager.Panels[((IDockPanelView)_control).DockName].Image=icon.ToBitmap();
        }

      

        public int TabPosition
        {
            get { return (int)_dockingManager.Panels[((IDockPanelView)_control).DockName].TabsPosition; }
            set
            {
                _dockingManager.Panels[((IDockPanelView) _control).DockName].TabsPosition =
                    (DevExpress.XtraBars.Docking.TabsPosition) value;
            }
        }

        public string Name {
            get { return ((IDockPanelView) _control).DockName; }
        }

        public void Activate()
        {
            _dockingManager.ActivePanel = _dockingManager.Panels[((IDockPanelView) _control).DockName];
        }

        public bool IsFloating
        {
            get { return _dockingManager.Panels[((IDockPanelView)_control).DockName].Dock== DockingStyle.Float; }
        }

        public void Float(Rectangle rect, bool tabFloating)
        {
            _dockingManager.Panels[((IDockPanelView)_control).DockName].MakeFloat(rect.Location);
        }

        public bool AutoHidden
        {
            get { return _dockingManager.Panels[((IDockPanelView)_control).DockName].Options.ShowAutoHideButton; }
            set { _dockingManager.Panels[((IDockPanelView) _control).DockName].Options.ShowAutoHideButton = value; }
        }

        public void Focus()
        {
            var control = Control as DockPanelControlBase;
            if (control != null)
            {
                control.SetFocus();
            }
        }
    }
}
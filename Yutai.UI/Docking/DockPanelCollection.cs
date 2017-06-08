using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Shared;
using Yutai.UI.Controls;
using Yutai.UI.Style;
using DockPanelCancelEventArgs = Yutai.Plugins.Events.DockPanelCancelEventArgs;
using DockPanelEventArgs = Yutai.Plugins.Events.DockPanelEventArgs;

namespace Yutai.UI.Docking
{
    //说明：DockPanel和插件里面的DockPanelView的关系，在进行Dock的时候，是动态创建了一个DockPanb
    internal class DockPanelCollection : IDockPanelCollection
    {
        private bool _locked;
        private readonly Form _mainForm;
        private readonly IBroadcasterService _broadcaster;
        private readonly IStyleService _styleService;
        private readonly DevExpress.XtraBars.Docking.DockManager _dockingManager;
        private readonly Dictionary<DockPanelInfo,IDockPanelView> _dict = new Dictionary<DockPanelInfo,IDockPanelView>();

     

        internal DockPanelCollection(object dockingManager, Form mainForm, IBroadcasterService broadcaster,
            IStyleService styleService)
        {
            if (mainForm == null) throw new ArgumentNullException("mainForm");
            if (broadcaster == null) throw new ArgumentNullException("broadcaster");
            if (styleService == null) throw new ArgumentNullException("styleService");

            _mainForm = mainForm;
            _broadcaster = broadcaster;
            _styleService = styleService;
            _dockingManager = dockingManager as DockManager;

            if (_dockingManager == null)
            {
                throw new ApplicationException(
                    "Failed to initialize DockPanelCollection. No docking manager is provided.");
            }

            _dockingManager.VisibilityChanged += _dockingManager_VisibilityChanged;
          
        }

    

        private void _dockingManager_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            string ctrlKey = e.Panel.Name;
            KeyValuePair<DockPanelInfo, IDockPanelView> pair = _dict.FirstOrDefault(c => c.Value.DockName == ctrlKey);

            //检索控件名称


            var panel = _dockingManager.Panels[ctrlKey];
           
            var args = new DockPanelEventArgs(panel,pair.Key.Key);

            if (panel.Visible)
            {
                _broadcaster.BroadcastEvent(p => p.DockPanelOpened_, panel, args, pair.Key.Identity);
            }
            else
            {
                _broadcaster.BroadcastEvent(p => p.DockPanelClosed_, panel, args, pair.Key.Identity);
            }
        }

       
      
        

        public void Lock()
        {
          
            _locked = true;
        }

        public void Unlock()
        {
       
            _locked = false;
        }

        public bool Locked
        {
            get { return _locked; }
        }

        public DockPanel Add(IDockPanelView view, PluginIdentity identity)
        {
            if (view == null)
            {
                throw new NullReferenceException();
            }

            if (string.IsNullOrWhiteSpace(view.DockName))
            {
                throw new ApplicationException("Dock panel must have a unique key.");
            }

            if(_dict.ContainsValue(view))
            {
                throw new ApplicationException("This control has been already added as a docking window.");
            }

            //_dockingManager.SetEnableDocking(control, true);
          
            _dict.Add(new DockPanelInfo(identity,view.DockName),view);

           return LoadDockPanel(view);
        }

        private DockPanel DockTo(string parentName, IDockPanelView view)
        {
            DevExpress.XtraBars.Docking.DockPanel parentPanel;
            if (string.IsNullOrEmpty(parentName))
                parentPanel = null;
            else
            {
                parentPanel = _dockingManager.Panels[parentName];

                if (parentPanel != null && parentPanel.Visibility == DockVisibility.Hidden)
                {
                    parentPanel = null;
                }
            }
            DevExpress.XtraBars.Docking.DockingStyle style = DockHelper.MapWindowToDevExpress(view.DefaultDock);
            if (style == DockingStyle.Fill) style = DockingStyle.Float;

            DevExpress.XtraBars.Docking.DockPanel panel = null;
            if (parentPanel == null)
                panel = _dockingManager.AddPanel(style);
            else
                panel = parentPanel.AddPanel();
            panel.Name = view.DockName;
            panel.Header = view.Caption;
            panel.Image =view.Image;
            //panel.Dock = DockHelper.MapWindowToDevExpress(((IDockPanelView)_control).DefaultDock);
            panel.FloatSize =view.DefaultSize;
            ((Control)view).Dock = DockStyle.Fill;
            panel.TabText = view.Caption;
            panel.Text = view.Caption;
            panel.Controls.Add((Control)view);
            return panel;

        }
        private DockPanel LoadDockPanel(IDockPanelView view)
        {
             return DockTo(view.DefaultNestDockName,view);
        }
        public void Remove(string panelName, PluginIdentity identity)
        {
            throw new NotImplementedException();
        }


        public void Remove(IDockPanelView view, PluginIdentity identity)
        {
            if (view == null)
            {
                throw new ArgumentException("panel");
            }

            if (!_dict.ContainsValue(view))
            {
                throw new ApplicationException("Dock panel isn't registed in the collection");
            }

            _dict.Remove(new DockPanelInfo(identity, view.DockName));

            _dockingManager.RemovePanel(_dockingManager.Panels[view.DockName]);

        }

        public void RemoveItemsForPlugin(PluginIdentity identity)
        {
            List<DockPanelInfo> panels = new List<DockPanelInfo>();

            foreach (var p in _dict)
            {
                if (p.Key.Identity == identity)
                {
                    panels.Add(p.Key);
                }
            }

            bool locked = _locked;
            if (!_locked) Lock();

            foreach (var ctrl in panels)
            {
               // _dockingManager.SetEnableDocking(ctrl, false);
                _dict.Remove(ctrl);
                _dockingManager.RemovePanel(_dockingManager.Panels[ctrl.Key]);
            }

            if (!locked) Unlock();
        }

        public DockPanel GetDockPanel(string key)
        {
            return _dockingManager.Panels[key];
        }

        public void ShowDockPanel(string dockName, bool isVisible, bool isActive)
        {
            SetDockVisible(dockName, isVisible, isActive);
        }

        public void SetActivePanel(string dockName)
        {
            SetDockVisible(dockName, true, true);
        }

        public DevExpress.XtraBars.Docking.DockPanel Find(string key)
        {
           return _dockingManager.Panels[key] as DevExpress.XtraBars.Docking.DockPanel;
        }

        public void SetDockVisible(string dockName, bool isVisible, bool isActive)
        {
            DevExpress.XtraBars.Docking.DockPanel panel = _dockingManager.Panels[dockName];
            if (panel == null)
            {
                if (isVisible == true)
                {
                    IDockPanelView view = _dict.FirstOrDefault(c => c.Key.Key == dockName).Value;
                    DockTo("", view);
                }
                return;
            }
            panel.Visible = isVisible;
            if (isVisible == true && isActive)
                _dockingManager.ActivePanel = panel;
        }


        public IEnumerator<IDockPanelView> GetEnumerator()
        {
            return (IEnumerator<IDockPanelView>) (from view in _dict select view.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
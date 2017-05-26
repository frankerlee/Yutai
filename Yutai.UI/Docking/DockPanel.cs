﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.UI.Docking
{
    internal class DockPanel : IDockPanel
    {
        private readonly DockingManager _dockingManager;
        private readonly Control _control;
        private readonly Control _parent;

        internal DockPanel(DockingManager dockingManager, Control control, Control parent)
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
                var style = _dockingManager.GetDockStyle(_control);
                return DockHelper.SyncfusionToMapWindow(style);
            }
        }

        public bool Visible
        {
            get { return _dockingManager.GetDockVisibility(_control); }
            set { _dockingManager.SetDockVisibility(_control, value); }
        }

        public void DockTo(IDockPanel parent, DockPanelState state, int size)
        {
            if (parent != null && !parent.Visible)
            {
                return;     // no need to throw exception if it not visible
            }

            var ctrl = parent != null ? parent.Control : _parent;
            _dockingManager.DockControl(_control, ctrl, DockHelper.MapWindowToSyncfusion(state), size);
        }

        public void DockTo(DockPanelState state, int size)
        {
            DockTo(null, state, size);
        }

        public string Caption
        {
            get { return _dockingManager.GetDockLabel(_control); }
            set { _dockingManager.SetDockLabel(_control, value); }
        }

        public Size Size
        {
            get { return _dockingManager.GetControlSize(_control); }
            set { _dockingManager.SetControlSize(_control, value); }
        }

        public bool FloatOnly
        {
            get { return _dockingManager.GetFloatOnly(_control); }
            set { _dockingManager.SetFloatOnly(_control, value); }
        }

        public bool AllowFloating
        {
            get { return _dockingManager.GetAllowFloating(_control); }
            set { _dockingManager.SetAllowFloating(_control, value); }
        }

        public Image GetIcon()
        {
            var list = _dockingManager.ImageList;
            if (list == null) return null;

            int index = _dockingManager.GetDockIcon(_control);
            if (index >= 0 && index < list.Images.Count)
            {
                return list.Images[index];
            }

            return null;
        }

        public void SetIcon(Icon icon)
        {
            _dockingManager.SetDockIcon(_control, icon);
        }

        public int IconIndex
        {
            get { return _dockingManager.GetDockIcon(_control); }
            set { _dockingManager.SetDockIcon(_control, value); }
        }

        public int TabPosition
        {
            get { return _dockingManager.GetTabPosition(_control); }
            set
            {
                _dockingManager.SetTabPosition(_control, value);
            }
        }

        public void Activate()
        {
            _dockingManager.ActivateControl(_control);
        }

        public bool IsFloating
        {
            get { return _dockingManager.IsFloating(_control); }
        }

        public void Float(Rectangle rect, bool tabFloating)
        {
            _dockingManager.FloatControl(_control, rect, tabFloating);
        }

        public bool AutoHidden
        {
            get { return _dockingManager.GetAutoHideMode(_control); }
            set { _dockingManager.SetAutoHideMode(_control, value); }
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
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;
using Yutai.UI.Forms;
using Yutai.Views.Abstract;

namespace Yutai.Views
{
    public partial class ConfigView : ConfigViewBase, IConfigView, IMessageFilter
    {
        public event Action SaveClicked;
        public event Action PageShown;

        public ConfigView()
        {
            InitializeComponent();

            Application.AddMessageFilter(this);

            MouseWheel += (s, e) => configPageControl1.HandleMouseWheel(e);

            btnSave.Click += (s, e) => Invoke(SaveClicked);

            FormClosed += ConfigView_FormClosed;

            InitTreeViewStyle();
        }

        private void InitTreeViewStyle()
        {
            _treeViewAdv1.SelectedNodeBackground = new BrushInfo(Color.FromKnownColor(KnownColor.Control));
            _treeViewAdv1.FullRowSelect = true;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x20a)
            {
                var pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);

                var panel = configPageControl1.Panel;
                Point p1 = panel.PointToScreen(Point.Empty);
                Point p2 = panel.PointToScreen(new Point(configPageControl1.Width, configPageControl1.Height));

                if (pos.X >= p1.X && pos.X <= p2.X && pos.Y >= p1.Y && pos.Y <= p2.Y)
                {
                    Win32Api.SendMessage(panel.Handle, m.Msg, m.WParam, m.LParam);
                    return true;
                }
            }
            return false;
        }

        public void Initialize()
        {
            _treeViewAdv1.Initialize(Model);
            _treeViewAdv1.AfterSelect += (s, e) => DisplaySelectedPage();

            if (Model.UseSelectedPage)
            {
                _treeViewAdv1.SetSelectedPage(Model.SelectedPage);
            }
            else
            {
                _treeViewAdv1.RestoreSelectedNode(AppConfig.Instance.LastConfigPage);
            }
        }

        private IConfigPage SelectedPage
        {
            get
            {
                var node = _treeViewAdv1.SelectedNode;
                if (node != null)
                {
                    return node.Tag as IConfigPage;
                }
                return null;
            }
        }

        private void ConfigView_FormClosed(object sender, FormClosedEventArgs e)
        {
            var page = SelectedPage;
            if (page != null)
            {
                AppConfig.Instance.LastConfigPage = page.PageName;
            }

            Application.RemoveMessageFilter(this);
        }

        private void DisplaySelectedPage()
        {
            var page = SelectedPage;
            var ctrl = page as Control;
            if (page == null || ctrl == null) return;

            if (page.OriginalSize == default(Size))
            {
                page.OriginalSize = ctrl.Size;
            }

            configPageControl1.Description = SelectedPage.Description;
            configPageControl1.Icon = SelectedPage.Icon;
            configPageControl1.ConfigPage = ctrl;
            ctrl.Dock = DockStyle.Fill;
            ctrl.BringToFront();

            Invoke(PageShown);

            _treeViewAdv1.SelectedNode.Expand();
        }

        public ButtonBase OkButton
        {
            get { return btnOk; }
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield return toolOptions.DropDownItems; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield return btnSave; }
        }
    }

    public class ConfigViewBase : MapWindowView<ConfigViewModel> { }

   
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Controls.Editor.UI;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Plugins.Editor.Views
{
    public partial class AttributeEditView : DockPanelControlBase, IAttributeEditView
    {
        private IAppContext _context;
        public AttributeEditView(IAppContext context)
        {
            InitializeComponent();
            _context = context;

        }

        #region Override DockPanelControlBase
        public override Bitmap Image
        {
            get { return Properties.Resources.icon_edit_attribute; }
        }

        public override string Caption
        {
            get { return "属性编辑"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock
        {
            get { return DockPanelState.Right; }
        }

        public override string DockName
        {
            get { return DefaultDockName; }
        }

        public override string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "Editor_Attribute";
        #endregion

        public IEnumerable<ToolStripItemCollection> ToolStrips { get {yield break;} }
        public IEnumerable<Control> Buttons { get {yield break;} }
        public void Initialize(IAppContext context)
        {
            attributeEditControlExtendEx1.FocusMap = _context.FocusMap;
        }
    }
    }




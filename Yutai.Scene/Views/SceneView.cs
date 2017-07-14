using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Controls;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Plugins.Scene.Views
{
    public partial class SceneView : DockPanelControlBase, ISceneView
    {
        public const string DefaultDockName = "Scene_Viewer";
        private ITOCControl _tocBuddy;
        private bool _isLinkMap;

        private IAppContext _context;
        private ScenePlugin _plugin;
        private ISceneControl _sceneControl;

        public SceneView()
        {
            InitializeComponent();
        }
        #region Override DockPanelControlBase
        public override Bitmap Image
        {
            get { return Properties.Resources.icon_secne; }
        }

        public override string Caption
        {
            get { return "三维窗口"; }
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

        
        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get {yield break;}
        }
        #endregion

        public ISceneControl SceneControl
        {
            get { return axSceneControl1.Object as ISceneControl; }
        }

        public IScene Scene
        {
            get { return axSceneControl1.Scene; }
           
        }

        public ITOCControl TOCBuddy
        {
            get { return _tocBuddy; }
            set { _tocBuddy = value; }
        }

        public bool IsLinkMap
        {
            get { return _isLinkMap; }
            set { _isLinkMap = value; }
        }

        public void OpenSXD(string docName)
        {
            axSceneControl1.LoadSxFile(docName);
        }

        public void Initialize(IAppContext context, ScenePlugin plugin)
        {
            _context = context;
            _plugin = plugin;
            _isLinkMap = false;

        }

       
    }
}

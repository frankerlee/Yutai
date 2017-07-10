using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Wrapper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    public class CmdSelectTopology : YutaiCommand,ICommandComboBox
    {
       
     
        private bool _showCaption;
        private int _layoutType;
        private object[] _items;
        private string _selectedText;
        private BarEditItem _linkComboBox;
        private bool _dropDownList;
        internal static ITopologyGraph m_TopologyGraph;
        internal static ITopology m_Topology;
        internal static ITopologyLayer m_TopologyLayer;


    
        static CmdSelectTopology()
        {
            CmdSelectTopology.old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            CmdSelectTopology.m_TopologyGraph = null;
            CmdSelectTopology.m_Topology = null;
            CmdSelectTopology.m_TopologyLayer = null;
        }
        public CmdSelectTopology(IAppContext context)
        {
            OnCreate(context);
        }

        private bool CheckWorkspaceEqual(IWorkspace iworkspace_0, IWorkspace iworkspace_1)
        {
            bool flag;
            flag = (!iworkspace_0.ConnectionProperties.IsEqual(iworkspace_1.ConnectionProperties) ? false : true);
            return flag;
        }
        private void SetTopologyObjects(object sender, EventArgs e)
        {
            if (!(this._linkComboBox.EditValue is LayerObject))
            {
                CmdSelectTopology.m_TopologyGraph = null;
            }
            else
            {
                ILayer layer = (this._linkComboBox.EditValue as LayerObject).Layer;
                CmdSelectTopology.m_TopologyLayer = layer as ITopologyLayer;
                CmdSelectTopology.m_Topology = (layer as ITopologyLayer).Topology;
                CmdSelectTopology.m_TopologyGraph = CmdSelectTopology.m_Topology.Cache;
            }
            this._context.UpdateUI();
        }
        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_absolutexy;
            this.m_caption = "拓扑：";
            this.m_category = "拓扑";
            this.m_message = "拓扑";
            this.m_name = "Edit_Topology_Select";
            this._key = "Edit_Topology_Select";
            this.m_toolTip = "拓扑";
            _context = hook as IAppContext;
            _itemType = RibbonItemType.ComboBox;
            _items = null;
            _showCaption = true;
            _dropDownList = true;
            _layoutType = 0;
        }

        public override bool Enabled
        {
            get
            {

                bool flag;
                if (this._linkComboBox != null)
                {
                    ((RepositoryItemComboBox)_linkComboBox.Edit).Items.Clear();
                }
                if (this._context.FocusMap == null)
                {
                    flag = false;
                }
                else if (this._context.FocusMap.LayerCount == 0)
                {
                    flag = false;
                }

                if (_context.MapControl.Map == null)
                {
                    return false;
                }
                if (Yutai.ArcGIS.Common.Editor.Editor.EditMap == null || Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.MapControl.Map)
                {
                    return false;
                }
                if (_context.MapControl.Map.LayerCount == 0)
                {
                    return false;
                }
                else 
                {
                    bool flag1 = false;
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                    {
                        if (this._context.FocusMap.LayerCount == 0)
                        {
                            if (this.LinkComboBox != null)
                            {
                                this.LinkComboBox.Enabled = false;
                            }
                            CmdSelectTopology.m_Topology = null;
                            CmdSelectTopology.m_TopologyGraph = null;
                            flag = false;
                            return flag;
                        }
                        
                        for (int i = 0; i < this._context.FocusMap.LayerCount; i++)
                        {
                            ILayer layer = this._context.FocusMap.Layer[i];
                            if (!(layer is IGroupLayer) && layer is ITopologyLayer && this.CheckWorkspaceEqual(Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace as IWorkspace, ((layer as ITopologyLayer).Topology as IDataset).Workspace))
                            {
                                flag1 = true;
                                if (this.LinkComboBox != null)
                                {
                                    ((RepositoryItemComboBox)_linkComboBox.Edit).Items.Add(new LayerObject(layer));
                                }
                            }
                        }
                        if (this.LinkComboBox != null)
                        {
                            if (((RepositoryItemComboBox)_linkComboBox.Edit).Items.Count != 0)
                            {
                                this._linkComboBox.Enabled = true;
                            }
                            else
                            {
                                this._linkComboBox.Enabled = false;
                            }
                        }
                        if (!flag1)
                        {
                            CmdSelectTopology.m_Topology = null;
                            CmdSelectTopology.m_TopologyGraph = null;
                        }
                    }
                    flag = flag1;
                }
               
                return flag;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
          
        }

        private void RibbonEditItemOnOnEditValueChanged(object sender, EventArgs eventArgs)
        {
           
        }


        public string Caption
        {
            get { return m_caption; }
            set { m_caption = value; }
        }

        public bool ShowCaption
        {
            get { return _showCaption; }
            set { _showCaption = value; }
        }

        public int LayoutType
        {
            get { return _layoutType; }
            set { _layoutType = value; }
        }

        public object[] Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public void OnEditValueChanged(object sender, EventArgs args)
        {
            if (!(this._linkComboBox.EditValue is LayerObject))
            {
                CmdSelectTopology.m_TopologyGraph = null;
            }
            else
            {
                ILayer layer = (this._linkComboBox.EditValue as LayerObject).Layer;
                CmdSelectTopology.m_TopologyLayer = layer as ITopologyLayer;
                CmdSelectTopology.m_Topology = (layer as ITopologyLayer).Topology;
                CmdSelectTopology.m_TopologyGraph = CmdSelectTopology.m_Topology.Cache;
            }
            this._context.UpdateUI();
        }

        public string SelectedText
        {
            get { return _selectedText; }
            set { _selectedText = value; }
        }

        public BarEditItem LinkComboBox
        {
            get { return _linkComboBox; }
            set { _linkComboBox = value; }
        }

        public bool DropDownList
        {
            get { return _dropDownList; }
        }
    }
}

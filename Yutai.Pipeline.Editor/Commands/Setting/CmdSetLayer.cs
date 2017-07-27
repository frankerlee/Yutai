// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdSetLayer.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/27  11:50
// 更新时间 :  2017/07/27  11:50

using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using ESRI.ArcGIS.Carto;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Setting
{
    public class CmdSetLayer : YutaiCommand, ICommandComboBox
    {
        private PipelineEditorPlugin _plugin;
        private string _caption;
        private bool _showCaption;
        private int _layoutType;
        private List<object> _items;
        private string _selectedText;
        private BarEditItem _linkComboBox;
        private List<IFeatureLayer> _featureLayers;

        public CmdSetLayer(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "选择图层：";
            base.m_category = "Query";
            base.m_name = "PipelineEditor_SetLayer";
            base._key = "PipelineEditor_SetLayer";
            base.m_toolTip = "选择图层";
            base.m_checked = false;
            base._itemType = RibbonItemType.ComboBox;
            _items = new List<object>();
            _layoutType = 0;
            _showCaption = true;
        }

        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null)
                    return false;
                if (_context.FocusMap.LayerCount <= 0)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap == null)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    return false;
                if (_plugin.PipeConfig.Layers.Count <= 0)
                    return false;
                if (_linkComboBox == null)
                    return false;
                if (_plugin.PipeConfig.Layers.Count <= 0)
                    return false;
                _items.Clear();
                _featureLayers = MapHelper.GetFeatureLayers(_context.FocusMap);
                foreach (IPipelineLayer pipelineLayer in _plugin.PipeConfig.Layers)
                {
                    bool noExist = pipelineLayer.Layers.Count <= 0;
                    foreach (IBasicLayerInfo basicLayerInfo in pipelineLayer.Layers)
                    {

                        if (_featureLayers.All(c => c.Name != basicLayerInfo.AliasName))
                        {
                            noExist = true;
                            break;
                        }
                    }
                    if (noExist == false)
                    {
                        LayerItem item = new LayerItem(pipelineLayer.Name, pipelineLayer);
                        _items.Add(item);
                    }
                }


                ((RepositoryItemComboBox)_linkComboBox.Edit).Items.Clear();
                ((RepositoryItemComboBox)_linkComboBox.Edit).Items.AddRange(_items.ToArray());
                return true;
            }
        }

        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
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

        object[] ICommandComboBox.Items
        {
            get { return _items.ToArray(); }
            set
            {
                _items.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    _items.Add(value[i]);
                }
            }
        }
        
        public void OnEditValueChanged(object sender, EventArgs args)
        {
            BarEditItem barEditItem = sender as BarEditItem;
            LayerItem layerItem = barEditItem.EditValue as LayerItem;
            if (layerItem != null)
                _plugin.CurrentLayer = layerItem.Value as IPipelineLayer;
            else
                _plugin.CurrentLayer = null;
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
            get { return true; }
        }
    }
}
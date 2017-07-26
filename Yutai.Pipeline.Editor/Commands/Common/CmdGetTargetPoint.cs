// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  CmdGetTargetPoint.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/26  11:23
// 更新时间 :  2017/07/26  11:23

using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Common
{
    public delegate void MouseDownEventArgs(IPoint point);
    public class CmdGetTargetPoint : YutaiTool
    {
        public event MouseDownEventArgs MouseDownEventHandler;
        private PipelineEditorPlugin _plugin;
        public CmdGetTargetPoint(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            _context.SetCurrentTool(this);
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "";
            base.m_category = "PipelineEditor";
            //base.m_bitmap = Properties.Resources.icon_valve;
            base.m_name = "PipelineEditor_GetTargetPoint";
            base._key = "PipelineEditor_GetTargetPoint";
            base.m_toolTip = "";
            base.m_checked = false;
            base.m_message = "";
            base._itemType = RibbonItemType.Tool;
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button != 1)
                return;

            IActiveView activeView = _context.ActiveView;
            IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            OnMouseDownEventHandler(point);
        }

        protected virtual void OnMouseDownEventHandler(IPoint point)
        {
            MouseDownEventHandler?.Invoke(point);
        }
    }
}
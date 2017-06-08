using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSnapOff : YutaiCommand
    {
        public CmdSnapOff(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap;
            this.m_caption = "启动捕捉";
            this.m_category = "Edit";
            this.m_message = "启动捕捉";
            this.m_name = "Edit_Snap_SnapOff";
            this._key = "Edit_Snap_SnapOff";
            this.m_toolTip = "启动捕捉";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            base._needUpdateEvent = true;
        }

        public override bool Enabled
        {
            get { return true; }
        }

        public override bool Checked
        {
            get { return _context.Config.UseSnap; }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
            //if (sender != null && base._needUpdateEvent)
            //{
            //    _context.UpdateUI();
            //}
        }

        public override void OnClick()
        {
            _context.Config.UseSnap = !_context.Config.UseSnap;
        }
    }

    public class CmdSnapPoint : YutaiCommand
    {
        public CmdSnapPoint(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_point;
            this.m_caption = "点捕捉";
            this.m_category = "Edit";
            this.m_message = "点捕捉";
            this.m_name = "Edit_Snap_Config_SnapPoint";
            this._key = "Edit_Snap_Config_SnapPoint";
            this.m_toolTip = "点捕捉";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            base._needUpdateEvent = true;
        }

        public override bool Enabled
        {
            get { return true; }
        }

        public override bool Checked
        {
            get { return _context.Config.IsSnapPoint; }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
            //if (sender != null && base._needUpdateEvent)
            //{
            //    _context.UpdateUI();
            //}
        }

        public override void OnClick()
        {
            _context.Config.IsSnapPoint = !_context.Config.IsSnapPoint;
        }
    }

   

    public class CmdSnapEndPointt : YutaiCommand
    {
        public CmdSnapEndPointt(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_endpoint;
            this.m_caption = "终点捕捉";
            this.m_category = "Edit";
            this.m_message = "终点捕捉";
            this.m_name = "Edit_Snap_Config_SnapEndPoint";
            this._key = "Edit_Snap_Config_SnapEndPoint";
            this.m_toolTip = "终点捕捉";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            base._needUpdateEvent = true;
        }

        public override bool Enabled
        {
            get { return true; }
        }

        public override bool Checked
        {
            get { return _context.Config.IsSnapEndPoint; }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
            //if (sender != null && base._needUpdateEvent)
            //{
            //    _context.UpdateUI();
            //}
        }

        public override void OnClick()
        {
            _context.Config.IsSnapEndPoint = !_context.Config.IsSnapEndPoint;
        }
    }

    public class CmdSnapVertexPoint : YutaiCommand
    {
        public CmdSnapVertexPoint(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_vertex;
            this.m_caption = "端点捕捉";
            this.m_category = "Edit";
            this.m_message = "端点捕捉";
            this.m_name = "Edit_Snap_Config_SnapVertexPoint";
            this._key = "Edit_Snap_Config_SnapVertexPoint";
            this.m_toolTip = "端点捕捉";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            base._needUpdateEvent = true;
        }

        public override bool Enabled
        {
            get { return true; }
        }

        public override bool Checked
        {
            get { return _context.Config.IsSnapVertexPoint; }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
            if (sender != null && base._needUpdateEvent)
            {
                _context.UpdateUI();
            }
        }

        public override void OnClick()
        {
            _context.Config.IsSnapVertexPoint = !_context.Config.IsSnapVertexPoint;
        }
    }

    public class CmdSnapBoundary : YutaiCommand
    {
        public CmdSnapBoundary(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_boundary;
            this.m_caption = "边线捕捉";
            this.m_category = "Edit";
            this.m_message = "边线捕捉";
            this.m_name = "Edit_Snap_Config_SnapBoundary";
            this._key = "Edit_Snap_Config_SnapBoundary";
            this.m_toolTip = "边线捕捉";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            base._needUpdateEvent = true;
        }

        public override bool Enabled
        {
            get { return true; }
        }

        public override bool Checked
        {
            get { return _context.Config.IsSnapBoundary; }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
            if (sender != null && base._needUpdateEvent)
            {
                _context.UpdateUI();
            }
        }

        public override void OnClick()
        {
            _context.Config.IsSnapBoundary = !_context.Config.IsSnapBoundary;
        }
    }


    public class CmdSnapTangent : YutaiCommand
    {
        public CmdSnapTangent(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_tangent;
            this.m_caption = "切线捕捉";
            this.m_category = "Edit";
            this.m_message = "切线捕捉";
            this.m_name = "Edit_Snap_Config_SnapTangent";
            this._key = "Edit_Snap_Config_SnapTangent";
            this.m_toolTip = "切线捕捉";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            base._needUpdateEvent = true;
        }

        public override bool Enabled
        {
            get { return true; }
        }

        public override bool Checked
        {
            get { return _context.Config.IsSnapBoundary; }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
            //if (sender != null && base._needUpdateEvent)
            //{
            //    _context.UpdateUI();
            //}
        }

        public override void OnClick()
        {
            _context.Config.IsSnapTangent = !_context.Config.IsSnapTangent;
        }
    }
}
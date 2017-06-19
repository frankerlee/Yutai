using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Forms;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdEditCopy : YutaiCommand
    {
        internal static bool m_bIsCopyFeature;

        internal static IArray m_pArray;

        internal static List<IFeature> m_pFeatureList;

        internal static IEnumFeature m_pEnumFeature;

        private bool bool_0 = false;

        static CmdEditCopy()
        {
            // 注意: 此类型已标记为 'beforefieldinit'.
            CmdEditCopy.old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            CmdEditCopy.m_bIsCopyFeature = false;
            CmdEditCopy.m_pArray = new ESRI.ArcGIS.esriSystem.Array();
            CmdEditCopy.m_pFeatureList = new List<IFeature>();
            CmdEditCopy.m_pEnumFeature = null;
        }
        public CmdEditCopy(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_copy;
            this.m_caption = "复制";
            this.m_category = "Edit";
            this.m_message = "复制";
            this.m_name = "Edit_Copy";
            this._key = "Edit_Copy";
            this.m_toolTip = "复制";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
            
        }

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else
                {
                    this.bool_0 = false;
                    if (_context.FocusMap.LayerCount > 0 && _context.FocusMap.SelectionCount > 0)
                    {
                        this.bool_0 = true;
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;
            }
        }

      


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {

            if (this.bool_0 && _context.FocusMap.LayerCount > 0 && _context.FocusMap.SelectionCount > 0)
            {
                CmdEditCopy.m_bIsCopyFeature = true;
                CmdEditCopy.m_pFeatureList.Clear();
                CmdEditCopy.m_pEnumFeature = (_context.FocusMap.FeatureSelection as IEnumFeature);
                CmdEditCopy.m_pEnumFeature.Reset();
                for (IFeature feature = CmdEditCopy.m_pEnumFeature.Next(); feature != null; feature = CmdEditCopy.m_pEnumFeature.Next())
                {
                    CmdEditCopy.m_pFeatureList.Add(feature);
                }
            }
        }
    }
}
    
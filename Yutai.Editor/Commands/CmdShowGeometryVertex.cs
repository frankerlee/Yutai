using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdShowGeometryVertex:YutaiCommand,ITask
    {
        private IActiveView activeView;


        public CmdShowGeometryVertex(IAppContext context)
        {
            OnCreate(context);
        }

       
        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "编辑折点";
            base.m_category = "Editor";
            base.m_bitmap = Properties.Resource.ShowVert;
            base.m_name = "Editor_Show_Vertex";
            base._key = "Editor_Show_Vertex";
            base.m_toolTip = "编辑折点";
            base._itemType = RibbonItemType.Button;
          
        }

        public override bool Checked
        {
            get
            {
                return SketchToolAssist.CurrentTask == this;
            }
        }

        public override bool Enabled
        {
            get
            {
                bool flag;
                if (_context.FocusMap == null)
                {
                    flag = false;
                }
                else if (_context.Config.CanEdited)
                {
                    flag = (_context.FocusMap.SelectionCount != 1 ? false : true);
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }

        public string DefaultTool
        {
            get
            {
                return "EditTool";
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            Yutai.ArcGIS.Common.Editor.Editor.DrawNode = true;
            SketchToolAssist.CurrentTask = this;
            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
            featureSelection.Reset();
            IEnvelope envelope = featureSelection.Next().Shape.Envelope;
            envelope.Expand(10, 10, false);
            _context.MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, envelope);
            base.OnClick();
        }

        public void Excute()
        {
            
        }

        public bool CheckTaskStatue(ITool itool_0)
        {
            string name = (itool_0 as ICommand).Name;
            bool flag = false;
            if (string.Compare(name, "EditTool", true) == 0)
            {
                flag = true;
            }
            if (!flag)
            {
                Yutai.ArcGIS.Common.Editor.Editor.DrawNode = false;
                SketchToolAssist.ReSet();
            }
            return flag;
        }
    }
}

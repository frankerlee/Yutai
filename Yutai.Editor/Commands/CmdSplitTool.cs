using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSplitTool : YutaiTool
    {
        public override bool Enabled
        {
            get
            {
                bool result;
                try
                {
                    if (_context.FocusMap == null)
                    {
                        result = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    {
                        result = false;
                    }
                    else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
                catch
                {
                    result = false;
                }
                return result;
            }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "拆分";
            this.m_category = "高级编辑工具";
            this.m_name = "Edit_SplitTool";
            this.m_message = "拆分一个线要素";
            this.m_toolTip = "拆分";
            this.m_bitmap = Properties.Resources.icon_edit_splittool;
            this.m_cursor = new System.Windows.Forms.Cursor(new MemoryStream(Resource.Digitise));

            _context = hook as IAppContext;
            this._key = "Edit_SplitTool";
            base._itemType = RibbonItemType.Tool;
        }

        public CmdSplitTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            IPoint mapPoint = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
            IEnumFeature featureSelection = _context.FocusMap.FeatureSelection as IEnumFeature;
            featureSelection.Reset();
            IFeature feature = featureSelection.Next();
            IInvalidArea invalidAreaClass = new InvalidAreaClass()
            {
                Display = (_context.FocusMap as IActiveView).ScreenDisplay
            };
            invalidAreaClass.Add(feature);
            Yutai.ArcGIS.Common.Editor.Editor.StartEditOperation(feature.Class as IDataset);
            try
            {
                (feature as IFeatureEdit).Split(mapPoint);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            Yutai.ArcGIS.Common.Editor.Editor.StopEditOperation(feature.Class as IDataset);
            invalidAreaClass.Invalidate(-2);
            _context.ClearCurrentTool();
        }
    }
}
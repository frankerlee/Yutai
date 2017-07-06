using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using ICommandSubType = ESRI.ArcGIS.SystemUI.ICommandSubType;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdAlignGraphicElement : YutaiCommand, ICommandSubType
    {
        private int AlignType = 0;

        public override bool Enabled
        {
            get
            {
                IActiveView activeView = this._context.ActiveView;
                return activeView != null && activeView is IGraphicsContainerSelect &&
                       (activeView as IGraphicsContainerSelect).ElementSelectionCount > 1;
            }
        }

        public override void OnCreate(object hook)
        {
            base.m_name = "AlignGraphicElementTop";
            base.m_caption = "";
            base.m_toolTip = "顶对齐";
            base.m_bitmap = Properties.Resources.icon_align_left;
            base.m_name = "Printing_AlignElement";
            _key = "Printing_AlignElement";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdAlignGraphicElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IEnumElement selectedElements = (this._context.ActiveView as IGraphicsContainerSelect).SelectedElements;
            IAlignElementsOperation alignElementsOperation = new AlignElementsOperation();
            IElement dominantElement = (this._context.ActiveView as IGraphicsContainerSelect).DominantElement;
            if (dominantElement != null)
            {
                IEnvelope envelope = new Envelope() as IEnvelope;
                dominantElement.QueryBounds(this._context.ActiveView.ScreenDisplay, envelope);
                alignElementsOperation.Elements = selectedElements;
                alignElementsOperation.AlignEnvelope = envelope;
                alignElementsOperation.AlignType = (enumAlignType) this.AlignType;
                alignElementsOperation.ActiveView = this._context.ActiveView;
                this._context.OperationStack.Do(alignElementsOperation);
                //DocumentManager.DocumentChanged(this._context.Hook);
            }
        }

        public int SubType
        {
            get { return AlignType; }
            set { SetSubType(value); }
        }

        public void SetSubType(int alignType)
        {
            this.AlignType = alignType;
            string name;
            switch (this.AlignType)
            {
                case 0:
                    this.m_name = "Printing_AlignGraphicElementTop";
                    this._key = "Printing_AlignGraphicElementTop";
                    this.m_caption = "";
                    this.m_toolTip = "顶对齐";
                    m_bitmap = Properties.Resources.icon_align_top;
                    break;
                case 1:
                    this.m_name = "Printing_AlignGraphicElementBottom";
                    this._key = "Printing_AlignGraphicElementBottom";
                    this.m_caption = "";
                    this.m_toolTip = "底对齐";
                    m_bitmap = Properties.Resources.icon_align_bottom;
                    break;
                case 2:
                    this.m_name = "Printing_AlignGraphicElementLeft";
                    this._key = "Printing_AlignGraphicElementLeft";
                    this.m_caption = "";
                    this.m_toolTip = "左对齐";
                    m_bitmap = Properties.Resources.icon_align_left;
                    break;
                case 3:
                    this.m_name = "Printing_AlignGraphicElementRight";
                    this._key = "Printing_AlignGraphicElementRight";
                    this.m_caption = "";
                    this.m_toolTip = "右对齐";
                    m_bitmap = Properties.Resources.icon_align_right;
                    break;
                default:
                    return;
            }
            //this.m_bitmap = new System.Drawing.Bitmap(base.GetType().Assembly.GetManifestResourceStream(name));
        }

        public int GetCount()
        {
            return 4;
        }
    }
}
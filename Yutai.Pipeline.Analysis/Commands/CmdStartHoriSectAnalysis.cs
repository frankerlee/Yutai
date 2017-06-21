using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using stdole;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdStartHoriSectAnalysis : YutaiTool
    {
        private IPoint _iPoint;
        private INewLineFeedback _lineFeedback;
        private bool _inLine;
        private Cursor _cursor;
        private Cursor _cursor1;

        private IPolyline _sectLine;

        public CmdStartHoriSectAnalysis(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            _inLine = false;
            _context.SetCurrentTool(this);
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "横断面分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_transect;
            _cursor =
                new System.Windows.Forms.Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Pipeline.Analysis.Resources.Cursor.Hand.cur"));
            _cursor1 =
                new Cursor(
                    base.GetType()
                        .Assembly.GetManifestResourceStream("Yutai.Pipeline.Analysis.Resources.Cursor.MoveHand.cur"));
            base.m_name = "PipeAnalysis_HoriSectionStart";
            base._key = "PipeAnalysis_HoriSectionStart";
            base.m_toolTip = "横断面分析";
            base.m_checked = false;
            base.m_message = "横断面分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (_inLine && keyCode == 27)
            {
                this._lineFeedback = null;
                this._inLine = false;
                base.m_cursor = this._cursor1;
            }
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button == 2) return;

            this._iPoint = (_context.ActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IActiveView focusMap = (IActiveView) _context.ActiveView;
            if (_lineFeedback == null)
            {
                _lineFeedback = new NewLineFeedback()
                {
                    Display = focusMap.ScreenDisplay
                };
                _lineFeedback.Start(_iPoint);
                this.m_cursor = this._cursor1;
                _inLine = true;
                return;
            }
            if (this._inLine)
            {
                
                this.m_cursor = this._cursor;
                this._inLine = false;
                if (this._lineFeedback != null)
                {
                    _lineFeedback.AddPoint(_iPoint);
                    IPolyline line = this._lineFeedback.Stop();
                    if (line.Length > 0)
                    {
                        //开始启动断面分析
                        _sectLine = line;
                        StartSection();
                    }
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int x, int y)
        {
            if (this._inLine)
            {
                IActiveView focusMap = (IActiveView) _context.ActiveView;

                _lineFeedback.MoveTo(focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y));
            }
        }

        public IElement GetTextElement(double dblAngle, string sNoteText, IPoint PosPt)
        {
            return this.GetTextElement(dblAngle, sNoteText, PosPt, 0);
        }

        public IElement GetTextElement(double dblAngle, string sNoteText, IPoint PosPt, int iColor)
        {
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.RGB=(iColor);
            IFont systemFontClass = new SystemFont()
            {
                Name = "宋体",
                Size = new decimal(8)
            } as IFont;
            ITextSymbol textSymbolClass = new TextSymbol();
            textSymbolClass.Color=(rgbColorClass);
            textSymbolClass.Font=((IFontDisp)systemFontClass);
            textSymbolClass.Angle=(dblAngle);
            textSymbolClass.RightToLeft=(false);
            textSymbolClass.VerticalAlignment=(esriTextVerticalAlignment)0;
            textSymbolClass.HorizontalAlignment=(esriTextHorizontalAlignment)(0);
            ITextElement textElementClass = new TextElement() as ITextElement;
            textElementClass.Symbol=(textSymbolClass);
            textElementClass.Text=(sNoteText);
            textElementClass.ScaleText=(true);
            ((IElement)textElementClass).Geometry=(PosPt);
            ((IElementProperties)textElementClass).Name=("EMMapNote");
            return (IElement)textElementClass;
        }

        private void StartSection()
        {
            DMCoordForm dMCoordForm = new DMCoordForm()
            {
                pPolyline = _sectLine
            };
            if (dMCoordForm.ShowDialog() == DialogResult.OK)
            {
                _sectLine = dMCoordForm.pPolyline;
            }

            ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
            IRgbColor rgbColorClass = ColorManage.CreatColor(255, 0, 0) as IRgbColor;
            simpleLineSymbolClass.Color = (rgbColorClass);
            simpleLineSymbolClass.Width = 2;

            if (_sectLine != null)
            {
                CommonUtils.NewLineElement(_context.FocusMap, _sectLine);
                double num = _sectLine.FromPoint.X;
                string str = num.ToString("f2");
                num = _sectLine.FromPoint.Y;
                string str1 = string.Concat("X =", str, " Y=", num.ToString("f2"));
                IElement textElement = this.GetTextElement(0, str1, _sectLine.FromPoint);
                ((IGraphicsContainer) _context.ActiveView).AddElement(textElement, 0);
                num = _sectLine.ToPoint.X;
                string str2 = num.ToString("f2");
                num = _sectLine.ToPoint.Y;
                str1 = string.Concat("X =", str2, " Y=", num.ToString("f2"));
                textElement = this.GetTextElement(0, str1, _sectLine.ToPoint);
                ((IGraphicsContainer) _context.ActiveView).AddElement(textElement, 0);
                this._lineFeedback = null;
                SectionViewFrm sectionViewFrm = new SectionViewFrm(SectionViewFrm.SectionType.SectionTypeTransect,
                    _context);
                sectionViewFrm.PutBaseLine(_sectLine);
                sectionViewFrm.ShowDialog();
            }
        }
    }
}
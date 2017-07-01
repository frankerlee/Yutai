using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ExtendClass;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateTableElement : MapTemplateElement
    {
        private bool bool_0;
        private SortedList<int, SortedList<int, string>> sortedList_0;

        public MapTemplateTableElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            this.sortedList_0 = new SortedList<int, SortedList<int, string>>();
            this.bool_0 = true;
            base.MapTemplateElementType = MapTemplateElementType.TableElement;
            base.Name = "表格";
            this.Width = 4.0;
            this.Height = 4.0;
            this.RowNumber = 2;
            this.ColumnNumber = 4;
        }

        public MapTemplateTableElement(int int_3, MapTemplate mapTemplate_1) : base(int_3, mapTemplate_1)
        {
            this.sortedList_0 = new SortedList<int, SortedList<int, string>>();
            this.bool_0 = true;
            base.MapTemplateElementType = MapTemplateElementType.TableElement;
            base.Name = "表格";
            this.Width = 4.0;
            this.Height = 4.0;
            this.RowNumber = 2;
            this.ColumnNumber = 4;
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplateTableElement element = new MapTemplateTableElement(mapTemplate_1);
            try
            {
                element.ColumnNumber = this.ColumnNumber;
                element.Height = this.Height;
                element.Width = this.Width;
                element.RowNumber = this.RowNumber;
            }
            catch (Exception)
            {
            }
            this.CopyTo(element);
            return element;
        }

        public override IElement CreateElement(IPageLayout ipageLayout_0)
        {
            IPoint position = this.GetPosition(ipageLayout_0);
            TableElement element = new TableElement
            {
                Height = this.Height,
                Width = this.Width,
                RowNumber = this.RowNumber,
                ColumnNumber = this.ColumnNumber
            };
            element.CreateTable(ipageLayout_0 as IActiveView, position, this.sortedList_0);
            this.Element = element;
            return this.Element;
        }

        public string GetCellElement(int int_3, int int_4)
        {
            if (this.sortedList_0.ContainsKey(int_3))
            {
                SortedList<int, string> list = this.sortedList_0[int_3];
                if (list.ContainsKey(int_4))
                {
                    return list[int_4];
                }
            }
            return "";
        }

        public override IElement GetElement(IPageLayout ipageLayout_0)
        {
            IElement element = base.GetElement(ipageLayout_0);
            if (this.bool_0)
            {
                IEnvelope envelope = (MapFrameAssistant.GetFocusMapFrame(ipageLayout_0) as IElement).Geometry.Envelope;
                IEnvelope from = element.Geometry.Envelope;
                IEnvelope to = element.Geometry.Envelope;
                to.PutCoords(from.XMin, from.YMin, from.XMin + envelope.Width, from.YMax);
                IAffineTransformation2D transformation = new AffineTransformation2DClass();
                transformation.DefineFromEnvelopes(from, to);
                (element as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
            }
            return element;
        }

        public override void Init()
        {
            for (int i = 0; i < this.RowNumber; i++)
            {
                for (int j = 0; j < this.ColumnNumber; j++)
                {
                    string cellElement = this.GetCellElement(i, j);
                    if ((cellElement.Length != 0) && (cellElement[0] == '='))
                    {
                        ExpressionCalculator calculator = new ExpressionCalculator();
                        calculator.Init(cellElement.Substring(1));
                        for (int k = 0; k < calculator.ParamList.Count; k++)
                        {
                            string str2 = calculator.ParamList.Keys[k];
                            MapTemplateParam param = base.MapTemplate.FindParamByName(str2);
                            if (param != null)
                            {
                                calculator.ParamList[str2] = param.Value;
                            }
                        }
                        object obj2 = calculator.Calculate();
                        ITextElement element = (this.Element as ITableElement).GetCellElement(i, j) as ITextElement;
                        if (element != null)
                        {
                            element.Text = obj2.ToString();
                        }
                    }
                }
            }
        }

        public void SetTableCell(int int_3, int int_4, string string_1)
        {
            SortedList<int, string> list;
            if (this.sortedList_0.ContainsKey(int_3))
            {
                list = this.sortedList_0[int_3];
                if (list.ContainsKey(int_4))
                {
                    list[int_4] = string_1;
                }
                else
                {
                    list.Add(int_4, string_1);
                }
            }
            else
            {
                list = new SortedList<int, string>();
                list.Add(int_4, string_1);
                this.sortedList_0.Add(int_3, list);
            }
        }

        public override void Update(IPageLayout ipageLayout_0)
        {
            if (this.Element != null)
            {
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.Element, null);
                IPoint position = this.GetPosition(ipageLayout_0);
                (this.Element as TableElement).RowNumber = this.RowNumber;
                (this.Element as TableElement).ColumnNumber = this.ColumnNumber;
                IEnvelope to = this.Element.Geometry.Envelope;
                (this.Element as TableElement).CreateTable(ipageLayout_0 as IActiveView, position, this.sortedList_0);
                IEnvelope envelope = this.Element.Geometry.Envelope;
                this.Element.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelope);
                try
                {
                    IAffineTransformation2D transformation = new AffineTransformation2DClass();
                    transformation.DefineFromEnvelopes(envelope, to);
                    (base.m_pElement as ITransform2D).Transform(esriTransformDirection.esriTransformForward,
                        transformation);
                }
                catch
                {
                }
                (ipageLayout_0 as IActiveView).GraphicsContainer.UpdateElement(base.m_pElement);
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                this.Save();
            }
        }

        public int ColumnNumber { get; set; }

        public double Height { get; set; }

        protected override IPropertySet PropertySet
        {
            get
            {
                IPropertySet set = new PropertySetClass();
                set.SetProperty("Width", this.Width);
                set.SetProperty("Height", this.Height);
                set.SetProperty("Row", this.RowNumber);
                set.SetProperty("Col", this.ColumnNumber);
                for (int i = 0; i < this.RowNumber; i++)
                {
                    for (int j = 0; j < this.ColumnNumber; j++)
                    {
                        set.SetProperty(string.Format("{0},{1}", i, j), this.GetCellElement(i, j));
                    }
                }
                return set;
            }
            set
            {
                try
                {
                    this.Width = Convert.ToDouble(value.GetProperty("Width"));
                    this.Height = Convert.ToDouble(value.GetProperty("Height"));
                    this.RowNumber = Convert.ToInt32(value.GetProperty("Row"));
                    this.ColumnNumber = Convert.ToInt32(value.GetProperty("Col"));
                    for (int i = 0; i < this.RowNumber; i++)
                    {
                        for (int j = 0; j < this.ColumnNumber; j++)
                        {
                            this.SetTableCell(i, j, value.GetProperty(string.Format("{0},{1}", i, j)).ToString());
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public int RowNumber { get; set; }

        public double Width { get; set; }
    }
}
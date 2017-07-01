using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    internal class RenderInfoListView : ListView
    {
        private bool bool_0 = false;
        public bool[] ColumnEditables = null;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private ImageList imageList_1;
        private int int_0;
        private int int_1;
        private int int_2 = 0;
        private IStyleGallery istyleGallery_0 = null;
        private ListViewItem listViewItem_0;
        private TextBox textBox;

        public event OnValueChangedHandler OnValueChanged;

        public RenderInfoListView()
        {
            this.method_0();
            this.textBox.LostFocus += new EventHandler(this.textBox_LostFocus);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.Controls.Add(this.textBox);
            base.View = View.Details;
        }

        public ListViewItemEx Add(object[] object_0)
        {
            int size;
            if (base.Items.Count == 0)
            {
                this.imageList_0.ImageSize = new Size(16, 16);
            }
            if (object_0 == null)
            {
                return null;
            }
            string[] strArray = new string[base.Columns.Count];
            strArray[0] = "";
            object obj2 = null;
            for (int i = 0; i < object_0.Length; i++)
            {
                if (i == 0)
                {
                    obj2 = object_0[i];
                }
                else if (object_0[i] != null)
                {
                    strArray[i] = object_0[i].ToString();
                }
                else
                {
                    strArray[i] = "";
                }
            }
            if (obj2 is IMarkerSymbol)
            {
                size = (int) (obj2 as IMarkerSymbol).Size;
                if (size > 40)
                {
                    size = 40;
                }
                if (size > this.imageList_0.ImageSize.Height)
                {
                    this.imageList_0.ImageSize = new Size(size, size);
                }
            }
            else if (obj2 is ILineSymbol)
            {
                size = (int) (obj2 as ILineSymbol).Width;
                if (size > 40)
                {
                    size = 40;
                }
                if (size > this.imageList_0.ImageSize.Height)
                {
                    this.imageList_0.ImageSize = new Size(size, size);
                }
            }
            ListViewItemEx ex = new ListViewItemEx(strArray)
            {
                Style = obj2
            };
            base.Items.Add(ex);
            return ex;
        }

        public void Add(IStyleGalleryItem istyleGalleryItem_0)
        {
            ListViewItemEx ex = new ListViewItemEx(new string[] {istyleGalleryItem_0.Name, istyleGalleryItem_0.Category})
            {
                Tag = istyleGalleryItem_0
            };
            base.Items.Add(ex);
        }

        public void Add(ListViewItemEx listViewItemEx_0)
        {
            int size;
            if (base.Items.Count == 0)
            {
                this.imageList_0.ImageSize = new Size(16, 16);
            }
            object tag = listViewItemEx_0.Tag;
            if (tag is IMarkerSymbol)
            {
                size = (int) (tag as IMarkerSymbol).Size;
                if (size > 40)
                {
                    size = 40;
                }
                if (size > this.imageList_0.ImageSize.Height)
                {
                    this.imageList_0.ImageSize = new Size(size, size);
                }
            }
            else if (tag is ILineSymbol)
            {
                size = (int) (tag as ILineSymbol).Width;
                if (size > 40)
                {
                    size = 40;
                }
                if (size > this.imageList_0.ImageSize.Height)
                {
                    this.imageList_0.ImageSize = new Size(size, size);
                }
            }
            base.Items.Add(listViewItemEx_0);
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        protected void DrawSymbol(int int_3, Rectangle rectangle_0, object object_0)
        {
            if (object_0 != null)
            {
                tagRECT grect;
                ISymbol symbol;
                IDisplayTransformation transformation = new DisplayTransformationClass();
                IEnvelope bounds = new EnvelopeClass();
                bounds.PutCoords((double) rectangle_0.Left, (double) rectangle_0.Top, (double) rectangle_0.Right,
                    (double) rectangle_0.Bottom);
                grect.left = rectangle_0.Left;
                grect.right = rectangle_0.Right;
                grect.bottom = rectangle_0.Bottom;
                grect.top = rectangle_0.Top;
                transformation.set_DeviceFrame(ref grect);
                transformation.Bounds = bounds;
                transformation.Resolution = 96.0;
                transformation.ReferenceScale = 1.0;
                transformation.ScaleRatio = 1.0;
                if (object_0 is ISymbol)
                {
                    symbol = object_0 as ISymbol;
                }
                else if (object_0 is IColorRamp)
                {
                    IGradientFillSymbol symbol2 = new GradientFillSymbolClass();
                    ILineSymbol outline = symbol2.Outline;
                    outline.Width = 0.0;
                    symbol2.Outline = outline;
                    symbol2.ColorRamp = object_0 as IColorRamp;
                    symbol2.GradientAngle = 180.0;
                    symbol2.GradientPercentage = 1.0;
                    symbol2.IntervalCount = 100;
                    symbol2.Style = esriGradientFillStyle.esriGFSLinear;
                    symbol = (ISymbol) symbol2;
                }
                else if (object_0 is IColor)
                {
                    IColorSymbol symbol4 = new ColorSymbolClass
                    {
                        Color = object_0 as IColor
                    };
                    symbol = (ISymbol) symbol4;
                }
                else if (object_0 is IAreaPatch)
                {
                    symbol = new SimpleFillSymbolClass();
                    IRgbColor color = new RgbColorClass
                    {
                        Red = 227,
                        Green = 236,
                        Blue = 19
                    };
                    ((IFillSymbol) symbol).Color = color;
                }
                else
                {
                    if (!(object_0 is ILinePatch))
                    {
                        IDisplay display;
                        if (object_0 is INorthArrow)
                        {
                            display = new ScreenDisplayClass();
                            display.StartDrawing(int_3, 0);
                            display.DisplayTransformation = transformation;
                            ((IMapSurround) object_0).Draw(display, null, bounds);
                            display.FinishDrawing();
                            ((IMapSurround) object_0).Refresh();
                        }
                        else if (object_0 is IMapSurround)
                        {
                            bool flag;
                            display = new ScreenDisplayClass();
                            display.StartDrawing(int_3, 0);
                            display.DisplayTransformation = transformation;
                            IEnvelope newBounds = new EnvelopeClass();
                            newBounds.PutCoords((double) (rectangle_0.Left + 5), (double) (rectangle_0.Top + 5),
                                (double) (rectangle_0.Right - 5), (double) (rectangle_0.Bottom - 5));
                            ((IMapSurround) object_0).QueryBounds(display, bounds, newBounds);
                            ((IMapSurround) object_0).FitToBounds(display, newBounds, out flag);
                            ((IMapSurround) object_0).Draw(display, null, newBounds);
                            display.FinishDrawing();
                            ((IMapSurround) object_0).Refresh();
                        }
                        else
                        {
                            IGeometry geometry;
                            if (object_0 is IBackground)
                            {
                                display = new ScreenDisplayClass();
                                display.StartDrawing(int_3, 0);
                                display.DisplayTransformation = transformation;
                                geometry = ((IBackground) object_0).GetGeometry(display, bounds);
                                ((IBackground) object_0).Draw(display, geometry);
                                display.FinishDrawing();
                            }
                            else if (object_0 is IShadow)
                            {
                                display = new ScreenDisplayClass();
                                display.StartDrawing(int_3, 0);
                                display.DisplayTransformation = transformation;
                                double horizontalSpacing = ((IShadow) object_0).HorizontalSpacing;
                                double verticalSpacing = ((IShadow) object_0).VerticalSpacing;
                                ((IShadow) object_0).HorizontalSpacing = 0.0;
                                ((IShadow) object_0).VerticalSpacing = 0.0;
                                geometry = ((IShadow) object_0).GetGeometry(display, bounds);
                                ((IShadow) object_0).Draw(display, geometry);
                                ((IShadow) object_0).HorizontalSpacing = horizontalSpacing;
                                ((IShadow) object_0).VerticalSpacing = verticalSpacing;
                                display.FinishDrawing();
                            }
                            else if (object_0 is IBorder)
                            {
                                display = new ScreenDisplayClass();
                                display.StartDrawing(int_3, 0);
                                display.DisplayTransformation = transformation;
                                IPointCollection points = new PolylineClass();
                                object before = Missing.Value;
                                IPoint inPoint = new PointClass();
                                inPoint.PutCoords((double) (rectangle_0.X + 4), (double) rectangle_0.Top);
                                points.AddPoint(inPoint, ref before, ref before);
                                inPoint.PutCoords((double) (rectangle_0.X + 4), (double) rectangle_0.Bottom);
                                points.AddPoint(inPoint, ref before, ref before);
                                inPoint.PutCoords((double) (rectangle_0.Right - 4), (double) rectangle_0.Bottom);
                                points.AddPoint(inPoint, ref before, ref before);
                                geometry = ((IBorder) object_0).GetGeometry(display, (IGeometry) points);
                                ((IBorder) object_0).Draw(display, geometry);
                                display.FinishDrawing();
                            }
                        }
                        return;
                    }
                    symbol = new SimpleLineSymbolClass();
                }
                if ((symbol is IPictureFillSymbol) || (symbol is IPictureLineSymbol))
                {
                    symbol.SetupDC(int_3, null);
                }
                else
                {
                    symbol.SetupDC(int_3, transformation);
                }
                if (object_0 is IPatch)
                {
                    this.method_3(symbol, (IPatch) object_0, rectangle_0);
                }
                else if (symbol is IMarkerSymbol)
                {
                    this.method_2((IMarkerSymbol) symbol, rectangle_0);
                }
                else if (symbol is ILineSymbol)
                {
                    this.method_4((ILineSymbol) symbol, rectangle_0);
                }
                else if (symbol is IFillSymbol)
                {
                    this.method_5((IFillSymbol) symbol, rectangle_0);
                }
                else if (symbol is ITextSymbol)
                {
                    this.method_7((ITextSymbol) symbol, rectangle_0);
                }
                symbol.ResetDC();
            }
        }

        public ListViewItemEx Insert(object[] object_0, int int_3)
        {
            int size;
            if (base.Items.Count == 0)
            {
                this.imageList_0.ImageSize = new Size(16, 16);
            }
            if (object_0 == null)
            {
                return null;
            }
            string[] strArray = new string[base.Columns.Count];
            strArray[0] = "";
            object obj2 = null;
            for (int i = 0; i < object_0.Length; i++)
            {
                if (i == 0)
                {
                    obj2 = object_0[i];
                }
                else if (object_0[i] != null)
                {
                    strArray[i] = object_0[i].ToString();
                }
                else
                {
                    strArray[i] = "";
                }
            }
            if (obj2 is IMarkerSymbol)
            {
                size = (int) (obj2 as IMarkerSymbol).Size;
                if (size > 40)
                {
                    size = 40;
                }
                if (size > this.imageList_0.ImageSize.Height)
                {
                    this.imageList_0.ImageSize = new Size(size, size);
                }
            }
            else if (obj2 is ILineSymbol)
            {
                size = (int) (obj2 as ILineSymbol).Width;
                if (size > 40)
                {
                    size = 40;
                }
                if (size > this.imageList_0.ImageSize.Height)
                {
                    this.imageList_0.ImageSize = new Size(size, size);
                }
            }
            ListViewItemEx item = new ListViewItemEx(strArray)
            {
                Style = obj2
            };
            base.Items.Insert(int_3, item);
            return item;
        }

        private void method_0()
        {
            this.icontainer_0 = new Container();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.imageList_1 = new ImageList(this.icontainer_0);
            this.textBox = new TextBox();
            this.imageList_0.ImageSize = new Size(16, 16);
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_1.ImageSize = new Size(48, 48);
            this.imageList_1.TransparentColor = Color.Transparent;
            this.textBox.AutoSize = false;
            this.textBox.BorderStyle = BorderStyle.FixedSingle;
            this.textBox.Location = new System.Drawing.Point(243, 17);
            this.textBox.Name = "textBox";
            this.textBox.TabIndex = 0;
            this.textBox.Text = "";
            this.textBox.Visible = false;
            this.textBox.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
            this.textBox.Leave += new EventHandler(this.textBox_Leave);
            base.FullRowSelect = true;
            base.LargeImageList = this.imageList_1;
            base.SmallImageList = this.imageList_0;
            base.MouseDown += new MouseEventHandler(this.RenderInfoListView_MouseDown);
            base.DoubleClick += new EventHandler(this.RenderInfoListView_DoubleClick);
            base.SelectedIndexChanged += new EventHandler(this.RenderInfoListView_SelectedIndexChanged);
        }

        private void method_1()
        {
            double num = 0.0;
            for (int i = 0; i < base.Items.Count; i++)
            {
                ListViewItem item = base.Items[i];
                if ((item as ListViewItemEx).Style is ISymbol)
                {
                    ISymbol style = (item as ListViewItemEx).Style as ISymbol;
                    if (style is IMarkerSymbol)
                    {
                        num = (num > (style as IMarkerSymbol).Size) ? num : (style as IMarkerSymbol).Size;
                    }
                    else
                    {
                        if (!(style is ILineSymbol))
                        {
                            return;
                        }
                        num = (num > (style as ILineSymbol).Width) ? num : (style as ILineSymbol).Width;
                    }
                }
            }
            if ((num < 16.0) && (base.SmallImageList.ImageSize.Height > 16))
            {
                base.SmallImageList.ImageSize = new Size(16, 16);
            }
            if (num > 40.0)
            {
                num = 40.0;
            }
            if (base.SmallImageList.ImageSize.Height < num)
            {
                base.SmallImageList.ImageSize = new Size((int) num, (int) num);
            }
        }

        private void method_2(IMarkerSymbol imarkerSymbol_0, Rectangle rectangle_0)
        {
            IPoint geometry = new PointClass
            {
                X = (rectangle_0.Left + rectangle_0.Right)/2,
                Y = (rectangle_0.Bottom + rectangle_0.Top)/2
            };
            ((ISymbol) imarkerSymbol_0).Draw(geometry);
        }

        private void method_3(ISymbol isymbol_0, IPatch ipatch_0, Rectangle rectangle_0)
        {
            IEnvelope bounds = new EnvelopeClass();
            bounds.PutCoords((double) rectangle_0.Left, (double) rectangle_0.Top, (double) rectangle_0.Right,
                (double) rectangle_0.Bottom);
            IGeometry geometry = ipatch_0.get_Geometry(bounds);
            isymbol_0.Draw(geometry);
        }

        private void method_4(ILineSymbol ilineSymbol_0, Rectangle rectangle_0)
        {
            if (ilineSymbol_0 is IPictureLineSymbol)
            {
                if (((IPictureLineSymbol) ilineSymbol_0).Picture == null)
                {
                    return;
                }
            }
            else if ((ilineSymbol_0 is IMarkerLineSymbol) || (ilineSymbol_0 is IHashLineSymbol))
            {
                ITemplate template = ((ILineProperties) ilineSymbol_0).Template;
                if (template != null)
                {
                    bool flag = false;
                    for (int i = 0; i < template.PatternElementCount; i++)
                    {
                        double num2;
                        double num3;
                        template.GetPatternElement(i, out num2, out num3);
                        if ((num2 + num3) > 0.0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return;
                    }
                }
            }
            object before = Missing.Value;
            IPointCollection points = new PolylineClass();
            IPoint inPoint = new PointClass();
            inPoint.PutCoords((double) (rectangle_0.Left + 3), (double) ((rectangle_0.Bottom + rectangle_0.Top)/2));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Right - 3), (double) ((rectangle_0.Bottom + rectangle_0.Top)/2));
            points.AddPoint(inPoint, ref before, ref before);
            ((ISymbol) ilineSymbol_0).Draw((IGeometry) points);
        }

        private void method_5(IFillSymbol ifillSymbol_0, Rectangle rectangle_0)
        {
            object before = Missing.Value;
            IPoint inPoint = new PointClass();
            IPointCollection points = new PolygonClass();
            inPoint.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Top + 3));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Right - 3), (double) (rectangle_0.Top + 3));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Right - 3), (double) (rectangle_0.Bottom - 3));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Bottom - 3));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Left + 3), (double) (rectangle_0.Top + 3));
            points.AddPoint(inPoint, ref before, ref before);
            ((ISymbol) ifillSymbol_0).Draw((IGeometry) points);
        }

        private void method_6(IFillSymbol ifillSymbol_0, Rectangle rectangle_0, bool bool_1)
        {
            object before = Missing.Value;
            IPoint inPoint = new PointClass();
            IPointCollection points = new PolygonClass();
            inPoint.PutCoords((double) (rectangle_0.Top + 3), (double) (rectangle_0.Left + 3));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Top + 3), (double) (rectangle_0.Right - 3));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Bottom - 3), (double) (rectangle_0.Right - 3));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Bottom - 3), (double) (rectangle_0.Left + 3));
            points.AddPoint(inPoint, ref before, ref before);
            inPoint.PutCoords((double) (rectangle_0.Top + 3), (double) (rectangle_0.Left + 3));
            points.AddPoint(inPoint, ref before, ref before);
            ((ISymbol) ifillSymbol_0).Draw((IGeometry) points);
        }

        private void method_7(ITextSymbol itextSymbol_0, Rectangle rectangle_0)
        {
            IPoint geometry = new PointClass
            {
                X = (rectangle_0.Left + rectangle_0.Right)/2,
                Y = (rectangle_0.Bottom + rectangle_0.Top)/2
            };
            ISimpleTextSymbol symbol = (ISimpleTextSymbol) itextSymbol_0;
            string text = symbol.Text;
            bool clip = symbol.Clip;
            symbol.Text = "AaBbYyZz";
            symbol.Clip = true;
            ((ISymbol) itextSymbol_0).Draw(geometry);
            symbol.Text = text;
            symbol.Clip = clip;
        }

        protected override void OnPaint(PaintEventArgs paintEventArgs_0)
        {
            int num;
            Rectangle bounds;
            Rectangle rectangle3;
            RectangleF ef;
            Graphics graphics;
            IntPtr hdc;
            SolidBrush brush;
            SolidBrush brush2;
            base.OnPaint(paintEventArgs_0);
            StringFormat format = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };
            if (base.View == View.Details)
            {
                for (num = 0; num < base.Items.Count; num++)
                {
                    bounds = base.Items[num].GetBounds(ItemBoundsPortion.Icon);
                    if ((bounds.Bottom >= 0) && (bounds.Top <= base.ClientRectangle.Height))
                    {
                        if (base.Items[num].SubItems[0].Text.Length == 0)
                        {
                            bounds.Width = base.Columns[0].Width + bounds.Width;
                        }
                        if (bounds.Width > 40)
                        {
                            bounds.Width = 40;
                        }
                        rectangle3 = base.Items[num].GetBounds(ItemBoundsPortion.Entire);
                        float x = rectangle3.Left + bounds.Width;
                        float top = rectangle3.Top;
                        ef = new RectangleF(x, top, 0f, (float) rectangle3.Height);
                        if (base.Items[num] is ListViewItemEx)
                        {
                            graphics = paintEventArgs_0.Graphics;
                            hdc = graphics.GetHdc();
                            if (base.Items[num].Tag is YTLegendItem)
                            {
                                if ((base.Items[num].Tag as YTLegendItem).BackSymbol != null)
                                {
                                    this.DrawSymbol(hdc.ToInt32(), bounds,
                                        (base.Items[num].Tag as YTLegendItem).BackSymbol);
                                }
                                this.DrawSymbol(hdc.ToInt32(), bounds, (base.Items[num].Tag as YTLegendItem).Symbol);
                            }
                            else if ((base.Items[num] as ListViewItemEx).Style is ISymbol)
                            {
                                this.DrawSymbol(hdc.ToInt32(), bounds, (base.Items[num] as ListViewItemEx).Style);
                            }
                            else if ((base.Items[num] as ListViewItemEx).Style is IStyleGalleryItem)
                            {
                                this.DrawSymbol(hdc.ToInt32(), bounds,
                                    ((base.Items[num] as ListViewItemEx).Style as IStyleGalleryItem).Item);
                            }
                            graphics.ReleaseHdc(hdc);
                        }
                        if (base.Items[num].Selected)
                        {
                            brush = new SolidBrush(base.Items[num].ForeColor);
                            brush2 = new SolidBrush(base.Items[num].BackColor);
                        }
                        else
                        {
                            brush = new SolidBrush(base.Items[num].BackColor);
                            brush2 = new SolidBrush(base.Items[num].ForeColor);
                        }
                        for (int i = 0; i < base.Items[num].SubItems.Count; i++)
                        {
                            if (i > 1)
                            {
                                ef.X += base.Columns[i - 1].Width;
                            }
                            else if (i == 1)
                            {
                                ef.X = (ef.X + base.Columns[0].Width) - bounds.Width;
                            }
                            if (i == 0)
                            {
                                ef.Width = base.Columns[i].Width - bounds.Width;
                            }
                            else
                            {
                                ef.Width = base.Columns[i].Width;
                            }
                            if ((i == 0) || base.FullRowSelect)
                            {
                                if ((i != 0) || (base.Items[num].SubItems[i].Text.Length != 0))
                                {
                                    paintEventArgs_0.Graphics.FillRectangle(brush, ef);
                                    paintEventArgs_0.Graphics.DrawString(base.Items[num].SubItems[i].Text, this.Font,
                                        brush2, ef, format);
                                }
                            }
                            else
                            {
                                paintEventArgs_0.Graphics.DrawString(base.Items[num].SubItems[i].Text, this.Font,
                                    Brushes.Black, ef, format);
                            }
                        }
                        brush.Dispose();
                        brush2.Dispose();
                    }
                }
            }
            else if (((base.View == View.LargeIcon) || (base.View == View.SmallIcon)) || (base.View == View.List))
            {
                for (num = 0; num < base.Items.Count; num++)
                {
                    bounds = base.Items[num].GetBounds(ItemBoundsPortion.Icon);
                    if ((bounds.Bottom >= 0) && (bounds.Top <= base.ClientRectangle.Height))
                    {
                        if (base.Items[num] is ListViewItemEx)
                        {
                            graphics = paintEventArgs_0.Graphics;
                            hdc = graphics.GetHdc();
                            if ((base.Items[num] as ListViewItemEx).Style is ISymbol)
                            {
                                this.DrawSymbol(hdc.ToInt32(), bounds, (base.Items[num] as ListViewItemEx).Style);
                            }
                            else if ((base.Items[num] as ListViewItemEx).Style is IStyleGalleryItem)
                            {
                                this.DrawSymbol(hdc.ToInt32(), bounds,
                                    ((base.Items[num] as ListViewItemEx).Style as IStyleGalleryItem).Item);
                            }
                            graphics.ReleaseHdc(hdc);
                        }
                        if (base.Items[num].Selected)
                        {
                            brush = new SolidBrush(base.Items[num].ForeColor);
                            brush2 = new SolidBrush(base.Items[num].BackColor);
                        }
                        else
                        {
                            brush = new SolidBrush(base.Items[num].BackColor);
                            brush2 = new SolidBrush(base.Items[num].ForeColor);
                        }
                        if (base.Items[num].SubItems.Count > 1)
                        {
                            rectangle3 = base.Items[num].GetBounds(ItemBoundsPortion.Label);
                            ef = new RectangleF((float) rectangle3.X, (float) rectangle3.Y, (float) rectangle3.Width,
                                (float) rectangle3.Height);
                            paintEventArgs_0.Graphics.FillRectangle(brush, ef);
                            paintEventArgs_0.Graphics.DrawString(base.Items[num].SubItems[1].Text, this.Font, brush2, ef);
                        }
                        brush.Dispose();
                        brush2.Dispose();
                    }
                }
            }
        }

        private void RenderInfoListView_DoubleClick(object sender, EventArgs e)
        {
            if (base.SelectedItems.Count > 0)
            {
                Rectangle itemRect = base.GetItemRect(base.SelectedIndices[0]);
                int num = this.int_0;
                int left = itemRect.Left;
                int num3 = itemRect.Left;
                int index = 0;
                while (index < base.Columns.Count)
                {
                    left = num3;
                    num3 += base.Columns[index].Width;
                    if ((num > left) && (num < num3))
                    {
                        break;
                    }
                    index++;
                }
                if (index != 0)
                {
                    if (((base.SelectedItems.Count == 1) && (this.ColumnEditables != null)) &&
                        this.ColumnEditables[index])
                    {
                        this.int_2 = index;
                        this.listViewItem_0 = base.SelectedItems[0];
                        this.textBox.Size = new Size(num3 - left, this.listViewItem_0.Bounds.Height);
                        this.textBox.Location = new System.Drawing.Point(left, this.listViewItem_0.Bounds.Y);
                        this.textBox.Show();
                        this.textBox.Text = this.listViewItem_0.SubItems[this.int_2].Text;
                        this.textBox.SelectAll();
                        this.textBox.Focus();
                    }
                }
                else
                {
                    try
                    {
                        frmSymbolSelector selector = new frmSymbolSelector();
                        selector.SetStyleGallery(ApplicationBase.StyleGallery);
                        ListViewItemEx ex = base.Items[base.SelectedIndices[0]] as ListViewItemEx;
                        selector.SetSymbol(ex.Style);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            ex.Style = selector.GetSymbol();
                            this.method_1();
                            base.Invalidate();
                            if (this.OnValueChanged != null)
                            {
                                this.OnValueChanged(base.SelectedIndices[0], ex.Style);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void RenderInfoListView_MouseDown(object sender, MouseEventArgs e)
        {
            this.int_0 = e.X;
            this.int_1 = e.Y;
            this.listViewItem_0 = null;
            this.int_2 = -1;
        }

        private void RenderInfoListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        public void SetColumnEditable(int int_3, bool bool_1)
        {
            this.ColumnEditables = new bool[base.Columns.Count];
            for (int i = 0; i < this.ColumnEditables.Length; i++)
            {
                this.ColumnEditables[i] = false;
            }
            this.ColumnEditables[int_3] = bool_1;
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.bool_0 = false;
                this.textBox.Hide();
            }
            else if (e.KeyChar == '\x001b')
            {
                this.bool_0 = true;
                this.textBox.Hide();
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
        }

        private void textBox_LostFocus(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.textBox.Hide();
            }
            else
            {
                if (((this.listViewItem_0 != null) && (this.int_2 != -1)) &&
                    (this.listViewItem_0.SubItems[this.int_2].Text != this.textBox.Text))
                {
                    this.listViewItem_0.SubItems[this.int_2].Text = this.textBox.Text;
                    if (this.OnValueChanged != null)
                    {
                        this.OnValueChanged(base.Items.IndexOf(this.listViewItem_0), this.textBox.Text);
                    }
                }
                this.bool_0 = true;
                this.textBox.Hide();
            }
        }

        public IStyleGallery StyleGallery
        {
            set { this.istyleGallery_0 = value; }
        }

        public delegate void OnValueChangedHandler(int int_0, object object_0);
    }
}
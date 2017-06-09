using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    internal class RenderInfoListView : ListView
    {
        public bool[] ColumnEditables = null;

        private ListViewItem listViewItem_0;

        private bool bool_0 = false;

        private int int_0;

        private int int_1;

        private ImageList imageList_0;

        private ImageList imageList_1;

        private IContainer icontainer_0;

        private TextBox textBox;

        private IStyleGallery istyleGallery_0 = null;

        private RenderInfoListView.OnValueChangedHandler onValueChangedHandler_0;

        private int int_2 = 0;

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }

        public RenderInfoListView()
        {
            this.method_0();
            this.textBox.LostFocus += new EventHandler(this.textBox_LostFocus);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.Controls.Add(this.textBox);
            base.View = View.Details;
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
                size = (int)(tag as IMarkerSymbol).Size;
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
                size = (int)(tag as ILineSymbol).Width;
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

        public ListViewItemEx Add(object[] object_0)
        {
            int size;
            ListViewItemEx listViewItemEx;
            if (base.Items.Count == 0)
            {
                this.imageList_0.ImageSize = new Size(16, 16);
            }
            if (object_0 == null)
            {
                listViewItemEx = null;
            }
            else
            {
                string[] str = new string[base.Columns.Count];
                str[0] = "";
                object object0 = null;
                for (int i = 0; i < (int)object_0.Length; i++)
                {
                    if (i == 0)
                    {
                        object0 = object_0[i];
                    }
                    else if (object_0[i] == null)
                    {
                        str[i] = "";
                    }
                    else
                    {
                        str[i] = object_0[i].ToString();
                    }
                }
                if (object0 is IMarkerSymbol)
                {
                    size = (int)(object0 as IMarkerSymbol).Size;
                    if (size > 40)
                    {
                        size = 40;
                    }
                    if (size > this.imageList_0.ImageSize.Height)
                    {
                        this.imageList_0.ImageSize = new Size(size, size);
                    }
                }
                else if (object0 is ILineSymbol)
                {
                    size = (int)(object0 as ILineSymbol).Width;
                    if (size > 40)
                    {
                        size = 40;
                    }
                    if (size > this.imageList_0.ImageSize.Height)
                    {
                        this.imageList_0.ImageSize = new Size(size, size);
                    }
                }
                ListViewItemEx listViewItemEx1 = new ListViewItemEx(str)
                {
                    Style = object0
                };
                base.Items.Add(listViewItemEx1);
                listViewItemEx = listViewItemEx1;
            }
            return listViewItemEx;
        }

        public void Add(IStyleGalleryItem istyleGalleryItem_0)
        {
            string[] name = new string[] { istyleGalleryItem_0.Name, istyleGalleryItem_0.Category };
            ListViewItemEx listViewItemEx = new ListViewItemEx(name)
            {
                Tag = istyleGalleryItem_0
            };
            base.Items.Add(listViewItemEx);
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && this.icontainer_0 != null)
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        protected void DrawSymbol(int int_3, System.Drawing.Rectangle rectangle_0, object object_0)
        {
            if (object_0 != null)
            {
                IDisplayTransformation displayTransformation = new DisplayTransformationClass();
                IEnvelope envelope = new EnvelopeClass();
                envelope.PutCoords((double)rectangle_0.Left, (double)rectangle_0.Top, (double)rectangle_0.Right, (double)rectangle_0.Bottom);
                tagRECT tagRECT;
                tagRECT.left = rectangle_0.Left;
                tagRECT.right = rectangle_0.Right;
                tagRECT.bottom = rectangle_0.Bottom;
                tagRECT.top = rectangle_0.Top;
                displayTransformation.set_DeviceFrame( ref tagRECT);
                displayTransformation.Bounds = envelope;
                displayTransformation.Resolution = 96.0;
                displayTransformation.ReferenceScale = 1.0;
                displayTransformation.ScaleRatio = 1.0;
                ISymbol symbol;
                if (object_0 is ISymbol)
                {
                    symbol = (ISymbol)object_0;
                }
                else if (object_0 is IColorRamp)
                {
                    IGradientFillSymbol gradientFillSymbol = new GradientFillSymbolClass();
                    ILineSymbol outline = gradientFillSymbol.Outline;
                    outline.Width = 0.0;
                    gradientFillSymbol.Outline = outline;
                    gradientFillSymbol.ColorRamp = (IColorRamp)object_0;
                    gradientFillSymbol.GradientAngle = 180.0;
                    gradientFillSymbol.GradientPercentage = 1.0;
                    gradientFillSymbol.IntervalCount = 100;
                    gradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;
                    symbol = (ISymbol)gradientFillSymbol;
                }
                else if (object_0 is IColor)
                {
                    symbol = (ISymbol)new ColorSymbolClass
                    {
                        Color = (IColor)object_0
                    };
                }
                else if (object_0 is IAreaPatch)
                {
                    symbol = new SimpleFillSymbolClass();
                    IRgbColor rgbColor = new RgbColorClass();
                    rgbColor.Red = 227;
                    rgbColor.Green = 236;
                    rgbColor.Blue = 19;
                    ((IFillSymbol)symbol).Color = rgbColor;
                }
                else if (object_0 is ILinePatch)
                {
                    symbol = new SimpleLineSymbolClass();
                }
                else
                {
                    if (object_0 is INorthArrow)
                    {
                        IDisplay display = new ScreenDisplayClass();
                        display.StartDrawing(int_3, 0);
                        display.DisplayTransformation = displayTransformation;
                        ((IMapSurround)object_0).Draw(display, null, envelope);
                        display.FinishDrawing();
                        ((IMapSurround)object_0).Refresh();
                        return;
                    }
                    if (object_0 is IMapSurround)
                    {
                        IDisplay display = new ScreenDisplayClass();
                        display.StartDrawing(int_3, 0);
                        display.DisplayTransformation = displayTransformation;
                        IEnvelope envelope2 = new EnvelopeClass();
                        envelope2.PutCoords((double)(rectangle_0.Left + 5), (double)(rectangle_0.Top + 5), (double)(rectangle_0.Right - 5), (double)(rectangle_0.Bottom - 5));
                        ((IMapSurround)object_0).QueryBounds(display, envelope, envelope2);
                        bool flag;
                        ((IMapSurround)object_0).FitToBounds(display, envelope2, out flag);
                        ((IMapSurround)object_0).Draw(display, null, envelope2);
                        display.FinishDrawing();
                        ((IMapSurround)object_0).Refresh();
                        return;
                    }
                    if (object_0 is IBackground)
                    {
                        IDisplay display = new ScreenDisplayClass();
                        display.StartDrawing(int_3, 0);
                        display.DisplayTransformation = displayTransformation;
                        IGeometry geometry = ((IBackground)object_0).GetGeometry(display, envelope);
                        ((IBackground)object_0).Draw(display, geometry);
                        display.FinishDrawing();
                        return;
                    }
                    if (object_0 is IShadow)
                    {
                        IDisplay display = new ScreenDisplayClass();
                        display.StartDrawing(int_3, 0);
                        display.DisplayTransformation = displayTransformation;
                        double horizontalSpacing = ((IShadow)object_0).HorizontalSpacing;
                        double verticalSpacing = ((IShadow)object_0).VerticalSpacing;
                        ((IShadow)object_0).HorizontalSpacing = 0.0;
                        ((IShadow)object_0).VerticalSpacing = 0.0;
                        IGeometry geometry = ((IShadow)object_0).GetGeometry(display, envelope);
                        ((IShadow)object_0).Draw(display, geometry);
                        ((IShadow)object_0).HorizontalSpacing = horizontalSpacing;
                        ((IShadow)object_0).VerticalSpacing = verticalSpacing;
                        display.FinishDrawing();
                        return;
                    }
                    if (object_0 is IBorder)
                    {
                        IDisplay display = new ScreenDisplayClass();
                        display.StartDrawing(int_3, 0);
                        display.DisplayTransformation = displayTransformation;
                        IPointCollection pointCollection = new PolylineClass();
                        object value = System.Reflection.Missing.Value;
                        IPoint point = new PointClass();
                        point.PutCoords((double)(rectangle_0.X + 4), (double)rectangle_0.Top);
                        pointCollection.AddPoint(point, ref value, ref value);
                        point.PutCoords((double)(rectangle_0.X + 4), (double)rectangle_0.Bottom);
                        pointCollection.AddPoint(point, ref value, ref value);
                        point.PutCoords((double)(rectangle_0.Right - 4), (double)rectangle_0.Bottom);
                        pointCollection.AddPoint(point, ref value, ref value);
                        IGeometry geometry = ((IBorder)object_0).GetGeometry(display, (IGeometry)pointCollection);
                        ((IBorder)object_0).Draw(display, geometry);
                        display.FinishDrawing();
                        return;
                    }
                    return;
                }
                if (symbol is IPictureFillSymbol || symbol is IPictureLineSymbol)
                {
                    symbol.SetupDC(int_3, null);
                }
                else
                {
                    symbol.SetupDC(int_3, displayTransformation);
                }
                if (object_0 is IPatch)
                {
                    this.method_3(symbol, (IPatch)object_0, rectangle_0);
                }
                else if (symbol is IMarkerSymbol)
                {
                    this.method_2((IMarkerSymbol)symbol, rectangle_0);
                }
                else if (symbol is ILineSymbol)
                {
                    this.method_4((ILineSymbol)symbol, rectangle_0);
                }
                else if (symbol is IFillSymbol)
                {
                    this.method_5((IFillSymbol)symbol, rectangle_0);
                }
                else if (symbol is ITextSymbol)
                {
                    this.method_7((ITextSymbol)symbol, rectangle_0);
                }
                symbol.ResetDC();
            }
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
            double num = 0;
            int num1 = 0;
            while (true)
            {
                if (num1 < base.Items.Count)
                {
                    ListViewItem item = base.Items[num1];
                    if ((item as ListViewItemEx).Style is ISymbol)
                    {
                        ISymbol style = (item as ListViewItemEx).Style as ISymbol;
                        if (!(style is IMarkerSymbol))
                        {
                            if (!(style is ILineSymbol))
                            {
                                break;
                            }
                            num = (num > (style as ILineSymbol).Width ? num : (style as ILineSymbol).Width);
                        }
                        else
                        {
                            num = (num > (style as IMarkerSymbol).Size ? num : (style as IMarkerSymbol).Size);
                        }
                    }
                    num1++;
                }
                else
                {
                    if ((num >= 16 ? false : base.SmallImageList.ImageSize.Height > 16))
                    {
                        base.SmallImageList.ImageSize = new Size(16, 16);
                    }
                    if (num > 40)
                    {
                        num = 40;
                    }
                    if ((double)base.SmallImageList.ImageSize.Height >= num)
                    {
                        break;
                    }
                    base.SmallImageList.ImageSize = new Size((int)num, (int)num);
                    break;
                }
            }
        }

        private void method_2(IMarkerSymbol imarkerSymbol_0, Rectangle rectangle_0)
        {
            IPoint pointClass = new PointClass()
            {
                X = (double)((rectangle_0.Left + rectangle_0.Right) / 2),
                Y = (double)((rectangle_0.Bottom + rectangle_0.Top) / 2)
            };
            ((ISymbol)imarkerSymbol_0).Draw(pointClass);
        }

        private void method_3(ISymbol isymbol_0, IPatch ipatch_0, Rectangle rectangle_0)
        {
            IEnvelope envelopeClass = new EnvelopeClass();
            envelopeClass.PutCoords((double)rectangle_0.Left, (double)rectangle_0.Top, (double)rectangle_0.Right, (double)rectangle_0.Bottom);
            isymbol_0.Draw(ipatch_0.get_Geometry(envelopeClass));
        }

        private void method_4(ILineSymbol ilineSymbol_0, Rectangle rectangle_0)
        {
            double num;
            double num1;
            object value;
            IPointCollection polylineClass;
            IPoint pointClass;
            if (ilineSymbol_0 is IPictureLineSymbol)
            {
                if (((IPictureLineSymbol)ilineSymbol_0).Picture != null)
                {
                    value = Missing.Value;
                    polylineClass = new PolylineClass();
                    pointClass = new PointClass();
                    pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
                    polylineClass.AddPoint(pointClass, ref value, ref value);
                    pointClass.PutCoords((double)(rectangle_0.Right - 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
                    polylineClass.AddPoint(pointClass, ref value, ref value);
                    ((ISymbol)ilineSymbol_0).Draw((IGeometry)polylineClass);
                    return;
                }
                return;
            }
            else if ((ilineSymbol_0 is IMarkerLineSymbol ? true : ilineSymbol_0 is IHashLineSymbol))
            {
                ITemplate template = ((ILineProperties)ilineSymbol_0).Template;
                if (template != null)
                {
                    bool flag = false;
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 < template.PatternElementCount)
                        {
                            template.GetPatternElement(num2, out num, out num1);
                            if (num + num1 > 0)
                            {
                                flag = true;
                                break;
                            }
                            else
                            {
                                num2++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return;
                    }
                }
            }
            value = Missing.Value;
            polylineClass = new PolylineClass();
            pointClass = new PointClass();
            pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
            polylineClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Right - 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
            polylineClass.AddPoint(pointClass, ref value, ref value);
            ((ISymbol)ilineSymbol_0).Draw((IGeometry)polylineClass);
        }

        private void method_5(IFillSymbol ifillSymbol_0, Rectangle rectangle_0)
        {
            object value = Missing.Value;
            IPoint pointClass = new PointClass();
            IPointCollection polygonClass = new PolygonClass();
            pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Top + 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Bottom - 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Bottom - 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            ((ISymbol)ifillSymbol_0).Draw((IGeometry)polygonClass);
        }

        private void method_6(IFillSymbol ifillSymbol_0, Rectangle rectangle_0, bool bool_1)
        {
            object value = Missing.Value;
            IPoint pointClass = new PointClass();
            IPointCollection polygonClass = new PolygonClass();
            pointClass.PutCoords((double)(rectangle_0.Top + 3), (double)(rectangle_0.Left + 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Top + 3), (double)(rectangle_0.Right - 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Bottom - 3), (double)(rectangle_0.Right - 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Bottom - 3), (double)(rectangle_0.Left + 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            pointClass.PutCoords((double)(rectangle_0.Top + 3), (double)(rectangle_0.Left + 3));
            polygonClass.AddPoint(pointClass, ref value, ref value);
            ((ISymbol)ifillSymbol_0).Draw((IGeometry)polygonClass);
        }

        private void method_7(ITextSymbol itextSymbol_0, Rectangle rectangle_0)
        {
            IPoint pointClass = new PointClass()
            {
                X = (double)((rectangle_0.Left + rectangle_0.Right) / 2),
                Y = (double)((rectangle_0.Bottom + rectangle_0.Top) / 2)
            };
            ISimpleTextSymbol itextSymbol0 = (ISimpleTextSymbol)itextSymbol_0;
            string text = itextSymbol0.Text;
            bool clip = itextSymbol0.Clip;
            itextSymbol0.Text = "AaBbYyZz";
            itextSymbol0.Clip = true;
            ((ISymbol)itextSymbol_0).Draw(pointClass);
            itextSymbol0.Text = text;
            itextSymbol0.Clip = clip;
        }

        protected override void OnPaint(PaintEventArgs paintEventArgs_0)
        {
            int i;
            Rectangle bounds;
            Rectangle rectangle;
            RectangleF rectangleF;
            Graphics graphics;
            IntPtr hdc;
            SolidBrush solidBrush;
            SolidBrush solidBrush1;
            base.OnPaint(paintEventArgs_0);
            StringFormat stringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center
            };
            if (base.View == View.Details)
            {
                for (i = 0; i < base.Items.Count; i++)
                {
                    bounds = base.Items[i].GetBounds(ItemBoundsPortion.Icon);
                    if (bounds.Bottom >= 0 && bounds.Top <= base.ClientRectangle.Height)
                    {
                        if (base.Items[i].SubItems[0].Text.Length == 0)
                        {
                            bounds.Width = base.Columns[0].Width + bounds.Width;
                        }
                        if (bounds.Width > 40)
                        {
                            bounds.Width = 40;
                        }
                        rectangle = base.Items[i].GetBounds(ItemBoundsPortion.Entire);
                        float left = (float)(rectangle.Left + bounds.Width);
                        float top = (float)rectangle.Top;
                        rectangleF = new RectangleF(left, top, 0f, (float)rectangle.Height);
                        if (base.Items[i] is ListViewItemEx)
                        {
                            graphics = paintEventArgs_0.Graphics;
                            hdc = graphics.GetHdc();
                            if ((base.Items[i] as ListViewItemEx).Style is ISymbol)
                            {
                                this.DrawSymbol(hdc.ToInt32(), bounds, (base.Items[i] as ListViewItemEx).Style);
                            }
                            else if ((base.Items[i] as ListViewItemEx).Style is IStyleGalleryItem)
                            {
                                this.DrawSymbol(hdc.ToInt32(), bounds, ((base.Items[i] as ListViewItemEx).Style as IStyleGalleryItem).Item);
                            }
                            graphics.ReleaseHdc(hdc);
                        }
                        if (!base.Items[i].Selected)
                        {
                            solidBrush = new SolidBrush(base.Items[i].BackColor);
                            solidBrush1 = new SolidBrush(base.Items[i].ForeColor);
                        }
                        else
                        {
                            solidBrush = new SolidBrush(base.Items[i].ForeColor);
                            solidBrush1 = new SolidBrush(base.Items[i].BackColor);
                        }
                        for (int j = 0; j < base.Items[i].SubItems.Count; j++)
                        {
                            if (j > 1)
                            {
                                rectangleF.X = rectangleF.X + (float)base.Columns[j - 1].Width;
                            }
                            else if (j == 1)
                            {
                                rectangleF.X = rectangleF.X + (float)base.Columns[0].Width - (float)bounds.Width;
                            }
                            if (j != 0)
                            {
                                rectangleF.Width = (float)base.Columns[j].Width;
                            }
                            else
                            {
                                rectangleF.Width = (float)(base.Columns[j].Width - bounds.Width);
                            }
                            if ((j == 0 ? false : !base.FullRowSelect))
                            {
                                paintEventArgs_0.Graphics.DrawString(base.Items[i].SubItems[j].Text, this.Font, Brushes.Black, rectangleF, stringFormat);
                            }
                            else if ((j != 0 ? true : base.Items[i].SubItems[j].Text.Length != 0))
                            {
                                paintEventArgs_0.Graphics.FillRectangle(solidBrush, rectangleF);
                                paintEventArgs_0.Graphics.DrawString(base.Items[i].SubItems[j].Text, this.Font, solidBrush1, rectangleF, stringFormat);
                            }
                        }
                        solidBrush.Dispose();
                        solidBrush1.Dispose();
                    }
                }
            }
            else if ((base.View == View.LargeIcon || base.View == View.SmallIcon ? true : base.View == View.List))
            {
                for (i = 0; i < base.Items.Count; i++)
                {
                    bounds = base.Items[i].GetBounds(ItemBoundsPortion.Icon);
                    if (bounds.Bottom >= 0 && bounds.Top <= base.ClientRectangle.Height)
                    {
                        if (base.Items[i] is ListViewItemEx)
                        {
                            graphics = paintEventArgs_0.Graphics;
                            hdc = graphics.GetHdc();
                            if ((base.Items[i] as ListViewItemEx).Style is ISymbol)
                            {
                                this.DrawSymbol(hdc.ToInt32(), bounds, (base.Items[i] as ListViewItemEx).Style);
                            }
                            else if ((base.Items[i] as ListViewItemEx).Style is IStyleGalleryItem)
                            {
                                this.DrawSymbol(hdc.ToInt32(), bounds, ((base.Items[i] as ListViewItemEx).Style as IStyleGalleryItem).Item);
                            }
                            graphics.ReleaseHdc(hdc);
                        }
                        if (!base.Items[i].Selected)
                        {
                            solidBrush = new SolidBrush(base.Items[i].BackColor);
                            solidBrush1 = new SolidBrush(base.Items[i].ForeColor);
                        }
                        else
                        {
                            solidBrush = new SolidBrush(base.Items[i].ForeColor);
                            solidBrush1 = new SolidBrush(base.Items[i].BackColor);
                        }
                        if (base.Items[i].SubItems.Count > 1)
                        {
                            rectangle = base.Items[i].GetBounds(ItemBoundsPortion.Label);
                            rectangleF = new RectangleF((float)rectangle.X, (float)rectangle.Y, (float)rectangle.Width, (float)rectangle.Height);
                            paintEventArgs_0.Graphics.FillRectangle(solidBrush, rectangleF);
                            paintEventArgs_0.Graphics.DrawString(base.Items[i].SubItems[1].Text, this.Font, solidBrush1, rectangleF);
                        }
                        solidBrush.Dispose();
                        solidBrush1.Dispose();
                    }
                }
            }
        }

        private void RenderInfoListView_DoubleClick(object sender, EventArgs e)
        {
            int i;
            if (base.SelectedItems.Count > 0)
            {
                Rectangle itemRect = base.GetItemRect(base.SelectedIndices[0]);
                int int0 = this.int_0;
                int left = itemRect.Left;
                int width = itemRect.Left;
                for (i = 0; i < base.Columns.Count; i++)
                {
                    left = width;
                    width = width + base.Columns[i].Width;
                    if ((int0 <= left ? false : int0 < width))
                    {
                        break;
                    }
                }
                if (i == 0)
                {
                    try
                    {
                        frmSymbolSelector _frmSymbolSelector = new frmSymbolSelector();
                        _frmSymbolSelector.SetStyleGallery(this.istyleGallery_0);
                        ListViewItemEx item = base.Items[base.SelectedIndices[0]] as ListViewItemEx;
                        _frmSymbolSelector.SetSymbol(item.Style);
                        if (_frmSymbolSelector.ShowDialog() == DialogResult.OK)
                        {
                            item.Style = _frmSymbolSelector.GetSymbol();
                            this.method_1();
                            base.Invalidate();
                            if (this.onValueChangedHandler_0 != null)
                            {
                                this.onValueChangedHandler_0(base.SelectedIndices[0], item.Style);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                else if ((base.SelectedItems.Count != 1 ? false : this.ColumnEditables != null) && this.ColumnEditables[i])
                {
                    this.int_2 = i;
                    this.listViewItem_0 = base.SelectedItems[0];
                    TextBox size = this.textBox;
                    Rectangle bounds = this.listViewItem_0.Bounds;
                    size.Size = new Size(width - left, bounds.Height);
                    TextBox point = this.textBox;
                    bounds = this.listViewItem_0.Bounds;
                    point.Location = new System.Drawing.Point(left, bounds.Y);
                    this.textBox.Show();
                    this.textBox.Text = this.listViewItem_0.SubItems[this.int_2].Text;
                    this.textBox.SelectAll();
                    this.textBox.Focus();
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
            for (int i = 0; i < (int)this.ColumnEditables.Length; i++)
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
            else if (e.KeyChar == '\u001B')
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
            if (!this.bool_0)
            {
                if (this.listViewItem_0 != null && this.int_2 != -1 && this.listViewItem_0.SubItems[this.int_2].Text != this.textBox.Text)
                {
                    this.listViewItem_0.SubItems[this.int_2].Text = this.textBox.Text;
                    if (this.onValueChangedHandler_0 != null)
                    {
                        this.onValueChangedHandler_0(base.Items.IndexOf(this.listViewItem_0), this.textBox.Text);
                    }
                }
                this.bool_0 = true;
                this.textBox.Hide();
            }
            else
            {
                this.bool_0 = false;
                this.textBox.Hide();
            }
        }

        public event RenderInfoListView.OnValueChangedHandler OnValueChanged
        {
            add
            {
                RenderInfoListView.OnValueChangedHandler onValueChangedHandler;
                RenderInfoListView.OnValueChangedHandler onValueChangedHandler0 = this.onValueChangedHandler_0;
                do
                {
                    onValueChangedHandler = onValueChangedHandler0;
                    RenderInfoListView.OnValueChangedHandler onValueChangedHandler1 = (RenderInfoListView.OnValueChangedHandler)Delegate.Combine(onValueChangedHandler, value);
                    onValueChangedHandler0 = Interlocked.CompareExchange<RenderInfoListView.OnValueChangedHandler>(ref this.onValueChangedHandler_0, onValueChangedHandler1, onValueChangedHandler);
                }
                while ((object)onValueChangedHandler0 != (object)onValueChangedHandler);
            }
            remove
            {
                RenderInfoListView.OnValueChangedHandler onValueChangedHandler;
                RenderInfoListView.OnValueChangedHandler onValueChangedHandler0 = this.onValueChangedHandler_0;
                do
                {
                    onValueChangedHandler = onValueChangedHandler0;
                    RenderInfoListView.OnValueChangedHandler onValueChangedHandler1 = (RenderInfoListView.OnValueChangedHandler)Delegate.Remove(onValueChangedHandler, value);
                    onValueChangedHandler0 = Interlocked.CompareExchange<RenderInfoListView.OnValueChangedHandler>(ref this.onValueChangedHandler_0, onValueChangedHandler1, onValueChangedHandler);
                }
                while ((object)onValueChangedHandler0 != (object)onValueChangedHandler);
            }
        }

        public delegate void OnValueChangedHandler(int int_0, object object_0);
    }
}
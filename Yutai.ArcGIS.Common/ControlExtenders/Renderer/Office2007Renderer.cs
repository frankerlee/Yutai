using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders.Renderer
{
    public class Office2007Renderer : ToolStripProfessionalRenderer
    {
        private static Color _arrowDark;
        private static Color _arrowDisabled;
        private static Color _arrowLight;
        private static Color _c1;
        private static Color _c2;
        private static Color _c3;
        private static Color _c4;
        private static Color _c5;
        private static Color _c6;
        private static int _checkInset;
        private static Color _contextCheckBorder;
        private static Color _contextCheckTick;
        private static float _contextCheckTickThickness;
        private static Color _contextMenuBack;
        private static float _cutContextMenu;
        private static float _cutMenuItemBack;
        private static float _cutToolItemMenu;
        private static Color _gripDark;
        private static Color _gripLight;
        private static int _gripLines;
        private static int _gripMove;
        private static int _gripOffset;
        private static int _gripSize;
        private static int _gripSquare;
        private static Class7 _itemContextItemEnabledColors;
        private static Class7 _itemDisabledColors;
        private static Class7 _itemToolItemCheckedColors;
        private static Class7 _itemToolItemCheckPressColors;
        private static Class7 _itemToolItemPressedColors;
        private static Class7 _itemToolItemSelectedColors;
        private static int _marginInset;
        private static Color _r1;
        private static Color _r2;
        private static Color _r3;
        private static Color _r4;
        private static Color _r5;
        private static Color _r6;
        private static Color _r7;
        private static Color _r8;
        private static Color _r9;
        private static Color _rA;
        private static Color _rB;
        private static Color _rC;
        private static Color _rD;
        private static Color _rE;
        private static Color _rF;
        private static Color _rG;
        private static Color _rH;
        private static Color _rI;
        private static Color _rJ;
        private static Color _rK;
        private static Color _rL;
        private static Color _rM;
        private static Color _rN;
        private static Color _rO;
        private static Color _rP;
        private static Color _rQ;
        private static Color _rR;
        private static Color _rS;
        private static Color _rT;
        private static Color _rU;
        private static Color _rV;
        private static Color _rW;
        private static Color _rX;
        private static Color _rY;
        private static Color _rZ;
        private static int _separatorInset;
        private static Color _separatorMenuDark;
        private static Color _separatorMenuLight;
        private static Blend _statusStripBlend;
        private static Color _statusStripBorderDark;
        private static Color _statusStripBorderLight;
        private static Color _textContextMenuItem;
        private static Color _textDisabled;
        private static Color _textMenuStripItem;
        private static Color _textStatusStripItem;

        static Office2007Renderer()
        {
            old_acctor_mc();
        }

        public Office2007Renderer() : base(new Office2007ColorTable())
        {
        }

        private void method_0(Graphics graphics_0, ToolStripButton toolStripButton_0, ToolStrip toolStrip_0)
        {
            if (toolStripButton_0.Enabled)
            {
                if (toolStripButton_0.Checked)
                {
                    if (toolStripButton_0.Pressed)
                    {
                        this.method_3(graphics_0, toolStripButton_0, _itemToolItemPressedColors);
                    }
                    else if (toolStripButton_0.Selected)
                    {
                        this.method_3(graphics_0, toolStripButton_0, _itemToolItemCheckPressColors);
                    }
                    else
                    {
                        this.method_3(graphics_0, toolStripButton_0, _itemToolItemCheckedColors);
                    }
                }
                else if (toolStripButton_0.Pressed)
                {
                    this.method_3(graphics_0, toolStripButton_0, _itemToolItemPressedColors);
                }
                else if (toolStripButton_0.Selected)
                {
                    this.method_3(graphics_0, toolStripButton_0, _itemToolItemSelectedColors);
                }
            }
            else if (toolStripButton_0.Selected)
            {
                Point pt = toolStrip_0.PointToClient(Control.MousePosition);
                if (!toolStripButton_0.Bounds.Contains(pt))
                {
                    this.method_3(graphics_0, toolStripButton_0, _itemDisabledColors);
                }
            }
        }

        private void method_1(Graphics graphics_0, ToolStripItem toolStripItem_0, ToolStrip toolStrip_0)
        {
            if (toolStripItem_0.Selected || toolStripItem_0.Pressed)
            {
                if (toolStripItem_0.Enabled)
                {
                    if (toolStripItem_0.Pressed)
                    {
                        this.method_5(graphics_0, toolStripItem_0);
                    }
                    else
                    {
                        this.method_3(graphics_0, toolStripItem_0, _itemToolItemSelectedColors);
                    }
                }
                else
                {
                    Point pt = toolStrip_0.PointToClient(Control.MousePosition);
                    if (!toolStripItem_0.Bounds.Contains(pt))
                    {
                        this.method_3(graphics_0, toolStripItem_0, _itemDisabledColors);
                    }
                }
            }
        }

        private void method_10(Graphics graphics_0, int int_0, int int_1, Brush brush_0, Brush brush_1)
        {
            graphics_0.FillRectangle(brush_1, int_0 + _gripOffset, int_1 + _gripOffset, _gripSquare, _gripSquare);
            graphics_0.FillRectangle(brush_0, int_0, int_1, _gripSquare, _gripSquare);
        }

        private void method_11(Graphics graphics_0, bool bool_0, Rectangle rectangle_0, Pen pen_0, Pen pen_1, int int_0, bool bool_1)
        {
            int num;
            if (bool_0)
            {
                num = rectangle_0.Width / 2;
                int y = rectangle_0.Y;
                int bottom = rectangle_0.Bottom;
                graphics_0.DrawLine(pen_1, num, y, num, bottom);
                graphics_0.DrawLine(pen_0, num + 1, y, num + 1, bottom);
            }
            else
            {
                int num4 = rectangle_0.Height / 2;
                num = rectangle_0.X + (bool_1 ? 0 : int_0);
                int num5 = rectangle_0.Right - (bool_1 ? int_0 : 0);
                graphics_0.DrawLine(pen_1, num, num4, num5, num4);
                graphics_0.DrawLine(pen_0, num, num4 + 1, num5, num4 + 1);
            }
        }

        private GraphicsPath method_12(Rectangle rectangle_0, Rectangle rectangle_1, float float_0)
        {
            if (rectangle_1.IsEmpty)
            {
                return this.method_13(rectangle_0, float_0);
            }
            rectangle_0.Width--;
            rectangle_0.Height--;
            List<PointF> list = new List<PointF>();
            float x = rectangle_0.X;
            float y = rectangle_0.Y;
            float right = rectangle_0.Right;
            float bottom = rectangle_0.Bottom;
            float num5 = rectangle_0.X + float_0;
            float num6 = rectangle_0.Right - float_0;
            float num7 = rectangle_0.Y + float_0;
            float num8 = rectangle_0.Bottom - float_0;
            float num9 = (float_0 == 0f) ? 1f : float_0;
            if ((rectangle_0.Y >= rectangle_1.Top) && (rectangle_0.Y <= rectangle_1.Bottom))
            {
                float num10 = (rectangle_1.X - 1) - float_0;
                float num11 = rectangle_1.Right + float_0;
                if (num5 <= num10)
                {
                    list.Add(new PointF(num5, y));
                    list.Add(new PointF(num10, y));
                    list.Add(new PointF(num10 + float_0, y - num9));
                }
                else
                {
                    num10 = rectangle_1.X - 1;
                    list.Add(new PointF(num10, y));
                    list.Add(new PointF(num10, y - num9));
                }
                if (num6 > num11)
                {
                    list.Add(new PointF(num11 - float_0, y - num9));
                    list.Add(new PointF(num11, y));
                    list.Add(new PointF(num6, y));
                }
                else
                {
                    num11 = rectangle_1.Right;
                    list.Add(new PointF(num11, y - num9));
                    list.Add(new PointF(num11, y));
                }
            }
            else
            {
                list.Add(new PointF(num5, y));
                list.Add(new PointF(num6, y));
            }
            list.Add(new PointF(right, num7));
            list.Add(new PointF(right, num8));
            list.Add(new PointF(num6, bottom));
            list.Add(new PointF(num5, bottom));
            list.Add(new PointF(x, num8));
            list.Add(new PointF(x, num7));
            GraphicsPath path2 = new GraphicsPath();
            for (int i = 1; i < list.Count; i++)
            {
                path2.AddLine(list[i - 1], list[i]);
            }
            path2.AddLine(list[list.Count - 1], list[0]);
            return path2;
        }

        private GraphicsPath method_13(Rectangle rectangle_0, float float_0)
        {
            rectangle_0.Width--;
            rectangle_0.Height--;
            GraphicsPath path = new GraphicsPath();
            path.AddLine(rectangle_0.Left + float_0, (float) rectangle_0.Top, rectangle_0.Right - float_0, (float) rectangle_0.Top);
            path.AddLine(rectangle_0.Right - float_0, (float) rectangle_0.Top, (float) rectangle_0.Right, rectangle_0.Top + float_0);
            path.AddLine((float) rectangle_0.Right, rectangle_0.Top + float_0, (float) rectangle_0.Right, rectangle_0.Bottom - float_0);
            path.AddLine((float) rectangle_0.Right, rectangle_0.Bottom - float_0, rectangle_0.Right - float_0, (float) rectangle_0.Bottom);
            path.AddLine(rectangle_0.Right - float_0, (float) rectangle_0.Bottom, rectangle_0.Left + float_0, (float) rectangle_0.Bottom);
            path.AddLine(rectangle_0.Left + float_0, (float) rectangle_0.Bottom, (float) rectangle_0.Left, rectangle_0.Bottom - float_0);
            path.AddLine((float) rectangle_0.Left, rectangle_0.Bottom - float_0, (float) rectangle_0.Left, rectangle_0.Top + float_0);
            path.AddLine((float) rectangle_0.Left, rectangle_0.Top + float_0, rectangle_0.Left + float_0, (float) rectangle_0.Top);
            return path;
        }

        private GraphicsPath method_14(Rectangle rectangle_0, float float_0)
        {
            rectangle_0.Inflate(-1, -1);
            return this.method_13(rectangle_0, float_0);
        }

        private GraphicsPath method_15(Rectangle rectangle_0, Rectangle rectangle_1, float float_0)
        {
            rectangle_0.Inflate(-1, -1);
            return this.method_12(rectangle_0, rectangle_1, float_0);
        }

        private GraphicsPath method_16(Rectangle rectangle_0, float float_0)
        {
            rectangle_0.Width++;
            rectangle_0.Height++;
            return this.method_13(rectangle_0, float_0);
        }

        private GraphicsPath method_17(Rectangle rectangle_0, Rectangle rectangle_1, float float_0)
        {
            rectangle_0.Width++;
            rectangle_0.Height++;
            return this.method_12(rectangle_0, rectangle_1, float_0);
        }

        private GraphicsPath method_18(ToolStripItem toolStripItem_0, Rectangle rectangle_0, ArrowDirection arrowDirection_0)
        {
            int num;
            int num2;
            if ((arrowDirection_0 == ArrowDirection.Left) || (arrowDirection_0 == ArrowDirection.Right))
            {
                num = rectangle_0.Right - ((rectangle_0.Width - 4) / 2);
                num2 = rectangle_0.Y + (rectangle_0.Height / 2);
            }
            else
            {
                num = rectangle_0.X + (rectangle_0.Width / 2);
                num2 = rectangle_0.Bottom - ((rectangle_0.Height - 3) / 2);
                if ((toolStripItem_0 is ToolStripDropDownButton) && (toolStripItem_0.RightToLeft == RightToLeft.Yes))
                {
                    num++;
                }
            }
            GraphicsPath path = new GraphicsPath();
            switch (arrowDirection_0)
            {
                case ArrowDirection.Left:
                    path.AddLine(num - 4, num2, num, num2 - 4);
                    path.AddLine(num, num2 - 4, num, num2 + 4);
                    path.AddLine(num, num2 + 4, num - 4, num2);
                    return path;

                case ArrowDirection.Up:
                    path.AddLine(num + 3f, (float) num2, num - 3f, (float) num2);
                    path.AddLine(num - 3f, (float) num2, (float) num, num2 - 4f);
                    path.AddLine((float) num, num2 - 4f, num + 3f, (float) num2);
                    return path;

                case ArrowDirection.Right:
                    path.AddLine(num, num2, num - 4, num2 - 4);
                    path.AddLine((int) (num - 4), (int) (num2 - 4), (int) (num - 4), (int) (num2 + 4));
                    path.AddLine(num - 4, num2 + 4, num, num2);
                    return path;

                case ArrowDirection.Down:
                    path.AddLine((float) (num + 3f), (float) (num2 - 3f), (float) (num - 2f), (float) (num2 - 3f));
                    path.AddLine(num - 2f, num2 - 3f, (float) num, (float) num2);
                    path.AddLine((float) num, (float) num2, num + 3f, num2 - 3f);
                    return path;
            }
            return path;
        }

        private GraphicsPath method_19(Rectangle rectangle_0)
        {
            int num = rectangle_0.X + (rectangle_0.Width / 2);
            int num2 = rectangle_0.Y + (rectangle_0.Height / 2);
            GraphicsPath path = new GraphicsPath();
            path.AddLine(num - 4, num2, num - 2, num2 + 4);
            path.AddLine((int) (num - 2), (int) (num2 + 4), (int) (num + 3), (int) (num2 - 5));
            return path;
        }

        private void method_2(Graphics graphics_0, ToolStripSplitButton toolStripSplitButton_0, ToolStrip toolStrip_0)
        {
            if (toolStripSplitButton_0.Selected || toolStripSplitButton_0.Pressed)
            {
                if (toolStripSplitButton_0.Enabled)
                {
                    if (!(toolStripSplitButton_0.Pressed || !toolStripSplitButton_0.ButtonPressed))
                    {
                        this.method_4(graphics_0, toolStripSplitButton_0, _itemToolItemPressedColors, _itemToolItemSelectedColors, _itemContextItemEnabledColors);
                    }
                    else if (!(!toolStripSplitButton_0.Pressed || toolStripSplitButton_0.ButtonPressed))
                    {
                        this.method_5(graphics_0, toolStripSplitButton_0);
                    }
                    else
                    {
                        this.method_4(graphics_0, toolStripSplitButton_0, _itemToolItemSelectedColors, _itemToolItemSelectedColors, _itemContextItemEnabledColors);
                    }
                }
                else
                {
                    Point pt = toolStrip_0.PointToClient(Control.MousePosition);
                    if (!toolStripSplitButton_0.Bounds.Contains(pt))
                    {
                        this.method_3(graphics_0, toolStripSplitButton_0, _itemDisabledColors);
                    }
                }
            }
        }

        private GraphicsPath method_20(Rectangle rectangle_0)
        {
            int num = rectangle_0.X + (rectangle_0.Width / 2);
            int num2 = rectangle_0.Y + (rectangle_0.Height / 2);
            GraphicsPath path = new GraphicsPath();
            path.AddLine(num - 3, num2, num, num2 - 3);
            path.AddLine(num, num2 - 3, num + 3, num2);
            path.AddLine(num + 3, num2, num, num2 + 3);
            path.AddLine(num, num2 + 3, num - 3, num2);
            return path;
        }

        private void method_3(Graphics graphics_0, ToolStripItem toolStripItem_0, Class7 class7_0)
        {
            this.method_7(graphics_0, new Rectangle(Point.Empty, toolStripItem_0.Bounds.Size), class7_0);
        }

        private void method_4(Graphics graphics_0, ToolStripSplitButton toolStripSplitButton_0, Class7 class7_0, Class7 class7_1, Class7 class7_2)
        {
            Rectangle rectangle = new Rectangle(Point.Empty, toolStripSplitButton_0.Bounds.Size);
            Rectangle dropDownButtonBounds = toolStripSplitButton_0.DropDownButtonBounds;
            if ((((rectangle.Width > 0) && (dropDownButtonBounds.Width > 0)) && (rectangle.Height > 0)) && (dropDownButtonBounds.Height > 0))
            {
                int x;
                Rectangle rectangle4 = rectangle;
                if (dropDownButtonBounds.X > 0)
                {
                    rectangle4.Width = dropDownButtonBounds.Left;
                    dropDownButtonBounds.X--;
                    dropDownButtonBounds.Width++;
                    x = dropDownButtonBounds.X;
                }
                else
                {
                    rectangle4.Width -= dropDownButtonBounds.Width - 2;
                    rectangle4.X = dropDownButtonBounds.Right - 1;
                    dropDownButtonBounds.Width++;
                    x = dropDownButtonBounds.Right - 1;
                }
                using (this.method_13(rectangle, _cutMenuItemBack))
                {
                    this.method_8(graphics_0, rectangle4, class7_0);
                    this.method_8(graphics_0, dropDownButtonBounds, class7_1);
                    using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(rectangle.X + x, rectangle.Top, 1, rectangle.Height + 1), class7_2.Border1, class7_2.Border2, 90f))
                    {
                        brush.SetSigmaBellShape(0.5f);
                        using (Pen pen = new Pen(brush))
                        {
                            graphics_0.DrawLine(pen, (int) (rectangle.X + x), (int) (rectangle.Top + 1), (int) (rectangle.X + x), (int) (rectangle.Bottom - 1));
                        }
                    }
                    this.method_9(graphics_0, rectangle, class7_0);
                }
            }
        }

        private void method_5(Graphics graphics_0, ToolStripItem toolStripItem_0)
        {
            Rectangle rectangle = new Rectangle(Point.Empty, toolStripItem_0.Bounds.Size);
            using (GraphicsPath path = this.method_13(rectangle, _cutToolItemMenu))
            {
                using (this.method_14(rectangle, _cutToolItemMenu))
                {
                    using (GraphicsPath path3 = this.method_16(rectangle, _cutToolItemMenu))
                    {
                        using (new UseClipping(graphics_0, path3))
                        {
                            using (SolidBrush brush = new SolidBrush(_separatorMenuLight))
                            {
                                graphics_0.FillPath(brush, path);
                            }
                            using (Pen pen = new Pen(base.ColorTable.MenuBorder))
                            {
                                graphics_0.DrawPath(pen, path);
                            }
                        }
                    }
                }
            }
        }

        private void method_6(Graphics graphics_0, ToolStripItem toolStripItem_0, Class7 class7_0)
        {
            Rectangle rectangle = new Rectangle(2, 0, toolStripItem_0.Bounds.Width - 3, toolStripItem_0.Bounds.Height);
            this.method_7(graphics_0, rectangle, class7_0);
        }

        private void method_7(Graphics graphics_0, Rectangle rectangle_0, Class7 class7_0)
        {
            if ((rectangle_0.Width > 0) && (rectangle_0.Height > 0))
            {
                this.method_8(graphics_0, rectangle_0, class7_0);
                this.method_9(graphics_0, rectangle_0, class7_0);
            }
        }

        private void method_8(Graphics graphics_0, Rectangle rectangle_0, Class7 class7_0)
        {
            rectangle_0.Inflate(-1, -1);
            int height = rectangle_0.Height / 2;
            Rectangle rect = new Rectangle(rectangle_0.X, rectangle_0.Y, rectangle_0.Width, height);
            Rectangle rectangle2 = new Rectangle(rectangle_0.X, rectangle_0.Y + height, rectangle_0.Width, rectangle_0.Height - height);
            Rectangle rectangle3 = rect;
            Rectangle rectangle4 = rectangle2;
            rectangle3.Inflate(1, 1);
            rectangle4.Inflate(1, 1);
            using (LinearGradientBrush brush = new LinearGradientBrush(rectangle3, class7_0.InsideTop1, class7_0.InsideTop2, 90f))
            {
                using (LinearGradientBrush brush2 = new LinearGradientBrush(rectangle4, class7_0.InsideBottom1, class7_0.InsideBottom2, 90f))
                {
                    graphics_0.FillRectangle(brush, rect);
                    graphics_0.FillRectangle(brush2, rectangle2);
                }
            }
            height = rectangle_0.Height / 2;
            rect = new Rectangle(rectangle_0.X, rectangle_0.Y, rectangle_0.Width, height);
            rectangle2 = new Rectangle(rectangle_0.X, rectangle_0.Y + height, rectangle_0.Width, rectangle_0.Height - height);
            rectangle3 = rect;
            rectangle4 = rectangle2;
            rectangle3.Inflate(1, 1);
            rectangle4.Inflate(1, 1);
            using (LinearGradientBrush brush3 = new LinearGradientBrush(rectangle3, class7_0.FillTop1, class7_0.FillTop2, 90f))
            {
                using (LinearGradientBrush brush4 = new LinearGradientBrush(rectangle4, class7_0.FillBottom1, class7_0.FillBottom2, 90f))
                {
                    rectangle_0.Inflate(-1, -1);
                    height = rectangle_0.Height / 2;
                    rect = new Rectangle(rectangle_0.X, rectangle_0.Y, rectangle_0.Width, height);
                    rectangle2 = new Rectangle(rectangle_0.X, rectangle_0.Y + height, rectangle_0.Width, rectangle_0.Height - height);
                    graphics_0.FillRectangle(brush3, rect);
                    graphics_0.FillRectangle(brush4, rectangle2);
                }
            }
        }

        private void method_9(Graphics graphics_0, Rectangle rectangle_0, Class7 class7_0)
        {
            using (new UseAntiAlias(graphics_0))
            {
                Rectangle rect = rectangle_0;
                rect.Inflate(1, 1);
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, class7_0.Border1, class7_0.Border2, 90f))
                {
                    brush.SetSigmaBellShape(0.5f);
                    using (Pen pen = new Pen(brush))
                    {
                        using (GraphicsPath path = this.method_13(rectangle_0, _cutMenuItemBack))
                        {
                            graphics_0.DrawPath(pen, path);
                        }
                    }
                }
            }
        }

        private static void old_acctor_mc()
        {
            _gripOffset = 1;
            _gripSquare = 2;
            _gripSize = 3;
            _gripMove = 4;
            _gripLines = 3;
            _checkInset = 1;
            _marginInset = 2;
            _separatorInset = 0x1f;
            _cutToolItemMenu = 1f;
            _cutContextMenu = 0f;
            _cutMenuItemBack = 1.2f;
            _contextCheckTickThickness = 1.6f;
            _c1 = Color.FromArgb(0xa7, 0xa7, 0xa7);
            _c2 = Color.FromArgb(0x15, 0x42, 0x8b);
            _c3 = Color.FromArgb(0x4c, 0x53, 0x5c);
            _c4 = Color.FromArgb(250, 250, 250);
            _c5 = Color.FromArgb(0xf8, 0xf8, 0xf8);
            _c6 = Color.FromArgb(0xf3, 0xf3, 0xf3);
            _r1 = Color.FromArgb(0xff, 0xff, 0xfb);
            _r2 = Color.FromArgb(0xff, 0xf9, 0xe3);
            _r3 = Color.FromArgb(0xff, 0xf2, 0xc9);
            _r4 = Color.FromArgb(0xff, 0xf8, 0xb5);
            _r5 = Color.FromArgb(0xff, 0xfc, 0xe5);
            _r6 = Color.FromArgb(0xff, 0xeb, 0xa6);
            _r7 = Color.FromArgb(0xff, 0xd5, 0x67);
            _r8 = Color.FromArgb(0xff, 0xe4, 0x91);
            _r9 = Color.FromArgb(160, 0xbc, 0xe4);
            _rA = Color.FromArgb(0x79, 0x99, 0xc2);
            _rB = Color.FromArgb(0xb6, 190, 0xc0);
            _rC = Color.FromArgb(0x9b, 0xa3, 0xa7);
            _rD = Color.FromArgb(0xe9, 0xa8, 0x61);
            _rE = Color.FromArgb(0xf7, 0xa4, 0x27);
            _rF = Color.FromArgb(0xf6, 0x9c, 0x18);
            _rG = Color.FromArgb(0xfd, 0xad, 0x11);
            _rH = Color.FromArgb(0xfe, 0xb9, 0x6c);
            _rI = Color.FromArgb(0xfd, 0xa4, 0x61);
            _rJ = Color.FromArgb(0xfc, 0x8f, 0x3d);
            _rK = Color.FromArgb(0xff, 0xd0, 0x86);
            _rL = Color.FromArgb(0xf9, 0xc0, 0x67);
            _rM = Color.FromArgb(250, 0xc3, 0x5d);
            _rN = Color.FromArgb(0xf8, 190, 0x51);
            _rO = Color.FromArgb(0xff, 0xd0, 0x31);
            _rP = Color.FromArgb(0xfe, 0xd6, 0xa8);
            _rQ = Color.FromArgb(0xfc, 180, 100);
            _rR = Color.FromArgb(0xfc, 0xa1, 0x36);
            _rS = Color.FromArgb(0xfe, 0xee, 170);
            _rT = Color.FromArgb(0xf9, 0xca, 0x71);
            _rU = Color.FromArgb(250, 0xcd, 0x67);
            _rV = Color.FromArgb(0xf8, 200, 0x5b);
            _rW = Color.FromArgb(0xff, 0xda, 0x3b);
            _rX = Color.FromArgb(0xfe, 0xb9, 0x6c);
            _rY = Color.FromArgb(0xfc, 0xa1, 0x36);
            _rZ = Color.FromArgb(0xfe, 0xee, 170);
            _textDisabled = _c1;
            _textMenuStripItem = _c2;
            _textStatusStripItem = _c2;
            _textContextMenuItem = _c2;
            _arrowDisabled = _c1;
            _arrowLight = Color.FromArgb(0x6a, 0x7e, 0xc5);
            _arrowDark = Color.FromArgb(0x40, 70, 90);
            _separatorMenuLight = Color.FromArgb(0xf5, 0xf5, 0xf5);
            _separatorMenuDark = Color.FromArgb(0xc5, 0xc5, 0xc5);
            _contextMenuBack = _c4;
            _contextCheckBorder = Color.FromArgb(0xf2, 0x95, 0x36);
            _contextCheckTick = Color.FromArgb(0x42, 0x4b, 0x8a);
            _statusStripBorderDark = Color.FromArgb(0x56, 0x7d, 0xb0);
            _statusStripBorderLight = Color.White;
            _gripDark = Color.FromArgb(0x72, 0x98, 0xcc);
            _gripLight = _c5;
            _itemContextItemEnabledColors = new Class7(_r1, _r2, _r3, _r4, _r5, _r6, _r7, _r8, Color.FromArgb(0xd9, 0xcb, 150), Color.FromArgb(0xc0, 0xa7, 0x76));
            _itemDisabledColors = new Class7(_c4, _c6, Color.FromArgb(0xec, 0xec, 0xec), Color.FromArgb(230, 230, 230), _c6, Color.FromArgb(0xe0, 0xe0, 0xe0), Color.FromArgb(200, 200, 200), Color.FromArgb(210, 210, 210), Color.FromArgb(0xd4, 0xd4, 0xd4), Color.FromArgb(0xc3, 0xc3, 0xc3));
            _itemToolItemSelectedColors = new Class7(_r1, _r2, _r3, _r4, _r5, _r6, _r7, _r8, _r9, _rA);
            _itemToolItemPressedColors = new Class7(_rD, _rE, _rF, _rG, _rH, _rI, _rJ, _rK, _r9, _rA);
            _itemToolItemCheckedColors = new Class7(_rL, _rM, _rN, _rO, _rP, _rQ, _rR, _rS, _r9, _rA);
            _itemToolItemCheckPressColors = new Class7(_rT, _rU, _rV, _rW, _rX, _rI, _rY, _rZ, _r9, _rA);
            _statusStripBlend = new Blend();
            _statusStripBlend.Positions = new float[] { 0f, 0.25f, 0.25f, 0.57f, 0.86f, 1f };
            _statusStripBlend.Factors = new float[] { 0.1f, 0.6f, 1f, 0.4f, 0f, 0.95f };
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs toolStripArrowRenderEventArgs_0)
        {
            if ((toolStripArrowRenderEventArgs_0.ArrowRectangle.Width > 0) && (toolStripArrowRenderEventArgs_0.ArrowRectangle.Height > 0))
            {
                using (GraphicsPath path = this.method_18(toolStripArrowRenderEventArgs_0.Item, toolStripArrowRenderEventArgs_0.ArrowRectangle, toolStripArrowRenderEventArgs_0.Direction))
                {
                    RectangleF bounds = path.GetBounds();
                    bounds.Inflate(1f, 1f);
                    Color color = toolStripArrowRenderEventArgs_0.Item.Enabled ? _arrowLight : _arrowDisabled;
                    Color color2 = toolStripArrowRenderEventArgs_0.Item.Enabled ? _arrowDark : _arrowDisabled;
                    float angle = 0f;
                    switch (toolStripArrowRenderEventArgs_0.Direction)
                    {
                        case ArrowDirection.Left:
                            angle = 180f;
                            break;

                        case ArrowDirection.Up:
                            angle = 270f;
                            break;

                        case ArrowDirection.Right:
                            angle = 0f;
                            break;

                        case ArrowDirection.Down:
                            angle = 90f;
                            break;
                    }
                    using (LinearGradientBrush brush = new LinearGradientBrush(bounds, color, color2, angle))
                    {
                        toolStripArrowRenderEventArgs_0.Graphics.FillPath(brush, path);
                    }
                }
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs toolStripItemRenderEventArgs_0)
        {
            ToolStripButton item = (ToolStripButton) toolStripItemRenderEventArgs_0.Item;
            if ((item.Selected || item.Pressed) || item.Checked)
            {
                this.method_0(toolStripItemRenderEventArgs_0.Graphics, item, toolStripItemRenderEventArgs_0.ToolStrip);
            }
        }

        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs toolStripItemRenderEventArgs_0)
        {
            if (toolStripItemRenderEventArgs_0.Item.Selected || toolStripItemRenderEventArgs_0.Item.Pressed)
            {
                this.method_1(toolStripItemRenderEventArgs_0.Graphics, toolStripItemRenderEventArgs_0.Item, toolStripItemRenderEventArgs_0.ToolStrip);
            }
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs toolStripRenderEventArgs_0)
        {
            if ((toolStripRenderEventArgs_0.ToolStrip is ContextMenuStrip) || (toolStripRenderEventArgs_0.ToolStrip is ToolStripDropDownMenu))
            {
                Rectangle affectedBounds = toolStripRenderEventArgs_0.AffectedBounds;
                bool flag = toolStripRenderEventArgs_0.ToolStrip.RightToLeft == RightToLeft.Yes;
                affectedBounds.Y += _marginInset;
                affectedBounds.Height -= _marginInset * 2;
                if (!flag)
                {
                    affectedBounds.X += _marginInset;
                }
                else
                {
                    affectedBounds.X += _marginInset / 2;
                }
                using (SolidBrush brush = new SolidBrush(base.ColorTable.ImageMarginGradientBegin))
                {
                    toolStripRenderEventArgs_0.Graphics.FillRectangle(brush, affectedBounds);
                }
                using (Pen pen = new Pen(_separatorMenuLight))
                {
                    using (Pen pen2 = new Pen(_separatorMenuDark))
                    {
                        if (!flag)
                        {
                            toolStripRenderEventArgs_0.Graphics.DrawLine(pen, affectedBounds.Right, affectedBounds.Top, affectedBounds.Right, affectedBounds.Bottom);
                            toolStripRenderEventArgs_0.Graphics.DrawLine(pen2, affectedBounds.Right - 1, affectedBounds.Top, affectedBounds.Right - 1, affectedBounds.Bottom);
                        }
                        else
                        {
                            toolStripRenderEventArgs_0.Graphics.DrawLine(pen, affectedBounds.Left - 1, affectedBounds.Top, affectedBounds.Left - 1, affectedBounds.Bottom);
                            toolStripRenderEventArgs_0.Graphics.DrawLine(pen2, affectedBounds.Left, affectedBounds.Top, affectedBounds.Left, affectedBounds.Bottom);
                        }
                    }
                    return;
                }
            }
            base.OnRenderImageMargin(toolStripRenderEventArgs_0);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs toolStripItemImageRenderEventArgs_0)
        {
            int num;
            Rectangle imageRectangle = toolStripItemImageRenderEventArgs_0.ImageRectangle;
            imageRectangle.Inflate(1, 1);
            if (imageRectangle.Top > _checkInset)
            {
                num = imageRectangle.Top - _checkInset;
                imageRectangle.Y -= num;
                imageRectangle.Height += num;
            }
            if (imageRectangle.Height <= (toolStripItemImageRenderEventArgs_0.Item.Bounds.Height - (_checkInset * 2)))
            {
                num = (toolStripItemImageRenderEventArgs_0.Item.Bounds.Height - (_checkInset * 2)) - imageRectangle.Height;
                imageRectangle.Height += num;
            }
            using (new UseAntiAlias(toolStripItemImageRenderEventArgs_0.Graphics))
            {
                using (GraphicsPath path = this.method_13(imageRectangle, _cutMenuItemBack))
                {
                    CheckState @unchecked;
                    GraphicsPath path2;
                    using (SolidBrush brush = new SolidBrush(base.ColorTable.CheckBackground))
                    {
                        toolStripItemImageRenderEventArgs_0.Graphics.FillPath(brush, path);
                    }
                    using (Pen pen = new Pen(_contextCheckBorder))
                    {
                        toolStripItemImageRenderEventArgs_0.Graphics.DrawPath(pen, path);
                    }
                    if (toolStripItemImageRenderEventArgs_0.Image != null)
                    {
                        @unchecked = CheckState.Unchecked;
                        if (!(toolStripItemImageRenderEventArgs_0.Item is ToolStripMenuItem))
                        {
                            goto Label_020E;
                        }
                        @unchecked = CheckState.Unchecked;
                        @unchecked = CheckState.Unchecked;
                        ToolStripMenuItem item = (ToolStripMenuItem) toolStripItemImageRenderEventArgs_0.Item;
                        switch (item.CheckState)
                        {
                            case CheckState.Checked:
                                goto Label_017D;

                            case CheckState.Indeterminate:
                                goto Label_01C8;
                        }
                    }
                    return;
                Label_017D:
                    using (path2 = this.method_19(imageRectangle))
                    {
                        using (Pen pen2 = new Pen(_contextCheckTick, _contextCheckTickThickness))
                        {
                            toolStripItemImageRenderEventArgs_0.Graphics.DrawPath(pen2, path2);
                        }
                        return;
                    }
                Label_01C8:
                    using (path2 = this.method_20(imageRectangle))
                    {
                        using (SolidBrush brush2 = new SolidBrush(_contextCheckTick))
                        {
                            toolStripItemImageRenderEventArgs_0.Graphics.FillPath(brush2, path2);
                        }
                        return;
                    }
                Label_020E:
                    @unchecked = CheckState.Unchecked;
                    @unchecked = CheckState.Unchecked;
                }
            }
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs toolStripItemImageRenderEventArgs_0)
        {
            if ((toolStripItemImageRenderEventArgs_0.ToolStrip is ContextMenuStrip) || (toolStripItemImageRenderEventArgs_0.ToolStrip is ToolStripDropDownMenu))
            {
                if (toolStripItemImageRenderEventArgs_0.Image != null)
                {
                    if (toolStripItemImageRenderEventArgs_0.Item.Enabled)
                    {
                        toolStripItemImageRenderEventArgs_0.Graphics.DrawImage(toolStripItemImageRenderEventArgs_0.Image, toolStripItemImageRenderEventArgs_0.ImageRectangle);
                    }
                    else
                    {
                        ControlPaint.DrawImageDisabled(toolStripItemImageRenderEventArgs_0.Graphics, toolStripItemImageRenderEventArgs_0.Image, toolStripItemImageRenderEventArgs_0.ImageRectangle.X, toolStripItemImageRenderEventArgs_0.ImageRectangle.Y, Color.Transparent);
                    }
                }
            }
            else
            {
                base.OnRenderItemImage(toolStripItemImageRenderEventArgs_0);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs toolStripItemTextRenderEventArgs_0)
        {
            if ((((toolStripItemTextRenderEventArgs_0.ToolStrip is MenuStrip) || (toolStripItemTextRenderEventArgs_0.ToolStrip != null)) || (toolStripItemTextRenderEventArgs_0.ToolStrip is ContextMenuStrip)) || (toolStripItemTextRenderEventArgs_0.ToolStrip is ToolStripDropDownMenu))
            {
                if (!toolStripItemTextRenderEventArgs_0.Item.Enabled)
                {
                    toolStripItemTextRenderEventArgs_0.TextColor = _textDisabled;
                }
                else if (!((!(toolStripItemTextRenderEventArgs_0.ToolStrip is MenuStrip) || toolStripItemTextRenderEventArgs_0.Item.Pressed) || toolStripItemTextRenderEventArgs_0.Item.Selected))
                {
                    toolStripItemTextRenderEventArgs_0.TextColor = _textMenuStripItem;
                }
                else if (!((!(toolStripItemTextRenderEventArgs_0.ToolStrip is StatusStrip) || toolStripItemTextRenderEventArgs_0.Item.Pressed) || toolStripItemTextRenderEventArgs_0.Item.Selected))
                {
                    toolStripItemTextRenderEventArgs_0.TextColor = _textStatusStripItem;
                }
                else
                {
                    toolStripItemTextRenderEventArgs_0.TextColor = _textContextMenuItem;
                }
                using (new UseClearTypeGridFit(toolStripItemTextRenderEventArgs_0.Graphics))
                {
                    base.OnRenderItemText(toolStripItemTextRenderEventArgs_0);
                    return;
                }
            }
            base.OnRenderItemText(toolStripItemTextRenderEventArgs_0);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs toolStripItemRenderEventArgs_0)
        {
            if (((toolStripItemRenderEventArgs_0.ToolStrip is MenuStrip) || (toolStripItemRenderEventArgs_0.ToolStrip is ContextMenuStrip)) || (toolStripItemRenderEventArgs_0.ToolStrip is ToolStripDropDownMenu))
            {
                if (toolStripItemRenderEventArgs_0.Item.Pressed && (toolStripItemRenderEventArgs_0.ToolStrip is MenuStrip))
                {
                    this.method_5(toolStripItemRenderEventArgs_0.Graphics, toolStripItemRenderEventArgs_0.Item);
                }
                else if (toolStripItemRenderEventArgs_0.Item.Selected)
                {
                    if (toolStripItemRenderEventArgs_0.Item.Enabled)
                    {
                        if (toolStripItemRenderEventArgs_0.ToolStrip is MenuStrip)
                        {
                            this.method_3(toolStripItemRenderEventArgs_0.Graphics, toolStripItemRenderEventArgs_0.Item, _itemToolItemSelectedColors);
                        }
                        else
                        {
                            this.method_6(toolStripItemRenderEventArgs_0.Graphics, toolStripItemRenderEventArgs_0.Item, _itemContextItemEnabledColors);
                        }
                    }
                    else
                    {
                        Point pt = toolStripItemRenderEventArgs_0.ToolStrip.PointToClient(Control.MousePosition);
                        if (!toolStripItemRenderEventArgs_0.Item.Bounds.Contains(pt))
                        {
                            if (toolStripItemRenderEventArgs_0.ToolStrip is MenuStrip)
                            {
                                this.method_3(toolStripItemRenderEventArgs_0.Graphics, toolStripItemRenderEventArgs_0.Item, _itemDisabledColors);
                            }
                            else
                            {
                                this.method_6(toolStripItemRenderEventArgs_0.Graphics, toolStripItemRenderEventArgs_0.Item, _itemDisabledColors);
                            }
                        }
                    }
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(toolStripItemRenderEventArgs_0);
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs toolStripSeparatorRenderEventArgs_0)
        {
            Pen pen;
            Pen pen2;
            if ((toolStripSeparatorRenderEventArgs_0.ToolStrip is ContextMenuStrip) || (toolStripSeparatorRenderEventArgs_0.ToolStrip is ToolStripDropDownMenu))
            {
                using (pen = new Pen(_separatorMenuLight))
                {
                    using (pen2 = new Pen(_separatorMenuDark))
                    {
                        this.method_11(toolStripSeparatorRenderEventArgs_0.Graphics, toolStripSeparatorRenderEventArgs_0.Vertical, toolStripSeparatorRenderEventArgs_0.Item.Bounds, pen, pen2, _separatorInset, toolStripSeparatorRenderEventArgs_0.ToolStrip.RightToLeft == RightToLeft.Yes);
                    }
                    return;
                }
            }
            if (toolStripSeparatorRenderEventArgs_0.ToolStrip is StatusStrip)
            {
                using (pen = new Pen(base.ColorTable.SeparatorLight))
                {
                    using (pen2 = new Pen(base.ColorTable.SeparatorDark))
                    {
                        this.method_11(toolStripSeparatorRenderEventArgs_0.Graphics, toolStripSeparatorRenderEventArgs_0.Vertical, toolStripSeparatorRenderEventArgs_0.Item.Bounds, pen, pen2, 0, false);
                    }
                    return;
                }
            }
            base.OnRenderSeparator(toolStripSeparatorRenderEventArgs_0);
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs toolStripItemRenderEventArgs_0)
        {
            if (toolStripItemRenderEventArgs_0.Item.Selected || toolStripItemRenderEventArgs_0.Item.Pressed)
            {
                ToolStripSplitButton item = (ToolStripSplitButton) toolStripItemRenderEventArgs_0.Item;
                this.method_2(toolStripItemRenderEventArgs_0.Graphics, item, toolStripItemRenderEventArgs_0.ToolStrip);
                Rectangle dropDownButtonBounds = item.DropDownButtonBounds;
                this.OnRenderArrow(new ToolStripArrowRenderEventArgs(toolStripItemRenderEventArgs_0.Graphics, item, dropDownButtonBounds, SystemColors.ControlText, ArrowDirection.Down));
            }
            else
            {
                base.OnRenderSplitButtonBackground(toolStripItemRenderEventArgs_0);
            }
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs toolStripRenderEventArgs_0)
        {
            using (SolidBrush brush = new SolidBrush(_gripDark))
            {
                using (SolidBrush brush2 = new SolidBrush(_gripLight))
                {
                    bool flag = toolStripRenderEventArgs_0.ToolStrip.RightToLeft == RightToLeft.Yes;
                    int num = (toolStripRenderEventArgs_0.AffectedBounds.Bottom - (_gripSize * 2)) + 1;
                    for (int i = _gripLines; i >= 1; i--)
                    {
                        int num3 = flag ? (toolStripRenderEventArgs_0.AffectedBounds.Left + 1) : ((toolStripRenderEventArgs_0.AffectedBounds.Right - (_gripSize * 2)) + 1);
                        for (int j = 0; j < i; j++)
                        {
                            this.method_10(toolStripRenderEventArgs_0.Graphics, num3, num, brush, brush2);
                            num3 -= flag ? -_gripMove : _gripMove;
                        }
                        num -= _gripMove;
                    }
                }
            }
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs toolStripRenderEventArgs_0)
        {
            if ((toolStripRenderEventArgs_0.ToolStrip is ContextMenuStrip) || (toolStripRenderEventArgs_0.ToolStrip is ToolStripDropDownMenu))
            {
                using (GraphicsPath path = this.method_13(toolStripRenderEventArgs_0.AffectedBounds, _cutContextMenu))
                {
                    using (GraphicsPath path2 = this.method_16(toolStripRenderEventArgs_0.AffectedBounds, _cutContextMenu))
                    {
                        using (new UseClipping(toolStripRenderEventArgs_0.Graphics, path2))
                        {
                            using (SolidBrush brush = new SolidBrush(_contextMenuBack))
                            {
                                toolStripRenderEventArgs_0.Graphics.FillPath(brush, path);
                            }
                            return;
                        }
                    }
                }
            }
            if (toolStripRenderEventArgs_0.ToolStrip is StatusStrip)
            {
                RectangleF rect = new RectangleF(0f, 1.5f, (float) toolStripRenderEventArgs_0.ToolStrip.Width, (float) (toolStripRenderEventArgs_0.ToolStrip.Height - 2));
                if ((rect.Width <= 0f) || (rect.Height <= 0f))
                {
                    return;
                }
                using (LinearGradientBrush brush2 = new LinearGradientBrush(rect, base.ColorTable.StatusStripGradientBegin, base.ColorTable.StatusStripGradientEnd, 90f))
                {
                    brush2.Blend = _statusStripBlend;
                    toolStripRenderEventArgs_0.Graphics.FillRectangle(brush2, rect);
                    return;
                }
            }
            base.OnRenderToolStripBackground(toolStripRenderEventArgs_0);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs toolStripRenderEventArgs_0)
        {
            if ((toolStripRenderEventArgs_0.ToolStrip is ContextMenuStrip) || (toolStripRenderEventArgs_0.ToolStrip is ToolStripDropDownMenu))
            {
                if (!toolStripRenderEventArgs_0.ConnectedArea.IsEmpty)
                {
                    using (SolidBrush brush = new SolidBrush(_contextMenuBack))
                    {
                        toolStripRenderEventArgs_0.Graphics.FillRectangle(brush, toolStripRenderEventArgs_0.ConnectedArea);
                    }
                }
                using (GraphicsPath path = this.method_12(toolStripRenderEventArgs_0.AffectedBounds, toolStripRenderEventArgs_0.ConnectedArea, _cutContextMenu))
                {
                    using (GraphicsPath path2 = this.method_15(toolStripRenderEventArgs_0.AffectedBounds, toolStripRenderEventArgs_0.ConnectedArea, _cutContextMenu))
                    {
                        using (GraphicsPath path3 = this.method_17(toolStripRenderEventArgs_0.AffectedBounds, toolStripRenderEventArgs_0.ConnectedArea, _cutContextMenu))
                        {
                            using (Pen pen = new Pen(base.ColorTable.MenuBorder))
                            {
                                using (Pen pen2 = new Pen(_separatorMenuLight))
                                {
                                    using (new UseClipping(toolStripRenderEventArgs_0.Graphics, path3))
                                    {
                                        using (new UseAntiAlias(toolStripRenderEventArgs_0.Graphics))
                                        {
                                            toolStripRenderEventArgs_0.Graphics.DrawPath(pen2, path2);
                                            toolStripRenderEventArgs_0.Graphics.DrawPath(pen, path);
                                        }
                                        toolStripRenderEventArgs_0.Graphics.DrawLine(pen, toolStripRenderEventArgs_0.AffectedBounds.Right, toolStripRenderEventArgs_0.AffectedBounds.Bottom, toolStripRenderEventArgs_0.AffectedBounds.Right - 1, toolStripRenderEventArgs_0.AffectedBounds.Bottom - 1);
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            if (toolStripRenderEventArgs_0.ToolStrip is StatusStrip)
            {
                using (Pen pen3 = new Pen(_statusStripBorderDark))
                {
                    using (Pen pen4 = new Pen(_statusStripBorderLight))
                    {
                        toolStripRenderEventArgs_0.Graphics.DrawLine(pen3, 0, 0, toolStripRenderEventArgs_0.ToolStrip.Width, 0);
                        toolStripRenderEventArgs_0.Graphics.DrawLine(pen4, 0, 1, toolStripRenderEventArgs_0.ToolStrip.Width, 1);
                    }
                    return;
                }
            }
            base.OnRenderToolStripBorder(toolStripRenderEventArgs_0);
        }

        protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs toolStripContentPanelRenderEventArgs_0)
        {
            base.OnRenderToolStripContentPanelBackground(toolStripContentPanelRenderEventArgs_0);
            if ((toolStripContentPanelRenderEventArgs_0.ToolStripContentPanel.Width > 0) && (toolStripContentPanelRenderEventArgs_0.ToolStripContentPanel.Height > 0))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(toolStripContentPanelRenderEventArgs_0.ToolStripContentPanel.ClientRectangle, base.ColorTable.ToolStripContentPanelGradientEnd, base.ColorTable.ToolStripContentPanelGradientBegin, 90f))
                {
                    toolStripContentPanelRenderEventArgs_0.Graphics.FillRectangle(brush, toolStripContentPanelRenderEventArgs_0.ToolStripContentPanel.ClientRectangle);
                }
            }
        }

        private class Class7
        {
            public Color Border1;
            public Color Border2;
            public Color FillBottom1;
            public Color FillBottom2;
            public Color FillTop1;
            public Color FillTop2;
            public Color InsideBottom1;
            public Color InsideBottom2;
            public Color InsideTop1;
            public Color InsideTop2;

            public Class7(Color color_0, Color color_1, Color color_2, Color color_3, Color color_4, Color color_5, Color color_6, Color color_7, Color color_8, Color color_9)
            {
                this.InsideTop1 = color_0;
                this.InsideTop2 = color_1;
                this.InsideBottom1 = color_2;
                this.InsideBottom2 = color_3;
                this.FillTop1 = color_4;
                this.FillTop2 = color_5;
                this.FillBottom1 = color_6;
                this.FillBottom2 = color_7;
                this.Border1 = color_8;
                this.Border2 = color_9;
            }
        }
    }
}


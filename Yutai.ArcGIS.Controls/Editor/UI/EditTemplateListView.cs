using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class EditTemplateListView : ListView
    {

        public EditTemplateListView()
        {
            this.InitializeComponent();
            base.MultiSelect = false;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetExStyles();
        }

        public void AddItem(ListViewItem li)
        {
            YTEditTemplate tag = li.Tag as YTEditTemplate;
            if (((tag != null) && !this.imageList1.Images.ContainsKey(tag.ImageKey)) && (tag.Bitmap != null))
            {
                this.imageList1.Images.Add(tag.ImageKey, tag.Bitmap);
            }
            li.ImageKey = tag.ImageKey;
            base.Items.Add(li);
        }

 protected override void OnPaint(PaintEventArgs e)
        {
            StringFormat format = new StringFormat {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            };
            for (int i = 0; i < base.Groups.Count; i++)
            {
                ListViewGroup group = base.Groups[i];
                if (group.Items.Count > 0)
                {
                    SolidBrush brush;
                    SolidBrush brush2;
                    Rectangle bounds = group.Items[0].GetBounds(ItemBoundsPortion.Entire);
                    if ((bounds.Bottom >= 0) && (bounds.Bottom <= base.ClientRectangle.Height))
                    {
                        brush = new SolidBrush(group.Items[0].BackColor);
                        brush2 = new SolidBrush(Color.FromArgb(0, 51, 153));
                        e.Graphics.DrawString(group.Header, this.Font, brush2, (float) bounds.X, (float) (bounds.Y - 21));
                        Pen pen = new Pen(Color.FromArgb(178, 193, 224));
                        SizeF ef = e.Graphics.MeasureString(group.Header, this.Font);
                        e.Graphics.DrawLine(pen, bounds.X + ef.Width, (bounds.Y - 21f) + (ef.Height / 2f), (float) (bounds.X + base.ClientRectangle.Width), (bounds.Y - 21f) + (ef.Height / 2f));
                        pen.Dispose();
                        brush.Dispose();
                        brush2.Dispose();
                    }
                    for (int j = 0; j < group.Items.Count; j++)
                    {
                        Rectangle rectangle2 = group.Items[j].GetBounds(ItemBoundsPortion.Icon);
                        if ((rectangle2.Bottom >= 0) && (rectangle2.Bottom <= base.ClientRectangle.Height))
                        {
                            if (this.imageList1.Images.ContainsKey(group.Items[j].ImageKey))
                            {
                                Image image = this.imageList1.Images[group.Items[j].ImageKey];
                                Rectangle rectangle3 = new Rectangle(rectangle2.X, rectangle2.Y, 16, 16);
                                e.Graphics.DrawImage(image, rectangle3);
                            }
                            if (group.Items[j].Selected)
                            {
                                brush = new SolidBrush(group.Items[j].ForeColor);
                                brush2 = new SolidBrush(group.Items[j].BackColor);
                            }
                            else
                            {
                                brush = new SolidBrush(group.Items[j].BackColor);
                                brush2 = new SolidBrush(group.Items[j].ForeColor);
                            }
                            Rectangle rectangle4 = group.Items[j].GetBounds(ItemBoundsPortion.Label);
                            RectangleF rect = new RectangleF((float) rectangle4.X, (float) rectangle4.Y, (float) rectangle4.Width, 16f);
                            e.Graphics.FillRectangle(brush, rect);
                            e.Graphics.DrawString(group.Items[j].Text, this.Font, brush2, (float) rectangle4.X, (float) rectangle4.Y);
                            brush.Dispose();
                            brush2.Dispose();
                        }
                    }
                }
            }
            base.OnPaint(e);
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int SendMessage(IntPtr handle, int messg, int wparam, int lparam);
        public void SetExStyles()
        {
            this.styles = (LVS_EX) SendMessage(base.Handle, 4151, 0, 0);
            this.styles |= LVS_EX.LVS_EX_DOUBLEBUFFER | LVS_EX.LVS_EX_BORDERSELECT;
            SendMessage(base.Handle, 4150, 0, (int) this.styles);
        }

        public void SetExStyles(LVS_EX exStyle)
        {
            this.styles = (LVS_EX) SendMessage(base.Handle, 4151, 0, 0);
            this.styles |= exStyle;
            SendMessage(base.Handle, 4150, 0, (int) this.styles);
        }
    }
}


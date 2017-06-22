using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtendEx
{
    partial class ItemImagesContainer
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.imageListBox = new ListBox();
            base.SuspendLayout();
            this.imageListBox.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.imageListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imageListBox.IntegralHeight = false;
            this.imageListBox.ItemHeight = 12;
            this.imageListBox.Location = new Point(0, 0);
            this.imageListBox.MultiColumn = true;
            this.imageListBox.Name = "imageListBox";
            this.imageListBox.Size = new Size(180, 155);
            this.imageListBox.TabIndex = 0;
            this.imageListBox.DrawItem += new DrawItemEventHandler(this.imageListBox_DrawItem);
            this.imageListBox.SelectedIndexChanged += new EventHandler(this.imageListBox_SelectedIndexChanged);
            this.imageListBox.SizeChanged += new EventHandler(this.imageListBox_SizeChanged);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(178, 88);
            base.ControlBox = false;
            base.Controls.Add(this.imageListBox);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ItemImagesContainer";
            base.ShowInTaskbar = false;
            base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            base.Load += new EventHandler(this.ItemImagesContainer_Load);
            base.ResumeLayout(false);
        }

       
        private ListBox imageListBox;
    }
}
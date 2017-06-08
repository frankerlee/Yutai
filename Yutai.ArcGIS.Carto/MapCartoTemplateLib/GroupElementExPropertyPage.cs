using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class GroupElementExPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private RadioButton rdoFiexdSize;
        private RadioButton rdoSameAsWidth;
        private RadioButton rdoWidthScale;
        private TextBox txtScale;

        public event OnValueChangeEventHandler OnValueChange;

        public GroupElementExPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                if (this.rdoFiexdSize.Checked)
                {
                    (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeStyle = SizeStyle.Fixed;
                }
                else if (this.rdoSameAsWidth.Checked)
                {
                    (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeStyle = SizeStyle.SameAsInsideWidth;
                }
                else if (this.rdoWidthScale.Checked)
                {
                    (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeStyle = SizeStyle.InsideWidthScale;
                    try
                    {
                        (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeScale = Convert.ToDouble(this.txtScale.Text);
                    }
                    catch
                    {
                        MessageBox.Show("数据输入错误!");
                        return;
                    }
                }
                this.bool_1 = false;
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void GroupElementExPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void InitializeComponent()
        {
            this.rdoFiexdSize = new RadioButton();
            this.rdoSameAsWidth = new RadioButton();
            this.rdoWidthScale = new RadioButton();
            this.txtScale = new TextBox();
            base.SuspendLayout();
            this.rdoFiexdSize.AutoSize = true;
            this.rdoFiexdSize.Location = new Point(0x15, 0x1a);
            this.rdoFiexdSize.Name = "rdoFiexdSize";
            this.rdoFiexdSize.Size = new Size(0x47, 0x10);
            this.rdoFiexdSize.TabIndex = 2;
            this.rdoFiexdSize.Text = "固定大小";
            this.rdoFiexdSize.UseVisualStyleBackColor = true;
            this.rdoFiexdSize.CheckedChanged += new EventHandler(this.rdoWidthScale_CheckedChanged);
            this.rdoSameAsWidth.AutoSize = true;
            this.rdoSameAsWidth.Checked = true;
            this.rdoSameAsWidth.Location = new Point(0x15, 0x37);
            this.rdoSameAsWidth.Name = "rdoSameAsWidth";
            this.rdoSameAsWidth.Size = new Size(0x5f, 0x10);
            this.rdoSameAsWidth.TabIndex = 3;
            this.rdoSameAsWidth.TabStop = true;
            this.rdoSameAsWidth.Text = "同内图廓宽度";
            this.rdoSameAsWidth.UseVisualStyleBackColor = true;
            this.rdoSameAsWidth.CheckedChanged += new EventHandler(this.rdoWidthScale_CheckedChanged);
            this.rdoWidthScale.AutoSize = true;
            this.rdoWidthScale.Location = new Point(0x15, 0x56);
            this.rdoWidthScale.Name = "rdoWidthScale";
            this.rdoWidthScale.Size = new Size(0x9b, 0x10);
            this.rdoWidthScale.TabIndex = 4;
            this.rdoWidthScale.Text = "同内图廓宽度按比例缩放";
            this.rdoWidthScale.UseVisualStyleBackColor = true;
            this.rdoWidthScale.CheckedChanged += new EventHandler(this.rdoWidthScale_CheckedChanged);
            this.txtScale.Location = new Point(40, 0x6c);
            this.txtScale.Name = "txtScale";
            this.txtScale.ReadOnly = true;
            this.txtScale.Size = new Size(0x88, 0x15);
            this.txtScale.TabIndex = 5;
            this.txtScale.Text = "1";
            this.txtScale.TextChanged += new EventHandler(this.txtScale_TextChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.txtScale);
            base.Controls.Add(this.rdoWidthScale);
            base.Controls.Add(this.rdoSameAsWidth);
            base.Controls.Add(this.rdoFiexdSize);
            base.Name = "GroupElementExPropertyPage";
            base.Size = new Size(0xfc, 0xce);
            base.Load += new EventHandler(this.GroupElementExPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_1()
        {
            this.bool_0 = false;
            if (this.mapTemplateElement_0 != null)
            {
                SizeStyle sizeStyle = (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeStyle;
                double sizeScale = (this.mapTemplateElement_0 as MapTemplateGroupElement).SizeScale;
                if (sizeStyle == SizeStyle.Fixed)
                {
                    this.rdoFiexdSize.Checked = true;
                    this.txtScale.Enabled = false;
                }
                else if (sizeStyle == SizeStyle.InsideWidthScale)
                {
                    this.rdoWidthScale.Checked = true;
                    this.txtScale.Enabled = true;
                }
                else
                {
                    this.rdoSameAsWidth.Checked = true;
                    this.txtScale.Enabled = false;
                }
                this.txtScale.Text = sizeScale.ToString();
            }
            this.bool_0 = true;
        }

        private void rdoWidthScale_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoWidthScale.Checked)
            {
                this.txtScale.Enabled = true;
            }
            else
            {
                this.txtScale.Enabled = false;
            }
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_1();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapCartoTemplateLib.MapTemplateElement;
        }

        private void txtScale_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                this.mapTemplateElement_0 = value;
                this.method_1();
            }
        }

        public string Title
        {
            get
            {
                return "组合元素缩放";
            }
            set
            {
            }
        }
    }
}


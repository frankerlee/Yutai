using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    [ToolboxItem(false)]
    public class TFInfoPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtInOutDis;
        private TextBox txtOutBorderWidth;
        private TextBox txtStartMultiple;
        private TextBox txtTitleSpace;
        private TextBox txtXInterval;
        private TextBox txtYInterval;

        public event OnValueChangeEventHandler OnValueChange;

        public TFInfoPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                try
                {
                    CartoTemplateClass.InOutDist = double.Parse(this.txtInOutDis.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.XInterval = double.Parse(this.txtXInterval.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.YInterval = double.Parse(this.txtYInterval.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.TitleDist = double.Parse(this.txtTitleSpace.Text);
                }
                catch
                {
                }
                try
                {
                    CartoTemplateClass.StartCoodinateMultiple = double.Parse(this.txtStartMultiple.Text);
                }
                catch
                {
                }
                CartoTemplateClass.TuKuoInfo = this.SaveToXml();
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

        public void Init()
        {
            if (CartoTemplateClass.TuKuoInfo.Length != 0)
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(CartoTemplateClass.TuKuoInfo);
                    XmlNode node = document.ChildNodes[0];
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        XmlNode node2 = node.ChildNodes[i];
                        string str = node2.Attributes["name"].Value;
                        string str2 = node2.Attributes["value"].Value;
                        switch (str)
                        {
                            case "InOutDist":
                                this.txtInOutDis.Text = str2;
                                break;

                            case "TitleDist":
                                this.txtTitleSpace.Text = str2;
                                break;

                            case "XInterval":
                                this.txtXInterval.Text = str2;
                                break;

                            case "YInterval":
                                this.txtYInterval.Text = str2;
                                break;

                            case "OutBorderWidth":
                                this.txtOutBorderWidth.Text = str2;
                                break;

                            case "StartCoodinateMultiple":
                                this.txtStartMultiple.Text = str2;
                                break;
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void InitializeComponent()
        {
            this.label6 = new Label();
            this.label5 = new Label();
            this.txtYInterval = new TextBox();
            this.txtXInterval = new TextBox();
            this.label7 = new Label();
            this.label8 = new Label();
            this.txtStartMultiple = new TextBox();
            this.label9 = new Label();
            this.txtTitleSpace = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtOutBorderWidth = new TextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtInOutDis = new TextBox();
            this.label10 = new Label();
            this.label11 = new Label();
            base.SuspendLayout();
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xc2, 0x7e);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x11, 12);
            this.label6.TabIndex = 0x13;
            this.label6.Text = "米";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xc2, 0x5f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 12);
            this.label5.TabIndex = 0x12;
            this.label5.Text = "米";
            this.txtYInterval.Location = new Point(0x72, 0x7b);
            this.txtYInterval.Name = "txtYInterval";
            this.txtYInterval.Size = new Size(0x4a, 0x15);
            this.txtYInterval.TabIndex = 0x11;
            this.txtYInterval.Text = "1000";
            this.txtYInterval.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.txtXInterval.Location = new Point(0x72, 0x5f);
            this.txtXInterval.Name = "txtXInterval";
            this.txtXInterval.Size = new Size(0x4a, 0x15);
            this.txtXInterval.TabIndex = 0x10;
            this.txtXInterval.Text = "1000";
            this.txtXInterval.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x13, 0x7e);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x4d, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "格网竖直间距";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x13, 0x5f);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x4d, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "格网水平间距";
            this.txtStartMultiple.Location = new Point(0x72, 0x95);
            this.txtStartMultiple.Name = "txtStartMultiple";
            this.txtStartMultiple.Size = new Size(0x4a, 0x15);
            this.txtStartMultiple.TabIndex = 0x15;
            this.txtStartMultiple.Text = "10";
            this.txtStartMultiple.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x13, 0x98);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x59, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "起始点坐标倍数";
            this.txtTitleSpace.Location = new Point(0x72, 11);
            this.txtTitleSpace.Name = "txtTitleSpace";
            this.txtTitleSpace.Size = new Size(0x4a, 0x15);
            this.txtTitleSpace.TabIndex = 0x44;
            this.txtTitleSpace.Text = "0.1";
            this.txtTitleSpace.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 12);
            this.label1.TabIndex = 0x43;
            this.label1.Text = "图名与外框间距";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xc2, 70);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0x42;
            this.label2.Text = "厘米";
            this.txtOutBorderWidth.Location = new Point(0x72, 0x43);
            this.txtOutBorderWidth.Name = "txtOutBorderWidth";
            this.txtOutBorderWidth.Size = new Size(0x4a, 0x15);
            this.txtOutBorderWidth.TabIndex = 0x41;
            this.txtOutBorderWidth.Text = "0.1";
            this.txtOutBorderWidth.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 0x40;
            this.label3.Text = "外框宽度";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xc2, 0x29);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x3f;
            this.label4.Text = "厘米";
            this.txtInOutDis.Location = new Point(0x72, 0x26);
            this.txtInOutDis.Name = "txtInOutDis";
            this.txtInOutDis.Size = new Size(0x4a, 0x15);
            this.txtInOutDis.TabIndex = 0x3e;
            this.txtInOutDis.Text = "1";
            this.txtInOutDis.TextChanged += new EventHandler(this.txtInOutDis_TextChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x13, 0x29);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x41, 12);
            this.label10.TabIndex = 0x3d;
            this.label10.Text = "内外框间距";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xc2, 14);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x1d, 12);
            this.label11.TabIndex = 0x45;
            this.label11.Text = "厘米";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.label11);
            base.Controls.Add(this.txtStartMultiple);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.txtTitleSpace);
            base.Controls.Add(this.txtYInterval);
            base.Controls.Add(this.txtXInterval);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.txtOutBorderWidth);
            base.Controls.Add(this.txtInOutDis);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Name = "TFInfoPage";
            base.Size = new Size(0x138, 0xc4);
            base.Load += new EventHandler(this.TFInfoPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0(StringBuilder stringBuilder_0, string string_0, string string_1)
        {
            stringBuilder_0.Append("<attribute name=\"");
            stringBuilder_0.Append(string_0);
            stringBuilder_0.Append("\" value=\"");
            stringBuilder_0.Append(string_1 + "\"/>");
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.Init();
            this.bool_0 = true;
        }

        public string SaveToXml()
        {
            StringBuilder builder = new StringBuilder("<TFElement>");
            this.method_0(builder, "InOutDist", this.txtInOutDis.Text);
            this.method_0(builder, "TitleDist", this.txtTitleSpace.Text);
            this.method_0(builder, "XInterval", this.txtXInterval.Text);
            this.method_0(builder, "YInterval", this.txtYInterval.Text);
            this.method_0(builder, "OutBorderWidth", this.txtOutBorderWidth.Text);
            this.method_0(builder, "StartCoodinateMultiple", this.txtStartMultiple.Text);
            builder.Append("</TFElement>");
            return builder.ToString();
        }

        public void SetObjects(object object_0)
        {
        }

        private void TFInfoPage_Load(object sender, EventArgs e)
        {
            this.Init();
            this.bool_0 = true;
        }

        private void txtInOutDis_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
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

        public string Title
        {
            get
            {
                return "图幅";
            }
            set
            {
            }
        }
    }
}


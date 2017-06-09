using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.UI
{
    public class ElementGeometryInfoPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IElement ielement_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private string string_0 = "面积";
        private TextEdit txtArea;
        private TextEdit txtPerimeter;
        private TextEdit txtX;
        private TextEdit txtY;

        public event OnValueChangeEventHandler OnValueChange;

        public ElementGeometryInfoPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
        }

        public void Cancel()
        {
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void ElementGeometryInfoPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.txtArea = new TextEdit();
            this.txtPerimeter = new TextEdit();
            this.txtY = new TextEdit();
            this.txtX = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtArea.Properties.BeginInit();
            this.txtPerimeter.Properties.BeginInit();
            this.txtY.Properties.BeginInit();
            this.txtX.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "面积";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "周长";
            this.groupBox1.Controls.Add(this.txtY);
            this.groupBox1.Controls.Add(this.txtX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(160, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "中心";
            this.txtArea.EditValue = "";
            this.txtArea.Location = new System.Drawing.Point(0x38, 0x10);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new Size(80, 0x15);
            this.txtArea.TabIndex = 3;
            this.txtPerimeter.EditValue = "";
            this.txtPerimeter.Location = new System.Drawing.Point(0x38, 40);
            this.txtPerimeter.Name = "txtPerimeter";
            this.txtPerimeter.Size = new Size(80, 0x15);
            this.txtPerimeter.TabIndex = 4;
            this.txtY.EditValue = "";
            this.txtY.Location = new System.Drawing.Point(0x20, 40);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(80, 0x15);
            this.txtY.TabIndex = 8;
            this.txtX.EditValue = "";
            this.txtX.Location = new System.Drawing.Point(0x20, 0x10);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(80, 0x15);
            this.txtX.TabIndex = 7;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0x2a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(11, 0x11);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 0x12);
            this.label4.Name = "label4";
            this.label4.Size = new Size(11, 0x11);
            this.label4.TabIndex = 5;
            this.label4.Text = "X";
            base.Controls.Add(this.txtPerimeter);
            base.Controls.Add(this.txtArea);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "ElementGeometryInfoPropertyPage";
            base.Size = new Size(0x108, 0xb8);
            base.Load += new EventHandler(this.ElementGeometryInfoPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtArea.Properties.EndInit();
            this.txtPerimeter.Properties.EndInit();
            this.txtY.Properties.EndInit();
            this.txtX.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0()
        {
            if (this.ielement_0 != null)
            {
                if (this.ielement_0.Geometry.IsEmpty)
                {
                    this.txtPerimeter.Text = "";
                    this.txtX.Text = "";
                    this.txtY.Text = "";
                }
                else
                {
                    IEnvelope envelope = this.ielement_0.Geometry.Envelope;
                    this.txtArea.Text = (envelope as IArea).Area.ToString("0.#####");
                    this.txtPerimeter.Text = ((envelope.Width + envelope.Height) * 2.0).ToString("0.#####");
                    double num = (envelope.XMin + envelope.XMax) / 2.0;
                    this.txtX.Text = num.ToString("0.#####");
                    this.txtY.Text = ((envelope.YMin + envelope.YMax) / 2.0).ToString("0.#####");
                }
            }
        }

        public void ResetControl()
        {
            this.method_0();
        }

        public void SetObjects(object object_0)
        {
            this.ielement_0 = object_0 as IElement;
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
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
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}


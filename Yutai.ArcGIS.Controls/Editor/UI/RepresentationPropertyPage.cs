using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class RepresentationPropertyPage : UserControl
    {
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private Label label1;
        private IRepresentation m_pRepentation = null;
        private Panel panel1;

        public RepresentationPropertyPage()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.checkBox1 = new CheckBox();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "制图表现规则";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(4, 0x19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xa4, 20);
            this.comboBox1.TabIndex = 1;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(3, 0x33);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x30, 0x10);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "可见";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x4c);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xb1, 0xb2);
            this.panel1.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label1);
            base.Name = "RepresentationPropertyPage";
            base.Size = new Size(0xb1, 0xfe);
            base.Load += new EventHandler(this.RepresentationPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void RepresentationPropertyPage_Load(object sender, EventArgs e)
        {
            int num;
            IRepresentationRule rule;
            IRepresentationRules representationRules = this.m_pRepentation.RepresentationClass.RepresentationRules;
            representationRules.Reset();
            representationRules.Next(out num, out rule);
            while (rule != null)
            {
                string name = representationRules.get_Name(num);
                this.comboBox1.Items.Add(new RepresentationRuleWrap(num, name, rule));
            }
        }

        public IRepresentation Representation
        {
            get
            {
                return this.m_pRepentation;
            }
            set
            {
                this.m_pRepentation = value;
            }
        }

        internal class RepresentationRuleWrap
        {
            private int _id;
            private string _name;
            private IRepresentationRule _RepresentationRule;

            internal RepresentationRuleWrap(int id, string name, IRepresentationRule pRepresentationRule)
            {
                this._id = id;
                this._name = name;
                this._RepresentationRule = pRepresentationRule;
            }

            public string Name
            {
                get
                {
                    return this._name;
                }
            }

            public IRepresentationRule RepresentationRule
            {
                get
                {
                    return this._RepresentationRule;
                }
            }

            public int RuleID
            {
                get
                {
                    return this._id;
                }
            }
        }
    }
}


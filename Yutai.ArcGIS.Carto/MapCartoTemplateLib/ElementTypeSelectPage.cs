using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class ElementTypeSelectPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBox cboMapTemplateElementType;
        private IContainer icontainer_0 = null;
        private Label label1;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;

        public event OnValueChangeEventHandler OnValueChange;

        public ElementTypeSelectPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            switch (this.cboMapTemplateElementType.SelectedIndex)
            {
                case 0:
                    this.MapTemplateElement = new MapTemplateTextElement(this.MapTemplate);
                    break;

                case 1:
                    this.MapTemplateElement = new MapTemplateScaleTextElement(this.MapTemplate);
                    break;

                case 2:
                    this.MapTemplateElement = new MapTemplateScaleBarElement(this.MapTemplate);
                    break;

                case 3:
                    this.MapTemplateElement = new MapTemplateLegendElement(this.MapTemplate);
                    break;

                case 4:
                    this.MapTemplateElement = new MapTemplatePictureElement(this.MapTemplate);
                    break;

                case 5:
                    this.MapTemplateElement = new MapTemplateOLEElement(this.MapTemplate);
                    break;

                case 6:
                    this.MapTemplateElement = new MapTemplateNorthElement(this.MapTemplate);
                    break;

                case 7:
                    this.MapTemplateElement = new MapTemplateJoinTableElement(this.MapTemplate);
                    break;

                case 8:
                    this.MapTemplateElement = new MapTemplateCustomLegendElement(this.MapTemplate);
                    break;

                case 9:
                    this.MapTemplateElement = new MapTemplateTableElement(this.MapTemplate);
                    break;
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

        private void ElementTypeSelectPage_Load(object sender, EventArgs e)
        {
            this.cboMapTemplateElementType.SelectedIndex = 0;
            this.bool_0 = true;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cboMapTemplateElementType = new ComboBox();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x12, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "元素类型";
            this.cboMapTemplateElementType.FormattingEnabled = true;
            this.cboMapTemplateElementType.Items.AddRange(new object[] { "文本    ", "比例尺文本", "比例尺栏", "图例", "图片", "OLE对象", "指北针", "接图表", "自定义图例", "表格" });
            this.cboMapTemplateElementType.Location = new Point(0x4d, 0x15);
            this.cboMapTemplateElementType.Name = "cboMapTemplateElementType";
            this.cboMapTemplateElementType.Size = new Size(0x79, 20);
            this.cboMapTemplateElementType.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.cboMapTemplateElementType);
            base.Controls.Add(this.label1);
            base.Name = "ElementTypeSelectPage";
            base.Size = new Size(0x125, 320);
            base.Load += new EventHandler(this.ElementTypeSelectPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void method_0(object sender, EventArgs e)
        {
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
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

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            protected get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplateElement_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.mapTemplateElement_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return "类型";
            }
            set
            {
            }
        }
    }
}


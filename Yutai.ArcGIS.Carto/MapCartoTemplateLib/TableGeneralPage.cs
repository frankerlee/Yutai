using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class TableGeneralPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;


        public event OnValueChangeEventHandler OnValueChange;

        public TableGeneralPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                MapTemplateTableElement element = this.mapTemplateElement_0 as MapTemplateTableElement;
                element.Width = Convert.ToDouble(this.txtWidth.Text);
                element.Height = Convert.ToDouble(this.txtHeight.Text);
                if (!this.IsEdit)
                {
                    element.RowNumber = Convert.ToInt32(this.txtRow.Text);
                    element.ColumnNumber = Convert.ToInt32(this.txtCol.Text);
                }
            }
        }

        public bool CanApply()
        {
            try
            {
                double num = Convert.ToDouble(this.txtWidth.Text);
                double num2 = Convert.ToDouble(this.txtHeight.Text);
                double num3 = Convert.ToDouble(this.txtRow.Text);
                double num4 = Convert.ToDouble(this.txtCol.Text);
                if (num <= 0.0)
                {
                    MessageBox.Show("宽度必须大于0!");
                    return false;
                }
                if (num2 <= 0.0)
                {
                    MessageBox.Show("高度必须大于0!");
                    return false;
                }
                if (num3 <= 0.0)
                {
                    MessageBox.Show("行数必须大于0!");
                    return false;
                }
                if (num4 <= 0.0)
                {
                    MessageBox.Show("列数必须大于0!");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("请检查输入数据是否正确!");
                return false;
            }
            return true;
        }

        public void Cancel()
        {
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapTemplateElement;
        }

        private void TableGeneralPage_Load(object sender, EventArgs e)
        {
            this.txtCol.Visible = !this.IsEdit;
            this.txtRow.Visible = !this.IsEdit;
            this.label1.Visible = !this.IsEdit;
            this.label2.Visible = !this.IsEdit;
            MapTemplateTableElement element = this.mapTemplateElement_0 as MapTemplateTableElement;
            this.txtWidth.Text = element.Width.ToString();
            this.txtHeight.Text = element.Height.ToString();
            this.txtRow.Text = element.RowNumber.ToString();
            this.txtCol.Text = element.ColumnNumber.ToString();
            this.bool_0 = true;
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsEdit { get; set; }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return "常规"; }
            set { throw new NotImplementedException(); }
        }
    }
}
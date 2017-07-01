using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class FrmFastSelSpatial : Form
    {
        private double double_0 = -10000.0;
        private double double_1 = 11474.83645;
        private double double_2 = -10000.0;
        private double double_3 = 11474.83645;
        private IContainer icontainer_0 = null;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private string string_0 = (Application.StartupPath + @"\Application.prj");

        public FrmFastSelSpatial()
        {
            this.InitializeComponent();
            this.method_2();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string str2;
            if (this.rdGeo.Checked)
            {
                string str;
                if (this.rdBj54.Checked)
                {
                    str = "1954";
                }
                else
                {
                    str = "1980";
                }
                str2 = this.method_4(this.lstGeographic, str);
            }
            else
            {
                string str3;
                string str4;
                if (this.rdBj54.Checked)
                {
                    str3 = "54";
                }
                else
                {
                    str3 = "80";
                }
                if (this.rdBand3.Checked)
                {
                    str4 = "3";
                }
                else
                {
                    str4 = "6";
                }
                string str5 = this.cmbBandNum.Text.Trim();
                str2 = this.method_4(this.lstProjected, str3 + ":" + str4 + ":" + str5);
            }
            if (str2 != "")
            {
                try
                {
                    if (this.Line(str2))
                    {
                        this.ispatialReference_0 =
                            ((ISpatialReferenceFactory2) this.ispatialReferenceFactory_0)
                                .CreateESRISpatialReferenceFromPRJFile(this.string_0);
                        this.method_0();
                        this.ispatialReference_0.SetDomain(this.double_0, this.double_1, this.double_2, this.double_3);
                        base.DialogResult = DialogResult.OK;
                        File.Delete(this.string_0);
                        base.Close();
                    }
                }
                catch
                {
                }
            }
            else
            {
                MessageBox.Show("错误的选择", "提示");
            }
        }

        private void cmbBandNum_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public bool Line(string string_2)
        {
            StreamWriter writer = new StreamWriter(this.string_0, false);
            bool flag = false;
            try
            {
                writer.WriteLine(string_2);
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
                throw exception;
            }
            finally
            {
                writer.Dispose();
                writer.Close();
                writer = null;
            }
            return flag;
        }

        private void method_0()
        {
            string str = "";
            str = this.method_1(this.string_1, ':', 0);
            if (str != "")
            {
                this.double_0 = Convert.ToDouble(str);
            }
            str = this.method_1(this.string_1, ':', 1);
            if (str != "")
            {
                this.double_2 = Convert.ToDouble(str);
            }
            str = this.method_1(this.string_1, ':', 2);
            if (str != "")
            {
                this.double_1 = Convert.ToDouble(str);
            }
            str = this.method_1(this.string_1, ':', 3);
            if (str != "")
            {
                this.double_3 = Convert.ToDouble(str);
            }
        }

        private string method_1(string string_2, char char_0, int int_0)
        {
            try
            {
                string[] strArray = string_2.Split(new char[] {char_0});
                if (strArray.GetUpperBound(0) >= 0)
                {
                    return strArray[int_0];
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        private void method_2()
        {
            int num;
            this.cmbBandNum.Text = "";
            this.cmbBandNum.Items.Clear();
            if (this.rdBand3.Checked)
            {
                for (num = 25; num <= 45; num++)
                {
                    this.cmbBandNum.Items.Add(num.ToString());
                }
            }
            else
            {
                for (num = 13; num <= 23; num++)
                {
                    this.cmbBandNum.Items.Add(num.ToString());
                }
            }
        }

        private void method_3(bool bool_0)
        {
            this.groupBox2.Enabled = bool_0;
            this.rdBand3.Enabled = bool_0;
            this.rdBand6.Enabled = bool_0;
            this.cmbBandNum.Enabled = bool_0;
        }

        private string method_4(ListView listView_0, string string_2)
        {
            for (int i = 0; i < listView_0.Items.Count; i++)
            {
                if (string_2.ToUpper() == listView_0.Items[i].Text.ToUpper())
                {
                    this.string_1 = listView_0.Items[i].ToolTipText;
                    return listView_0.Items[i].Tag.ToString();
                }
            }
            return "";
        }

        private void rdBand3_CheckedChanged(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void rdBand6_CheckedChanged(object sender, EventArgs e)
        {
            this.method_2();
        }

        private void rdGeo_Click(object sender, EventArgs e)
        {
            this.method_3(false);
        }

        private void rdPrj_Click(object sender, EventArgs e)
        {
            this.method_3(true);
        }

        public ISpatialReference SpatialRefrence
        {
            get { return this.ispatialReference_0; }
        }
    }
}
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class FrmFastSelSpatial : Form
    {
        private string m_FilePathName = (Application.StartupPath + @"\Application.prj");
        private ISpatialReferenceFactory m_SpatialFactory = new SpatialReferenceEnvironmentClass();
        private double xmax = 11474.83645;
        private double xmin = -10000.0;
        private double ymax = 11474.83645;
        private double ymin = -10000.0;

        public FrmFastSelSpatial()
        {
            this.InitializeComponent();
            this.LoadBandNum();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string pRJ;
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
                pRJ = this.GetPRJ(this.lstGeographic, str);
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
                pRJ = this.GetPRJ(this.lstProjected, str3 + ":" + str4 + ":" + str5);
            }
            if (pRJ != "")
            {
                try
                {
                    if (this.Line(pRJ))
                    {
                        this.m_pSpatialRefrence = ((ISpatialReferenceFactory2) this.m_SpatialFactory).CreateESRISpatialReferenceFromPRJFile(this.m_FilePathName);
                        this.IniBound();
                        this.m_pSpatialRefrence.SetDomain(this.xmin, this.xmax, this.ymin, this.ymax);
                        base.DialogResult = DialogResult.OK;
                        File.Delete(this.m_FilePathName);
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

 private void EnableControl(bool isValue)
        {
            this.groupBox2.Enabled = isValue;
            this.rdBand3.Enabled = isValue;
            this.rdBand6.Enabled = isValue;
            this.cmbBandNum.Enabled = isValue;
        }

        private string GetPRJ(ListView mListView, string mItemText)
        {
            for (int i = 0; i < mListView.Items.Count; i++)
            {
                if (mItemText.ToUpper() == mListView.Items[i].Text.ToUpper())
                {
                    this.BoundText = mListView.Items[i].ToolTipText;
                    return mListView.Items[i].Tag.ToString();
                }
            }
            return "";
        }

        private string Getstr(string str, char car, int index)
        {
            try
            {
                string[] strArray = str.Split(new char[] { car });
                if (strArray.GetUpperBound(0) >= 0)
                {
                    return strArray[index];
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        private void IniBound()
        {
            string str = "";
            str = this.Getstr(this.BoundText, ':', 0);
            if (str != "")
            {
                this.xmin = Convert.ToDouble(str);
            }
            str = this.Getstr(this.BoundText, ':', 1);
            if (str != "")
            {
                this.ymin = Convert.ToDouble(str);
            }
            str = this.Getstr(this.BoundText, ':', 2);
            if (str != "")
            {
                this.xmax = Convert.ToDouble(str);
            }
            str = this.Getstr(this.BoundText, ':', 3);
            if (str != "")
            {
                this.ymax = Convert.ToDouble(str);
            }
        }

 public bool Line(string strPrj)
        {
            StreamWriter writer = new StreamWriter(this.m_FilePathName, false);
            bool flag = false;
            try
            {
                writer.WriteLine(strPrj);
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

        private void LoadBandNum()
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

        private void rdBand3_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadBandNum();
        }

        private void rdBand6_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadBandNum();
        }

        private void rdGeo_Click(object sender, EventArgs e)
        {
            this.EnableControl(false);
        }

        private void rdPrj_Click(object sender, EventArgs e)
        {
            this.EnableControl(true);
        }

        public ISpatialReference SpatialRefrence
        {
            get
            {
                return this.m_pSpatialRefrence;
            }
            set
            {
                this.m_pSpatialRefrence = value;
            }
        }
    }
}


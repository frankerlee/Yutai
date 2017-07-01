using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    public partial class frmAppConfigSetting : Form
    {
        private GDBSettingPropertyPage m_GDBSettingPropertyPage = new GDBSettingPropertyPage();
        private bool m_IsChange = false;
        private LayerSettingInfoPropertyPage m_LayerSettingInfoPropertyPage = new LayerSettingInfoPropertyPage();
        private OLEDBSettingPropertyPage m_OLEDBSettingPropertyPage = new OLEDBSettingPropertyPage(0);
        private OtherSettingPropertyPage m_OtherSettingPropertyPage = new OtherSettingPropertyPage();
        private SymbolsSettingPropertyPage m_pSymbolsSettingPage = new SymbolsSettingPropertyPage();
        private OLEDBSettingPropertyPage m_SySSettingPropertyPage = new OLEDBSettingPropertyPage(1);

        public frmAppConfigSetting()
        {
            this.InitializeComponent();
            string path = Application.ExecutablePath + ".config";
            XmlDocument document = new XmlDocument();
            document.Load(Application.StartupPath + @"\ConfigSetting.xml");
            XmlNode node = document.GetElementsByTagName("ConfigFile")[0];
            string str2 = node.Attributes[0].Value;
            if (str2[1] != ':')
            {
                str2 = Application.StartupPath + @"\" + str2;
            }
            if (File.Exists(path))
            {
                AppConfig.m_strConfigfile = path;
                AppConfig.m_AppConfig.Load(AppConfig.m_strConfigfile);
                this.m_OLEDBSettingPropertyPage.Dock = DockStyle.Fill;
                this.tabPage1.Controls.Add(this.m_GDBSettingPropertyPage);
                this.tabPage4.Controls.Add(this.m_OLEDBSettingPropertyPage);
                this.tabPage2.Controls.Add(this.m_pSymbolsSettingPage);
                this.tabPage3.Controls.Add(this.m_OtherSettingPropertyPage);
                this.tabPage5.Controls.Add(this.m_LayerSettingInfoPropertyPage);
                this.tabPage6.Controls.Add(this.m_SySSettingPropertyPage);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppConfig.m_strConfigfile != null)
                {
                    AppConfig.m_AppConfig.Save(AppConfig.m_strConfigfile);
                }
                MessageBox.Show("保存成功!");
                this.m_IsChange = true;
            }
            catch
            {
                MessageBox.Show("保存失败!");
            }
        }

        private void Init()
        {
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public bool IsChange
        {
            get { return this.m_IsChange; }
        }
    }
}
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.ApplicationStyle;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    public partial class frmAppConfigSettingEx : Form
    {
        private GDBSettingPropertyPage m_GDBSettingPropertyPage = new GDBSettingPropertyPage();
        private LayerSettingInfoPropertyPage m_LayerSettingInfoPropertyPage = new LayerSettingInfoPropertyPage();
        private OLEDBSettingPropertyPage m_OLEDBSettingPropertyPage = new OLEDBSettingPropertyPage(0);
        private OtherSettingPropertyPage m_OtherSettingPropertyPage = new OtherSettingPropertyPage();
        private SymbolsSettingPropertyPage m_pSymbolsSettingPage = new SymbolsSettingPropertyPage();
        private OLEDBSettingPropertyPage m_SySSettingPropertyPage = new OLEDBSettingPropertyPage(1);

        public frmAppConfigSettingEx()
        {
            this.InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxEdit1.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择配置文件!");
                }
                else
                {
                    if (AppConfig.m_strConfigfile != null)
                    {
                        AppConfig.m_AppConfig.Save(AppConfig.m_strConfigfile);
                        string path = AppConfig.m_strConfigfile.Substring(0, AppConfig.m_strConfigfile.Length - 10) +
                                      "vshost.exe.config";
                        if (File.Exists(path))
                        {
                            AppConfig.m_AppConfig.Save(path);
                        }
                    }
                    MessageBox.Show("保存成功!");
                }
            }
            catch
            {
                MessageBox.Show("保存失败!");
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxEdit1.SelectedIndex != -1)
                {
                    AppConfig.m_strConfigfile = this.comboBoxEdit1.Text;
                    AppConfig.m_AppConfig.Load(AppConfig.m_strConfigfile);
                    this.m_pSymbolsSettingPage.Init();
                    this.m_GDBSettingPropertyPage.Init();
                    this.m_OtherSettingPropertyPage.Init();
                    this.m_OLEDBSettingPropertyPage.Init();
                    this.m_LayerSettingInfoPropertyPage.Init();
                    this.m_SySSettingPropertyPage.Init();
                }
            }
            catch (Exception)
            {
            }
        }

        private void frmAppConfigSettingEx_Load(object sender, EventArgs e)
        {
            PaintStyleMenuItem.SetDefaultStyle();
            string[] files = Directory.GetFiles(Application.StartupPath, "*.config");
            for (int i = 0; i < files.Length; i++)
            {
                if ((files[i].LastIndexOf(".exe.config") != -1) && (files[i].LastIndexOf(".vshost.exe.config") == -1))
                {
                    this.comboBoxEdit1.Properties.Items.Add(files[i]);
                }
            }
            this.m_OLEDBSettingPropertyPage.Dock = DockStyle.Fill;
            this.tabPage1.Controls.Add(this.m_GDBSettingPropertyPage);
            this.tabPage4.Controls.Add(this.m_OLEDBSettingPropertyPage);
            this.tabPage2.Controls.Add(this.m_pSymbolsSettingPage);
            this.tabPage3.Controls.Add(this.m_OtherSettingPropertyPage);
            this.tabPage5.Controls.Add(this.m_LayerSettingInfoPropertyPage);
            this.tabPage6.Controls.Add(this.m_SySSettingPropertyPage);
            if (this.comboBoxEdit1.Properties.Items.Count > 0)
            {
                this.comboBoxEdit1.SelectedIndex = 0;
                AppConfig.m_strConfigfile = this.comboBoxEdit1.Text;
                AppConfig.m_AppConfig.Load(AppConfig.m_strConfigfile);
                this.m_pSymbolsSettingPage.Init();
                this.m_GDBSettingPropertyPage.Init();
                this.m_OtherSettingPropertyPage.Init();
                this.m_OLEDBSettingPropertyPage.Init();
                this.m_LayerSettingInfoPropertyPage.Init();
                this.m_SySSettingPropertyPage.Init();
            }
        }

        [STAThread]
        private static void Main()
        {
            AoLicenseManager.Initialize();
            Application.Run(new frmAppConfigSettingEx());
        }
    }
}
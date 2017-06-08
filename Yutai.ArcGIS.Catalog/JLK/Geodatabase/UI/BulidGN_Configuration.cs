namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BulidGN_Configuration : UserControl
    {
        private ComboBoxEdit comboBoxEdit;
        private Container container_0 = null;
        private Label label1;
        private RadioGroup radioGroup1;

        public BulidGN_Configuration()
        {
            this.InitializeComponent();
        }

        private void BulidGN_Configuration_Load(object sender, EventArgs e)
        {
            if (BulidGeometryNetworkHelper.BulidGNHelper.UseDefaultConfigKey)
            {
                this.radioGroup1.SelectedIndex = 0;
                BulidGeometryNetworkHelper.BulidGNHelper.ConfigurationKeyword = "";
            }
            else
            {
                this.radioGroup1.SelectedIndex = 1;
                BulidGeometryNetworkHelper.BulidGNHelper.ConfigurationKeyword = this.comboBoxEdit.Text;
            }
            IWorkspaceConfiguration workspace = BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Workspace as IWorkspaceConfiguration;
            if (workspace != null)
            {
                IEnumConfigurationKeyword configurationKeywords = workspace.ConfigurationKeywords;
                configurationKeywords.Reset();
                for (IConfigurationKeyword keyword2 = configurationKeywords.Next(); keyword2 != null; keyword2 = configurationKeywords.Next())
                {
                    if (keyword2.KeywordType == esriConfigurationKeywordType.esriConfigurationKeywordNetwork)
                    {
                        this.comboBoxEdit.Properties.Items.Add(keyword2.Name);
                    }
                    this.comboBoxEdit.SelectedIndex = 0;
                }
            }
        }

        private void comboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 1)
            {
                BulidGeometryNetworkHelper.BulidGNHelper.ConfigurationKeyword = this.comboBoxEdit.Text;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.radioGroup1 = new RadioGroup();
            this.comboBoxEdit = new ComboBoxEdit();
            this.label1 = new Label();
            this.radioGroup1.Properties.BeginInit();
            this.comboBoxEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.radioGroup1.Location = new Point(0x10, 0x10);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用默认值"), new RadioGroupItem(null, "使用以下关键字") });
            this.radioGroup1.Size = new Size(0xe0, 0x48);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.comboBoxEdit.EditValue = "";
            this.comboBoxEdit.Location = new Point(0x10, 120);
            this.comboBoxEdit.Name = "comboBoxEdit";
            this.comboBoxEdit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit.Size = new Size(0xa8, 0x17);
            this.comboBoxEdit.TabIndex = 1;
            this.comboBoxEdit.SelectedIndexChanged += new EventHandler(this.comboBoxEdit_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x60);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x42, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "配置关键字";
            base.Controls.Add(this.label1);
            base.Controls.Add(this.comboBoxEdit);
            base.Controls.Add(this.radioGroup1);
            base.Name = "BulidGN_Configuration";
            base.Size = new Size(0x110, 0xd8);
            base.Load += new EventHandler(this.BulidGN_Configuration_Load);
            this.radioGroup1.Properties.EndInit();
            this.comboBoxEdit.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                BulidGeometryNetworkHelper.BulidGNHelper.UseDefaultConfigKey = true;
            }
            else
            {
                BulidGeometryNetworkHelper.BulidGNHelper.UseDefaultConfigKey = false;
            }
        }
    }
}


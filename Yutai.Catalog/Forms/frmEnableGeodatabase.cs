using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Controls.Controls;

namespace Yutai.Plugins.Catalog.Forms
{
    public class frmEnableGeodatabase : Form
    {
        private bool m_IsSucccess = false;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        private Label label1;

        private Button btnSelectGDB;

        private TextBox textBox1;

        private TextBox textBox2;

        private Button btnSelectAuoFile;

        private Label label2;

        private Button btnOK;

        private Button btnCancle;

        /// <summary>
        /// 授权文件
        /// </summary>
        public string AuthorizeFileName { get; set; }

        public IGxObject GxObject { get; set; }

        public IWorkspace Workspace { get; set; }

        public frmEnableGeodatabase()
        {
            this.InitializeComponent();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Workspace == null)
            {
                if (this.textBox1.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请选择要转换的空间数据库连接！");
                    return;
                }
            }
            if (this.textBox2.Text.Trim().Length != 0)
            {
                this.GxObject = this.textBox1.Tag as IGxObject;
                this.AuthorizeFileName = this.textBox2.Text;
                EnableEnterpriseGeodatabase enableEnterpriseGeodatabase = new EnableEnterpriseGeodatabase();
                string pathName = "";
                bool flag = false;
                if (this.Workspace != null)
                {
                    pathName = this.Workspace.PathName;
                    if (string.IsNullOrEmpty(pathName))
                    {
                        string startupPath = Application.StartupPath;
                        Guid guid = Guid.NewGuid();
                        string str = string.Concat(guid.ToString(), ".sde");
                        this.Workspace.WorkspaceFactory.Create(startupPath, str, this.Workspace.ConnectionProperties, 0);
                        pathName = Path.Combine(startupPath, str);
                        flag = true;
                    }
                    enableEnterpriseGeodatabase.input_database = pathName;
                }
                else
                {
                    enableEnterpriseGeodatabase.input_database = (this.GxObject as IGxDatabase).WorkspaceName.PathName;
                }
                enableEnterpriseGeodatabase.authorization_file = this.AuthorizeFileName;
                frmGeoProcessorInfo _frmGeoProcessorInfo = new frmGeoProcessorInfo()
                {
                    GeoProcessor = "启用地理数据库"
                };
                _frmGeoProcessorInfo.FormClosing += new FormClosingEventHandler(this.frm_FormClosing);
                base.Visible = false;
                _frmGeoProcessorInfo.Show();
                _frmGeoProcessorInfo.StartTimer(0, -1);
                _frmGeoProcessorInfo.GPProcess = enableEnterpriseGeodatabase;
                this.m_IsSucccess = _frmGeoProcessorInfo.Excute();
                if (flag)
                {
                    File.Delete(pathName);
                }
                if (this.m_IsSucccess)
                {
                    base.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("请选择授权文件！");
            }
        }

        private void btnSelectAuoFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox2.Text = openFileDialog.FileName;
            }
        }

        private void btnSelectGDB_Click(object sender, EventArgs e)
        {
            frmOpenFile _frmOpenFile = new frmOpenFile();
            _frmOpenFile.AddFilter(new MyGxFilterEnteripesGeoDatabases(), true);
            if (_frmOpenFile.DoModalOpen() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Tag = _frmOpenFile.SelectedItems[0];
                this.textBox1.Text = (_frmOpenFile.SelectedItems[0] as IGxObject).FullName;
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if ((!disposing ? false : this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.m_IsSucccess)
            {
                base.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                base.Visible = true;
            }
        }

        private void frmEnableGeodatabase_Load(object sender, EventArgs e)
        {
            if (this.GxObject == null)
            {
                this.textBox1.Visible = false;
                this.label1.Visible = false;
                this.btnSelectGDB.Visible = false;
            }
            else
            {
                this.textBox1.Tag = this.GxObject;
                this.textBox1.Text = this.GxObject.FullName;
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.btnSelectGDB = new Button();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.btnSelectAuoFile = new Button();
            this.label2 = new Label();
            this.btnOK = new Button();
            this.btnCancle = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入数据库连接";
            this.btnSelectGDB.Location = new Point(409, 6);
            this.btnSelectGDB.Name = "btnSelectGDB";
            this.btnSelectGDB.Size = new System.Drawing.Size(37, 23);
            this.btnSelectGDB.TabIndex = 1;
            this.btnSelectGDB.Text = "...";
            this.btnSelectGDB.UseVisualStyleBackColor = true;
            this.btnSelectGDB.Click += new EventHandler(this.btnSelectGDB_Click);
            this.textBox1.Location = new Point(107, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(296, 21);
            this.textBox1.TabIndex = 2;
            this.textBox2.Location = new Point(107, 39);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(296, 21);
            this.textBox2.TabIndex = 5;
            this.btnSelectAuoFile.Location = new Point(409, 39);
            this.btnSelectAuoFile.Name = "btnSelectAuoFile";
            this.btnSelectAuoFile.Size = new System.Drawing.Size(37, 23);
            this.btnSelectAuoFile.TabIndex = 4;
            this.btnSelectAuoFile.Text = "...";
            this.btnSelectAuoFile.UseVisualStyleBackColor = true;
            this.btnSelectAuoFile.Click += new EventHandler(this.btnSelectAuoFile_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "授权文件";
            this.btnOK.Location = new Point(194, 90);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new Point(275, 90);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(455, 134);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.btnSelectAuoFile);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.btnSelectGDB);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmEnableGeodatabase";
            this.Text = "启用地理数据库";
            base.Load += new EventHandler(this.frmEnableGeodatabase_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}
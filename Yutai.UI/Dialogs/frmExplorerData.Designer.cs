namespace Yutai.UI.Dialogs
{
    partial class frmExplorerData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExplorerData));
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpper = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnLarge = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnFolder = new System.Windows.Forms.Button();
            this.btnNewGDB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboShowType = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.txtName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ColumnHeader_0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gisDataComboBox1 = new Yutai.UI.Controls.GISDataComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.cboShowType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader_0,
            this.columnHeader1});
            this.listView1.Location = new System.Drawing.Point(16, 55);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(536, 230);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(463, 291);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 28);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(463, 325);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "查找";
            // 
            // btnUpper
            // 
            this.btnUpper.ImageIndex = 5;
            this.btnUpper.ImageList = this.imageList1;
            this.btnUpper.Location = new System.Drawing.Point(338, 7);
            this.btnUpper.Name = "btnUpper";
            this.btnUpper.Size = new System.Drawing.Size(32, 32);
            this.btnUpper.TabIndex = 5;
            this.btnUpper.UseVisualStyleBackColor = true;
            this.btnUpper.Click += new System.EventHandler(this.btnUpper_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ConnectFolder");
            this.imageList1.Images.SetKeyName(1, "Large");
            this.imageList1.Images.SetKeyName(2, "List");
            this.imageList1.Images.SetKeyName(3, "NewGDB");
            this.imageList1.Images.SetKeyName(4, "Detail.png");
            this.imageList1.Images.SetKeyName(5, "Parent.png");
            // 
            // btnLarge
            // 
            this.btnLarge.ImageIndex = 1;
            this.btnLarge.ImageList = this.imageList1;
            this.btnLarge.Location = new System.Drawing.Point(383, 7);
            this.btnLarge.Name = "btnLarge";
            this.btnLarge.Size = new System.Drawing.Size(32, 32);
            this.btnLarge.TabIndex = 6;
            this.btnLarge.UseVisualStyleBackColor = true;
            this.btnLarge.Click += new System.EventHandler(this.btnLarge_Click);
            // 
            // btnList
            // 
            this.btnList.ImageIndex = 2;
            this.btnList.ImageList = this.imageList1;
            this.btnList.Location = new System.Drawing.Point(414, 7);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(32, 32);
            this.btnList.TabIndex = 7;
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.ImageIndex = 4;
            this.btnDetail.ImageList = this.imageList1;
            this.btnDetail.Location = new System.Drawing.Point(445, 7);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(32, 32);
            this.btnDetail.TabIndex = 8;
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnFolder
            // 
            this.btnFolder.ImageIndex = 0;
            this.btnFolder.ImageList = this.imageList1;
            this.btnFolder.Location = new System.Drawing.Point(477, 7);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(32, 32);
            this.btnFolder.TabIndex = 9;
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // btnNewGDB
            // 
            this.btnNewGDB.ImageIndex = 3;
            this.btnNewGDB.ImageList = this.imageList1;
            this.btnNewGDB.Location = new System.Drawing.Point(511, 7);
            this.btnNewGDB.Name = "btnNewGDB";
            this.btnNewGDB.Size = new System.Drawing.Size(32, 32);
            this.btnNewGDB.TabIndex = 10;
            this.btnNewGDB.UseVisualStyleBackColor = true;
            this.btnNewGDB.Click += new System.EventHandler(this.btnNewGDB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "名字";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "显示类型";
            // 
            // cboShowType
            // 
            this.cboShowType.BackColor = System.Drawing.Color.White;
            this.cboShowType.BeforeTouchSize = new System.Drawing.Size(367, 20);
            this.cboShowType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShowType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboShowType.Location = new System.Drawing.Point(90, 325);
            this.cboShowType.Name = "cboShowType";
            this.cboShowType.Size = new System.Drawing.Size(367, 20);
            this.cboShowType.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.cboShowType.TabIndex = 13;
            this.cboShowType.SelectedIndexChanged += new System.EventHandler(this.cboShowType_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.BeforeTouchSize = new System.Drawing.Size(365, 21);
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.Location = new System.Drawing.Point(91, 295);
            this.txtName.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(365, 21);
            this.txtName.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.txtName.TabIndex = 14;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ColumnHeader_0
            // 
            this.ColumnHeader_0.Text = "名字";
            this.ColumnHeader_0.Width = 214;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "类型";
            this.columnHeader1.Width = 207;
            // 
            // gisDataComboBox1
            // 
            this.gisDataComboBox1.Location = new System.Drawing.Point(50, 12);
            this.gisDataComboBox1.Name = "gisDataComboBox1";
            this.gisDataComboBox1.SelectedIndex = -1;
            this.gisDataComboBox1.Size = new System.Drawing.Size(282, 27);
            this.gisDataComboBox1.TabIndex = 0;
            this.gisDataComboBox1.SelectedItemChanged += new Yutai.UI.Controls.GISDataComboBox.SelectedItemChangedEvent(this.gisDataComboBox1_SelectedItemChanged);
            // 
            // frmExplorerData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 368);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cboShowType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNewGDB);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnLarge);
            this.Controls.Add(this.btnUpper);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.gisDataComboBox1);
            this.Name = "frmExplorerData";
            this.Text = "frmExplorerData";
            this.Load += new System.EventHandler(this.frmExplorerData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboShowType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.GISDataComboBox gisDataComboBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpper;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnLarge;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Button btnNewGDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv cboShowType;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtName;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ColumnHeader ColumnHeader_0;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
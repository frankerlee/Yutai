using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.Library
{
    public class frmCartoConfig : Form
    {
        private Button btnOK;
        private Button btnSetFolder;
        private Button button2;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private GroupBox group;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private IMap imap_0 = null;
        private int int_0 = -1;
        private Label label1;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListView listView1;
        private ListViewItem.ListViewSubItem listViewSubItem_0 = null;
        private TextBox textBox_0;
        private TextBox txtDatabase;
        private TextBox txtFloder;
        private TextBox txtInstance;
        private TextBox txtMapNameField;
        private TextBox txtMapNoFieldName;
        private TextBox txtPassword;
        private TextBox txtServer;
        private TextBox txtUser;

        public frmCartoConfig()
        {
            this.InitializeComponent();
            this.textBox_0 = new TextBox();
            this.listView1.Controls.Add(this.textBox_0);
            this.textBox_0.Visible = false;
            this.textBox_0.Leave += new EventHandler(this.textBox_0_Leave);
            this.textBox_0.KeyPress += new KeyPressEventHandler(this.textBox_0_KeyPress);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        private void btnSetFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                SelectedPath = this.txtFloder.Text
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFloder.Text = dialog.SelectedPath;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCartoConfig_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.txtPassword = new TextBox();
            this.label11 = new Label();
            this.label12 = new Label();
            this.txtUser = new TextBox();
            this.label13 = new Label();
            this.txtInstance = new TextBox();
            this.label14 = new Label();
            this.txtDatabase = new TextBox();
            this.label15 = new Label();
            this.txtServer = new TextBox();
            this.button2 = new Button();
            this.btnOK = new Button();
            this.group = new GroupBox();
            this.btnSetFolder = new Button();
            this.txtFloder = new TextBox();
            this.label4 = new Label();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.label3 = new Label();
            this.txtMapNameField = new TextBox();
            this.label2 = new Label();
            this.txtMapNoFieldName = new TextBox();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.group.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtInstance);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x19f, 120);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SDE数据库配置";
            this.txtPassword.Location = new Point(0x111, 50);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(0x7b, 0x15);
            this.txtPassword.TabIndex = 9;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xd1, 0x3b);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x1d, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "密码";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0xd0, 0x1b);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x29, 12);
            this.label12.TabIndex = 6;
            this.label12.Text = "用户名";
            this.txtUser.Location = new Point(0x111, 0x12);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(0x7b, 0x15);
            this.txtUser.TabIndex = 7;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(15, 0x59);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x1d, 12);
            this.label13.TabIndex = 4;
            this.label13.Text = "实例";
            this.txtInstance.Location = new Point(0x4f, 80);
            this.txtInstance.Name = "txtInstance";
            this.txtInstance.Size = new Size(0x7b, 0x15);
            this.txtInstance.TabIndex = 5;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(14, 0x3e);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x29, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "数据库";
            this.txtDatabase.Location = new Point(0x4f, 0x35);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new Size(0x7b, 0x15);
            this.txtDatabase.TabIndex = 3;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(15, 0x1b);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x29, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "服务器";
            this.txtServer.Location = new Point(0x4f, 0x18);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(0x7b, 0x15);
            this.txtServer.TabIndex = 1;
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0x157, 0x179);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x3d, 0x17);
            this.button2.TabIndex = 0x16;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x114, 0x179);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x3d, 0x17);
            this.btnOK.TabIndex = 0x15;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.group.Controls.Add(this.btnSetFolder);
            this.group.Controls.Add(this.txtFloder);
            this.group.Controls.Add(this.label4);
            this.group.Controls.Add(this.listView1);
            this.group.Controls.Add(this.label3);
            this.group.Controls.Add(this.txtMapNameField);
            this.group.Controls.Add(this.label2);
            this.group.Controls.Add(this.txtMapNoFieldName);
            this.group.Controls.Add(this.label1);
            this.group.Location = new Point(12, 0x8a);
            this.group.Name = "group";
            this.group.Size = new Size(0x19f, 0xe9);
            this.group.TabIndex = 0x17;
            this.group.TabStop = false;
            this.group.Text = "图幅信息表配置";
            this.btnSetFolder.Location = new Point(360, 0xbc);
            this.btnSetFolder.Name = "btnSetFolder";
            this.btnSetFolder.Size = new Size(0x20, 0x17);
            this.btnSetFolder.TabIndex = 0x16;
            this.btnSetFolder.Text = "...";
            this.btnSetFolder.UseVisualStyleBackColor = true;
            this.btnSetFolder.Click += new EventHandler(this.btnSetFolder_Click);
            this.txtFloder.Location = new Point(0x65, 190);
            this.txtFloder.Name = "txtFloder";
            this.txtFloder.ReadOnly = true;
            this.txtFloder.Size = new Size(0xf2, 0x15);
            this.txtFloder.TabIndex = 10;
            this.txtFloder.TextChanged += new EventHandler(this.txtFloder_TextChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 0xc1);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x59, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "图幅模板文件夹";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(0x10, 0x21);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x17f, 0x70);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.MouseDoubleClick += new MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "比例尺";
            this.columnHeader_0.Width = 0x53;
            this.columnHeader_1.Text = "图幅索引表名";
            this.columnHeader_1.Width = 150;
            this.columnHeader_2.Text = "模板文件名";
            this.columnHeader_2.Width = 0x58;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xd1, 0x9e);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "图 名 字 段";
            this.txtMapNameField.Location = new Point(0x11e, 0x9b);
            this.txtMapNameField.Name = "txtMapNameField";
            this.txtMapNameField.Size = new Size(0x71, 0x15);
            this.txtMapNameField.TabIndex = 7;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x9e);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "图幅编号字段";
            this.txtMapNoFieldName.Location = new Point(0x59, 0x9b);
            this.txtMapNoFieldName.Name = "txtMapNoFieldName";
            this.txtMapNoFieldName.Size = new Size(0x71, 0x15);
            this.txtMapNoFieldName.TabIndex = 5;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "图幅信息表名";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1bc, 0x19c);
            base.Controls.Add(this.group);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCartoConfig";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "制图配置";
            base.Load += new EventHandler(this.frmCartoConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            base.ResumeLayout(false);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem itemAt = this.listView1.GetItemAt(e.X, e.Y);
            if (itemAt != null)
            {
                int left = itemAt.Bounds.Left;
                int num2 = 0;
                while (num2 < this.listView1.Columns.Count)
                {
                    left += this.listView1.Columns[num2].Width;
                    if (left > e.X)
                    {
                        left -= this.listView1.Columns[num2].Width;
                        this.listViewSubItem_0 = itemAt.SubItems[num2];
                        this.int_0 = num2;
                        break;
                    }
                    num2++;
                }
                if (this.int_0 != 0)
                {
                    Control control = this.textBox_0;
                    control.Location = new Point(left, this.listView1.GetItemRect(this.listView1.Items.IndexOf(itemAt)).Y);
                    control.Width = this.listView1.Columns[num2].Width;
                    if (control.Width > this.listView1.Width)
                    {
                        control.Width = this.listView1.ClientRectangle.Width;
                    }
                    control.Text = this.listViewSubItem_0.Text;
                    control.Visible = true;
                    control.BringToFront();
                    control.Focus();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.listViewSubItem_0.Text = this.textBox_0.Text;
                this.textBox_0.Visible = false;
            }
        }

        private void textBox_0_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            this.listViewSubItem_0.Text = control.Text;
            control.Visible = false;
        }

        private void txtFloder_TextChanged(object sender, EventArgs e)
        {
        }

        public IMap Map
        {
            get
            {
                return this.imap_0;
            }
            set
            {
                this.imap_0 = value;
            }
        }
    }
}


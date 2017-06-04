using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SQLDMO;

namespace Yutai.ArcGIS.Common.Data
{
	public class frmDatabaseBack : Form
	{
		private TabPage dataBaseSelection;

		private GroupBox groupBox1;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private TabPage tabConnection;

		private Panel panel1;

		private Label label1;

		private Label label4;

		private Button button1;

		private TextBox SQLUserPwd;

		private Label label3;

		private Label label2;

		private TextBox SQLUserName;

		private Panel panel3;

		private GroupBox groupSelection;

		private RadioButton radioButtonSQLRe;

		private RadioButton radioButtonSQLBak;

		private Panel panel4;

		private GroupBox groupBox2;

		private RadioButton radioButtonRe;

		private RadioButton radioButtonBak;

		private Panel panel2;

		private Button buttonConnection;

		private TextBox passWord;

		private TextBox userID;

		private TextBox serverName;

		private Label label5;

		private Label label6;

		private Label label7;

		private TabPage tabOperSelection;

		private TabControl tabControl1;

		private Button bHelp;

		private Button bCancel;

		private TabPage tabImpExp;

		private Panel panelImp;

		private Panel panelExp;

		private Button bexpDir;

		private Label label19;

		private Label label18;

		private Button bExportion;

		private TextBox exportDir;

		private TextBox exportUserID;

		private Label label20;

		private TabPage tabSQLOper;

		private Panel panelSQLBak;

		private Panel panelSQLRe;

		private ComboBox MachineName;

		private ComboBox SQLSDE;

		private GroupBox groupBox4;

		private Button bTabSpaceDir;

		private Label label10;

		private Button btabSpaceDelete;

		private Button btabSpaceCreate;

		private TextBox tabSpaceSize;

		private TextBox tabSpaceDir;

		private TextBox tabSpace;

		private Label label11;

		private Label label12;

		private Label label13;

		private GroupBox groupBox5;

		private Button bimpDir;

		private Button bImportion;

		private Label label14;

		private TextBox toUser;

		private Label label15;

		private TextBox fromUser;

		private Label label16;

		private TextBox importDir;

		private Label label17;

		private GroupBox groupBox3;

		private Button bPassWord;

		private Button bUserID;

		private TextBox createPassWord;

		private TextBox createUserID;

		private Label label9;

		private Label label8;

		private Button button6;

		private GroupBox groupBox6;

		private RadioButton radioButton3;

		private RadioButton radioButton4;

		private Panel panel5;

		private Button button5;

		private Button button2;

		private TextBox textBox1;

		private TextBox textBox2;

		private Label label24;

		private Label label25;

		private Panel panel6;

		private Button button7;

		private TextBox textBox3;

		private Label label26;

		private Button button8;

		private TextBox textBox4;

		private TextBox textBox5;

		private Label label27;

		private Label label28;

		private GroupBox groupBox7;

		private Label label29;

		private Label label30;

		private Button bAddUser;

		private Button button9;

		private TextBox newLogin;

		private TextBox newPwd;

		private System.ComponentModel.Container container_0 = null;

		private ProgressBar progressBar2;

		private Label label31;

		private Button button11;

		private TextBox SQLDataFilePath;

		private CheckBox checkBox1;

		private CheckBox checkBox2;

		private GroupBox groupBox8;

		private ProgressBar progressBar1;

		private Button button4;

		private Button button3;

		private TextBox SQLBakDescript;

		private TextBox SQLBakName;

		private TextBox SQLBAKPath;

		private Label label21;

		private Label label22;

		private Label label23;

		private GroupBox groupBox9;

		private Button button10;

		private TextBox textBox6;

		private CheckBox checkBox3;

		private Button button12;

		private CheckBox checkBox4;

		public Label label32;

		private SQlDBManage sqlDBManage_0 = new SQlDBManage();

		public frmDatabaseBack()
		{
			this.InitializeComponent();
		}

		private void bAddUser_Click(object sender, EventArgs e)
		{
			string text = "";
			string str = "";
			string str1 = "";
			string text1 = "";
			string str2 = "";
			string text2 = "";
			text = this.MachineName.Text;
			str = this.SQLUserName.Text.Trim();
			str1 = this.SQLUserPwd.Text.Trim();
			str2 = this.newLogin.Text.Trim();
			text2 = this.newPwd.Text;
			text1 = this.SQLSDE.Text;
			if (text == "")
			{
				MessageBox.Show("主机名不能为空");
			}
			else if (!(str == null ? false : !(str == "")))
			{
				MessageBox.Show("登录用户名不能为空!");
			}
			else if (!(text1 == null ? false : !(text1 == "")))
			{
				MessageBox.Show("要新建用户的数据库名称不能为空!");
			}
			else if ((str2 == null ? false : !(str2 == "")))
			{
				string[] strArrays = new string[] { "Provider=SQLOLEDB;Data Source=", text, ";Initial Catalog=", text1, ";User Id=", str, ";Password=", str1 };
				OleDbDataAccessLayer oleDbDataAccessLayer = new OleDbDataAccessLayer(string.Concat(strArrays));
				try
				{
					try
					{
						oleDbDataAccessLayer.Open();
						strArrays = new string[] { "BEGIN exec sp_addlogin N'", str2, "', '", text2, "', N'", text1, "', N'简体中文'  EXEC sp_grantdbaccess N'", str2, "', N'", str2, "'  END " };
						string str3 = string.Concat(strArrays);
						oleDbDataAccessLayer.ExecuteNonQuery(str3, new object[0]);
						if (this.checkBox2.Checked)
						{
							string str4 = string.Concat("BEGIN EXEC sp_addrolemember 'db_owner','", str2, "' END");
							oleDbDataAccessLayer.ExecuteNonQuery(str4, new object[0]);
						}
						MessageBox.Show("新增访问数据库用户成功！");
					}
					catch (Exception exception)
					{
						exception.Message.ToString();
						MessageBox.Show("新增访问数据库用户失败！");
						return;
					}
				}
				finally
				{
					oleDbDataAccessLayer.Close();
				}
			}
			else
			{
				MessageBox.Show("新建用户名不能为空");
			}
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void bexpDir_Click(object sender, EventArgs e)
		{
			if (this.exportUserID.Text != "")
			{
				FolderBrowser folderBrowser = new FolderBrowser()
				{
					Description = "请选择目标目录",
					StartLocation = FolderBrowser.fbFolder.Desktop,
					Style = FolderBrowser.fbStyles.BrowseForEverything
				};
				if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					string str = OracleFuctions.isFileOrFolder(folderBrowser.DirectoryPath);
					string str1 = OracleFuctions.addUserList(this.exportUserID.Text.Trim(), str);
					this.exportDir.Text = str1;
				}
			}
			else
			{
				MessageBox.Show("导出Oracle用户列表不能为空");
			}
		}

		private void bExportion_Click(object sender, EventArgs e)
		{
			string str = this.userID.Text.Trim();
			string str1 = this.passWord.Text.Trim();
			string str2 = this.exportDir.Text.Trim();
			string str3 = this.exportUserID.Text.Trim();
			string str4 = this.serverName.Text.Trim();
			if ((str2.Substring(str2.Length - 1, 1) == "\\" ? false : !(str2.Substring(str2.Length - 4, 4).ToLower() != ".dmp")))
			{
				OracleFuctions.ExportUsers(str, str1, str2, str3, str4);
			}
			else
			{
				MessageBox.Show("必须输入完整并且正确的数据库文件名!");
			}
		}

		private void bimpDir_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Filter = "dmp files (*.dmp)|*.dmp"
			};
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.importDir.Text = openFileDialog.FileName;
			}
		}

		private void bImportion_Click(object sender, EventArgs e)
		{
			string str = this.userID.Text.Trim();
			string str1 = this.passWord.Text.Trim();
			string str2 = this.serverName.Text.Trim();
			string str3 = this.importDir.Text.Trim();
			string str4 = this.fromUser.Text.Trim();
			string str5 = this.toUser.Text.Trim();
			if (!(str3 == null ? false : !(str3 == "")))
			{
				MessageBox.Show("导入的数据库文件路径不能为空!");
			}
			else if ((str3.Substring(str3.Length - 1, 1) == "\\" ? false : !(str3.Substring(str3.Length - 4, 4).ToLower() != ".dmp")))
			{
				OracleFuctions.ImportUsers(str, str1, str2, str3, str4, str5);
			}
			else
			{
				MessageBox.Show("必须输入完整并且正确的数据库文件名!");
			}
		}

		private void bPassWord_Click(object sender, EventArgs e)
		{
			string str = this.userID.Text.Trim();
			string str1 = this.passWord.Text.Trim();
			string str2 = this.serverName.Text.Trim();
			string upper = this.createUserID.Text.Trim().ToUpper();
			OracleFuctions.DeleteUsers(str, str1, str2, upper);
		}

		private void btabSpaceCreate_Click(object sender, EventArgs e)
		{
			string str = this.userID.Text.Trim();
			string str1 = this.passWord.Text.Trim();
			string str2 = this.serverName.Text.Trim();
			string upper = this.tabSpace.Text.Trim().ToUpper();
			string str3 = this.tabSpaceDir.Text.Trim();
			string str4 = this.tabSpaceSize.Text.Trim();
			if (!(str == null ? false : !(str == "")))
			{
				MessageBox.Show("用户名不能为空");
			}
			else if (!(str1 == null ? false : !(str1 == "")))
			{
				MessageBox.Show("密码不能为空");
			}
			else if (!(str2 == null ? false : !(str2 == "")))
			{
				MessageBox.Show("Oracle服务名不能为空");
			}
			else if (!(upper == null ? false : !(upper == "")))
			{
				MessageBox.Show("表空间名不能为空");
			}
			else if (!(str3 == null ? false : !(str3 == "")))
			{
				MessageBox.Show("表空间路径不能为空");
			}
			else if ((str4 == null ? false : !(str4 == "")))
			{
				OracleFuctions.CreateTableSpace(str, str1, str2, upper, str3, str4);
			}
			else
			{
				MessageBox.Show("表空间大小不能为空");
			}
		}

		private void btabSpaceDelete_Click(object sender, EventArgs e)
		{
			string str = this.userID.Text.Trim();
			string str1 = this.passWord.Text.Trim();
			string str2 = this.serverName.Text.Trim();
			string upper = this.tabSpace.Text.Trim().ToUpper();
			string str3 = this.tabSpaceDir.Text.Trim();
			OracleFuctions.DeleteTableSpaceFile(str, str1, str2, upper, str3);
		}

		private void bTabSpaceDir_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				if (selectedPath[selectedPath.Length - 1] != '\\')
				{
					selectedPath = string.Concat(selectedPath, '\\');
				}
				this.tabSpaceDir.Text = string.Concat(selectedPath, this.tabSpace.Text.Trim().ToUpper(), ".ORA");
			}
		}

		private void bUserID_Click(object sender, EventArgs e)
		{
			string str = this.userID.Text.Trim();
			string str1 = this.passWord.Text.Trim();
			string str2 = this.serverName.Text.Trim();
			string upper = this.createUserID.Text.Trim().ToUpper();
			string str3 = this.createPassWord.Text.Trim();
			string upper1 = this.tabSpace.Text.Trim().ToUpper();
			OracleFuctions.CreateOracleUsers(str, str1, str2, upper, str3, upper1);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string str = "";
			string str1 = this.SQLUserName.Text.Trim();
			string str2 = this.SQLUserPwd.Text.Trim();
			this.SQLSDE.Text.Trim();
			if (str1 != "")
			{
				str = this.MachineName.Text.Trim();
				if (str != "")
				{
					try
					{
						if (!DbOper.GetMachineSQL(str, str1, str2))
						{
							MessageBox.Show("连接失败");
						}
						else
						{
							MessageBox.Show("连接成功");
						}
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.Message.ToString());
					}
				}
				else
				{
					MessageBox.Show("主机名不能为空");
				}
			}
			else
			{
				MessageBox.Show("用户名不能为空");
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			string text = "";
			string str = "";
			string str1 = "";
			string str2 = "";
			bool @checked = true;
			bool flag = false;
			string str3 = "";
			this.label32.Text = "正在执行操作，请稍候...";
			text = this.MachineName.Text;
			str = this.SQLUserName.Text.Trim();
			str1 = this.SQLUserPwd.Text.Trim();
			this.newLogin.Text.Trim();
			@checked = this.checkBox4.Checked;
			flag = this.checkBox3.Checked;
			str3 = this.textBox6.Text.Trim();
			str2 = this.SQLSDE.Text.Trim();
			if (text == "")
			{
				MessageBox.Show("主机名不能为空");
			}
			else if (!(str == null ? false : !(str == "")))
			{
				MessageBox.Show("登录用户名不能为空!");
			}
			else if ((str2 == null ? false : !(str2 == "")))
			{
				string[] strArrays = new string[] { "Provider=SQLOLEDB;Data Source=", text, ";User Id=", str, ";Password=", str1 };
				string str4 = string.Concat(strArrays);
				if (!this.sqlDBManage_0.detachDB(str4, str2, @checked, flag, str3))
				{
					MessageBox.Show("数据库分离失败");
				}
				else
				{
					MessageBox.Show("数据库分离成功");
				}
				this.label32.Text = "";
			}
			else
			{
				MessageBox.Show("数据库的名称不能为空!");
			}
		}

		private void button11_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				if (selectedPath.Substring(selectedPath.Length - 1) != "\\")
				{
					selectedPath = string.Concat(selectedPath, "\\");
				}
				this.SQLDataFilePath.Text = selectedPath.Trim();
			}
		}

		private void button12_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				if (selectedPath.Substring(selectedPath.Length - 1) != "\\")
				{
					selectedPath = string.Concat(selectedPath, "\\");
				}
				this.textBox6.Text = selectedPath.Trim();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			FolderBrowser folderBrowser = new FolderBrowser()
			{
				Description = "请选择目标目录",
				StartLocation = FolderBrowser.fbFolder.Desktop,
				Style = FolderBrowser.fbStyles.BrowseForEverything
			};
			if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.textBox1.Text = folderBrowser.DirectoryPath;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			FolderBrowser folderBrowser = new FolderBrowser()
			{
				Description = "请选择目标目录",
				StartLocation = FolderBrowser.fbFolder.Desktop,
				Style = FolderBrowser.fbStyles.BrowseForEverything
			};
			if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string str = DbOper.isFileOrFolder(folderBrowser.DirectoryPath, this.SQLSDE.Text.Trim());
				this.SQLBAKPath.Text = str;
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			str = this.MachineName.Text.Trim();
			str1 = this.SQLUserName.Text.Trim();
			this.SQLUserPwd.Text.Trim();
			str2 = this.SQLSDE.Text.Trim();
			str3 = this.SQLBAKPath.Text.Trim();
			this.SQLBakName.Text.Trim();
			this.SQLBakDescript.Text.Trim();
			this.sqlDBManage_0.ServerName = this.MachineName.Text;
			this.sqlDBManage_0.UserName = this.SQLUserName.Text.Trim();
			this.sqlDBManage_0.Password = this.SQLUserPwd.Text.Trim();
			if (!(str == null ? false : !(str == "")))
			{
				MessageBox.Show("主机名不能为空!");
			}
			else if (!(str1 == null ? false : !(str1 == "")))
			{
				MessageBox.Show("登录用户名不能为空!");
			}
			else if (!(str2 == null ? false : !(str2 == "")))
			{
				MessageBox.Show("备份数据库名称不能为空!");
			}
			else if (!(str3 == null ? false : !(str3 == "")))
			{
				MessageBox.Show("数据库备份的路径不能为空!");
			}
			else if (!(str3.Substring(str3.Length - 1, 1) == "\\" ? false : !(str3.Substring(str3.Length - 4, 4).ToLower() != ".bak")))
			{
				MessageBox.Show("必须输入完整并且正确的备份文件名!");
			}
			else if (MessageBox.Show("你确认进行数据库备份吗?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
			{
				this.progressBar1.Visible = true;
				if (!this.sqlDBManage_0.BackUPDB(str2, str3, this.progressBar1))
				{
					MessageBox.Show("数据库备份失败!");
					this.progressBar1.Visible = false;
				}
				else
				{
					MessageBox.Show("数据库备份成功!");
					this.progressBar1.Visible = false;
				}
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			string[] strArrays;
			string str;
			OleDbDataAccessLayer oleDbDataAccessLayer;
			string text = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			string str4 = "";
			string str5 = "";
			string sQLHome = "";
			if (!this.radioButton3.Checked)
			{
				text = this.MachineName.Text.Trim();
				str1 = this.SQLUserName.Text.Trim();
				str2 = this.SQLUserPwd.Text.Trim();
				str3 = this.textBox5.Text.Trim();
				str4 = this.textBox4.Text.Trim();
				str5 = this.textBox3.Text.Trim();
				if (!(text == null ? false : !(text == "")))
				{
					MessageBox.Show("主机名不能为空");
				}
				else if (!(str1 == null ? false : !(str1 == "")))
				{
					MessageBox.Show("登录用户名不能为空!");
				}
				else if (!(str3 == null ? false : !(str3 == "")))
				{
					MessageBox.Show("附加数据库的名称不能为空!");
				}
				else if (!(str4 == null ? false : !(str4 == "")))
				{
					MessageBox.Show("数据库附加的路径不能为空!");
				}
				else if (!(str5 == null ? false : !(str5 == "")))
				{
					MessageBox.Show("数据库附加日志文件的路径不能为空!");
				}
				else if (!(str4.Substring(str4.Length - 1, 1) == "\\" ? false : !(str4.Substring(str4.Length - 4, 4).ToLower() != ".mdf")))
				{
					MessageBox.Show("必须输入完整并且正确的备份文件名!");
				}
				else if ((str5.Substring(str5.Length - 1, 1) == "\\" ? false : !(str5.Substring(str5.Length - 4, 4).ToLower() != ".ldf")))
				{
					strArrays = new string[] { "server=", text, ";User Id=", str1, ";Password=", str2 };
					str = string.Concat(strArrays);
					if (MessageBox.Show("你确认进行数据库附加吗?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						if (!this.method_0(str, str3, str4, str5))
						{
							MessageBox.Show("数据库附加失败!");
						}
						else
						{
							MessageBox.Show("数据库附加成功!");
							if (str3.Trim().ToUpper() == "SDE")
							{
								strArrays = new string[] { "Provider=SQLOLEDB;Data Source=", text, ";Initial Catalog='master';User Id=", str1, ";Password=", str2 };
								str = string.Concat(strArrays);
								oleDbDataAccessLayer = new OleDbDataAccessLayer(str);
								try
								{
									try
									{
										oleDbDataAccessLayer.Open();
										oleDbDataAccessLayer.ExecuteNonQuery("BEGIN exec sp_addlogin  'sde','go','sde' END ", new object[0]);
										MessageBox.Show("恢复SDE数据库添加SDE登录用户成功！");
									}
									catch (Exception exception)
									{
									}
								}
								finally
								{
									oleDbDataAccessLayer.Close();
								}
								strArrays = new string[] { "Provider=SQLOLEDB;Data Source=", text, ";Initial Catalog='sde';User Id=", str1, ";Password=", str2 };
								str = string.Concat(strArrays);
								oleDbDataAccessLayer = new OleDbDataAccessLayer(str);
								try
								{
									try
									{
										oleDbDataAccessLayer.Open();
										oleDbDataAccessLayer.ExecuteNonQuery("BEGIN exec sp_change_users_login 'update_one','sde','sde'  END ", new object[0]);
										MessageBox.Show("恢复SDE数据库以后解决SID不一致问题成功！");
									}
									catch (Exception exception1)
									{
									}
								}
								finally
								{
									oleDbDataAccessLayer.Close();
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show("必须输入完整并且正确的备份文件名!");
				}
			}
			else
			{
				text = this.MachineName.Text;
				str1 = this.SQLUserName.Text.Trim();
				str2 = this.SQLUserPwd.Text.Trim();
				str3 = this.textBox2.Text.Trim();
				str4 = this.textBox1.Text.Trim();
				this.SQLBakName.Text.Trim();
				this.SQLBakDescript.Text.Trim();
				sQLHome = SQlDBManage.getSQLHome();
				if (!this.checkBox1.Checked)
				{
					sQLHome = this.SQLDataFilePath.Text.Trim();
				}
				this.sqlDBManage_0.ServerName = this.MachineName.Text;
				this.sqlDBManage_0.UserName = this.SQLUserName.Text.Trim();
				this.sqlDBManage_0.Password = this.SQLUserPwd.Text.Trim();
				if (text == "")
				{
					MessageBox.Show("主机名不能为空");
				}
				else if (!(str1 == null ? false : !(str1 == "")))
				{
					MessageBox.Show("登录用户名不能为空!");
				}
				else if (!(str3 == null ? false : !(str3 == "")))
				{
					MessageBox.Show("还原数据库的名称不能为空!");
				}
				else if (!(str4 == null || str4 == "" ? false : !this.sqlDBManage_0.hasBlack(str4)))
				{
					MessageBox.Show("要还原数据库备份文件的路径不能为空或含有空格!");
				}
				else if (!(str4 == null ? false : !(str4 == "")))
				{
					MessageBox.Show("要还原数据库备份文件的路径不能为空!");
				}
				else if ((str4.Substring(str4.Length - 1, 1) == "\\" ? false : !(str4.Substring(str4.Length - 4, 4).ToLower() != ".bak")))
				{
					if (!this.checkBox1.Checked)
					{
						if ((sQLHome == null || sQLHome == "" ? false : !this.sqlDBManage_0.hasBlack(sQLHome)))
						{
							goto Label1;
						}
						MessageBox.Show("数据存放路径不能为空或含有空格!");
						return;
					}
				Label1:
					if (MessageBox.Show("你确认进行数据库恢复吗?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						this.progressBar2.Visible = true;
						if (!this.sqlDBManage_0.RestoreDB(str3, str4, sQLHome, this.progressBar2))
						{
							MessageBox.Show("数据库恢复失败!");
							this.progressBar2.Visible = false;
						}
						else
						{
							MessageBox.Show("数据库恢复成功!");
							this.progressBar2.Visible = false;
							if (str3.Trim().ToUpper() == "SDE")
							{
								strArrays = new string[] { "Provider=SQLOLEDB;Data Source=", text, ";Initial Catalog='master';User Id=", str1, ";Password=", str2 };
								str = string.Concat(strArrays);
								oleDbDataAccessLayer = new OleDbDataAccessLayer(str);
								try
								{
									try
									{
										oleDbDataAccessLayer.Open();
										oleDbDataAccessLayer.ExecuteNonQuery("BEGIN exec sp_addlogin  'sde','go','sde' END ", new object[0]);
										MessageBox.Show("恢复SDE数据库添加SDE登录用户成功！");
									}
									catch (Exception exception2)
									{
									}
								}
								finally
								{
									oleDbDataAccessLayer.Close();
								}
								strArrays = new string[] { "Provider=SQLOLEDB;Data Source=", text, ";Initial Catalog='sde';User Id=", str1, ";Password=", str2 };
								str = string.Concat(strArrays);
								oleDbDataAccessLayer = new OleDbDataAccessLayer(str);
								try
								{
									try
									{
										oleDbDataAccessLayer.Open();
										oleDbDataAccessLayer.ExecuteNonQuery("BEGIN exec sp_change_users_login 'update_one','sde','sde'  END ", new object[0]);
										MessageBox.Show("恢复SDE数据库以后解决SID不一致问题成功！");
									}
									catch (Exception exception3)
									{
									}
								}
								finally
								{
									oleDbDataAccessLayer.Close();
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show("必须输入完整并且正确的备份文件名!");
				}
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			try
			{
				this.MachineName.Items.Clear();
			}
			catch
			{
			}
			try
			{
				this.MachineName.DataSource = this.sqlDBManage_0.GetServerList();
				MessageBox.Show("刷新成功");
			}
			catch
			{
				this.MachineName.Text = "(local)";
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			FolderBrowser folderBrowser = new FolderBrowser()
			{
				Description = "请选择目标目录",
				StartLocation = FolderBrowser.fbFolder.Desktop,
				Style = FolderBrowser.fbStyles.BrowseForEverything
			};
			if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.textBox3.Text = folderBrowser.DirectoryPath;
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			FolderBrowser folderBrowser = new FolderBrowser()
			{
				Description = "请选择目标目录",
				StartLocation = FolderBrowser.fbFolder.Desktop,
				Style = FolderBrowser.fbStyles.BrowseForEverything
			};
			if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.textBox4.Text = folderBrowser.DirectoryPath;
			}
		}

		private void button9_Click(object sender, EventArgs e)
		{
			string text = "";
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			text = this.MachineName.Text;
			str = this.SQLUserName.Text.Trim();
			str1 = this.SQLUserPwd.Text.Trim();
			str3 = this.newLogin.Text.Trim();
			str2 = (!this.radioButton3.Checked ? this.textBox5.Text.Trim() : this.textBox2.Text.Trim());
			if (text == "")
			{
				MessageBox.Show("主机名不能为空");
			}
			else if (!(str == null ? false : !(str == "")))
			{
				MessageBox.Show("登录用户名不能为空!");
			}
			else if (!(str2 == null ? false : !(str2 == "")))
			{
				MessageBox.Show("数据库的名称不能为空!");
			}
			else if ((str3 == null ? false : !(str3 == "")))
			{
				string[] strArrays = new string[] { "Provider=SQLOLEDB;Data Source=", text, ";Initial Catalog=", str2, ";User Id=", str, ";Password=", str1 };
				OleDbDataAccessLayer oleDbDataAccessLayer = new OleDbDataAccessLayer(string.Concat(strArrays));
				try
				{
					try
					{
						oleDbDataAccessLayer.Open();
						strArrays = new string[] { "BEGIN exec  sp_revokedbaccess N'", str3, "'  exec sp_droplogin N'", str3, "' END " };
						string str4 = string.Concat(strArrays);
						oleDbDataAccessLayer.ExecuteNonQuery(str4, new object[0]);
						MessageBox.Show("删除访问数据库用户成功！");
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.Message.ToString());
						return;
					}
				}
				finally
				{
					oleDbDataAccessLayer.Close();
				}
			}
			else
			{
				MessageBox.Show("被删除的用户名不能为空!");
			}
		}

		private void buttonConnection_Click(object sender, EventArgs e)
		{
			string str = "";
			string str1 = "";
			string str2 = "";
			str = this.userID.Text.Trim();
			str1 = this.passWord.Text.Trim();
			str2 = this.serverName.Text.Trim();
			OracleFuctions.ConnectTest(str2, str, str1);
		}

		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void frmDatabaseBack_Load(object sender, EventArgs e)
		{
			this.tabControl1.TabPages.Remove(this.tabImpExp);
			this.tabControl1.TabPages.Remove(this.tabSQLOper);
			this.radioButton1.Checked = true;
			this.tabControl1.SelectedTab = this.dataBaseSelection;
			this.MachineName.Items.Add("(local)");
			this.MachineName.SelectedIndex = 0;
			this.SQLUserPwd.PasswordChar = '*';
			this.passWord.PasswordChar = '*';
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmDatabaseBack));
			this.tabControl1 = new TabControl();
			this.dataBaseSelection = new TabPage();
			this.groupBox1 = new GroupBox();
			this.radioButton2 = new RadioButton();
			this.radioButton1 = new RadioButton();
			this.tabConnection = new TabPage();
			this.panel1 = new Panel();
			this.button6 = new Button();
			this.SQLSDE = new ComboBox();
			this.MachineName = new ComboBox();
			this.label1 = new Label();
			this.label4 = new Label();
			this.button1 = new Button();
			this.SQLUserPwd = new TextBox();
			this.label3 = new Label();
			this.label2 = new Label();
			this.SQLUserName = new TextBox();
			this.panel2 = new Panel();
			this.buttonConnection = new Button();
			this.passWord = new TextBox();
			this.userID = new TextBox();
			this.serverName = new TextBox();
			this.label5 = new Label();
			this.label6 = new Label();
			this.label7 = new Label();
			this.tabOperSelection = new TabPage();
			this.panel3 = new Panel();
			this.groupSelection = new GroupBox();
			this.radioButtonSQLRe = new RadioButton();
			this.radioButtonSQLBak = new RadioButton();
			this.panel4 = new Panel();
			this.groupBox2 = new GroupBox();
			this.radioButtonRe = new RadioButton();
			this.radioButtonBak = new RadioButton();
			this.tabSQLOper = new TabPage();
			this.panelSQLBak = new Panel();
			this.groupBox9 = new GroupBox();
			this.label32 = new Label();
			this.checkBox4 = new CheckBox();
			this.button12 = new Button();
			this.checkBox3 = new CheckBox();
			this.textBox6 = new TextBox();
			this.button10 = new Button();
			this.groupBox8 = new GroupBox();
			this.progressBar1 = new ProgressBar();
			this.button4 = new Button();
			this.button3 = new Button();
			this.SQLBakDescript = new TextBox();
			this.SQLBakName = new TextBox();
			this.SQLBAKPath = new TextBox();
			this.label21 = new Label();
			this.label22 = new Label();
			this.label23 = new Label();
			this.panelSQLRe = new Panel();
			this.checkBox1 = new CheckBox();
			this.progressBar2 = new ProgressBar();
			this.groupBox6 = new GroupBox();
			this.radioButton4 = new RadioButton();
			this.radioButton3 = new RadioButton();
			this.button5 = new Button();
			this.groupBox7 = new GroupBox();
			this.checkBox2 = new CheckBox();
			this.button9 = new Button();
			this.bAddUser = new Button();
			this.newPwd = new TextBox();
			this.newLogin = new TextBox();
			this.label30 = new Label();
			this.label29 = new Label();
			this.panel5 = new Panel();
			this.button11 = new Button();
			this.label31 = new Label();
			this.SQLDataFilePath = new TextBox();
			this.button2 = new Button();
			this.textBox1 = new TextBox();
			this.textBox2 = new TextBox();
			this.label24 = new Label();
			this.label25 = new Label();
			this.panel6 = new Panel();
			this.button7 = new Button();
			this.textBox3 = new TextBox();
			this.label26 = new Label();
			this.button8 = new Button();
			this.textBox4 = new TextBox();
			this.textBox5 = new TextBox();
			this.label27 = new Label();
			this.label28 = new Label();
			this.tabImpExp = new TabPage();
			this.panelImp = new Panel();
			this.groupBox3 = new GroupBox();
			this.bPassWord = new Button();
			this.bUserID = new Button();
			this.createPassWord = new TextBox();
			this.createUserID = new TextBox();
			this.label9 = new Label();
			this.label8 = new Label();
			this.groupBox4 = new GroupBox();
			this.bTabSpaceDir = new Button();
			this.label10 = new Label();
			this.btabSpaceDelete = new Button();
			this.btabSpaceCreate = new Button();
			this.tabSpaceSize = new TextBox();
			this.tabSpaceDir = new TextBox();
			this.tabSpace = new TextBox();
			this.label11 = new Label();
			this.label12 = new Label();
			this.label13 = new Label();
			this.groupBox5 = new GroupBox();
			this.bimpDir = new Button();
			this.bImportion = new Button();
			this.label14 = new Label();
			this.toUser = new TextBox();
			this.label15 = new Label();
			this.fromUser = new TextBox();
			this.label16 = new Label();
			this.importDir = new TextBox();
			this.label17 = new Label();
			this.panelExp = new Panel();
			this.bexpDir = new Button();
			this.label19 = new Label();
			this.label18 = new Label();
			this.bExportion = new Button();
			this.exportDir = new TextBox();
			this.exportUserID = new TextBox();
			this.label20 = new Label();
			this.bHelp = new Button();
			this.bCancel = new Button();
			this.tabControl1.SuspendLayout();
			this.dataBaseSelection.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabConnection.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabOperSelection.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupSelection.SuspendLayout();
			this.panel4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabSQLOper.SuspendLayout();
			this.panelSQLBak.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.panelSQLRe.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel6.SuspendLayout();
			this.tabImpExp.SuspendLayout();
			this.panelImp.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.panelExp.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.dataBaseSelection);
			this.tabControl1.Controls.Add(this.tabConnection);
			this.tabControl1.Controls.Add(this.tabOperSelection);
			this.tabControl1.Controls.Add(this.tabSQLOper);
			this.tabControl1.Controls.Add(this.tabImpExp);
			this.tabControl1.Location = new Point(5, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(355, 368);
			this.tabControl1.TabIndex = 0;
			this.dataBaseSelection.Controls.Add(this.groupBox1);
			this.dataBaseSelection.Location = new Point(4, 21);
			this.dataBaseSelection.Name = "dataBaseSelection";
			this.dataBaseSelection.Size = new System.Drawing.Size(347, 343);
			this.dataBaseSelection.TabIndex = 0;
			this.dataBaseSelection.Text = "数据库选择";
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new Point(60, 60);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(228, 202);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "数据库选择";
			this.radioButton2.Location = new Point(48, 120);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(104, 24);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "Oracle数据库";
			this.radioButton1.Location = new Point(48, 56);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(128, 24);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.Text = "SQLSERVER数据库";
			this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
			this.tabConnection.Controls.Add(this.panel1);
			this.tabConnection.Controls.Add(this.panel2);
			this.tabConnection.Location = new Point(4, 21);
			this.tabConnection.Name = "tabConnection";
			this.tabConnection.Size = new System.Drawing.Size(347, 343);
			this.tabConnection.TabIndex = 1;
			this.tabConnection.Text = "数据库连接";
			this.panel1.Controls.Add(this.button6);
			this.panel1.Controls.Add(this.SQLSDE);
			this.panel1.Controls.Add(this.MachineName);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.SQLUserPwd);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.SQLUserName);
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(352, 344);
			this.panel1.TabIndex = 0;
			this.button6.Location = new Point(281, 54);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(40, 23);
			this.button6.TabIndex = 49;
			this.button6.Text = "刷新";
			this.button6.Click += new EventHandler(this.button6_Click);
			this.SQLSDE.Location = new Point(104, 200);
			this.SQLSDE.Name = "SQLSDE";
			this.SQLSDE.Size = new System.Drawing.Size(176, 20);
			this.SQLSDE.TabIndex = 48;
			this.SQLSDE.DropDown += new EventHandler(this.SQLSDE_DropDown);
			this.MachineName.Location = new Point(104, 56);
			this.MachineName.Name = "MachineName";
			this.MachineName.Size = new System.Drawing.Size(176, 20);
			this.MachineName.TabIndex = 47;
			this.label1.Location = new Point(32, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 21);
			this.label1.TabIndex = 41;
			this.label1.Text = "计算机名称";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.label4.Location = new Point(32, 200);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 21);
			this.label4.TabIndex = 46;
			this.label4.Text = "数据库名称";
			this.label4.TextAlign = ContentAlignment.MiddleLeft;
			this.button1.Location = new Point(184, 256);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 23);
			this.button1.TabIndex = 40;
			this.button1.Text = "连接测试";
			this.button1.Click += new EventHandler(this.button1_Click);
			this.SQLUserPwd.BorderStyle = BorderStyle.FixedSingle;
			this.SQLUserPwd.Location = new Point(104, 152);
			this.SQLUserPwd.Name = "SQLUserPwd";
			this.SQLUserPwd.Size = new System.Drawing.Size(176, 21);
			this.SQLUserPwd.TabIndex = 44;
			this.label3.Location = new Point(32, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 21);
			this.label3.TabIndex = 45;
			this.label3.Text = "密码";
			this.label3.TextAlign = ContentAlignment.MiddleLeft;
			this.label2.Location = new Point(32, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 21);
			this.label2.TabIndex = 43;
			this.label2.Text = "用户名";
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.SQLUserName.BorderStyle = BorderStyle.FixedSingle;
			this.SQLUserName.Location = new Point(104, 104);
			this.SQLUserName.Name = "SQLUserName";
			this.SQLUserName.Size = new System.Drawing.Size(176, 21);
			this.SQLUserName.TabIndex = 42;
			this.panel2.Controls.Add(this.buttonConnection);
			this.panel2.Controls.Add(this.passWord);
			this.panel2.Controls.Add(this.userID);
			this.panel2.Controls.Add(this.serverName);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Location = new Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(352, 344);
			this.panel2.TabIndex = 1;
			this.buttonConnection.Location = new Point(200, 232);
			this.buttonConnection.Name = "buttonConnection";
			this.buttonConnection.Size = new System.Drawing.Size(96, 23);
			this.buttonConnection.TabIndex = 13;
			this.buttonConnection.Text = "连接测试";
			this.buttonConnection.Click += new EventHandler(this.buttonConnection_Click);
			this.passWord.Location = new Point(120, 184);
			this.passWord.Name = "passWord";
			this.passWord.Size = new System.Drawing.Size(176, 21);
			this.passWord.TabIndex = 12;
			this.userID.Location = new Point(120, 136);
			this.userID.Name = "userID";
			this.userID.Size = new System.Drawing.Size(176, 21);
			this.userID.TabIndex = 11;
			this.serverName.Location = new Point(120, 88);
			this.serverName.Name = "serverName";
			this.serverName.Size = new System.Drawing.Size(176, 21);
			this.serverName.TabIndex = 10;
			this.serverName.Leave += new EventHandler(this.serverName_Leave);
			this.label5.Location = new Point(32, 184);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 21);
			this.label5.TabIndex = 9;
			this.label5.Text = "密码:";
			this.label5.TextAlign = ContentAlignment.MiddleLeft;
			this.label6.Location = new Point(32, 136);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(88, 21);
			this.label6.TabIndex = 8;
			this.label6.Text = "用户名:";
			this.label6.TextAlign = ContentAlignment.MiddleLeft;
			this.label7.Location = new Point(32, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 21);
			this.label7.TabIndex = 7;
			this.label7.Text = "Oracle服务名:";
			this.label7.TextAlign = ContentAlignment.MiddleLeft;
			this.tabOperSelection.Controls.Add(this.panel3);
			this.tabOperSelection.Controls.Add(this.panel4);
			this.tabOperSelection.Location = new Point(4, 21);
			this.tabOperSelection.Name = "tabOperSelection";
			this.tabOperSelection.Size = new System.Drawing.Size(347, 343);
			this.tabOperSelection.TabIndex = 2;
			this.tabOperSelection.Text = "备份\\恢复选择";
			this.panel3.Controls.Add(this.groupSelection);
			this.panel3.Location = new Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(352, 344);
			this.panel3.TabIndex = 0;
			this.groupSelection.Controls.Add(this.radioButtonSQLRe);
			this.groupSelection.Controls.Add(this.radioButtonSQLBak);
			this.groupSelection.Location = new Point(40, 72);
			this.groupSelection.Name = "groupSelection";
			this.groupSelection.Size = new System.Drawing.Size(268, 176);
			this.groupSelection.TabIndex = 2;
			this.groupSelection.TabStop = false;
			this.groupSelection.Text = "操作方式选择";
			this.radioButtonSQLRe.Location = new Point(32, 104);
			this.radioButtonSQLRe.Name = "radioButtonSQLRe";
			this.radioButtonSQLRe.Size = new System.Drawing.Size(216, 24);
			this.radioButtonSQLRe.TabIndex = 1;
			this.radioButtonSQLRe.Text = "SQLServer数据恢复及登录用户设置";
			this.radioButtonSQLBak.Location = new Point(32, 48);
			this.radioButtonSQLBak.Name = "radioButtonSQLBak";
			this.radioButtonSQLBak.Size = new System.Drawing.Size(216, 24);
			this.radioButtonSQLBak.TabIndex = 0;
			this.radioButtonSQLBak.Text = "SQLServer数据备份及数据库分离";
			this.radioButtonSQLBak.CheckedChanged += new EventHandler(this.radioButtonSQLBak_CheckedChanged);
			this.panel4.Controls.Add(this.groupBox2);
			this.panel4.Location = new Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(352, 344);
			this.panel4.TabIndex = 1;
			this.groupBox2.Controls.Add(this.radioButtonRe);
			this.groupBox2.Controls.Add(this.radioButtonBak);
			this.groupBox2.Location = new Point(40, 72);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(268, 167);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "操作方式选择";
			this.radioButtonRe.Location = new Point(72, 104);
			this.radioButtonRe.Name = "radioButtonRe";
			this.radioButtonRe.Size = new System.Drawing.Size(128, 24);
			this.radioButtonRe.TabIndex = 1;
			this.radioButtonRe.Text = "Oracle数据恢复";
			this.radioButtonBak.Location = new Point(72, 48);
			this.radioButtonBak.Name = "radioButtonBak";
			this.radioButtonBak.Size = new System.Drawing.Size(128, 24);
			this.radioButtonBak.TabIndex = 0;
			this.radioButtonBak.Text = "Oracle数据备份";
			this.radioButtonBak.CheckedChanged += new EventHandler(this.radioButtonBak_CheckedChanged);
			this.tabSQLOper.Controls.Add(this.panelSQLBak);
			this.tabSQLOper.Controls.Add(this.panelSQLRe);
			this.tabSQLOper.Location = new Point(4, 21);
			this.tabSQLOper.Name = "tabSQLOper";
			this.tabSQLOper.Size = new System.Drawing.Size(347, 343);
			this.tabSQLOper.TabIndex = 5;
			this.tabSQLOper.Text = "SQL备份\\恢复配置";
			this.panelSQLBak.Controls.Add(this.groupBox9);
			this.panelSQLBak.Controls.Add(this.groupBox8);
			this.panelSQLBak.Location = new Point(0, 0);
			this.panelSQLBak.Name = "panelSQLBak";
			this.panelSQLBak.Size = new System.Drawing.Size(352, 344);
			this.panelSQLBak.TabIndex = 0;
			this.groupBox9.Controls.Add(this.label32);
			this.groupBox9.Controls.Add(this.checkBox4);
			this.groupBox9.Controls.Add(this.button12);
			this.groupBox9.Controls.Add(this.checkBox3);
			this.groupBox9.Controls.Add(this.textBox6);
			this.groupBox9.Controls.Add(this.button10);
			this.groupBox9.Location = new Point(8, 192);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(332, 144);
			this.groupBox9.TabIndex = 35;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "数据库分离";
			this.label32.Location = new Point(24, 112);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(288, 23);
			this.label32.TabIndex = 42;
			this.label32.TextAlign = ContentAlignment.MiddleCenter;
			this.checkBox4.Location = new Point(24, 24);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(128, 24);
			this.checkBox4.TabIndex = 41;
			this.checkBox4.Text = "保留数据文件";
			this.button12.FlatStyle = FlatStyle.Popup;
			this.button12.Location = new Point(296, 72);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(24, 21);
			this.button12.TabIndex = 40;
			this.button12.Text = ">";
			this.button12.Click += new EventHandler(this.button12_Click);
			this.checkBox3.Location = new Point(24, 48);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(128, 24);
			this.checkBox3.TabIndex = 36;
			this.checkBox3.Text = "重设数据文件路径";
			this.textBox6.Location = new Point(24, 72);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(272, 21);
			this.textBox6.TabIndex = 35;
			this.button10.FlatStyle = FlatStyle.Popup;
			this.button10.Location = new Point(208, 24);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(104, 23);
			this.button10.TabIndex = 34;
			this.button10.Text = "分离数据库";
			this.button10.Click += new EventHandler(this.button10_Click);
			this.groupBox8.Controls.Add(this.progressBar1);
			this.groupBox8.Controls.Add(this.button4);
			this.groupBox8.Controls.Add(this.button3);
			this.groupBox8.Controls.Add(this.SQLBakDescript);
			this.groupBox8.Controls.Add(this.SQLBakName);
			this.groupBox8.Controls.Add(this.SQLBAKPath);
			this.groupBox8.Controls.Add(this.label21);
			this.groupBox8.Controls.Add(this.label22);
			this.groupBox8.Controls.Add(this.label23);
			this.groupBox8.Location = new Point(8, 3);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(332, 181);
			this.groupBox8.TabIndex = 34;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "数据库备份";
			this.progressBar1.Location = new Point(16, 144);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(184, 23);
			this.progressBar1.TabIndex = 41;
			this.progressBar1.Visible = false;
			this.button4.FlatStyle = FlatStyle.Popup;
			this.button4.Location = new Point(208, 144);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(104, 23);
			this.button4.TabIndex = 40;
			this.button4.Text = "执行备份";
			this.button4.Click += new EventHandler(this.button4_Click);
			this.button3.FlatStyle = FlatStyle.Popup;
			this.button3.Location = new Point(298, 33);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(24, 21);
			this.button3.TabIndex = 39;
			this.button3.Text = ">";
			this.button3.Click += new EventHandler(this.button3_Click);
			this.SQLBakDescript.BorderStyle = BorderStyle.FixedSingle;
			this.SQLBakDescript.Location = new Point(98, 112);
			this.SQLBakDescript.Name = "SQLBakDescript";
			this.SQLBakDescript.Size = new System.Drawing.Size(200, 21);
			this.SQLBakDescript.TabIndex = 38;
			this.SQLBakName.BorderStyle = BorderStyle.FixedSingle;
			this.SQLBakName.Location = new Point(98, 72);
			this.SQLBakName.Name = "SQLBakName";
			this.SQLBakName.Size = new System.Drawing.Size(200, 21);
			this.SQLBakName.TabIndex = 37;
			this.SQLBAKPath.BorderStyle = BorderStyle.FixedSingle;
			this.SQLBAKPath.Location = new Point(98, 33);
			this.SQLBAKPath.Name = "SQLBAKPath";
			this.SQLBAKPath.Size = new System.Drawing.Size(200, 21);
			this.SQLBAKPath.TabIndex = 34;
			this.label21.Location = new Point(10, 112);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(72, 23);
			this.label21.TabIndex = 36;
			this.label21.Text = "相关描述";
			this.label21.TextAlign = ContentAlignment.MiddleLeft;
			this.label22.Location = new Point(10, 72);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(72, 23);
			this.label22.TabIndex = 35;
			this.label22.Text = "执行名称";
			this.label22.TextAlign = ContentAlignment.MiddleLeft;
			this.label23.Location = new Point(10, 33);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(80, 32);
			this.label23.TabIndex = 33;
			this.label23.Text = "备份文件名（含路径）";
			this.label23.TextAlign = ContentAlignment.MiddleLeft;
			this.panelSQLRe.Controls.Add(this.checkBox1);
			this.panelSQLRe.Controls.Add(this.progressBar2);
			this.panelSQLRe.Controls.Add(this.groupBox6);
			this.panelSQLRe.Controls.Add(this.button5);
			this.panelSQLRe.Controls.Add(this.groupBox7);
			this.panelSQLRe.Controls.Add(this.panel5);
			this.panelSQLRe.Controls.Add(this.panel6);
			this.panelSQLRe.Location = new Point(0, 0);
			this.panelSQLRe.Name = "panelSQLRe";
			this.panelSQLRe.Size = new System.Drawing.Size(352, 344);
			this.panelSQLRe.TabIndex = 1;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = CheckState.Checked;
			this.checkBox1.Location = new Point(232, 22);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(104, 32);
			this.checkBox1.TabIndex = 39;
			this.checkBox1.Text = "默认存放路径";
			this.progressBar2.Location = new Point(16, 184);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(240, 23);
			this.progressBar2.TabIndex = 38;
			this.progressBar2.Visible = false;
			this.groupBox6.Controls.Add(this.radioButton4);
			this.groupBox6.Controls.Add(this.radioButton3);
			this.groupBox6.Location = new Point(10, 2);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(206, 62);
			this.groupBox6.TabIndex = 33;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "附加还原选择";
			this.radioButton4.Location = new Point(120, 24);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(64, 24);
			this.radioButton4.TabIndex = 1;
			this.radioButton4.Text = "附加";
			this.radioButton3.Location = new Point(32, 24);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(64, 24);
			this.radioButton3.TabIndex = 0;
			this.radioButton3.Text = "还原";
			this.radioButton3.CheckedChanged += new EventHandler(this.radioButton3_CheckedChanged);
			this.button5.FlatStyle = FlatStyle.Popup;
			this.button5.Location = new Point(272, 182);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(64, 23);
			this.button5.TabIndex = 35;
			this.button5.Text = "执行还原";
			this.button5.Click += new EventHandler(this.button5_Click);
			this.groupBox7.Controls.Add(this.checkBox2);
			this.groupBox7.Controls.Add(this.button9);
			this.groupBox7.Controls.Add(this.bAddUser);
			this.groupBox7.Controls.Add(this.newPwd);
			this.groupBox7.Controls.Add(this.newLogin);
			this.groupBox7.Controls.Add(this.label30);
			this.groupBox7.Controls.Add(this.label29);
			this.groupBox7.Location = new Point(10, 216);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(330, 123);
			this.groupBox7.TabIndex = 37;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "数据库登录用户";
			this.checkBox2.Location = new Point(48, 88);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(104, 24);
			this.checkBox2.TabIndex = 6;
			this.checkBox2.Text = "设置操作权限";
			this.button9.FlatStyle = FlatStyle.Popup;
			this.button9.Location = new Point(256, 88);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(58, 23);
			this.button9.TabIndex = 5;
			this.button9.Text = "删除";
			this.button9.Click += new EventHandler(this.button9_Click);
			this.bAddUser.FlatStyle = FlatStyle.Popup;
			this.bAddUser.Location = new Point(192, 88);
			this.bAddUser.Name = "bAddUser";
			this.bAddUser.Size = new System.Drawing.Size(58, 23);
			this.bAddUser.TabIndex = 4;
			this.bAddUser.Text = "新增";
			this.bAddUser.Click += new EventHandler(this.bAddUser_Click);
			this.newPwd.BorderStyle = BorderStyle.FixedSingle;
			this.newPwd.Location = new Point(112, 58);
			this.newPwd.Name = "newPwd";
			this.newPwd.PasswordChar = '*';
			this.newPwd.Size = new System.Drawing.Size(152, 21);
			this.newPwd.TabIndex = 3;
			this.newLogin.BorderStyle = BorderStyle.FixedSingle;
			this.newLogin.Location = new Point(112, 24);
			this.newLogin.Name = "newLogin";
			this.newLogin.Size = new System.Drawing.Size(152, 21);
			this.newLogin.TabIndex = 2;
			this.label30.Location = new Point(48, 58);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(56, 21);
			this.label30.TabIndex = 1;
			this.label30.Text = "密码";
			this.label30.TextAlign = ContentAlignment.MiddleLeft;
			this.label29.Location = new Point(48, 24);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(56, 21);
			this.label29.TabIndex = 0;
			this.label29.Text = "用户名";
			this.label29.TextAlign = ContentAlignment.MiddleLeft;
			this.panel5.Controls.Add(this.button11);
			this.panel5.Controls.Add(this.label31);
			this.panel5.Controls.Add(this.SQLDataFilePath);
			this.panel5.Controls.Add(this.button2);
			this.panel5.Controls.Add(this.textBox1);
			this.panel5.Controls.Add(this.textBox2);
			this.panel5.Controls.Add(this.label24);
			this.panel5.Controls.Add(this.label25);
			this.panel5.Location = new Point(5, 70);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(339, 106);
			this.panel5.TabIndex = 34;
			this.button11.FlatStyle = FlatStyle.Popup;
			this.button11.Location = new Point(304, 80);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(24, 21);
			this.button11.TabIndex = 37;
			this.button11.Text = ">";
			this.button11.Click += new EventHandler(this.button11_Click);
			this.label31.Location = new Point(16, 80);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(80, 23);
			this.label31.TabIndex = 36;
			this.label31.Text = "数据存放路径";
			this.label31.TextAlign = ContentAlignment.MiddleLeft;
			this.SQLDataFilePath.BorderStyle = BorderStyle.FixedSingle;
			this.SQLDataFilePath.Location = new Point(104, 80);
			this.SQLDataFilePath.Name = "SQLDataFilePath";
			this.SQLDataFilePath.Size = new System.Drawing.Size(200, 21);
			this.SQLDataFilePath.TabIndex = 35;
			this.button2.FlatStyle = FlatStyle.Popup;
			this.button2.Location = new Point(304, 47);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(24, 21);
			this.button2.TabIndex = 34;
			this.button2.Text = ">";
			this.button2.Click += new EventHandler(this.button2_Click);
			this.textBox1.BorderStyle = BorderStyle.FixedSingle;
			this.textBox1.Location = new Point(104, 47);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(200, 21);
			this.textBox1.TabIndex = 33;
			this.textBox2.BorderStyle = BorderStyle.FixedSingle;
			this.textBox2.Location = new Point(104, 13);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(200, 21);
			this.textBox2.TabIndex = 30;
			this.label24.Location = new Point(16, 39);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(72, 40);
			this.label24.TabIndex = 32;
			this.label24.Text = "还原文件(含路径)";
			this.label24.TextAlign = ContentAlignment.MiddleLeft;
			this.label25.Location = new Point(16, 13);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(72, 23);
			this.label25.TabIndex = 31;
			this.label25.Text = "数据库名称";
			this.label25.TextAlign = ContentAlignment.MiddleLeft;
			this.panel6.Controls.Add(this.button7);
			this.panel6.Controls.Add(this.textBox3);
			this.panel6.Controls.Add(this.label26);
			this.panel6.Controls.Add(this.button8);
			this.panel6.Controls.Add(this.textBox4);
			this.panel6.Controls.Add(this.textBox5);
			this.panel6.Controls.Add(this.label27);
			this.panel6.Controls.Add(this.label28);
			this.panel6.Location = new Point(5, 70);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(339, 106);
			this.panel6.TabIndex = 36;
			this.button7.FlatStyle = FlatStyle.Popup;
			this.button7.Location = new Point(293, 80);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(24, 21);
			this.button7.TabIndex = 45;
			this.button7.Text = ">";
			this.button7.Click += new EventHandler(this.button7_Click);
			this.textBox3.BorderStyle = BorderStyle.FixedSingle;
			this.textBox3.Location = new Point(93, 80);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(200, 21);
			this.textBox3.TabIndex = 44;
			this.label26.Location = new Point(21, 80);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(72, 23);
			this.label26.TabIndex = 43;
			this.label26.Text = "日志文件";
			this.label26.TextAlign = ContentAlignment.MiddleLeft;
			this.button8.FlatStyle = FlatStyle.Popup;
			this.button8.Location = new Point(293, 48);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(24, 21);
			this.button8.TabIndex = 42;
			this.button8.Text = ">";
			this.button8.Click += new EventHandler(this.button8_Click);
			this.textBox4.BorderStyle = BorderStyle.FixedSingle;
			this.textBox4.Location = new Point(93, 48);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(200, 21);
			this.textBox4.TabIndex = 41;
			this.textBox5.BorderStyle = BorderStyle.FixedSingle;
			this.textBox5.Location = new Point(93, 16);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(200, 21);
			this.textBox5.TabIndex = 38;
			this.label27.Location = new Point(21, 48);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(72, 23);
			this.label27.TabIndex = 40;
			this.label27.Text = "附加路径";
			this.label27.TextAlign = ContentAlignment.MiddleLeft;
			this.label28.Location = new Point(21, 16);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(72, 23);
			this.label28.TabIndex = 39;
			this.label28.Text = "数据库名称";
			this.label28.TextAlign = ContentAlignment.MiddleLeft;
			this.tabImpExp.Controls.Add(this.panelImp);
			this.tabImpExp.Controls.Add(this.panelExp);
			this.tabImpExp.Location = new Point(4, 21);
			this.tabImpExp.Name = "tabImpExp";
			this.tabImpExp.Size = new System.Drawing.Size(347, 343);
			this.tabImpExp.TabIndex = 4;
			this.tabImpExp.Text = "Oracle导入\\导出配置";
			this.panelImp.Controls.Add(this.groupBox3);
			this.panelImp.Controls.Add(this.groupBox4);
			this.panelImp.Controls.Add(this.groupBox5);
			this.panelImp.Location = new Point(0, 0);
			this.panelImp.Name = "panelImp";
			this.panelImp.Size = new System.Drawing.Size(352, 360);
			this.panelImp.TabIndex = 0;
			this.groupBox3.Controls.Add(this.bPassWord);
			this.groupBox3.Controls.Add(this.bUserID);
			this.groupBox3.Controls.Add(this.createPassWord);
			this.groupBox3.Controls.Add(this.createUserID);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Location = new Point(12, 112);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(328, 103);
			this.groupBox3.TabIndex = 31;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "创建用户";
			this.bPassWord.Location = new Point(264, 72);
			this.bPassWord.Name = "bPassWord";
			this.bPassWord.Size = new System.Drawing.Size(50, 23);
			this.bPassWord.TabIndex = 9;
			this.bPassWord.Text = "删除";
			this.bPassWord.Click += new EventHandler(this.bPassWord_Click);
			this.bUserID.Location = new Point(208, 72);
			this.bUserID.Name = "bUserID";
			this.bUserID.Size = new System.Drawing.Size(50, 23);
			this.bUserID.TabIndex = 8;
			this.bUserID.Text = "创建";
			this.bUserID.Click += new EventHandler(this.bUserID_Click);
			this.createPassWord.Location = new Point(88, 44);
			this.createPassWord.Name = "createPassWord";
			this.createPassWord.Size = new System.Drawing.Size(176, 21);
			this.createPassWord.TabIndex = 6;
			this.createUserID.Location = new Point(88, 16);
			this.createUserID.Name = "createUserID";
			this.createUserID.Size = new System.Drawing.Size(176, 21);
			this.createUserID.TabIndex = 5;
			this.label9.Location = new Point(16, 44);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 21);
			this.label9.TabIndex = 4;
			this.label9.Text = "密码:";
			this.label9.TextAlign = ContentAlignment.MiddleLeft;
			this.label8.Location = new Point(16, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 21);
			this.label8.TabIndex = 3;
			this.label8.Text = "用户名:";
			this.label8.TextAlign = ContentAlignment.MiddleLeft;
			this.groupBox4.Controls.Add(this.bTabSpaceDir);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.btabSpaceDelete);
			this.groupBox4.Controls.Add(this.btabSpaceCreate);
			this.groupBox4.Controls.Add(this.tabSpaceSize);
			this.groupBox4.Controls.Add(this.tabSpaceDir);
			this.groupBox4.Controls.Add(this.tabSpace);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Location = new Point(12, 5);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(328, 105);
			this.groupBox4.TabIndex = 29;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "创建表空间";
			this.bTabSpaceDir.Location = new Point(269, 44);
			this.bTabSpaceDir.Name = "bTabSpaceDir";
			this.bTabSpaceDir.Size = new System.Drawing.Size(25, 21);
			this.bTabSpaceDir.TabIndex = 20;
			this.bTabSpaceDir.Text = ">";
			this.bTabSpaceDir.Click += new EventHandler(this.bTabSpaceDir_Click);
			this.label10.Location = new Point(176, 72);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(16, 21);
			this.label10.TabIndex = 8;
			this.label10.Text = "M";
			this.label10.TextAlign = ContentAlignment.MiddleCenter;
			this.btabSpaceDelete.Location = new Point(264, 75);
			this.btabSpaceDelete.Name = "btabSpaceDelete";
			this.btabSpaceDelete.Size = new System.Drawing.Size(50, 23);
			this.btabSpaceDelete.TabIndex = 7;
			this.btabSpaceDelete.Text = "删除";
			this.btabSpaceDelete.Click += new EventHandler(this.btabSpaceDelete_Click);
			this.btabSpaceCreate.Location = new Point(208, 75);
			this.btabSpaceCreate.Name = "btabSpaceCreate";
			this.btabSpaceCreate.Size = new System.Drawing.Size(50, 23);
			this.btabSpaceCreate.TabIndex = 6;
			this.btabSpaceCreate.Text = "创建";
			this.btabSpaceCreate.Click += new EventHandler(this.btabSpaceCreate_Click);
			this.tabSpaceSize.Location = new Point(88, 72);
			this.tabSpaceSize.Name = "tabSpaceSize";
			this.tabSpaceSize.Size = new System.Drawing.Size(80, 21);
			this.tabSpaceSize.TabIndex = 5;
			this.tabSpaceDir.Location = new Point(88, 44);
			this.tabSpaceDir.Name = "tabSpaceDir";
			this.tabSpaceDir.Size = new System.Drawing.Size(176, 21);
			this.tabSpaceDir.TabIndex = 4;
			this.tabSpace.Location = new Point(88, 16);
			this.tabSpace.Name = "tabSpace";
			this.tabSpace.Size = new System.Drawing.Size(176, 21);
			this.tabSpace.TabIndex = 3;
			this.tabSpace.Leave += new EventHandler(this.tabSpace_Leave);
			this.label11.Location = new Point(16, 71);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(72, 21);
			this.label11.TabIndex = 2;
			this.label11.Text = "大小:";
			this.label11.TextAlign = ContentAlignment.MiddleLeft;
			this.label12.Location = new Point(16, 43);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(72, 21);
			this.label12.TabIndex = 1;
			this.label12.Text = "表空间路径:";
			this.label12.TextAlign = ContentAlignment.MiddleLeft;
			this.label13.Location = new Point(16, 17);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(72, 21);
			this.label13.TabIndex = 0;
			this.label13.Text = "表空间名:";
			this.label13.TextAlign = ContentAlignment.MiddleLeft;
			this.groupBox5.Controls.Add(this.bimpDir);
			this.groupBox5.Controls.Add(this.bImportion);
			this.groupBox5.Controls.Add(this.label14);
			this.groupBox5.Controls.Add(this.toUser);
			this.groupBox5.Controls.Add(this.label15);
			this.groupBox5.Controls.Add(this.fromUser);
			this.groupBox5.Controls.Add(this.label16);
			this.groupBox5.Controls.Add(this.importDir);
			this.groupBox5.Controls.Add(this.label17);
			this.groupBox5.Location = new Point(12, 217);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(328, 121);
			this.groupBox5.TabIndex = 30;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "导入";
			this.bimpDir.Location = new Point(294, 38);
			this.bimpDir.Name = "bimpDir";
			this.bimpDir.Size = new System.Drawing.Size(25, 21);
			this.bimpDir.TabIndex = 37;
			this.bimpDir.Text = ">";
			this.bimpDir.Click += new EventHandler(this.bimpDir_Click);
			this.bImportion.Location = new Point(232, 91);
			this.bImportion.Name = "bImportion";
			this.bImportion.Size = new System.Drawing.Size(64, 23);
			this.bImportion.TabIndex = 36;
			this.bImportion.Text = "导入";
			this.bImportion.Click += new EventHandler(this.bImportion_Click);
			this.label14.Location = new Point(270, 65);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(29, 21);
			this.label14.TabIndex = 35;
			this.label14.Text = "用户";
			this.label14.TextAlign = ContentAlignment.MiddleCenter;
			this.toUser.Location = new Point(182, 65);
			this.toUser.Name = "toUser";
			this.toUser.Size = new System.Drawing.Size(80, 21);
			this.toUser.TabIndex = 34;
			this.label15.Location = new Point(118, 65);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(67, 21);
			this.label15.TabIndex = 33;
			this.label15.Text = "用户导入到";
			this.label15.TextAlign = ContentAlignment.MiddleLeft;
			this.fromUser.Location = new Point(30, 65);
			this.fromUser.Name = "fromUser";
			this.fromUser.Size = new System.Drawing.Size(80, 21);
			this.fromUser.TabIndex = 32;
			this.label16.Location = new Point(6, 65);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(24, 21);
			this.label16.TabIndex = 31;
			this.label16.Text = "从";
			this.label16.TextAlign = ContentAlignment.MiddleCenter;
			this.importDir.Location = new Point(30, 38);
			this.importDir.Name = "importDir";
			this.importDir.Size = new System.Drawing.Size(260, 21);
			this.importDir.TabIndex = 30;
			this.label17.Location = new Point(14, 18);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(144, 15);
			this.label17.TabIndex = 29;
			this.label17.Text = "导入数据库(*.dmp)路径";
			this.label17.TextAlign = ContentAlignment.MiddleLeft;
			this.panelExp.Controls.Add(this.bexpDir);
			this.panelExp.Controls.Add(this.label19);
			this.panelExp.Controls.Add(this.label18);
			this.panelExp.Controls.Add(this.bExportion);
			this.panelExp.Controls.Add(this.exportDir);
			this.panelExp.Controls.Add(this.exportUserID);
			this.panelExp.Controls.Add(this.label20);
			this.panelExp.Location = new Point(0, 0);
			this.panelExp.Name = "panelExp";
			this.panelExp.Size = new System.Drawing.Size(352, 344);
			this.panelExp.TabIndex = 1;
			this.bexpDir.Location = new Point(304, 168);
			this.bexpDir.Name = "bexpDir";
			this.bexpDir.Size = new System.Drawing.Size(25, 21);
			this.bexpDir.TabIndex = 25;
			this.bexpDir.Text = ">";
			this.bexpDir.Click += new EventHandler(this.bexpDir_Click);
			this.label19.Location = new Point(24, 144);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(144, 21);
			this.label19.TabIndex = 24;
			this.label19.Text = "导出数据库(*.dmp)路径";
			this.label19.TextAlign = ContentAlignment.MiddleLeft;
			this.label18.Location = new Point(240, 96);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(80, 21);
			this.label18.TabIndex = 23;
			this.label18.Text = "(以逗号分隔)";
			this.label18.TextAlign = ContentAlignment.MiddleLeft;
			this.bExportion.Location = new Point(191, 216);
			this.bExportion.Name = "bExportion";
			this.bExportion.Size = new System.Drawing.Size(120, 23);
			this.bExportion.TabIndex = 22;
			this.bExportion.Text = "导出";
			this.bExportion.Click += new EventHandler(this.bExportion_Click);
			this.exportDir.Location = new Point(39, 168);
			this.exportDir.Name = "exportDir";
			this.exportDir.Size = new System.Drawing.Size(260, 21);
			this.exportDir.TabIndex = 21;
			this.exportUserID.Location = new Point(39, 96);
			this.exportUserID.Name = "exportUserID";
			this.exportUserID.Size = new System.Drawing.Size(193, 21);
			this.exportUserID.TabIndex = 20;
			this.label20.Location = new Point(23, 72);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(144, 21);
			this.label20.TabIndex = 19;
			this.label20.Text = "导出Oracle用户列表";
			this.label20.TextAlign = ContentAlignment.MiddleLeft;
			this.bHelp.Location = new Point(285, 376);
			this.bHelp.Name = "bHelp";
			this.bHelp.Size = new System.Drawing.Size(75, 23);
			this.bHelp.TabIndex = 6;
			this.bHelp.Text = "帮助";
			this.bCancel.Location = new Point(205, 376);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(75, 23);
			this.bCancel.TabIndex = 5;
			this.bCancel.Text = "关闭";
			this.bCancel.Click += new EventHandler(this.bCancel_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			base.ClientSize = new System.Drawing.Size(367, 408);
			base.Controls.Add(this.bHelp);
			base.Controls.Add(this.bCancel);
			base.Controls.Add(this.tabControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		
			base.Name = "frmDatabaseBack";
			this.Text = "数据库备份恢复工具";
			base.Load += new EventHandler(this.frmDatabaseBack_Load);
			this.tabControl1.ResumeLayout(false);
			this.dataBaseSelection.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabConnection.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.tabOperSelection.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.groupSelection.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabSQLOper.ResumeLayout(false);
			this.panelSQLBak.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.panelSQLRe.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.tabImpExp.ResumeLayout(false);
			this.panelImp.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.panelExp.ResumeLayout(false);
			this.panelExp.PerformLayout();
			base.ResumeLayout(false);
		}

		private bool method_0(string string_0, string string_1, string string_2, string string_3)
		{
			bool flag = false;
			try
			{
				SqlConnection sqlConnection = new SqlConnection(string_0);
				string[] string1 = new string[] { "EXEC sp_attach_db @dbname = '", string_1, "', @filename1 = '", string_2, "',@filename2='", string_3, "'" };
				SqlCommand sqlCommand = new SqlCommand(string.Concat(string1), sqlConnection);
				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
				flag = true;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message.ToString());
				flag = false;
			}
			return flag;
		}

		private void method_1(string string_0, string string_1, string string_2)
		{
			SQLServer sQLServerClass = new SQLServer();
			Database databaseClass = new Database();
			sQLServerClass.LoginSecure=false;
			int count = 0;
			this.SQLSDE.Items.Clear();
			try
			{
				sQLServerClass.Connect(string_0, string_1, string_2);
			    count = sQLServerClass.Databases.Count;
			}
			catch (Exception exception)
			{
				COMException cOMException = exception as COMException;
				if (cOMException.ErrorCode == -2147221504)
				{
					MessageBox.Show("服务器没有启动或不存在");
				}
				if (cOMException.ErrorCode == -2147203048)
				{
					MessageBox.Show(string.Concat("用户名'", string_1, "'登录失败"));
				}
				if (cOMException.ErrorCode == -2147204362)
				{
					MessageBox.Show("服务器暂停，不允许进行新的连接");
				}
				return;
			}
			try
			{
				try
				{
					for (int i = 1; i < count + 1; i++)
					{
						string name = "";
						databaseClass = (Database)sQLServerClass.Databases.ItemByID(i);
						name = databaseClass.Name;
						this.SQLSDE.Items.Add(name);
					}
				}
				catch (Exception exception1)
				{
				}
			}
			finally
			{
				sQLServerClass.DisConnect();
			}
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.radioButton1.Checked)
			{
				this.tabControl1.TabPages.Remove(this.tabSQLOper);
				this.tabControl1.TabPages.Add(this.tabImpExp);
				this.panel1.Visible = false;
				this.panel3.Visible = false;
				this.panel2.Visible = true;
				this.panel4.Visible = true;
				this.radioButtonBak.Checked = true;
				this.tabControl1.SelectedTab = this.dataBaseSelection;
			}
			else
			{
				this.tabControl1.TabPages.Remove(this.tabImpExp);
				this.tabControl1.TabPages.Add(this.tabSQLOper);
				this.panel1.Visible = true;
				this.panel3.Visible = true;
				this.panel2.Visible = false;
				this.panel4.Visible = false;
				this.radioButtonSQLBak.Checked = true;
				this.tabControl1.SelectedTab = this.dataBaseSelection;
			}
		}

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.radioButton3.Checked)
			{
				this.panel5.Visible = false;
				this.panel6.Visible = true;
				this.button5.Text = "执行附加";
			}
			else
			{
				this.panel5.Visible = true;
				this.panel6.Visible = false;
				this.button5.Text = "执行还原";
			}
		}

		private void radioButtonBak_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.radioButtonBak.Checked)
			{
				this.panelExp.Visible = false;
				this.panelImp.Visible = true;
			}
			else
			{
				this.panelExp.Visible = true;
				this.panelImp.Visible = false;
			}
		}

		private void radioButtonSQLBak_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.radioButtonSQLBak.Checked)
			{
				this.panelSQLBak.Visible = false;
				this.panelSQLRe.Visible = true;
				this.radioButton3.Checked = true;
			}
			else
			{
				this.panelSQLBak.Visible = true;
				this.panelSQLRe.Visible = false;
			}
		}

		private void serverName_Leave(object sender, EventArgs e)
		{
			string oraHome;
			try
			{
				oraHome = OracleFuctions.getOraHome();
				this.tabSpaceDir.Text = oraHome;
				if (this.tabSpaceDir.Text.Trim().IndexOf(this.serverName.Text.Trim()) < 0)
				{
					TextBox textBox = this.tabSpaceDir;
					textBox.Text = string.Concat(textBox.Text, this.serverName.Text, '\\');
				}
			}
			catch (Exception exception)
			{
				oraHome = exception.Message.ToString();
			}
		}

		private void SQLSDE_DropDown(object sender, EventArgs e)
		{
			string text = "";
			try
			{
				this.SQLSDE.Items.Clear();
			}
			catch
			{
			}
			try
			{
				text = this.MachineName.Text;
				string str = this.SQLUserName.Text.Trim();
				string str1 = this.SQLUserPwd.Text.Trim();
				if (text == "")
				{
					MessageBox.Show("主机名不能为空");
				}
				else if (str == "")
				{
					MessageBox.Show("用户名不能为空");
				}
				else if (!DbOper.GetMachineSQL(text, str, str1))
				{
					this.SQLSDE.DataSource = null;
				}
				else
				{
					this.SQLSDE.DataSource = this.sqlDBManage_0.GetDbList(text, str, str1);
				}
			}
			catch
			{
			}
		}

		private void tabSpace_Leave(object sender, EventArgs e)
		{
			try
			{
				if (this.tabSpaceDir.Text.Trim().Substring(this.tabSpaceDir.Text.Trim().Length - 4, 4) == ".ORA")
				{
					int num = this.tabSpaceDir.Text.Trim().LastIndexOf('\\');
					this.tabSpaceDir.Text = this.tabSpaceDir.Text.Trim().Substring(0, num + 1);
				}
				TextBox textBox = this.tabSpaceDir;
				textBox.Text = string.Concat(textBox.Text, this.tabSpace.Text.Trim().ToUpper());
				TextBox textBox1 = this.tabSpaceDir;
				textBox1.Text = string.Concat(textBox1.Text, ".ORA");
			}
			catch (Exception exception)
			{
				exception.Message.ToString();
			}
		}
	}
}
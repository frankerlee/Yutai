using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SQLDMO;

namespace Yutai.ArcGIS.Common.Data
{
	public partial class frmDatabaseBack : Form
	{







































































































		private System.ComponentModel.Container container_0 = null;























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
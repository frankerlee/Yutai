using System;
using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.Display;
using Yutai.Plugins.Scene.Classes;
using Yutai.Plugins.Scene.Helpers;

namespace Yutai.Plugins.Scene.Forms
{
	internal partial class frmTexturePallette : System.Windows.Forms.Form
	{
		private List<clsTextureGroup> list_0 = null;
		public clsTextureGroup m_pTG;
		private static short mnuAddImage_Click_keyindex;
        private short short_0;
        private bool bool_0;
        private bool bool_1;


        public List<clsTextureGroup> TextureGroups
		{
			set
			{
				this.list_0 = value;
			}
		}

		public void GetRoofColor()
		{
			try
			{
				clsTextureGroup pTG = this.m_pTG;
				this.DlgCommonColor.ShowDialog();
				pTG.RoofColorRGB = System.Drawing.ColorTranslator.ToOle(this.DlgCommonColor.Color);
				this.DoNodeClick(this.tv1.SelectedNode);
			}
			catch
			{
			}
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void method_0(clsTextureGroup clsTextureGroup_0)
		{
			System.Windows.Forms.TreeNode treeNode = new System.Windows.Forms.TreeNode(clsTextureGroup_0.name);
			treeNode.Tag = clsTextureGroup_0;
			this.tv1.Nodes.Add(treeNode);
			for (int i = 0; i < clsTextureGroup_0.TexturePaths.Count; i++)
			{
				string path = clsTextureGroup_0.TexturePaths[i];
				System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode(Path.GetFileNameWithoutExtension(path));
				treeNode2.Tag = clsTextureGroup_0.Symbols[i];
				treeNode.Nodes.Add(treeNode2);
			}
		}

		private void method_1()
		{
			this.tv1.Nodes.Clear();
			if (this.list_0 != null)
			{
				for (int i = 0; i < this.list_0.Count; i++)
				{
					clsTextureGroup clsTextureGroup_ = this.list_0[i];
					this.method_0(clsTextureGroup_);
				}
			}
		}

		private string method_2(bool bool_2)
		{
			return "";
		}

		private void cmdRemove_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.TreeNode selectedNode = this.tv1.SelectedNode;
			if (selectedNode == null)
			{
				this.cmdRemove.Enabled = false;
			}
			else
			{
				if (selectedNode.Tag is clsTextureGroup)
				{
					this.list_0.Remove(selectedNode.Tag as clsTextureGroup);
					this.tv1.Nodes.Remove(selectedNode);
					this.bool_1 = true;
				}
				else
				{
					this.cmdRemove.Enabled = false;
				}
				this.DoNodeClick(this.tv1.Nodes[0]);
			}
		}

		public void RemoveGroup(string string_0)
		{
			try
			{
			}
			catch
			{
			}
		}

		private void cmdTextureBrowse_Click(object sender, EventArgs e)
		{
			this.AddGroup();
			this.DoNodeClick(this.tv1.Nodes[0]);
			this.bool_1 = true;
		}

		private void frmTexturePallette_Load(object sender, EventArgs e)
		{
			this.mnuGroup.Visible = false;
			this.mnuImage.Visible = false;
			this.method_1();
		}

		public void mnuRemoveGroup_Click(object sender, EventArgs e)
		{
			this.RemoveGroup(this.m_pTG.name);
			this.DoNodeClick(this.tv1.Nodes[0]);
			this.bool_1 = true;
		}

		public void mnuAddImage_Click(object sender, EventArgs e)
		{
			try
			{
				string text = this.method_2(true).Trim();
				if (text.Length >= 1)
				{
					this.bool_1 = true;
					if (this.bool_0)
					{
						int arg_3E_0 = this.m_pTG.RoofColorRGB;
						System.Windows.Forms.TreeNode selectedNode = this.tv1.SelectedNode;
						for (System.Windows.Forms.TreeNode treeNode = selectedNode.Nodes[0]; treeNode != null; treeNode = treeNode.Nodes[0])
						{
							if (treeNode.Text.IndexOf("ROOFCOLOR", 0) > 1)
							{
								this.tv1.Nodes.Remove(treeNode);
								break;
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		public void mnuRemoveImage_Click(object sender, EventArgs e)
		{
			try
			{
			}
			catch
			{
			}
		}

		public void mnuRename_Click(object sender, EventArgs e)
		{
		}

		public void mnuRoofColor_Click(object sender, EventArgs e)
		{
			if (this.m_pTG != null)
			{
				this.GetRoofColor();
				this.bool_1 = true;
			}
			this.DoNodeClick(this.tv1.Nodes[0]);
		}

		private void tv1_DoubleClick(object sender, EventArgs e)
		{
			System.Windows.Forms.TreeNode selectedNode = this.tv1.SelectedNode;
			if (selectedNode.Text.ToUpper() == "ROOFCOLOR")
			{
				this.GetRoofColor();
				this.bool_1 = true;
			}
		}

		private void tv1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Delete)
			{
				System.Windows.Forms.TreeNode selectedNode = this.tv1.SelectedNode;
				if (selectedNode != null && selectedNode.Nodes.Count < 1 && selectedNode.Text.ToUpper() != "ROOFCOLOR")
				{
					this.RemoveImage(selectedNode);
				}
			}
		}

		private void tv1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				System.Windows.Forms.TreeNode selectedNode = this.tv1.SelectedNode;
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					if (selectedNode.Nodes.Count < 1)
					{
						bool enabled = selectedNode.Parent.Index != 1;
						this.mnuAddImage.Enabled = enabled;
						this.mnuRemoveImage.Enabled = enabled;
						this.mnuRemoveGroup.Enabled = enabled;
						this.mnuRename.Enabled = enabled;
						selectedNode.Parent.Tag.ToString();
						string text = selectedNode.Text;
						if (text.ToUpper() != "ROOFCOLOR")
						{
							this.contextMenuStrip1.Show();
						}
						else
						{
							this.GetRoofColor();
						}
					}
					else
					{
						bool enabled = selectedNode.Index != 1;
						this.mnuAddImage.Enabled = enabled;
						this.mnuRemoveImage.Enabled = enabled;
						this.mnuRemoveGroup.Enabled = enabled;
						this.mnuRename.Enabled = enabled;
						selectedNode.Tag.ToString();
					}
				}
			}
			catch
			{
			}
		}

		private void tv1_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			this.DoNodeClick(e.Node);
		}

		public void DoNodeClick(System.Windows.Forms.TreeNode treeNode_0)
		{
			try
			{
				if (treeNode_0.Tag is IPictureFillSymbol)
				{
					this.Image1.Visible = true;
					this.Image1.Image = System.Drawing.Image.FromHbitmap(new IntPtr((treeNode_0.Tag as IPictureFillSymbol).Picture.Handle));
				}
				else if (treeNode_0.Tag == null)
				{
					this.Image1.Image = null;
					this.Image1.Visible = false;
				}
				else
				{
					this.frRoofColor.Visible = false;
					string arg_86_0 = treeNode_0.Text;
				}
				this.m_pTexture = this.Image1.Image;
				if (this.cmdTextureBrowse.Visible || this.cmdRemove.Visible)
				{
					if (treeNode_0.Index == 0)
					{
						this.cmdTextureBrowse.Enabled = true;
						this.cmdRemove.Enabled = false;
					}
					else if (treeNode_0.Nodes.Count > 1)
					{
						this.cmdTextureBrowse.Enabled = true;
						this.cmdRemove.Enabled = true;
					}
					else if (treeNode_0.Nodes.Count < 1)
					{
						this.cmdTextureBrowse.Enabled = true;
						this.cmdRemove.Enabled = false;
					}
				}
			}
			catch
			{
			}
		}

		public void AllowEdits(bool bool_2)
		{
			this.cmdTextureBrowse.Visible = bool_2;
			this.cmdRemove.Visible = bool_2;
			if (!bool_2)
			{
			}
		}

		public void AddGroup()
		{
			System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
			openFileDialog.Filter = "*.bmp|*.bmp";
			openFileDialog.Multiselect = true;
			if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				clsTextureGroup clsTextureGroup = new clsTextureGroup();
				string[] fileNames = openFileDialog.FileNames;
				for (int i = 0; i < fileNames.Length; i++)
				{
					string text = fileNames[i];
					if (clsTextureGroup.name == null || clsTextureGroup.name.Length == 0)
					{
						clsTextureGroup.name = Path.GetFileNameWithoutExtension(text);
					}
					clsTextureGroup.TexturePaths.Add(text);
				}
				clsTextureGroup.Init();
				this.list_0.Add(clsTextureGroup);
				this.method_0(clsTextureGroup);
			}
		}

		public void RemoveImage(System.Windows.Forms.TreeNode treeNode_0)
		{
			try
			{
				string arg_07_0 = treeNode_0.Text;
				this.tv1.Nodes.Remove(this.tv1.SelectedNode);
				short num = 1;
				while ((int)num <= this.m_pTG.TexturePaths.Count)
				{
					num += 1;
				}
				this.bool_1 = true;
			}
			catch
			{
			}
		}

		private void tv1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (this.tv1.SelectedNode == null)
			{
				this.cmdRemove.Enabled = (this.tv1.SelectedNode != null);
			}
			else
			{
				this.DoNodeClick(this.tv1.SelectedNode);
			}
		}

		public frmTexturePallette()
		{
			this.InitializeComponent();
		}


    }
}
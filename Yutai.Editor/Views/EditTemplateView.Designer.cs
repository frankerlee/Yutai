using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Controls.Editor.UI;

namespace Yutai.Plugins.Editor.Views
{
    partial class EditTemplateView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.editorTemplateManageCtrl21 = new Yutai.ArcGIS.Controls.Editor.UI.EditorTemplateManageCtrl2();
            this.lstConstructionTools = new DevExpress.XtraEditors.ImageListBoxControl();
            this.hyperlinkLabelControl1 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstConstructionTools)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.editorTemplateManageCtrl21);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.lstConstructionTools);
            this.splitContainerControl1.Panel2.Controls.Add(this.hyperlinkLabelControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(308, 482);
            this.splitContainerControl1.SplitterPosition = 299;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // editorTemplateManageCtrl21
            // 
            this.editorTemplateManageCtrl21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorTemplateManageCtrl21.Location = new System.Drawing.Point(0, 0);
//            this.editorTemplateManageCtrl21.Map = null;
            this.editorTemplateManageCtrl21.Name = "editorTemplateManageCtrl21";
            this.editorTemplateManageCtrl21.Size = new System.Drawing.Size(308, 299);
            this.editorTemplateManageCtrl21.TabIndex = 0;
            // 
            // lstConstructionTools
            // 
            this.lstConstructionTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConstructionTools.Location = new System.Drawing.Point(0, 14);
            this.lstConstructionTools.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.lstConstructionTools.Name = "lstConstructionTools";
            this.lstConstructionTools.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lstConstructionTools.Size = new System.Drawing.Size(308, 164);
            this.lstConstructionTools.TabIndex = 1;
            this.lstConstructionTools.SelectedIndexChanged += new System.EventHandler(this.lstConstructionTools_SelectedIndexChanged);
            // 
            // hyperlinkLabelControl1
            // 
            this.hyperlinkLabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.hyperlinkLabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.hyperlinkLabelControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hyperlinkLabelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hyperlinkLabelControl1.Location = new System.Drawing.Point(0, 0);
            this.hyperlinkLabelControl1.Name = "hyperlinkLabelControl1";
            this.hyperlinkLabelControl1.Size = new System.Drawing.Size(308, 14);
            this.hyperlinkLabelControl1.TabIndex = 0;
            this.hyperlinkLabelControl1.Text = "构建工具";
            // 
            // EditTemplateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "EditTemplateView";
            this.Size = new System.Drawing.Size(308, 482);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstConstructionTools)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainerControl splitContainerControl1;
        private ImageListBoxControl lstConstructionTools;
        private HyperlinkLabelControl hyperlinkLabelControl1;
        private EditorTemplateManageCtrl2 editorTemplateManageCtrl21;
    }
}

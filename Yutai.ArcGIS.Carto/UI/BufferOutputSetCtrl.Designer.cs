using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class BufferOutputSetCtrl
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BufferOutputSetCtrl));
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();
            this.rdoOutputType = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.rdoBufferType = new RadioGroup();
            this.groupBox3 = new GroupBox();
            this.btnSelectInputFeatures = new SimpleButton();
            this.txtOutName = new TextEdit();
            this.cboEditingPolygonLayer = new ComboBoxEdit();
            this.rdoNewLayer = new RadioButton();
            this.rdoSaveToEditingLayer = new RadioButton();
            this.rdoSaveToGraphicLayer = new RadioButton();
            this.groupBox1.SuspendLayout();
            this.rdoOutputType.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.rdoBufferType.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtOutName.Properties.BeginInit();
            this.cboEditingPolygonLayer.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdoOutputType);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(464, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "缓冲输出类型";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(167, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "是否融合各缓冲间重合的部分?";
            this.rdoOutputType.Location = new System.Drawing.Point(224, 16);
            this.rdoOutputType.Name = "rdoOutputType";
            this.rdoOutputType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoOutputType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoOutputType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoOutputType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "是"), new RadioGroupItem(null, "否") });
            this.rdoOutputType.Size = new Size(184, 32);
            this.rdoOutputType.TabIndex = 0;
            this.rdoOutputType.SelectedIndexChanged += new EventHandler(this.rdoOutputType_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.rdoBufferType);
            this.groupBox2.Location = new System.Drawing.Point(8, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(464, 104);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "创建缓冲，使其";
            this.rdoBufferType.Location = new System.Drawing.Point(8, 24);
            this.rdoBufferType.Name = "rdoBufferType";
            this.rdoBufferType.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoBufferType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoBufferType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoBufferType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在多边形内部和外部"), new RadioGroupItem(null, "只在多边形外部"), new RadioGroupItem(null, "只在多边形内部"), new RadioGroupItem(null, "在多边形外部并包含多边形内部") });
            this.rdoBufferType.Size = new Size(232, 72);
            this.rdoBufferType.TabIndex = 0;
            this.rdoBufferType.SelectedIndexChanged += new EventHandler(this.rdoBufferType_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.btnSelectInputFeatures);
            this.groupBox3.Controls.Add(this.txtOutName);
            this.groupBox3.Controls.Add(this.cboEditingPolygonLayer);
            this.groupBox3.Controls.Add(this.rdoNewLayer);
            this.groupBox3.Controls.Add(this.rdoSaveToEditingLayer);
            this.groupBox3.Controls.Add(this.rdoSaveToGraphicLayer);
            this.groupBox3.Location = new System.Drawing.Point(8, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(464, 120);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "如何保存创建的缓冲";
            this.btnSelectInputFeatures.Image = (System.Drawing.Image)resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new System.Drawing.Point(416, 88);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 15;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.txtOutName.EditValue = "";
            this.txtOutName.Location = new System.Drawing.Point(32, 88);
            this.txtOutName.Name = "txtOutName";
            this.txtOutName.Size = new Size(376, 21);
            this.txtOutName.TabIndex = 4;
            this.cboEditingPolygonLayer.EditValue = "";
            this.cboEditingPolygonLayer.Location = new System.Drawing.Point(168, 40);
            this.cboEditingPolygonLayer.Name = "cboEditingPolygonLayer";
            this.cboEditingPolygonLayer.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboEditingPolygonLayer.Size = new Size(232, 21);
            this.cboEditingPolygonLayer.TabIndex = 3;
            this.cboEditingPolygonLayer.SelectedIndexChanged += new EventHandler(this.cboEditingPolygonLayer_SelectedIndexChanged);
            this.rdoNewLayer.Location = new System.Drawing.Point(8, 64);
            this.rdoNewLayer.Name = "rdoNewLayer";
            this.rdoNewLayer.Size = new Size(320, 24);
            this.rdoNewLayer.TabIndex = 2;
            this.rdoNewLayer.Text = "创建一个新的图层，指定输出的shape文件或要素类";
            this.rdoNewLayer.Click += new EventHandler(this.rdoNewLayer_Click);
            this.rdoSaveToEditingLayer.Location = new System.Drawing.Point(8, 40);
            this.rdoSaveToEditingLayer.Name = "rdoSaveToEditingLayer";
            this.rdoSaveToEditingLayer.Size = new Size(160, 24);
            this.rdoSaveToEditingLayer.TabIndex = 1;
            this.rdoSaveToEditingLayer.Text = "添加到可编辑要素类中";
            this.rdoSaveToEditingLayer.Click += new EventHandler(this.rdoSaveToEditingLayer_Click);
            this.rdoSaveToGraphicLayer.Checked = true;
            this.rdoSaveToGraphicLayer.Location = new System.Drawing.Point(8, 16);
            this.rdoSaveToGraphicLayer.Name = "rdoSaveToGraphicLayer";
            this.rdoSaveToGraphicLayer.Size = new Size(176, 24);
            this.rdoSaveToGraphicLayer.TabIndex = 0;
            this.rdoSaveToGraphicLayer.TabStop = true;
            this.rdoSaveToGraphicLayer.Text = "作为数据框中的图形图层";
            this.rdoSaveToGraphicLayer.Click += new EventHandler(this.rdoSaveToGraphicLayer_Click);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BufferOutputSetCtrl";
            base.Size = new Size(520, 336);
            base.Load += new EventHandler(this.BufferOutputSetCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.rdoOutputType.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.rdoBufferType.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtOutName.Properties.EndInit();
            this.cboEditingPolygonLayer.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnSelectInputFeatures;
        private ComboBoxEdit cboEditingPolygonLayer;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private RadioGroup rdoBufferType;
        private RadioButton rdoNewLayer;
        private RadioGroup rdoOutputType;
        private RadioButton rdoSaveToEditingLayer;
        private RadioButton rdoSaveToGraphicLayer;
        private TextEdit txtOutName;
    }
}
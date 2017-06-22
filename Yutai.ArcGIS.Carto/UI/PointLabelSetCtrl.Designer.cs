using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class PointLabelSetCtrl
    {
        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_3);
        }

       
        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PointLabelSetCtrl));
            this.btnLabelExpression = new GroupBox();
            this.btnEditSymbol = new SimpleButton();
            this.symbolItem = new SymbolItem();
            this.groupBox1 = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.simpleButton1 = new SimpleButton();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.btnRotateField = new SimpleButton();
            this.btnAngles = new SimpleButton();
            this.imageComboBoxEdit1 = new ImageComboBoxEdit();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.rdoPointPlacementMethod = new RadioGroup();
            this.btnSQL = new SimpleButton();
            this.btnScaleSet = new SimpleButton();
            this.btnLabelExpression.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.rdoPointPlacementMethod.Properties.BeginInit();
            base.SuspendLayout();
            this.btnLabelExpression.Controls.Add(this.btnEditSymbol);
            this.btnLabelExpression.Controls.Add(this.symbolItem);
            this.btnLabelExpression.Location = new Point(8, 80);
            this.btnLabelExpression.Name = "btnLabelExpression";
            this.btnLabelExpression.Size = new Size(360, 72);
            this.btnLabelExpression.TabIndex = 5;
            this.btnLabelExpression.TabStop = false;
            this.btnLabelExpression.Text = "文本符号";
            this.btnEditSymbol.Location = new Point(232, 24);
            this.btnEditSymbol.Name = "btnEditSymbol";
            this.btnEditSymbol.Size = new Size(64, 24);
            this.btnEditSymbol.TabIndex = 1;
            this.btnEditSymbol.Text = "符号...";
            this.btnEditSymbol.Click += new EventHandler(this.btnEditSymbol_Click);
            this.symbolItem.BackColor = SystemColors.ControlLight;
            this.symbolItem.Location = new Point(16, 24);
            this.symbolItem.Name = "symbolItem";
            this.symbolItem.Size = new Size(168, 40);
            this.symbolItem.Symbol = null;
            this.symbolItem.TabIndex = 0;
            this.groupBox1.Controls.Add(this.cboFields);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 64);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注表达式";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(80, 24);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(136, 23);
            this.cboFields.TabIndex = 5;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.simpleButton1.Location = new Point(240, 24);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(64, 24);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "表达式...";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "标注字段";
            this.groupBox2.Controls.Add(this.btnRotateField);
            this.groupBox2.Controls.Add(this.btnAngles);
            this.groupBox2.Controls.Add(this.imageComboBoxEdit1);
            this.groupBox2.Controls.Add(this.rdoPointPlacementMethod);
            this.groupBox2.Location = new Point(8, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 128);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "放值属性";
            this.btnRotateField.Location = new Point(280, 96);
            this.btnRotateField.Name = "btnRotateField";
            this.btnRotateField.Size = new Size(72, 24);
            this.btnRotateField.TabIndex = 5;
            this.btnRotateField.Text = "旋转字段...";
            this.btnRotateField.Click += new EventHandler(this.btnRotateField_Click);
            this.btnAngles.Location = new Point(281, 64);
            this.btnAngles.Name = "btnAngles";
            this.btnAngles.Size = new Size(71, 24);
            this.btnAngles.TabIndex = 4;
            this.btnAngles.Text = "角度...";
            this.btnAngles.Click += new EventHandler(this.btnAngles_Click);
            this.imageComboBoxEdit1.EditValue = "001000000";
            this.imageComboBoxEdit1.Location = new Point(8, 24);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.Items.AddRange(new object[] { 
                new ImageComboBoxItem("仅在右上", "001000000", 0), new ImageComboBoxItem("", "010000000", 1), new ImageComboBoxItem("", "100000000", 2), new ImageComboBoxItem("", "000001000", 3), new ImageComboBoxItem("", "000000001", 4), new ImageComboBoxItem("", "000000010", 5), new ImageComboBoxItem("", "000000100", 6), new ImageComboBoxItem("", "000100000", 7), new ImageComboBoxItem("", "221000000", 8), new ImageComboBoxItem("", "001002002", 9), new ImageComboBoxItem("", "212000000", 10), new ImageComboBoxItem("", "002001002", 11), new ImageComboBoxItem("", "002002001", 12), new ImageComboBoxItem("", "000000221", 13), new ImageComboBoxItem("", "000000212", 14), new ImageComboBoxItem("", "000000122", 15), 
                new ImageComboBoxItem("", "200200100", 16), new ImageComboBoxItem("", "200100200", 17), new ImageComboBoxItem("", "100200200", 18), new ImageComboBoxItem("", "122000000", 19), new ImageComboBoxItem("", "221003003", 20), new ImageComboBoxItem("", "331002002", 21), new ImageComboBoxItem("", "002002331", 22), new ImageComboBoxItem("", "003003221", 23), new ImageComboBoxItem("", "200200133", 24), new ImageComboBoxItem("", "300300122", 25), new ImageComboBoxItem("", "122300300", 26), new ImageComboBoxItem("", "133200200", 27), new ImageComboBoxItem("", "221302332", 28), new ImageComboBoxItem("", "212202232", 29), new ImageComboBoxItem("", "122203233", 30), new ImageComboBoxItem("", "222103222", 31), 
                new ImageComboBoxItem("", "233203122", 32), new ImageComboBoxItem("", "232202212", 33), new ImageComboBoxItem("", "332302221", 34), new ImageComboBoxItem("", "222301222", 35)
             });
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageList_0;
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList_0;
            this.imageComboBoxEdit1.Size = new Size(104, 67);
            this.imageComboBoxEdit1.TabIndex = 3;
            this.imageComboBoxEdit1.SelectedIndexChanged += new EventHandler(this.imageComboBoxEdit1_SelectedIndexChanged);
            this.imageList_0.ColorDepth = ColorDepth.Depth32Bit;
            this.imageList_0.ImageSize = new Size(78, 63);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.rdoPointPlacementMethod.Location = new Point(120, 16);
            this.rdoPointPlacementMethod.Name = "rdoPointPlacementMethod";
            this.rdoPointPlacementMethod.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoPointPlacementMethod.Properties.Appearance.Options.UseBackColor = true;
            this.rdoPointPlacementMethod.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoPointPlacementMethod.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在点周围水平偏移标注"), new RadioGroupItem(null, "在点上部放置标注"), new RadioGroupItem(null, "以指定角度放置标注"), new RadioGroupItem(null, "根据字段值设置标注角度") });
            this.rdoPointPlacementMethod.Size = new Size(160, 104);
            this.rdoPointPlacementMethod.TabIndex = 2;
            this.rdoPointPlacementMethod.SelectedIndexChanged += new EventHandler(this.rdoPointPlacementMethod_SelectedIndexChanged);
            this.btnSQL.Location = new Point(104, 296);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new Size(56, 24);
            this.btnSQL.TabIndex = 13;
            this.btnSQL.Text = "SQL查询";
            this.btnSQL.Click += new EventHandler(this.btnSQL_Click);
            this.btnScaleSet.Location = new Point(24, 296);
            this.btnScaleSet.Name = "btnScaleSet";
            this.btnScaleSet.Size = new Size(64, 24);
            this.btnScaleSet.TabIndex = 12;
            this.btnScaleSet.Text = "比例范围";
            this.btnScaleSet.Click += new EventHandler(this.btnScaleSet_Click);
            base.Controls.Add(this.btnSQL);
            base.Controls.Add(this.btnScaleSet);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnLabelExpression);
            base.Controls.Add(this.groupBox1);
            base.Name = "PointLabelSetCtrl";
            base.Size = new Size(392, 336);
            base.Load += new EventHandler(this.PointLabelSetCtrl_Load);
            this.btnLabelExpression.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.imageComboBoxEdit1.Properties.EndInit();
            this.rdoPointPlacementMethod.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAngles;
        private SimpleButton btnEditSymbol;
        private GroupBox btnLabelExpression;
        private SimpleButton btnRotateField;
        private SimpleButton btnScaleSet;
        private SimpleButton btnSQL;
        private ComboBoxEdit cboFields;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IContainer icontainer_0;
        private ImageComboBoxEdit imageComboBoxEdit1;
        private ImageList imageList_0;
        private Label label1;
        private RadioGroup rdoPointPlacementMethod;
        private SimpleButton simpleButton1;
        private SymbolItem symbolItem;
    }
}
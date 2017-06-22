using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    partial class ScaleTextFormatPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager  resources = new System.ComponentModel.ComponentResourceManager(typeof(ScaleTextFormatPropertyPage));
            this.groupBox1 = new GroupBox();
            this.btnSymbolSelector = new SimpleButton();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBoxEdit();
            this.colorTextSymbol = new ColorEdit();
            this.chkUnderline = new CheckBox();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.chkItalic = new CheckBox();
            this.chkBold = new CheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.imageList_1 = new ImageList(this.icontainer_0);
            this.groupBox3 = new GroupBox();
            this.btnStyleInfo = new SimpleButton();
            this.btnStyleSelector = new SimpleButton();
            this.cboStyle = new StyleComboBox(this.icontainer_0);
            this.groupBox1.SuspendLayout();
            this.cboFontSize.Properties.BeginInit();
            this.colorTextSymbol.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnSymbolSelector);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboFontSize);
            this.groupBox1.Controls.Add(this.colorTextSymbol);
            this.groupBox1.Controls.Add(this.chkUnderline);
            this.groupBox1.Controls.Add(this.chkItalic);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.cboFontName);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(224, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文本";
            this.btnSymbolSelector.Location = new Point(144, 72);
            this.btnSymbolSelector.Name = "btnSymbolSelector";
            this.btnSymbolSelector.Size = new Size(56, 24);
            this.btnSymbolSelector.TabIndex = 24;
            this.btnSymbolSelector.Text = "符号...";
            this.btnSymbolSelector.Click += new EventHandler(this.btnSymbolSelector_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 78);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "颜色:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "大小:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "字体:";
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(48, 40);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(64, 21);
            this.cboFontSize.TabIndex = 20;
            this.cboFontSize.EditValueChanging += new ChangingEventHandler(this.cboFontSize_EditValueChanging);
            this.colorTextSymbol.EditValue = Color.Empty;
            this.colorTextSymbol.Location = new Point(48, 72);
            this.colorTextSymbol.Name = "colorTextSymbol";
            this.colorTextSymbol.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorTextSymbol.Size = new Size(48, 21);
            this.colorTextSymbol.TabIndex = 19;
            this.colorTextSymbol.EditValueChanged += new EventHandler(this.colorTextSymbol_EditValueChanged);
            this.chkUnderline.Appearance = Appearance.Button;
            this.chkUnderline.ImageIndex = 2;
            this.chkUnderline.ImageList = this.imageList_0;
            this.chkUnderline.Location = new Point(184, 40);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new Size(28, 24);
            this.chkUnderline.TabIndex = 18;
            this.chkUnderline.Click += new EventHandler(this.chkUnderline_Click);
            this.imageList_0.ImageSize = new Size(16, 16);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Magenta;
            this.chkItalic.Appearance = Appearance.Button;
            this.chkItalic.ImageIndex = 1;
            this.chkItalic.ImageList = this.imageList_0;
            this.chkItalic.Location = new Point(156, 40);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new Size(28, 24);
            this.chkItalic.TabIndex = 17;
            this.chkItalic.Click += new EventHandler(this.chkItalic_Click);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList_0;
            this.chkBold.Location = new Point(128, 40);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(28, 24);
            this.chkBold.TabIndex = 16;
            this.chkBold.Click += new EventHandler(this.chkBold_Click);
            this.cboFontName.Location = new Point(48, 16);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(160, 20);
            this.cboFontName.TabIndex = 15;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.imageList_1.ImageSize = new Size(10, 10);
            this.imageList_1.ImageStream = (ImageListStreamer)resources.GetObject("imageList2.ImageStream");
            this.imageList_1.TransparentColor = Color.Transparent;
            this.groupBox3.Controls.Add(this.btnStyleInfo);
            this.groupBox3.Controls.Add(this.btnStyleSelector);
            this.groupBox3.Controls.Add(this.cboStyle);
            this.groupBox3.Location = new Point(8, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(224, 72);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "样式";
            this.btnStyleInfo.ButtonStyle = BorderStyles.Simple;
            this.btnStyleInfo.ImageIndex = 1;
            this.btnStyleInfo.ImageList = this.imageList_1;
            this.btnStyleInfo.Location = new Point(192, 40);
            this.btnStyleInfo.Name = "btnStyleInfo";
            this.btnStyleInfo.Size = new Size(16, 16);
            this.btnStyleInfo.TabIndex = 18;
            this.btnStyleSelector.ButtonStyle = BorderStyles.Simple;
            this.btnStyleSelector.ImageIndex = 0;
            this.btnStyleSelector.ImageList = this.imageList_1;
            this.btnStyleSelector.Location = new Point(192, 24);
            this.btnStyleSelector.Name = "btnStyleSelector";
            this.btnStyleSelector.Size = new Size(16, 16);
            this.btnStyleSelector.TabIndex = 17;
            this.btnStyleSelector.Click += new EventHandler(this.btnStyleSelector_Click);
            this.cboStyle.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboStyle.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboStyle.DropDownWidth = 160;
            this.cboStyle.Font = new System.Drawing.Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.cboStyle.Location = new Point(8, 24);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Size = new Size(188, 31);
            this.cboStyle.TabIndex = 16;
            this.cboStyle.SelectedIndexChanged += new EventHandler(this.cboStyle_SelectedIndexChanged);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox1);
            base.Name = "ScaleTextFormatPropertyPage";
            base.Size = new Size(264, 224);
            base.Load += new EventHandler(this.ScaleTextFormatPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorTextSymbol.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private SimpleButton btnStyleInfo;
        private SimpleButton btnStyleSelector;
        private SimpleButton btnSymbolSelector;
        private System.Windows.Forms.ComboBox cboFontName;
        private ComboBoxEdit cboFontSize;
        private StyleComboBox cboStyle;
        private CheckBox chkBold;
        private CheckBox chkItalic;
        private CheckBox chkUnderline;
        private ColorEdit colorTextSymbol;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private ImageList imageList_1;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
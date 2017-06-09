using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class StandardTickSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private StyleButton btnStyle;
        private ComboBox cboFontName;
        private ComboBox cboFontSize;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private IMarkerSymbol imarkerSymbol_0 = new SimpleMarkerSymbolClass();
        private Label label1;
        private Label label4;
        private MapCartoTemplateLib.MapTemplate mapTemplate_0 = null;
        private RadioButton rdoLine;
        private RadioButton rdoTick;
        private string string_0 = "线";

        public event OnValueChangeEventHandler OnValueChange;

        public StandardTickSymbolPropertyPage()
        {
            this.InitializeComponent();
            (this.imarkerSymbol_0 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
            (this.imarkerSymbol_0 as ISimpleMarkerSymbol).Size = 28.0;
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.mapTemplate_0.GridSymbol = this.btnStyle.Style as ISymbol;
                this.mapTemplate_0.BigFontSize = double.Parse(this.cboFontSize.Text);
                this.mapTemplate_0.FontName = this.cboFontName.Text;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.bool_1 = true;
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.rdoTick.Checked)
                    {
                        this.imarkerSymbol_0 = this.btnStyle.Style as IMarkerSymbol;
                    }
                    else
                    {
                        this.ilineSymbol_0 = this.btnStyle.Style as ILineSymbol;
                    }
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        public void Cancel()
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        public void Hide()
        {
        }

        private void InitializeComponent()
        {
            this.groupBox2 = new GroupBox();
            this.label4 = new Label();
            this.label1 = new Label();
            this.cboFontSize = new ComboBox();
            this.cboFontName = new ComboBox();
            this.groupBox1 = new GroupBox();
            this.btnStyle = new StyleButton();
            this.rdoTick = new RadioButton();
            this.rdoLine = new RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboFontSize);
            this.groupBox2.Controls.Add(this.cboFontName);
            this.groupBox2.Location = new Point(8, 0x6d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x108, 0x71);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标注样式";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x11, 0x29);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 0x22;
            this.label4.Text = "大小:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x11, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0x21;
            this.label1.Text = "字体:";
            this.cboFontSize.Text = "5";
            this.cboFontSize.Location = new Point(0x39, 0x29);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(0x40, 0x15);
            this.cboFontSize.TabIndex = 0x20;
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.cboFontName.Location = new Point(0x39, 0x11);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xb8, 20);
            this.cboFontName.TabIndex = 0x1f;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.btnStyle);
            this.groupBox1.Controls.Add(this.rdoTick);
            this.groupBox1.Controls.Add(this.rdoLine);
            this.groupBox1.Location = new Point(8, 0x13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 0x54);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示属性";
            this.btnStyle.Location = new Point(0x40, 0x2b);
            this.btnStyle.Name = "btnStyle";
            this.btnStyle.Size = new Size(80, 0x20);
            this.btnStyle.Style = null;
            this.btnStyle.TabIndex = 2;
            this.btnStyle.Click += new EventHandler(this.btnStyle_Click);
            this.rdoTick.AutoSize = true;
            this.rdoTick.Checked = true;
            this.rdoTick.Location = new Point(0x81, 0x15);
            this.rdoTick.Name = "rdoTick";
            this.rdoTick.Size = new Size(0x2f, 0x10);
            this.rdoTick.TabIndex = 1;
            this.rdoTick.TabStop = true;
            this.rdoTick.Text = "十字";
            this.rdoTick.UseVisualStyleBackColor = true;
            this.rdoTick.CheckedChanged += new EventHandler(this.rdoTick_CheckedChanged);
            this.rdoLine.AutoSize = true;
            this.rdoLine.Location = new Point(0x13, 0x15);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new Size(0x3b, 0x10);
            this.rdoLine.TabIndex = 0;
            this.rdoLine.Text = "网格线";
            this.rdoLine.UseVisualStyleBackColor = true;
            this.rdoLine.CheckedChanged += new EventHandler(this.rdoLine_CheckedChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "StandardTickSymbolPropertyPage";
            base.Size = new Size(280, 240);
            base.Load += new EventHandler(this.StandardTickSymbolPropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void rdoLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoLine.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Style = this.ilineSymbol_0;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoTick_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoTick.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Style = this.imarkerSymbol_0;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
        }

        private void StandardTickSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.mapTemplate_0.GridSymbol != null)
            {
                this.btnStyle.Style = this.mapTemplate_0.GridSymbol;
                if (this.mapTemplate_0.GridSymbol is IMarkerSymbol)
                {
                    this.rdoTick.Checked = true;
                }
                if (this.mapTemplate_0.GridSymbol is ILineSymbol)
                {
                    this.rdoLine.Checked = true;
                }
            }
            else
            {
                this.btnStyle.Style = this.imarkerSymbol_0;
            }
            string fontName = this.mapTemplate_0.FontName;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (fontName == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.mapTemplate_0.BigFontSize.ToString();
            this.bool_0 = true;
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            get
            {
                return this.mapTemplate_0;
            }
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}


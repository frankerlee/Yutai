using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    public class LayerDisplaySetCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBoxEdit cboFields;
        private CheckEdit chkHyperline;
        private CheckEdit chkScaleSymbols;
        private CheckEdit chkShowMapTip;
        private Container container_0 = null;
        private GroupBox HyperLinkGroup;
        private ILayer ilayer_0;
        private Label label2;
        private Label label3;
        private RadioGroup rdoHyperLinkeType;
        private TextEdit txtPercent;

        public LayerDisplaySetCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.ilayer_0.ShowTips = this.chkShowMapTip.Checked;
                if (this.ilayer_0 is ILayerEffects)
                {
                    try
                    {
                        short num = Convert.ToInt16(this.txtPercent.Text);
                        if ((num >= 0) && (num <= 100))
                        {
                            (this.ilayer_0 as ILayerEffects).Transparency = num;
                        }
                    }
                    catch
                    {
                    }
                }
                if (this.ilayer_0 is IFeatureLayer2)
                {
                    (this.ilayer_0 as IFeatureLayer2).ScaleSymbols = this.chkScaleSymbols.Checked;
                }
                if (this.ilayer_0 is IHotlinkContainer)
                {
                    if (this.cboFields.SelectedIndex > 0)
                    {
                        (this.ilayer_0 as IHotlinkContainer).HotlinkField = (this.cboFields.SelectedItem as FieldWrapEx).Name;
                        (this.ilayer_0 as IHotlinkContainer).HotlinkType = (esriHyperlinkType) this.rdoHyperLinkeType.SelectedIndex;
                    }
                    else
                    {
                        (this.ilayer_0 as IHotlinkContainer).HotlinkField = "";
                    }
                }
            }
            return true;
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rdoHyperLinkeType.Enabled = this.cboFields.SelectedIndex > 0;
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void chkScaleSymbols_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void chkShowMapTip_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
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

        private void InitializeComponent()
        {
            this.chkShowMapTip = new CheckEdit();
            this.label2 = new Label();
            this.label3 = new Label();
            this.txtPercent = new TextEdit();
            this.chkScaleSymbols = new CheckEdit();
            this.HyperLinkGroup = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.rdoHyperLinkeType = new RadioGroup();
            this.chkHyperline = new CheckEdit();
            this.chkShowMapTip.Properties.BeginInit();
            this.txtPercent.Properties.BeginInit();
            this.chkScaleSymbols.Properties.BeginInit();
            this.HyperLinkGroup.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            this.rdoHyperLinkeType.Properties.BeginInit();
            this.chkHyperline.Properties.BeginInit();
            base.SuspendLayout();
            this.chkShowMapTip.Location = new Point(12, 12);
            this.chkShowMapTip.Name = "chkShowMapTip";
            this.chkShowMapTip.Properties.Caption = "显示地图提示(用主显示字段)";
            this.chkShowMapTip.Size = new Size(200, 0x13);
            this.chkShowMapTip.TabIndex = 2;
            this.chkShowMapTip.CheckedChanged += new EventHandler(this.chkShowMapTip_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 0x44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "透明度";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x8a, 0x3e);
            this.label3.Name = "label3";
            this.label3.Size = new Size(11, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "%";
            this.txtPercent.EditValue = "0";
            this.txtPercent.Location = new Point(0x52, 60);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new Size(0x30, 0x15);
            this.txtPercent.TabIndex = 5;
            this.txtPercent.EditValueChanged += new EventHandler(this.txtPercent_EditValueChanged);
            this.chkScaleSymbols.Location = new Point(12, 0x23);
            this.chkScaleSymbols.Name = "chkScaleSymbols";
            this.chkScaleSymbols.Properties.Caption = "设置参考比例后是否缩放符号";
            this.chkScaleSymbols.Size = new Size(200, 0x13);
            this.chkScaleSymbols.TabIndex = 6;
            this.chkScaleSymbols.CheckedChanged += new EventHandler(this.chkScaleSymbols_CheckedChanged);
            this.HyperLinkGroup.Controls.Add(this.cboFields);
            this.HyperLinkGroup.Controls.Add(this.rdoHyperLinkeType);
            this.HyperLinkGroup.Controls.Add(this.chkHyperline);
            this.HyperLinkGroup.Location = new Point(12, 0x57);
            this.HyperLinkGroup.Name = "HyperLinkGroup";
            this.HyperLinkGroup.Size = new Size(0x139, 0x80);
            this.HyperLinkGroup.TabIndex = 7;
            this.HyperLinkGroup.TabStop = false;
            this.HyperLinkGroup.Text = "超链接";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(0x90, 0x12);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0x90, 0x15);
            this.cboFields.TabIndex = 5;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.rdoHyperLinkeType.Location = new Point(6, 0x2d);
            this.rdoHyperLinkeType.Name = "rdoHyperLinkeType";
            this.rdoHyperLinkeType.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.rdoHyperLinkeType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoHyperLinkeType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoHyperLinkeType.Properties.Columns = 3;
            this.rdoHyperLinkeType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "文档"), new RadioGroupItem(null, "URL"), new RadioGroupItem(null, "宏") });
            this.rdoHyperLinkeType.Size = new Size(0xa6, 0x1f);
            this.rdoHyperLinkeType.TabIndex = 4;
            this.rdoHyperLinkeType.SelectedIndexChanged += new EventHandler(this.rdoHyperLinkeType_SelectedIndexChanged);
            this.chkHyperline.Location = new Point(6, 20);
            this.chkHyperline.Name = "chkHyperline";
            this.chkHyperline.Properties.Caption = "使用支持设置超链接";
            this.chkHyperline.Size = new Size(0x91, 0x13);
            this.chkHyperline.TabIndex = 3;
            base.Controls.Add(this.HyperLinkGroup);
            base.Controls.Add(this.chkScaleSymbols);
            base.Controls.Add(this.txtPercent);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.chkShowMapTip);
            base.Name = "LayerDisplaySetCtrl";
            base.Size = new Size(0x150, 0xe8);
            base.Load += new EventHandler(this.LayerDisplaySetCtrl_Load);
            this.chkShowMapTip.Properties.EndInit();
            this.txtPercent.Properties.EndInit();
            this.chkScaleSymbols.Properties.EndInit();
            this.HyperLinkGroup.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            this.rdoHyperLinkeType.Properties.EndInit();
            this.chkHyperline.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LayerDisplaySetCtrl_Load(object sender, EventArgs e)
        {
            this.chkShowMapTip.Checked = this.ilayer_0.ShowTips;
            if (this.ilayer_0 is ILayerEffects)
            {
                this.txtPercent.Text = (this.ilayer_0 as ILayerEffects).Transparency.ToString();
            }
            else
            {
                this.txtPercent.Enabled = false;
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                this.chkScaleSymbols.Checked = (this.ilayer_0 as IFeatureLayer2).ScaleSymbols;
                this.chkScaleSymbols.Enabled = true;
                this.chkShowMapTip.Enabled = true;
            }
            else
            {
                this.chkScaleSymbols.Enabled = false;
                this.chkShowMapTip.Enabled = true;
            }
            ILayerFields fields = this.ilayer_0 as ILayerFields;
            this.cboFields.Properties.Items.Clear();
            this.cboFields.Properties.Items.Add("<无>");
            if (this.ilayer_0 is IHotlinkContainer)
            {
                int num2 = 0;
                string hotlinkField = (this.ilayer_0 as IHotlinkContainer).HotlinkField;
                this.HyperLinkGroup.Visible = true;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    IFieldInfo info = fields.get_FieldInfo(i);
                    if ((field.Type == esriFieldType.esriFieldTypeString) && info.Visible)
                    {
                        this.cboFields.Properties.Items.Add(new FieldWrapEx(field, info));
                        if (field.Name == hotlinkField)
                        {
                            num2 = this.cboFields.Properties.Items.Count - 1;
                        }
                    }
                }
                this.cboFields.SelectedIndex = num2;
                this.rdoHyperLinkeType.SelectedIndex = (int) (this.ilayer_0 as IHotlinkContainer).HotlinkType;
            }
            else
            {
                this.HyperLinkGroup.Visible = false;
            }
            this.bool_0 = true;
        }

        private ITable method_0()
        {
            if (this.ilayer_0 == null)
            {
                return null;
            }
            if (this.ilayer_0 is IDisplayTable)
            {
                return (this.ilayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.ilayer_0 is IAttributeTable)
            {
                return (this.ilayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                return ((this.ilayer_0 as IFeatureLayer).FeatureClass as ITable);
            }
            return (this.ilayer_0 as ITable);
        }

        private void method_1(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void rdoHyperLinkeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        private void txtPercent_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        public ILayer Layer
        {
            set
            {
                this.ilayer_0 = value;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ilayer_0 = value as ILayer;
            }
        }
    }
}


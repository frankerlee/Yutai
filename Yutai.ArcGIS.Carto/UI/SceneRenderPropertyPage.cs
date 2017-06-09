using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public class SceneRenderPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBox cboDepthPriorityValue;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox chkIlluminate;
        private CheckBox chkSmoothShading;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private I3DProperties i3DProperties_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private ILayer ilayer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private RadioButton rdordoRenderCache;
        private RadioButton rdoRenderAlways;
        private RadioButton rdoRenderImmediate;
        private RadioButton rdoRenderWhenNavigating;
        private RadioButton rdoRenderWhenStopped;
        private TrackBar trackBar1;
        private NumericUpDown txtRenderRefreshRate;

        public SceneRenderPropertyPage()
        {
            this.InitializeComponent();
            this.Text = "渲染设置";
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.rdordoRenderCache.Checked)
                {
                    this.i3DProperties_0.RenderMode = esriRenderMode.esriRenderCache;
                }
                else if (this.rdoRenderImmediate.Checked)
                {
                    this.i3DProperties_0.RenderMode = esriRenderMode.esriRenderImmediate;
                }
                if (this.rdoRenderAlways.Checked)
                {
                    this.i3DProperties_0.RenderVisibility = esriRenderVisibility.esriRenderAlways;
                }
                else if (this.rdoRenderWhenNavigating.Checked)
                {
                    this.i3DProperties_0.RenderVisibility = esriRenderVisibility.esriRenderWhenNavigating;
                }
                else if (this.rdoRenderWhenStopped.Checked)
                {
                    this.i3DProperties_0.RenderVisibility = esriRenderVisibility.esriRenderWhenStopped;
                }
                this.i3DProperties_0.RenderRefreshRate = (double) this.txtRenderRefreshRate.Value;
                this.i3DProperties_0.Illuminate = this.chkIlluminate.Checked;
                this.i3DProperties_0.SmoothShading = this.chkSmoothShading.Checked;
                this.i3DProperties_0.DepthPriorityValue = (short) (this.cboDepthPriorityValue.SelectedIndex + 1);
            }
            return true;
        }

        private void cboDepthPriorityValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void chkIlluminate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void chkSmoothShading_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

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
            this.groupBox1 = new GroupBox();
            this.txtRenderRefreshRate = new NumericUpDown();
            this.label2 = new Label();
            this.label1 = new Label();
            this.rdoRenderWhenNavigating = new RadioButton();
            this.rdoRenderWhenStopped = new RadioButton();
            this.rdoRenderAlways = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.chkSmoothShading = new CheckBox();
            this.chkIlluminate = new CheckBox();
            this.cboDepthPriorityValue = new ComboBox();
            this.label3 = new Label();
            this.groupBox3 = new GroupBox();
            this.trackBar1 = new TrackBar();
            this.checkBox3 = new CheckBox();
            this.checkBox4 = new CheckBox();
            this.label4 = new Label();
            this.rdordoRenderCache = new RadioButton();
            this.rdoRenderImmediate = new RadioButton();
            this.groupBox1.SuspendLayout();
            this.txtRenderRefreshRate.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.trackBar1.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtRenderRefreshRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdoRenderWhenNavigating);
            this.groupBox1.Controls.Add(this.rdoRenderWhenStopped);
            this.groupBox1.Controls.Add(this.rdoRenderAlways);
            this.groupBox1.Location = new Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x181, 0x73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "可见性";
            int[] bits = new int[4];
            bits[0] = 1;
            bits[3] = 0x10000;
            this.txtRenderRefreshRate.Increment = new decimal(bits);
            this.txtRenderRefreshRate.Location = new Point(280, 90);
            this.txtRenderRefreshRate.Name = "txtRenderRefreshRate";
            this.txtRenderRefreshRate.Size = new Size(0x3f, 0x15);
            this.txtRenderRefreshRate.TabIndex = 5;
            int[] bits2 = new int[4];
            bits2[0] = 0x4b;
            bits2[3] = 0x20000;
            this.txtRenderRefreshRate.Value = new decimal(bits2);
            this.txtRenderRefreshRate.ValueChanged += new EventHandler(this.txtRenderRefreshRate_ValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x15d, 0x5c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "秒";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 90);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "导航时刷新频率大于以下数字时，简单绘制图层";
            this.rdoRenderWhenNavigating.AutoSize = true;
            this.rdoRenderWhenNavigating.Location = new Point(10, 0x42);
            this.rdoRenderWhenNavigating.Name = "rdoRenderWhenNavigating";
            this.rdoRenderWhenNavigating.Size = new Size(0x6b, 0x10);
            this.rdoRenderWhenNavigating.TabIndex = 2;
            this.rdoRenderWhenNavigating.Text = "导航时渲染图层";
            this.rdoRenderWhenNavigating.UseVisualStyleBackColor = true;
            this.rdoRenderWhenNavigating.CheckedChanged += new EventHandler(this.rdoRenderWhenNavigating_CheckedChanged);
            this.rdoRenderWhenStopped.AutoSize = true;
            this.rdoRenderWhenStopped.Location = new Point(6, 0x2c);
            this.rdoRenderWhenStopped.Name = "rdoRenderWhenStopped";
            this.rdoRenderWhenStopped.Size = new Size(0x83, 0x10);
            this.rdoRenderWhenStopped.TabIndex = 1;
            this.rdoRenderWhenStopped.Text = "导航停止时渲染图层";
            this.rdoRenderWhenStopped.UseVisualStyleBackColor = true;
            this.rdoRenderWhenStopped.CheckedChanged += new EventHandler(this.rdoRenderWhenStopped_CheckedChanged);
            this.rdoRenderAlways.AutoSize = true;
            this.rdoRenderAlways.Checked = true;
            this.rdoRenderAlways.Location = new Point(6, 0x16);
            this.rdoRenderAlways.Name = "rdoRenderAlways";
            this.rdoRenderAlways.Size = new Size(0x6b, 0x10);
            this.rdoRenderAlways.TabIndex = 0;
            this.rdoRenderAlways.TabStop = true;
            this.rdoRenderAlways.Text = "始终渲染该图层";
            this.rdoRenderAlways.UseVisualStyleBackColor = true;
            this.rdoRenderAlways.CheckedChanged += new EventHandler(this.rdoRenderAlways_CheckedChanged);
            this.groupBox2.Controls.Add(this.chkSmoothShading);
            this.groupBox2.Controls.Add(this.chkIlluminate);
            this.groupBox2.Controls.Add(this.cboDepthPriorityValue);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new Point(10, 0x7c);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x17d, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "效果";
            this.chkSmoothShading.AutoSize = true;
            this.chkSmoothShading.Location = new Point(6, 0x2a);
            this.chkSmoothShading.Name = "chkSmoothShading";
            this.chkSmoothShading.Size = new Size(0x60, 0x10);
            this.chkSmoothShading.TabIndex = 3;
            this.chkSmoothShading.Text = "使用平滑阴影";
            this.chkSmoothShading.UseVisualStyleBackColor = true;
            this.chkSmoothShading.CheckedChanged += new EventHandler(this.chkSmoothShading_CheckedChanged);
            this.chkIlluminate.AutoSize = true;
            this.chkIlluminate.Location = new Point(6, 20);
            this.chkIlluminate.Name = "chkIlluminate";
            this.chkIlluminate.Size = new Size(180, 0x10);
            this.chkIlluminate.TabIndex = 2;
            this.chkIlluminate.Text = "根据屏幕光源显示要素阴影区";
            this.chkIlluminate.UseVisualStyleBackColor = true;
            this.chkIlluminate.CheckedChanged += new EventHandler(this.chkIlluminate_CheckedChanged);
            this.cboDepthPriorityValue.FormattingEnabled = true;
            this.cboDepthPriorityValue.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            this.cboDepthPriorityValue.Location = new Point(0x86, 0x40);
            this.cboDepthPriorityValue.Name = "cboDepthPriorityValue";
            this.cboDepthPriorityValue.Size = new Size(0x79, 20);
            this.cboDepthPriorityValue.TabIndex = 1;
            this.cboDepthPriorityValue.SelectedIndexChanged += new EventHandler(this.cboDepthPriorityValue_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 0x43);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "设置阴影绘制优先级";
            this.groupBox3.Controls.Add(this.trackBar1);
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.checkBox4);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.rdordoRenderCache);
            this.groupBox3.Controls.Add(this.rdoRenderImmediate);
            this.groupBox3.Location = new Point(12, 230);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x17d, 0x4d);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "优化";
            this.trackBar1.Location = new Point(0xa6, 0x67);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new Size(0x68, 0x2d);
            this.trackBar1.TabIndex = 7;
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new Point(13, 0x57);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new Size(0x60, 0x10);
            this.checkBox3.TabIndex = 6;
            this.checkBox3.Text = "禁用材质纹理";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new Point(0x73, 0x57);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new Size(120, 0x10);
            this.checkBox4.TabIndex = 5;
            this.checkBox4.Text = "启用压缩纹理渲染";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(13, 0x74);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4d, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "栅格图像品质";
            this.rdordoRenderCache.AutoSize = true;
            this.rdordoRenderCache.Location = new Point(7, 0x2b);
            this.rdordoRenderCache.Name = "rdordoRenderCache";
            this.rdordoRenderCache.Size = new Size(0x65, 0x10);
            this.rdordoRenderCache.TabIndex = 1;
            this.rdordoRenderCache.Text = "使用图层Cache";
            this.rdordoRenderCache.UseVisualStyleBackColor = true;
            this.rdordoRenderCache.CheckedChanged += new EventHandler(this.rdordoRenderCache_CheckedChanged);
            this.rdoRenderImmediate.AutoSize = true;
            this.rdoRenderImmediate.Checked = true;
            this.rdoRenderImmediate.Location = new Point(7, 0x15);
            this.rdoRenderImmediate.Name = "rdoRenderImmediate";
            this.rdoRenderImmediate.Size = new Size(0x47, 0x10);
            this.rdoRenderImmediate.TabIndex = 0;
            this.rdoRenderImmediate.TabStop = true;
            this.rdoRenderImmediate.Text = "直接渲染";
            this.rdoRenderImmediate.UseVisualStyleBackColor = true;
            this.rdoRenderImmediate.CheckedChanged += new EventHandler(this.rdoRenderImmediate_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "SceneRenderPropertyPage";
            base.Size = new Size(0x1c9, 0x146);
            base.Load += new EventHandler(this.SceneRenderPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtRenderRefreshRate.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.trackBar1.EndInit();
            base.ResumeLayout(false);
        }

        private void rdordoRenderCache_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdoRenderAlways_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdoRenderImmediate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdoRenderWhenNavigating_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void rdoRenderWhenStopped_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void SceneRenderPropertyPage_Load(object sender, EventArgs e)
        {
            ILayerExtensions extensions = this.ilayer_0 as ILayerExtensions;
            for (int i = 0; i <= (extensions.ExtensionCount - 1); i++)
            {
                if (extensions.get_Extension(i) is I3DProperties)
                {
                    I3DProperties properties = extensions.get_Extension(i) as I3DProperties;
                    this.i3DProperties_0 = properties;
                    if (this.i3DProperties_0.RenderMode == esriRenderMode.esriRenderCache)
                    {
                        this.rdordoRenderCache.Checked = true;
                    }
                    else if (this.i3DProperties_0.RenderMode == esriRenderMode.esriRenderImmediate)
                    {
                        this.rdoRenderImmediate.Checked = true;
                    }
                    if (this.i3DProperties_0.RenderVisibility == esriRenderVisibility.esriRenderAlways)
                    {
                        this.rdoRenderAlways.Checked = true;
                    }
                    else if (this.i3DProperties_0.RenderVisibility == esriRenderVisibility.esriRenderWhenNavigating)
                    {
                        this.rdoRenderWhenNavigating.Checked = true;
                    }
                    else if (this.i3DProperties_0.RenderVisibility == esriRenderVisibility.esriRenderWhenStopped)
                    {
                        this.rdoRenderWhenStopped.Checked = true;
                    }
                    this.txtRenderRefreshRate.Value = (decimal) this.i3DProperties_0.RenderRefreshRate;
                    this.chkIlluminate.Checked = this.i3DProperties_0.Illuminate;
                    this.chkSmoothShading.Checked = this.i3DProperties_0.SmoothShading;
                    this.cboDepthPriorityValue.SelectedIndex = this.i3DProperties_0.DepthPriorityValue - 1;
                    break;
                }
            }
            this.bool_1 = true;
        }

        private void txtRenderRefreshRate_ValueChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
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


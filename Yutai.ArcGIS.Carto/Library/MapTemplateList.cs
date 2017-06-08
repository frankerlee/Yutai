using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    public class MapTemplateList : UserControl
    {
        private bool bool_0 = false;
        [CompilerGenerated]
        private bool bool_1;
        private IContainer icontainer_0 = null;
        private Label label1;
        private ListBox listBox1;
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = null;
        private MapTemplateGallery mapTemplateGallery_0 = new MapTemplateGallery();
        private Panel panel1;

        public MapTemplateList()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择模版!");
                return false;
            }
            this.mapTemplateApplyHelp_0.CartoTemplateData = this.listBox1.SelectedItem as MapTemplate;
            return true;
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        public int GetScale(string string_0)
        {
            if (string_0.Length >= 10)
            {
                switch (string_0[3])
                {
                    case 'B':
                        return 0x7a120;

                    case 'C':
                        return 0x3d090;

                    case 'D':
                        return 0x186a0;

                    case 'E':
                        return 0xc350;

                    case 'F':
                        return 0x61a8;

                    case 'G':
                        return 0x2710;

                    case 'H':
                        return 0x1388;

                    case 'I':
                        return 0x7d0;

                    case 'J':
                        return 0x3e8;

                    case 'K':
                        return 500;
                }
            }
            return 0;
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.listBox1 = new ListBox();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xec, 0x17);
            this.panel1.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择模板";
            this.listBox1.Dock = DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(0, 0x17);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0xec, 0xd0);
            this.listBox1.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.panel1);
            base.Name = "MapTemplateList";
            base.Size = new Size(0xec, 0xeb);
            base.Load += new EventHandler(this.MapTemplateList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void MapTemplateList_Load(object sender, EventArgs e)
        {
            this.mapTemplateGallery_0.Init();
            foreach (MapTemplateClass class2 in this.mapTemplateGallery_0.MapTemplateClass)
            {
                class2.Load();
                foreach (MapTemplate template in class2.MapTemplate)
                {
                    template.IsTest = false;
                    if (this.bool_0)
                    {
                        if (template.MapFramingType == MapFramingType.StandardFraming)
                        {
                            this.listBox1.Items.Add(template);
                        }
                    }
                    else if (this.mapTemplateApplyHelp_0.FixDataRange)
                    {
                        if (template.MapFramingType == MapFramingType.AnyFraming)
                        {
                            this.listBox1.Items.Add(template);
                        }
                    }
                    else if (!string.IsNullOrEmpty(this.mapTemplateApplyHelp_0.MapNo))
                    {
                        int scale = this.GetScale(this.mapTemplateApplyHelp_0.MapNo);
                        if (((scale != 0) && (template.MapFramingType == MapFramingType.StandardFraming)) && ((template.MapFrameType == MapFrameType.MFTTrapezoid) && (scale == template.Scale)))
                        {
                            this.listBox1.Items.Add(template);
                        }
                    }
                }
            }
        }

        public bool HasMosueClick
        {
            set
            {
                this.bool_0 = value;
            }
        }

        public bool IsInputTF
        {
            [CompilerGenerated]
            get
            {
                return this.bool_1;
            }
            [CompilerGenerated]
            set
            {
                this.bool_1 = value;
            }
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get
            {
                return this.mapTemplateApplyHelp_0;
            }
            set
            {
                this.mapTemplateApplyHelp_0 = value;
            }
        }

        public MapTemplate SelectCartoTemplateData
        {
            get
            {
                return (this.listBox1.SelectedItem as MapTemplate);
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.mapTemplateGallery_0.Workspace = value;
            }
        }
    }
}


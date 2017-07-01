using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class MapTemplateList : UserControl
    {
        private bool bool_0 = false;

        private IContainer icontainer_0 = null;
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = null;
        private MapTemplateGallery mapTemplateGallery_0 = new MapTemplateGallery();

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

        public int GetScale(string string_0)
        {
            if (string_0.Length >= 10)
            {
                switch (string_0[3])
                {
                    case 'B':
                        return 500000;

                    case 'C':
                        return 250000;

                    case 'D':
                        return 100000;

                    case 'E':
                        return 50000;

                    case 'F':
                        return 25000;

                    case 'G':
                        return 10000;

                    case 'H':
                        return 5000;

                    case 'I':
                        return 2000;

                    case 'J':
                        return 1000;

                    case 'K':
                        return 500;
                }
            }
            return 0;
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
                        if (((scale != 0) && (template.MapFramingType == MapFramingType.StandardFraming)) &&
                            ((template.MapFrameType == MapFrameType.MFTTrapezoid) && (scale == template.Scale)))
                        {
                            this.listBox1.Items.Add(template);
                        }
                    }
                }
            }
        }

        public bool HasMosueClick
        {
            set { this.bool_0 = value; }
        }

        public bool IsInputTF { get; set; }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get { return this.mapTemplateApplyHelp_0; }
            set { this.mapTemplateApplyHelp_0 = value; }
        }

        public MapTemplate SelectCartoTemplateData
        {
            get { return (this.listBox1.SelectedItem as MapTemplate); }
        }

        public IWorkspace Workspace
        {
            set { this.mapTemplateGallery_0.Workspace = value; }
        }
    }
}
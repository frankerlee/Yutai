using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class MapCoordinatePage : UserControl, IPropertyPage
    {
        private bool _isDirty = false;
        private IContainer icontainer_0 = null;
        private MapTemplateApplyHelp mapTemplateApplyHelp = null;
        private List<IIndexMap> _indexMaps;
        public MapCoordinatePage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.rdoMapNo.Checked)
            {
                this.mapTemplateApplyHelp.MapNo = this.txtMapNo.Text;
            }
            else if (this.rdoLeftDown.Checked)
            {
                this.mapTemplateApplyHelp.HasStrip = this.checkBox1.Checked;
                this.mapTemplateApplyHelp.XOffset = double.Parse(this.textBox1.Text);
                this.mapTemplateApplyHelp.SetJWD(double.Parse(this.txtJD.Text), double.Parse(this.txtWD.Text),
                    double.Parse(this.txtJC.Text), double.Parse(this.txtWC.Text), double.Parse(this.txtScale.Text));
            }
            if (this.rdoFieldSearch.Checked)
            {
                
            }
            else
            {
                IPoint point = new PointClass();
                point.PutCoords(double.Parse(this.txtLeftUpperX.Text), double.Parse(this.txtLeftUpperY.Text));
                IPoint point2 = new PointClass();
                point2.PutCoords(double.Parse(this.txtRightUpperX.Text), double.Parse(this.txtRightUpperY.Text));
                IPoint point3 = new PointClass();
                point3.PutCoords(double.Parse(this.txtRightLowX.Text), double.Parse(this.txtRightLowY.Text));
                IPoint point4 = new PointClass();
                point4.PutCoords(double.Parse(this.txtLeftLowX.Text), double.Parse(this.txtLeftLowY.Text));
                this.mapTemplateApplyHelp.SetRouneCoordinate(point4, point, point2, point3,
                    double.Parse(this.txtProjScale.Text));
            }
        }

        public bool CanApply()
        {
            if (this.rdoMapNo.Checked)
            {
                MapNoAssistant assistant = MapNoAssistantFactory.CreateMapNoAssistant(this.txtMapNo.Text);
                if (assistant == null)
                {
                    MessageService.Current.Warn("图号输入不正确!");
                    return false;
                }
                if (!assistant.Validate())
                {
                    MessageService.Current.Warn("图号输入不正确!");
                    return false;
                }
                return true;
            }
            if (this.rdoLeftDown.Checked)
            {
                try
                {
                    double.Parse(this.txtJD.Text);
                    double.Parse(this.txtWD.Text);
                    double.Parse(this.txtJC.Text);
                    double.Parse(this.txtWC.Text);
                    double.Parse(this.txtScale.Text);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            if (this.rdoFieldSearch.Checked)
            {
                if (cmbIndexMap.SelectedIndex < 0)
                {
                    MessageService.Current.Warn("请选择索引图!");
                    return false;
                }
                //if (string.IsNullOrEmpty(txtSearchKey.EditValue.ToString()))
                //{
                //    MessageService.Current.Warn("请输入搜素关键字!");
                //    return false;
                //}
                if (lstIndexFeatures.SelectedIndex < 0)
                {
                    MessageService.Current.Warn("请选择搜索到的索引要素!");
                    return false;
                }
                this.txtSearchKey.Tag = this.lstIndexFeatures.SelectedItem;
                return true;
            }
            try
            {
                double.Parse(this.txtLeftUpperX.Text);
                double.Parse(this.txtLeftUpperY.Text);
                double.Parse(this.txtRightUpperX.Text);
                double.Parse(this.txtRightUpperY.Text);
                double.Parse(this.txtRightLowX.Text);
                double.Parse(this.txtRightLowY.Text);
                double.Parse(this.txtLeftLowX.Text);
                double.Parse(this.txtLeftLowY.Text);
                return true;
            }
            catch
            {
                return false;
            }
           
          
        }

        public List<IIndexMap> IndexMaps { get { return _indexMaps; } set { _indexMaps = value; } }
        public void Cancel()
        {
        }

        private void MapCoordinatePage_Load(object sender, EventArgs e)
        {
            //加入初始化索引图过程
            if (_indexMaps == null) return;
            foreach (var indexMap in _indexMaps)
            {
                cmbIndexMap.Items.Add(indexMap.Name);
            }
        }

        private void rdoLeftDown_CheckedChanged(object sender, EventArgs e)
        {
            this.panel2.Visible = this.rdoLeftDown.Checked;
        }

        private void rdoCoordinate_CheckedChanged(object sender, EventArgs e)
        {
            this.panel3.Visible = this.rdoCoordinate.Checked;
        }

        private void rdoMapNo_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Visible = this.rdoMapNo.Checked;
        }
        private void rdoFieldSearch_CheckedChanged(object sender, EventArgs e)
        {
            this.panel4.Visible = this.rdoFieldSearch.Checked;
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
        }

        public bool IsPageDirty
        {
            get { return this._isDirty; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get { return this.mapTemplateApplyHelp; }
            set { this.mapTemplateApplyHelp = value; }
        }

        public string Title
        {
            get { return "坐标"; }
            set { }
        }

        private void btnKeySearch_Click(object sender, EventArgs e)
        {
            if (cmbIndexMap.SelectedIndex<0)
            {
                MessageService.Current.Warn("请选择索引图!");
                return ;
            }
            if (string.IsNullOrEmpty(txtSearchKey.EditValue.ToString()))
            {
                MessageService.Current.Warn("请输入搜素关键字!");
                return ;
            }
            IIndexMap indexMap = _indexMaps[cmbIndexMap.SelectedIndex];
            IFeatureCursor pCursor = indexMap.Search(txtSearchKey.Text);
            if (pCursor == null) return;
            IFeature pFeature = pCursor.NextFeature();
            int nameIdx = pCursor.FindField(indexMap.NameField);
            lstIndexFeatures.Items.Clear();
            while (pFeature != null)
            {
                string pName = nameIdx >= 0 ? pFeature.Value[nameIdx].ToString() : "";
                if (!pFeature.Shape.IsEmpty)
                {
                    IndexFeatureItem item = new IndexFeatureItem(pFeature.OID, pFeature.Shape.Envelope, pName);
                    lstIndexFeatures.Items.Add(item);
                }

                pFeature = pCursor.NextFeature();

            }
            ComReleaser.ReleaseCOMObject(pCursor);
        }

        private class IndexFeatureItem
        {
            public int _OID;
            public IEnvelope _envelope;
            public string _Name;

            public IndexFeatureItem(int OID, IEnvelope envelope1, string name)
            {
                _OID = OID;
                _envelope = envelope1;
                _Name = name;
            }
            public override string ToString()
            {
                return string.Format("{0}-{1}", _OID, _Name);
            }
        }
    }
}
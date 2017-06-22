using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmGeographicTransformationConvert : Form
    {
        private bool bool_0 = false;
        private IBasicMap ibasicMap_0 = null;
        private IContainer icontainer_0 = null;
        private int[] int_0 = new int[] { 4214, 4610, 4326 };
        private List<string> list_0 = new List<string>();

        public frmGeographicTransformationConvert()
        {
            this.InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.txtConvertMethod.Tag != null)
            {
                frmGaphicTransformation transformation = new frmGaphicTransformation {
                    GeoTransformations = this.txtConvertMethod.Tag as IGeoTransformation
                };
                if (transformation.ShowDialog() == DialogResult.OK)
                {
                    this.txtConvertMethod.Text = transformation.GeoTransformations.Name;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex > -1)
            {
                frmGaphicTransformation transformation = new frmGaphicTransformation();
                ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
                ISpatialReference reference = null;
                if (this.cboTarget.SelectedIndex == 0)
                {
                    reference = factory.CreateGeographicCoordinateSystem(4214);
                }
                else if (this.cboTarget.SelectedIndex == 1)
                {
                    reference = factory.CreateGeographicCoordinateSystem(4610);
                }
                else
                {
                    reference = factory.CreateGeographicCoordinateSystem(4326);
                }
                transformation.SourceSpatialReference = (this.listBox1.SelectedItem as ObjectWrap).Object as ISpatialReference;
                transformation.TargetSpatialReference = reference;
                if (transformation.ShowDialog() == DialogResult.OK)
                {
                    this.txtConvertMethod.Text = transformation.GeoTransformations.Name;
                    this.txtConvertMethod.Tag = transformation.GeoTransformations;
                    this.btnEdit.Enabled = true;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtConvertMethod.Tag != null)
                {
                    esriTransformDirection direction;
                    IGeoTransformation transformation;
                    IGeoTransformationOperationSet geographicTransformations = (this.ibasicMap_0 as IMapGeographicTransformations).GeographicTransformations;
                    IGeographicCoordinateSystem pFromGCS = (this.listBox1.SelectedItem as ObjectWrap).Object as IGeographicCoordinateSystem;
                    IGeographicCoordinateSystem pToGCS = (this.cboTarget.SelectedItem as ObjectWrap).Object as IGeographicCoordinateSystem;
                    geographicTransformations.Get(pFromGCS, pToGCS, out direction, out transformation);
                    if (transformation != null)
                    {
                        geographicTransformations.Remove(direction, transformation);
                    }
                    geographicTransformations.Set(esriTransformDirection.esriTransformForward, this.txtConvertMethod.Tag as IGeoTransformation);
                    geographicTransformations.Set(esriTransformDirection.esriTransformReverse, this.txtConvertMethod.Tag as IGeoTransformation);
                }
            }
            catch (Exception exception)
            {
                exception.ToString();
            }
            base.DialogResult = DialogResult.OK;
        }

        private void cboTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listBox1_SelectedIndexChanged(this, e);
            }
        }

 private void frmGeographicTransformationConvert_Load(object sender, EventArgs e)
        {
            int num;
            this.btnEdit.Enabled = false;
            this.btnNew.Enabled = false;
            this.cboTarget.Items.Clear();
            ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
            ISpatialReference reference = null;
            for (num = 0; num < this.int_0.Length; num++)
            {
                reference = factory.CreateGeographicCoordinateSystem(this.int_0[num]);
                this.cboTarget.Items.Add(new ObjectWrap(reference));
            }
            this.cboTarget.SelectedIndex = 0;
            if (this.ibasicMap_0.SpatialReference is IProjectedCoordinateSystem)
            {
                this.list_0.Add((this.ibasicMap_0.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name);
                this.listBox1.Items.Add(new ObjectWrap((this.ibasicMap_0.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem));
            }
            else if (this.ibasicMap_0.SpatialReference is IGeographicCoordinateSystem)
            {
                this.list_0.Add(this.ibasicMap_0.SpatialReference.Name);
                this.listBox1.Items.Add(new ObjectWrap(this.ibasicMap_0.SpatialReference));
            }
            for (num = 0; num < this.ibasicMap_0.LayerCount; num++)
            {
                ILayer layer = this.ibasicMap_0.get_Layer(num);
                if (layer is IGroupLayer)
                {
                    for (int i = 0; i < (layer as ICompositeLayer).Count; i++)
                    {
                        this.method_0((layer as ICompositeLayer).get_Layer(i));
                    }
                }
                else if (layer is IGeoDataset)
                {
                    string name;
                    IGeoDataset dataset = layer as IGeoDataset;
                    if (dataset.SpatialReference is IProjectedCoordinateSystem)
                    {
                        name = (this.ibasicMap_0.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name;
                        if (this.list_0.IndexOf(name) == -1)
                        {
                            this.list_0.Add((this.ibasicMap_0.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name);
                            this.listBox1.Items.Add(new ObjectWrap((dataset.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem));
                        }
                    }
                    else if (dataset.SpatialReference is IGeographicCoordinateSystem)
                    {
                        name = this.ibasicMap_0.SpatialReference.Name;
                        if (this.list_0.IndexOf(name) == -1)
                        {
                            this.list_0.Add(name);
                            this.listBox1.Items.Add(new ObjectWrap(dataset.SpatialReference));
                        }
                    }
                }
            }
            this.bool_0 = true;
            if (this.listBox1.Items.Count > 0)
            {
                this.listBox1.SelectedIndex = 0;
            }
        }

 private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnNew.Enabled = this.listBox1.SelectedIndex != -1;
            if ((this.bool_0 && (this.listBox1.SelectedIndex != -1)) && (this.cboTarget.SelectedIndex != -1))
            {
                IGeographicCoordinateSystem pFromGCS = (this.listBox1.SelectedItem as ObjectWrap).Object as IGeographicCoordinateSystem;
                IGeographicCoordinateSystem pToGCS = (this.cboTarget.SelectedItem as ObjectWrap).Object as IGeographicCoordinateSystem;
                IGeoTransformationOperationSet geographicTransformations = (this.ibasicMap_0 as IMapGeographicTransformations).GeographicTransformations;
                if (geographicTransformations != null)
                {
                    esriTransformDirection direction;
                    IGeoTransformation transformation;
                    geographicTransformations.Get(pFromGCS, pToGCS, out direction, out transformation);
                    if (transformation != null)
                    {
                        this.txtConvertMethod.Text = transformation.Name;
                        this.txtConvertMethod.Tag = transformation;
                    }
                    else
                    {
                        this.txtConvertMethod.Text = "";
                        this.txtConvertMethod.Tag = transformation;
                    }
                }
            }
        }

        private void method_0(ILayer ilayer_0)
        {
            if (ilayer_0 is ICompositeLayer)
            {
                for (int i = 0; i < (ilayer_0 as ICompositeLayer).Count; i++)
                {
                    this.method_0((ilayer_0 as ICompositeLayer).get_Layer(i));
                }
            }
            else if (ilayer_0 is IGeoDataset)
            {
                IGeoDataset dataset = ilayer_0 as IGeoDataset;
                if (dataset.SpatialReference != null)
                {
                    string name;
                    if (dataset.SpatialReference is IProjectedCoordinateSystem)
                    {
                        name = (dataset.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name;
                        if (this.list_0.IndexOf(name) == -1)
                        {
                            this.list_0.Add(name);
                            this.listBox1.Items.Add(new ObjectWrap((dataset.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem));
                        }
                    }
                    else if (dataset.SpatialReference is IGeographicCoordinateSystem)
                    {
                        name = this.ibasicMap_0.SpatialReference.Name;
                        if (this.list_0.IndexOf(name) == -1)
                        {
                            this.list_0.Add(name);
                            this.listBox1.Items.Add(new ObjectWrap(dataset.SpatialReference));
                        }
                        this.listBox1.Items.Add(new ObjectWrap(dataset.SpatialReference));
                    }
                }
            }
        }

        public IBasicMap Map
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }
    }
}


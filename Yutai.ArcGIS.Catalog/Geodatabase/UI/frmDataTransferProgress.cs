using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmDataTransferProgress : Form
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IEnumNameMapping ienumNameMapping_0 = null;
        private IGeoDBDataTransfer igeoDBDataTransfer_0 = null;
        private IName iname_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private int int_2 = 0;
        private int int_3 = 0;
        private int int_4 = 0;
        private string string_0 = "";

        public frmDataTransferProgress()
        {
            this.InitializeComponent();
        }

        public void BeginTransfer()
        {
            try
            {
                this.igeoDBDataTransfer_0.Transfer(this.ienumNameMapping_0, this.iname_0);
                this.bool_0 = true;
                this.bool_1 = true;
            }
            catch
            {
                this.bool_0 = true;
                this.bool_1 = false;
            }
        }

        private void frmDataTransferProgress_Load(object sender, EventArgs e)
        {
            this.method_7();
        }

        private void method_0(string string_1)
        {
            this.int_1++;
            this.lblObjectClass.Text =
                string.Concat(new object[] {"传送第 ", this.int_1.ToString(), "个对象，共 ", this.int_0, "个对象"});
            this.lblObject.Text = "传送" + string_1;
            this.progressBarObjectClass.Increment(1);
            this.string_0 = string_1;
            this.int_2 = 0;
            this.progressBarObject.Value = 0;
        }

        private bool method_1()
        {
            return false;
        }

        private void method_2(int int_5)
        {
            this.progressBarObject.Minimum = int_5;
        }

        private void method_3(int int_5)
        {
            this.progressBarObject.Maximum = int_5;
            int_5 = int_5;
        }

        private void method_4(int int_5)
        {
            this.progressBarObject.Value = int_5;
            this.int_2++;
            this.lblObject.Text = "传送" + this.string_0 + " ，第 " + this.int_2.ToString() + " 个对象";
        }

        private void method_5(int int_5)
        {
        }

        private void method_6()
        {
        }

        private void method_7()
        {
            this.ienumNameMapping_0.Reset();
            for (INameMapping mapping = this.ienumNameMapping_0.Next();
                mapping != null;
                mapping = this.ienumNameMapping_0.Next())
            {
                if (mapping.SourceObject is IName)
                {
                    IName sourceObject = mapping.SourceObject as IName;
                    if (sourceObject is IFeatureClassName)
                    {
                        this.int_0++;
                    }
                    else if (sourceObject is ITableName)
                    {
                        this.int_0++;
                    }
                    IEnumNameMapping children = mapping.Children;
                    if (children != null)
                    {
                        children.Reset();
                        for (INameMapping mapping3 = children.Next(); mapping3 != null; mapping3 = children.Next())
                        {
                            sourceObject = mapping3.SourceObject as IName;
                            if (sourceObject is IFeatureClassName)
                            {
                                this.int_0++;
                            }
                            else if (sourceObject is ITableName)
                            {
                                this.int_0++;
                            }
                        }
                    }
                }
            }
            this.progressBarObjectClass.Minimum = 0;
            this.progressBarObjectClass.Maximum = this.int_0;
        }

        private void method_8()
        {
            Thread thread = new Thread(new ThreadStart(this.BeginTransfer));
            thread.Start();
            while (!thread.IsAlive)
            {
            }
        }

        public IEnumNameMapping EnumNameMapping
        {
            set { this.ienumNameMapping_0 = value; }
        }

        public IGeoDBDataTransfer GeoDBTransfer
        {
            set
            {
                this.igeoDBDataTransfer_0 = value;
                (this.igeoDBDataTransfer_0 as IFeatureProgress_Event).Step +=
                    (new IFeatureProgress_StepEventHandler(this.method_6));
            }
        }

        public IName ToName
        {
            set { this.iname_0 = value; }
        }

        public bool TransferOk
        {
            get { return this.bool_0; }
        }

        public bool TransferSuccess
        {
            get { return this.bool_1; }
        }
    }
}
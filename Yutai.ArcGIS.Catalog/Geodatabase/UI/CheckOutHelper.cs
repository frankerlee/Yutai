using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class CheckOutHelper : CheckOutInHelper
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private IEnumName ienumName_0 = null;
        private IWorkspaceName iworkspaceName_0 = null;
        public static CheckOutHelper m_pHelper;
        private string string_0 = "";

        static CheckOutHelper()
        {
            old_acctor_mc();
        }

        public override void Do()
        {
            this.method_11();
        }

        private void method_11()
        {
            IReplicaDescription rDDescription = new ReplicaDescriptionClass();
            rDDescription.Init(this.ienumName_0, this.iworkspaceName_0, this.bool_1,
                esriDataExtractionType.esriDataCheckOut);
            rDDescription.ReplicaModelType = esriReplicaModelType.esriModelTypeFullGeodatabase;
            ICheckOut @out = new CheckOutClass();
            ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event event2 =
                @out as ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event;
            base.InitEvent(event2);
            try
            {
                if (this.bool_2)
                {
                    @out.CheckOutSchema(rDDescription, this.string_0);
                }
                else
                {
                    @out.CheckOutData(rDDescription, this.bool_0, this.string_0);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
            finally
            {
                base.ReleaseEvent(event2);
            }
        }

        private static void old_acctor_mc()
        {
            m_pHelper = null;
        }

        public bool CheckOnlySchema
        {
            set { this.bool_2 = value; }
        }

        public string CheckOutName
        {
            set { this.string_0 = value; }
        }

        public IWorkspaceName CheckoutWorkspaceName
        {
            get { return this.iworkspaceName_0; }
            set { this.iworkspaceName_0 = value; }
        }

        public IEnumName EnumName
        {
            get { return this.ienumName_0; }
            set { this.ienumName_0 = value; }
        }

        public bool ReuseSchema
        {
            set { this.bool_1 = value; }
        }

        public bool TransferRelObjects
        {
            set { this.bool_0 = value; }
        }
    }
}
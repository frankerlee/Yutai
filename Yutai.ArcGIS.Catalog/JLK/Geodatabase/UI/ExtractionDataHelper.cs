namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.GeoDatabaseDistributed;
    using System;

    public class ExtractionDataHelper : CheckOutInHelper
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private IEnumName ienumName_0 = null;
        private IWorkspaceName iworkspaceName_0 = null;
        public static ExtractionDataHelper m_pHelper;

        static ExtractionDataHelper()
        {
            old_acctor_mc();
        }

        public override void Do()
        {
            this.method_11();
        }

        private void method_11()
        {
            try
            {
                IReplicaDescription rSDescription = new ReplicaDescriptionClass();
                rSDescription.Init(this.ienumName_0, this.iworkspaceName_0, this.bool_1, esriDataExtractionType.esriDataExtraction);
                rSDescription.ReplicaModelType = esriReplicaModelType.esriModelTypeFullGeodatabase;
                IDataExtraction extraction = new DataExtractionClass();
                ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event event2 = extraction as ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event;
                base.InitEvent(event2);
                try
                {
                    if (this.bool_2)
                    {
                        extraction.ExtractSchema(rSDescription, null);
                    }
                    else
                    {
                        extraction.Extract(rSDescription, this.bool_0);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    base.ReleaseEvent(event2);
                }
            }
            catch
            {
            }
        }

        private static void old_acctor_mc()
        {
            m_pHelper = null;
        }

        public bool CheckOnlySchema
        {
            set
            {
                this.bool_2 = value;
            }
        }

        public IWorkspaceName CheckoutWorkspaceName
        {
            get
            {
                return this.iworkspaceName_0;
            }
            set
            {
                this.iworkspaceName_0 = value;
            }
        }

        public IEnumName EnumName
        {
            get
            {
                return this.ienumName_0;
            }
            set
            {
                this.ienumName_0 = value;
            }
        }

        public bool ReuseSchema
        {
            set
            {
                this.bool_1 = value;
            }
        }

        public bool TransferRelObjects
        {
            set
            {
                this.bool_0 = value;
            }
        }
    }
}


using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class CheckOutInHelper : ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event, IReplicaProgress_Event
    {
        protected IWorkspaceName m_MasterWorkspaceName = null;

        public event IReplicaProgress_StartupEventHandler Startup;

        public event ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_StepEventHandler Step;

        public virtual void Do()
        {
        }

        protected void InitEvent(ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event ifeatureProgress_Event_0)
        {
            ifeatureProgress_Event_0.Step+=(new ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_StepEventHandler(this.method_6));
            (ifeatureProgress_Event_0 as IReplicaProgress_Event).Startup+=(new IReplicaProgress_StartupEventHandler(this.method_10));
        }

        private bool method_0()
        {
            return false;
        }

        private void method_1(string string_0)
        {
        }

        private void method_10(esriReplicaProgress esriReplicaProgress_0)
        {
            if (this.Startup != null)
            {
                this.Startup(esriReplicaProgress_0);
            }
        }

        private void method_2(int int_0)
        {
        }

        private void method_3(int int_0)
        {
        }

        private void method_4(int int_0)
        {
        }

        private void method_5(int int_0)
        {
        }

        private void method_6()
        {
            if (this.Step != null)
            {
                this.Step();
            }
        }

        private void method_7(esriReplicaProgress esriReplicaProgress_0)
        {
        }

        private void method_8(int int_0)
        {
        }

        private void method_9(int int_0)
        {
        }

        protected void ReleaseEvent(ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_Event ifeatureProgress_Event_0)
        {
            ifeatureProgress_Event_0.Step-=(new ESRI.ArcGIS.GeoDatabaseDistributed.IFeatureProgress_StepEventHandler(this.method_6));
            (ifeatureProgress_Event_0 as IReplicaProgress_Event).Startup-=(new IReplicaProgress_StartupEventHandler(this.method_10));
        }

        public IWorkspaceName MasterWorkspaceName
        {
            get
            {
                return this.m_MasterWorkspaceName;
            }
            set
            {
                this.m_MasterWorkspaceName = value;
            }
        }
    }
}


using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Pipeline.Config.Concretes
{
    public class PipeWorkspaceInfo
    {
        private IWorkspace m_workspace = null;
        private IArray m_array = new ESRI.ArcGIS.esriSystem.Array();
        public IArray ClassArray
        {
            get { return m_array; }
        }

        public IWorkspace Workspace
        {
            get { return m_workspace; }
        }
        public PipeWorkspaceInfo(IWorkspace workspace)
        {
            m_workspace = workspace;
        }

        public void AddClass(IFeatureClass pClass)
        {
            for(int i=0; i<m_array.Count;i++)
            {
                IFeatureClass pClass1 = m_array.Element[i] as IFeatureClass;
                if (pClass == pClass1) return;
            }
            m_array.Add(pClass);
        }
    }
}

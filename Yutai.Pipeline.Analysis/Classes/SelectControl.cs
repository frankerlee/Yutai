using System.Collections;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class SelectControl
    {
        public IAppContext m_pApp;

        private string string_0 = "";

        private ArrayList arrayList_0 = new ArrayList();

        public ArrayList m_arrSortInfo = new ArrayList();

        public string LayerName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public int Count
        {
            get
            {
                return this.arrayList_0.Count;
            }
        }

        public SelectControl(IAppContext pApp)
        {
            this.m_pApp = pApp;
        }

        public bool IsInSameLayer(string strLayerName)
        {
            return this.LayerName == "" || this.LayerName == strLayerName;
        }

        private int GetIndex(int num)
        {
            int result;
            for (int i = 0; i < this.Count; i++)
            {
                int num2 = (int)this.arrayList_0[i];
                if (num2 == num)
                {
                    result = i;
                    return result;
                }
            }
            result = -1;
            return result;
        }

        public void Add(int nOID)
        {
            int num = this.GetIndex(nOID);
            if (num == -1)
            {
                this.arrayList_0.Add(nOID);
            }
            else
            {
                this.arrayList_0.RemoveAt(num);
                if (this.Count == 0)
                {
                    this.LayerName = "";
                    this.m_pApp.FocusMap.ClearSelection();
                    this.m_pApp.ActiveView.Refresh();
                    return;
                }
            }
            this.RebuildSelection();
        }

        private void method_1(ILayer layer)
        {
            if (layer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = layer as IFeatureLayer;
                if (featureLayer.FeatureClass != null)
                {
                    string aliasName = featureLayer.FeatureClass.AliasName;
                    if (aliasName == this.LayerName)
                    {
                        IFeatureSelection featureSelection = (IFeatureSelection)layer;
                        ISelectionSet selectionSet = featureSelection.SelectionSet;
                        selectionSet.IDs.Reset();
                        for (int i = 0; i < this.Count; i++)
                        {
                            selectionSet.Add((int)this.arrayList_0[i]);
                        }
                    }
                }
            }
        }

        public void SortForVerAnalyseFun(ILayer pLayer)
        {
            if (pLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = pLayer as IFeatureLayer;
                string aliasName = featureLayer.FeatureClass.AliasName;
                if (aliasName == this.LayerName)
                {
                    IFeatureSelection featureSelection = (IFeatureSelection)pLayer;
                    ISelectionSet selectionSet = featureSelection.SelectionSet;
                    selectionSet.IDs.Reset();
                    for (int i = 0; i < this.Count; i++)
                    {
                        SortInfo sortInfo = (SortInfo)this.m_arrSortInfo[i];
                        selectionSet.Add(sortInfo.SmID);
                    }
                }
            }
        }

        public void RebuildSelection()
        {
            this.m_pApp.FocusMap.ClearSelection();
            CommonUtils.ThrougAllLayer(this.m_pApp.FocusMap, new CommonUtils.DealLayer(this.method_1));
            this.m_pApp.ActiveView.Refresh();
        }

        public bool SortForVerAnalyse()
        {
            this.m_pApp.FocusMap.ClearSelection();
            CommonUtils.ThrougAllLayer(this.m_pApp.FocusMap, new CommonUtils.DealLayer(this.SortForVerAnalyseFun));
            return false;
        }

        public void Clear()
        {
            this.arrayList_0.Clear();
        }
    }
}
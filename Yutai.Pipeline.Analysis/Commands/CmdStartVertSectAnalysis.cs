using System;
using System.Collections;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using stdole;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Cursor = System.Windows.Forms.Cursor;

namespace Yutai.Pipeline.Analysis.Commands
{
    class CmdStartVertSectAnalysis : YutaiTool
    {
        
        public SelectControl m_SectionControl;

        private PipelineAnalysisPlugin _plugin;


        public CmdStartVertSectAnalysis(IAppContext context, PipelineAnalysisPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick()
        {
            
            _context.SetCurrentTool(this);
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "纵断面分析";
            base.m_category = "PipelineAnalysus";
            base.m_bitmap = Properties.Resources.icon_versect;
            base.m_name = "PipeAnalysis_VertSectionStart";
            base._key = "PipeAnalysis_VertSectionStart";
            base.m_toolTip = "纵断面分析";
            base.m_checked = false;
            base.m_message = "纵断面分析";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;

            this.m_SectionControl = new SelectControl(_context);
            this.m_SectionControl.Clear();
            _context.FocusMap.ClearSelection();
            _context.ActiveView.Refresh();
            CommonUtils.AppContext = _context;
        }

        private void method_0(int x, int y)
        {
            IMap map = _context.FocusMap;
            IEnvelope envelope = new Envelope() as IEnvelope;
            IPoint point = _context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IActiveView activeView = (IActiveView)map;
            envelope.PutCoords(point.X, point.Y, point.X, point.Y);
            double num3 = activeView.Extent.Width / 200.0;
            envelope.XMin=(envelope.XMin - num3);
            envelope.YMin=(envelope.YMin - num3);
            envelope.YMax=(envelope.YMax + num3);
            envelope.XMax=(envelope.XMax + num3);
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            map.SelectByShape(envelope, selectionEnvironment, true);
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if ( keyCode == 27)
            {
              
            }
        }

        public override void OnMouseDown(int button, int Shift, int x, int y)
        {
            if (button == 1)
            {
                this.method_0(x, y);
                IMap map = _context.FocusMap;
                IEnumFeature enumFeature = (IEnumFeature)map.FeatureSelection;
                IFeature feature = enumFeature.Next();
                if (feature == null)
                {
                    if (Shift == 1)
                    {
                        this.m_SectionControl.RebuildSelection();
                    }
                    else
                    {
                        this.m_SectionControl.LayerName = "";
                        this.m_SectionControl.Clear();
                        this._context.ActiveView.Refresh();
                    }
                }
                else
                {
                    string aliasName = feature.Class.AliasName;
                    int oID;
                    if (feature.HasOID)
                    {
                        oID = feature.OID;
                    }
                    else
                    {
                        oID = feature.OID;
                    }
                    if (Shift != 1)
                    {
                        this.m_SectionControl.Clear();
                        this.m_SectionControl.LayerName = aliasName;
                        this.m_SectionControl.Add(oID);
                    }
                    else if (this.m_SectionControl.Count == 0)
                    {
                        this.m_SectionControl.LayerName = aliasName;
                        this.m_SectionControl.Add(oID);
                    }
                    else if (this.m_SectionControl.IsInSameLayer(aliasName))
                    {
                        this.m_SectionControl.LayerName = aliasName;
                        this.m_SectionControl.Add(oID);
                    }
                }
            }
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            if (button == 2)
            {
                this.m_SectionControl.m_arrSortInfo.Clear();
                this.GetSortInfos(this.m_SectionControl.m_arrSortInfo);
                bool flag;
                if (this.m_SectionControl.m_arrSortInfo.Count == 0)
                {
                    MessageBox.Show("没有选择到管线！", "纵断面分析");
                }
                else if (!(flag = !this.method_1(this.m_SectionControl.m_arrSortInfo)))
                {
                    MessageBox.Show("有三通情况不能分析！", "纵断面分析");
                }
                else if (!this.method_3(this.m_SectionControl.m_arrSortInfo))
                {
                    MessageBox.Show("所选择的管线不是连接的！", "纵断面分析");
                }
                else
                {
                    if (flag)
                    {
                        SectionViewFrm sectionViewFrm = new SectionViewFrm(SectionViewFrm.SectionType.SectionTypeVersect, _context,_plugin.PipeConfig);
                        sectionViewFrm.GetSelectedData();
                        sectionViewFrm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("没有首尾相接选择管线！", "纵断面分析");
                    }
                    this._context.FocusMap.ClearSelection();
                    this._context.ActiveView.Refresh();
                    this.m_SectionControl.Clear();
                }
            }
        }

        public void GetBaseLine()
        {
            IMap map = _context.FocusMap;
            IEnumFeature enumFeature = (IEnumFeature)map.FeatureSelection;
            if (enumFeature.Next() != null)
            {
                while (enumFeature.Next() != null)
                {
                }
            }
        }

        private bool method_1(ArrayList arrayList)
        {
            Hashtable hashtable = new Hashtable();
            int count = arrayList.Count;
            hashtable.Clear();
            for (int i = 0; i < count; i++)
            {
                SortInfo sortInfo = (SortInfo)arrayList[i];
                if (hashtable[sortInfo.SmFNode] == null)
                {
                    hashtable[sortInfo.SmFNode] = 1;
                }
                else
                {
                    int num = (int)hashtable[sortInfo.SmFNode];
                    hashtable[sortInfo.SmFNode] = num + 1;
                }
                if (hashtable[sortInfo.SmTNode] == null)
                {
                    hashtable[sortInfo.SmTNode] = 1;
                }
                else
                {
                    int num2 = (int)hashtable[sortInfo.SmTNode];
                    hashtable[sortInfo.SmTNode] = num2 + 1;
                }
            }
            bool result = false;
            foreach (DictionaryEntry dictionaryEntry in hashtable)
            {
                if ((int)dictionaryEntry.Value > 2)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void GetSortInfos(ArrayList pSortInfos)
        {
            IMap map = _context.FocusMap;
            IEnumFeature enumFeature = (IEnumFeature)map.FeatureSelection;
            IFeature feature = enumFeature.Next();
            if (feature != null)
            {
                do
                {
                    IEdgeFeature edgeFeature = (IEdgeFeature)feature;
                    pSortInfos.Add(new SortInfo
                    {
                        SmID = Convert.ToInt32(feature.get_Value(0).ToString()),
                        SmFNode = edgeFeature.FromJunctionEID,
                        SmTNode = edgeFeature.ToJunctionEID
                    });
                    feature = enumFeature.Next();
                }
                while (feature != null);
            }
        }

        private void method_2(ArrayList arrayList)
        {
            int count = arrayList.Count;
            for (int i = 0; i < count - 1; i++)
            {
                SortInfo sortInfo = (SortInfo)arrayList[i];
                SortInfo sortInfo2 = (SortInfo)arrayList[i + 1];
                int pointInfoRleation = sortInfo.GetPointInfoRleation(sortInfo2);
                if (pointInfoRleation == 21)
                {
                    sortInfo.bRightDirection = true;
                    sortInfo2.bRightDirection = true;
                }
                if (pointInfoRleation == 22)
                {
                    sortInfo.bRightDirection = true;
                    sortInfo2.bRightDirection = false;
                }
                if (pointInfoRleation == 12)
                {
                    sortInfo.bRightDirection = false;
                    sortInfo2.bRightDirection = false;
                }
                if (pointInfoRleation == 11)
                {
                    sortInfo.bRightDirection = false;
                    sortInfo2.bRightDirection = true;
                }
            }
        }

        private bool method_3(ArrayList arrayList)
        {
            ArrayList arrayList2 = new ArrayList();
            long num = 0L;
            bool result;
            while (arrayList.Count > 0)
            {
                num += 1L;
                if (num > 50000L)
                {
                    result = false;
                    return result;
                }
                long num2 = (long)arrayList.Count;
                int num3 = 0;
                while ((long)num3 < num2)
                {
                    SortInfo sortInfo = (SortInfo)arrayList[num3];
                    if (arrayList2.Count == 0)
                    {
                        arrayList2.Add(sortInfo);
                        arrayList.Remove(sortInfo);
                        break;
                    }
                    SortInfo sortInfo2 = (SortInfo)arrayList2[0];
                    SortInfo sortInfo3 = (SortInfo)arrayList2[arrayList2.Count - 1];
                    if (sortInfo2.GetPointInfoRleation(sortInfo) != -1 || sortInfo3.GetPointInfoRleation(sortInfo) != -1)
                    {
                        int pointInfoRleation = sortInfo2.GetPointInfoRleation(sortInfo);
                        int pointInfoRleation2 = sortInfo3.GetPointInfoRleation(sortInfo);
                        if (pointInfoRleation == 12)
                        {
                            sortInfo.bRightDirection = true;
                            arrayList2.Insert(0, sortInfo);
                            arrayList.Remove(sortInfo);
                            break;
                        }
                        if (pointInfoRleation == 11)
                        {
                            sortInfo.bRightDirection = false;
                            sortInfo.SwapFromWithTo();
                            arrayList2.Insert(0, sortInfo);
                            arrayList.Remove(sortInfo);
                            break;
                        }
                        if (pointInfoRleation2 == 21)
                        {
                            sortInfo.bRightDirection = true;
                            arrayList2.Add(sortInfo);
                            arrayList.Remove(sortInfo);
                            break;
                        }
                        if (pointInfoRleation2 == 22)
                        {
                            sortInfo.bRightDirection = false;
                            arrayList2.Add(sortInfo);
                            arrayList.Remove(sortInfo);
                            break;
                        }
                    }
                    num3++;
                }
            }
            this.m_SectionControl.m_arrSortInfo = arrayList2;
            this.m_SectionControl.SortForVerAnalyse();
            result = true;
            return result;
        }
    


}
}
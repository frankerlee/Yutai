using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.NetworkAnalysis;
using Yutai.ArcGIS.Common;
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class frmBurstReport : Form
	{
		private struct NearestEdgeInfo
		{
			public IGeometricNetwork GeometricNetwork;

			public int EdgeID;

			public IPoint Location;

			public double Percent;
		}

		private struct NetworkInfo
		{
			public IFeatureLayer LayerLine;

			public IFeatureLayer LayerPoint;

			public IFeature LineFeature;

			public ArrayList arrayList_0;

			public ArrayList arrayList_1;

			public NetworkInfo(IFeatureLayer featureLayer, IFeatureLayer featureLayer2, IFeature feature, ArrayList arrayList, ArrayList arrayList2)
			{
				this.LayerLine = featureLayer;
				this.LayerPoint = featureLayer2;
				this.LineFeature = feature;
				this.arrayList_0 = arrayList;
				this.arrayList_1 = arrayList2;
			}
		}

		private IContainer icontainer_0 = null;

        
		public IAppContext m_iApp;

	    public IPipelineConfig m_Config;


		private frmBurstReport.NearestEdgeInfo _nearestEdgeInfo = default(frmBurstReport.NearestEdgeInfo);

		private frmBurstReport.NetworkInfo _networkInfo = new frmBurstReport.NetworkInfo(null, null, null, new ArrayList(), new ArrayList());


		public int m_nTimerCounter;


	public IQueryFilter GetBarrierQuery()
		{
		    IQueryFilter queryFilter = new QueryFilter();
			ArrayList arrayList = new ArrayList();
			GetValues(arrayList);
			for (int i = 0; i < this.listFieldValues.Items.Count; i++)
			{
				if (i > 0)
				{
					IQueryFilter queryFilter2 = queryFilter;
					IQueryFilter expr_22 = queryFilter2;
					expr_22.WhereClause=(expr_22.WhereClause + " or ");
				}
				IQueryFilter queryFilter3 = queryFilter;
				IQueryFilter expr_3C = queryFilter3;
				expr_3C.WhereClause=(expr_3C.WhereClause + this.label1.Text);
				IQueryFilter queryFilter4 = queryFilter;
				queryFilter4.WhereClause=(queryFilter4.WhereClause + "='" + this.listFieldValues.Items[i].ToString() + "'");
			}
			arrayList.Clear();
			return queryFilter;
		}

		public esriFlowMethod GetFindMethod()
		{
			esriFlowMethod result;
			if (this.radioDown.Checked)
			{
				result = esriFlowMethod.esriFMDownstream;
			}
			else if (this.radioUp.Checked)
			{
				result = esriFlowMethod.esriFMUpstream;
			}
			else
			{
				result = esriFlowMethod.esriFMConnected;
			}
			return result;
		}

		public ISelectionSetBarriers GetSelectionSetBarries()
		{
			int featureClassID = this._networkInfo.LayerPoint.FeatureClass.FeatureClassID;
		    ISelectionSetBarriers selectionSetBarriers = new SelectionSetBarriers();
			IFeatureSelection featureSelection = (IFeatureSelection)this._networkInfo.LayerPoint;
			IQueryFilter barrierQuery = this.GetBarrierQuery();
			featureSelection.SelectFeatures(barrierQuery, 0, false);
			IEnumIDs iDs = featureSelection.SelectionSet.IDs;
			iDs.Reset();
			for (int i = iDs.Next(); i > 0; i = iDs.Next())
			{
				if (this.listOutBarriers.Items.IndexOf(i.ToString()) == -1)
				{
					selectionSetBarriers.Add(featureClassID, i);
				}
			}
			return selectionSetBarriers;
		}

		public ITraceFlowSolver GetTraceFlowSolver()
		{
			INetElements netElements = (INetElements)this._nearestEdgeInfo.GeometricNetwork.Network;
			int userClassID = 0;
			int userID = 0;
			int userSubID = 0;
			netElements.QueryIDs(this._nearestEdgeInfo.EdgeID, esriElementType.esriETEdge, out userClassID, out userID, out userSubID);
		    IEdgeFlag edgeFlag = new EdgeFlag();
			((INetFlag)edgeFlag).UserClassID=(userClassID);
			((INetFlag)edgeFlag).UserID=(userID);
			((INetFlag)edgeFlag).UserSubID=(userSubID);
		    ITraceFlowSolver traceFlowSolver = (ITraceFlowSolver) new TraceFlowSolver();
			((INetSolver)traceFlowSolver).SourceNetwork=(this._nearestEdgeInfo.GeometricNetwork.Network);
			((INetSolver)traceFlowSolver).SelectionSetBarriers=(this.GetSelectionSetBarries());
			traceFlowSolver.PutEdgeOrigins(1, ref edgeFlag);
			return traceFlowSolver;
		}

		public frmBurstReport()
		{
			this.InitializeComponent();
		}

		private void frmBurstReport_Load(object obj, EventArgs eventArgs)
		{
			ArrayList arrayList = new ArrayList();
			CMapOperator.GetMapILayers(m_iApp.FocusMap, null, arrayList);
			//string pointTableFieldName = m_Config.GetPointTableFieldName("点性");
			//this.label1.Text = pointTableFieldName;
			for (int i = 0; i < arrayList.Count; i++)
			{
				IFeatureLayer featureLayer = arrayList[i] as IFeatureLayer;
			    if (featureLayer == null || featureLayer.FeatureClass == null) continue;
                if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint) continue;

                //! 在这儿修改判断条件，判断图层为点图层而且为管线点图层的方式需要做修改
			    IBasicLayerInfo pipePoint = m_Config.GetBasicLayerInfo(featureLayer.FeatureClass.AliasName) as IBasicLayerInfo;

                if (pipePoint!=null)
				{
					IFeatureClass featureClass = featureLayer.FeatureClass;
                    this.label1.Text = pipePoint.GetFieldName(PipeConfigWordHelper.PointWords.TZW);
                    int num = featureClass.Fields.FindField(pipePoint.GetFieldName(PipeConfigWordHelper.PointWords.TZW));
					if (num != -1)
					{
						ArrayList uVByQueryDef = this.GetUVByQueryDef(featureLayer, pipePoint.GetFieldName(PipeConfigWordHelper.PointWords.TZW));
						for (int j = 0; j < uVByQueryDef.Count; j++)
						{
							string text = uVByQueryDef[j].ToString();
							if (text.Trim() != "" && !this.comboBox1.Items.Contains(text))
							{
								this.comboBox1.Items.Add(text);
							}
							this.comboBox1.Items.Add(text);
						}
					}
				}
			}
			if (this.comboBox1.Items.Count > 0)
			{
				this.comboBox1.SelectedIndex = 0;
			}
			
			ArrayList arrayList2 = new ArrayList();
			GetValues(arrayList2);
			for (int j = 0; j < arrayList2.Count; j++)
			{
				this.listFieldValues.Items.Add(arrayList2[j].ToString());
			}
			if (this.listFieldValues.Items.Count == 0)
			{
				this.listFieldValues.Items.Add("阀门");
				this.listFieldValues.Items.Add("阀门井");
			}
			this.radioUpAndDown.Checked = true;
		}

		public ArrayList GetUVByQueryDef(IFeatureLayer pFeatureLayer, string strField)
		{
			IDataset dataset = (IDataset)pFeatureLayer.FeatureClass;
			IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)dataset.Workspace;
			IQueryDef queryDef = featureWorkspace.CreateQueryDef();
			queryDef.Tables=(dataset.Name);
			queryDef.SubFields=("DISTINCT(" + strField + ")");
			ICursor cursor = queryDef.Evaluate();
			IRow row = cursor.NextRow();
			ArrayList arrayList = new ArrayList();
			while (row != null)
			{
				object obj = row.get_Value(0);
				arrayList.Add(obj.ToString());
				row = cursor.NextRow();
			}
			return arrayList;
		}

		public void SetMousePoint(int x, int y)
		{
			this.Cursor = Cursors.WaitCursor;
			try
			{
				IPoint pMousePoint = m_iApp.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
				this.lblPickPipeInfo.Text = "";
				if (this.btnPickBrokePipe.Checked)
				{
					this.listOutBarriers.Items.Clear();
					if (this.PickBrokePipe(pMousePoint))
					{
						this.InitBrokePoint();
						this.btnStartAnalysis_Click(null, null);
					}
				}
				else if (this.btnAddThroughValve.Checked)
				{
					this.PickThroughValve(pMousePoint);
				}
			}
			catch
			{
			}
			this.Cursor = Cursors.Default;
		}
        //! 通过点击获取需要分析的管线
		public bool PickBrokePipe(IPoint _pMousePoint)
		{
			this.InitClick();
			this.ipoint_0 = _pMousePoint;
			bool flag = false;
			double num = 2147483647.0;
			ArrayList arrayList = new ArrayList();
			GetMapVisibleILayers(m_iApp.FocusMap, null, arrayList);
			for (int i = 0; i < arrayList.Count; i++)
			{
				IFeatureLayer featureLayer = arrayList[i] as IFeatureLayer;
				if (featureLayer != null && featureLayer.FeatureClass != null && featureLayer.Visible && m_Config.IsPipelineLayer(featureLayer.Name, enumPipelineDataType.Line))
				{
					IFeatureDataset featureDataset = featureLayer.FeatureClass.FeatureDataset;
					IFeatureClassContainer featureClassContainer = featureDataset as IFeatureClassContainer;
					IFeatureClass featureClass = featureClassContainer.get_Class(1);
					if (featureClass != null && featureClass is INetworkClass)
					{
						IGeometricNetwork geometricNetwork = ((INetworkClass)featureClass).GeometricNetwork;
						IPointToEID pointToEIDClass = new PointToEID();
						pointToEIDClass.SourceMap=(m_iApp.FocusMap);
						pointToEIDClass.GeometricNetwork=(geometricNetwork);
						pointToEIDClass.SnapTolerance=(m_iApp.ActiveView.Extent.Width / 200.0);
						int edgeID = 0;
						IPoint location = null;
						double percent = 0;
						pointToEIDClass.GetNearestEdge(this.ipoint_0, out edgeID, out location, out percent);
						if (location != null && num > percent)
						{
							num = percent;
							this._nearestEdgeInfo.GeometricNetwork = geometricNetwork;
							this._nearestEdgeInfo.Percent = percent;
							this._nearestEdgeInfo.EdgeID = edgeID;
							this._nearestEdgeInfo.Location = location;
							flag = true;
						}
					}
				}
			}
			arrayList.Clear();
			if (flag)
			{
				this.DrawBrokeEdge();
			}
			return flag;
		}

		private void InitBrokePoint()
		{
            int userClassID;
            int userID;
            int userSubID;
            IFeatureClassContainer featureDataset = (IFeatureClassContainer)this._nearestEdgeInfo.GeometricNetwork.FeatureDataset;
            IFeatureClass pointClass = null;
            IFeatureClass lineClass = null;
            int num3 = 0;
            for(int i=0;i<featureDataset.ClassCount;i++)
            {
                    IFeatureClass pclass = featureDataset.Class[i];
                    if ((pclass.ShapeType != esriGeometryType.esriGeometryPoint ? false : !pclass.AliasName.Contains("Junctions")))
                    {
                        pointClass = pclass;
                        break;
                    }
            }
            for (int i = 0; i < featureDataset.ClassCount; i++)
            {
                IFeatureClass pclass = featureDataset.Class[i];

                if (pclass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    lineClass = pclass;
                    break;
                }
            }
            this._networkInfo.LayerLine = MapHelper.FindFeatureLayerByFCName(m_iApp.FocusMap as IBasicMap, ((IDataset)lineClass).Name,false) as IFeatureLayer;
            this._networkInfo.LayerPoint = MapHelper.FindFeatureLayerByFCName(m_iApp.FocusMap as IBasicMap, ((IDataset)pointClass).Name, false) as IFeatureLayer;
            INetElements network = (INetElements)this._nearestEdgeInfo.GeometricNetwork.Network;
            network.QueryIDs(this._nearestEdgeInfo.EdgeID, esriElementType.esriETEdge, out userClassID, out userID, out userSubID);
            this._networkInfo.LineFeature = lineClass.GetFeature(userID);
            Label label = this.lblPickPipeInfo;
            string[] name = new string[] { this._networkInfo.LayerLine.Name, ",", lineClass.OIDFieldName, "=", userID.ToString() };
            label.Text = string.Concat(name);
            string.Format("\r\n爆管点位置({0:f2},{1:f2},{2:f2})", this._nearestEdgeInfo.Location.X, this._nearestEdgeInfo.Location.Y, this._nearestEdgeInfo.Location.Z);
        }

		private void InitClick()
		{
			this.ipoint_0 = null;
			this._nearestEdgeInfo.GeometricNetwork = null;
			this._nearestEdgeInfo.Location = null;
			this._nearestEdgeInfo.EdgeID = 0;
			this._nearestEdgeInfo.Percent = 2147483647.0;
			this._networkInfo.arrayList_1.Clear();
			this._networkInfo.LineFeature = null;
			this._networkInfo.LayerLine = null;
			this._networkInfo.LayerPoint = null;
			this._networkInfo.arrayList_0.Clear();
			this.treeView1.Nodes.Clear();
			this.listView1.Items.Clear();
		}

		public static void GetMapVisibleILayers(IMap XMap, ILayer ilayer_0, ArrayList Layers)
		{
			if (ilayer_0 == null)
			{
				for (int i = 0; i < XMap.LayerCount; i++)
				{
					ILayer layer = XMap.get_Layer(i);
					if (layer.Visible)
					{
						if (layer is IGroupLayer)
						{
							GetMapVisibleILayers(XMap, layer, Layers);
						}
						else
						{
							Layers.Add(layer);
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < ((ICompositeLayer)ilayer_0).Count; j++)
				{
					ILayer layer2 = ((ICompositeLayer)ilayer_0).get_Layer(j);
					if (layer2.Visible)
					{
						if (layer2 is IGroupLayer)
						{
							frmBurstReport.GetMapVisibleILayers(XMap, layer2, Layers);
						}
						else
						{
							Layers.Add(layer2);
						}
					}
				}
			}
		}

		private void FillListView(IFeature feature)
		{
			if (feature != null)
			{
				this.listView1.Clear();
				this.listView1.Columns.Add("属性信息", 100, HorizontalAlignment.Center);
				this.listView1.Columns.Add("数值", 140, HorizontalAlignment.Left);
				for (int i = 0; i < feature.Fields.FieldCount; i++)
				{
					string name = feature.Fields.get_Field(i).Name;
					string text = feature.get_Value(i).ToString();
					ListViewItem listViewItem = this.listView1.Items.Add(name);
					listViewItem.SubItems.Add(text);
				}
			}
		}

		private void btnAddFieldValue_Click(object obj, EventArgs eventArgs)
		{
			if (this.listFieldValues.Items.IndexOf(this.comboBox1.Text) == -1)
			{
				this.listFieldValues.Items.Add(this.comboBox1.Text);
			}
		}

		private void btnDelFieldValue_Click(object obj, EventArgs eventArgs)
		{
			if (this.listFieldValues.SelectedItem != null)
			{
				this.listFieldValues.Items.Remove(this.listFieldValues.SelectedItem);
			}
		}

		private void treeView1_MouseClick(object obj, MouseEventArgs mouseEventArgs)
		{
			try
			{
				if (mouseEventArgs.Button != MouseButtons.Right)
				{
					TreeViewHitTestInfo treeViewHitTestInfo = this.treeView1.HitTest(mouseEventArgs.X, mouseEventArgs.Y);
					if (treeViewHitTestInfo != null)
					{
						if (treeViewHitTestInfo.Node.Parent == null)
						{
							if (treeViewHitTestInfo.Node.Nodes.Count > 0)
							{
								this.treeView1.SelectedNode = treeViewHitTestInfo.Node.Nodes[0];
							}
						}
						else if (treeViewHitTestInfo.Node.Parent.Text.Trim() == "爆管的线路")
						{
							this.FillListView(this._networkInfo.LineFeature);
							this.BuoawIbkuD = this._networkInfo.LineFeature.Shape;
							this.ScaleToGeo(m_iApp.ActiveView, this.BuoawIbkuD);
							this.timer_0.Start();
							this.m_nTimerCounter = 0;
							m_iApp.ActiveView.Refresh();
						}
						else if (treeViewHitTestInfo.Node.Parent.Text.Trim() == "受影响的阀门")
						{
							for (int i = 0; i <= this._networkInfo.arrayList_0.Count - 1; i++)
							{
								IFeature feature = (IFeature)this._networkInfo.arrayList_0[i];
								if (feature.OID == (int)Convert.ToInt16(treeViewHitTestInfo.Node.Text))
								{
									this.BuoawIbkuD = feature.Shape;
									this.FillListView(feature);
									this.ScaleToGeo(m_iApp.ActiveView , this.BuoawIbkuD);
									this.timer_0.Start();
									this.m_nTimerCounter = 0;
									m_iApp.ActiveView.Refresh();
									break;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void ScaleToGeo(IActiveView pView, IGeometry pGeo)
		{
			if (pGeo.GeometryType == (esriGeometryType) 1)
			{
				IEnvelope envelope = pGeo.Envelope;
			    IEnvelope extent = pView.Extent;
				double width = extent.Width;
				double height = extent.Height;
				envelope.Expand(width / 2.0, height / 2.0, false);
				pView.Extent=(envelope);
			}
			else
			{
				IEnvelope envelope2 = pGeo.Envelope;
				envelope2.Expand(3.0, 3.0, true);
                pView.Extent=(envelope2);
			}
		}

		private void treeView1_AfterSelect(object obj, TreeViewEventArgs treeViewEventArgs)
		{
		}

		public void PickThroughValve(IPoint _pMousePoint)
		{
			IPointToEID pointToEIDClass = new PointToEID();
			pointToEIDClass.GeometricNetwork=(this._nearestEdgeInfo.GeometricNetwork);
			pointToEIDClass.SourceMap=(m_iApp.FocusMap);
			pointToEIDClass.SnapTolerance=(m_iApp.ActiveView.Extent.Width / 200.0);
			int junctionEID = 0;
			IPoint location = null;
			pointToEIDClass.GetNearestJunction(_pMousePoint, out junctionEID, out location);
			if (location != null)
			{
				INetElements netElements = (INetElements)this._nearestEdgeInfo.GeometricNetwork.Network;
				int userClassID;
				int userID;
				int userSubID;
				netElements.QueryIDs(junctionEID, esriElementType.esriETJunction,out userClassID, out userID, out userSubID);
				IFeature feature = this._networkInfo.LayerPoint.FeatureClass.GetFeature(userID);
				this.listOutBarriers.Items.Add(feature.OID.ToString());
			}
		}

		private void btnDelThroughBarrier_Click(object obj, EventArgs eventArgs)
		{
			if (this.listOutBarriers.SelectedItem != null)
			{
				this.listOutBarriers.Items.Remove(this.listOutBarriers.SelectedItem);
			}
		}

		private void btnStartAnalysis_Click(object obj, EventArgs eventArgs)
		{
			if (this._nearestEdgeInfo.GeometricNetwork != null)
			{
				this._networkInfo.arrayList_1.Clear();
				this._networkInfo.arrayList_0.Clear();
				INetElements netElements = (INetElements)this._nearestEdgeInfo.GeometricNetwork.Network;
				ITraceFlowSolver traceFlowSolver = this.GetTraceFlowSolver();
				IEnumNetEID junctionEIDs;
				IEnumNetEID edgeEIDs;
			    traceFlowSolver.FindFlowEndElements(this.GetFindMethod(), esriFlowElements.esriFEJunctions,out junctionEIDs, out edgeEIDs);
				int num;
				int num2;
				int num3;
				for (int i = junctionEIDs.Next(); i > 0; i = junctionEIDs.Next())
				{
					netElements.QueryIDs(i, (esriElementType) 1, out num, out num2, out num3);
					IFeature feature = this._networkInfo.LayerPoint.FeatureClass.GetFeature(num2);
					int num4 = feature.Fields.FindField(this.label1.Text);
					if (num4 == -1)
					{
						return;
					}
					if (this.listFieldValues.Items.IndexOf(feature.get_Value(num4)) != -1)
					{
						this._networkInfo.arrayList_0.Add(feature);
					}
				}
				traceFlowSolver.FindFlowElements(this.GetFindMethod(), (esriFlowElements) 1, out junctionEIDs, out edgeEIDs);
				for (int i = edgeEIDs.Next(); i > 0; i = edgeEIDs.Next())
				{
					netElements.QueryIDs(i, (esriElementType) 2, out num, out num2, out num3);
					IFeature feature2 = this._networkInfo.LayerLine.FeatureClass.GetFeature(num2);
					this._networkInfo.arrayList_1.Add(feature2);
				}
				m_iApp.FocusMap.ClearSelection();
				this.CreateResultElements();
				this.CreateBlockElements();
				this.FillResultToTreeView();
			}
		}

		private void treeView1_MouseDoubleClick(object obj, MouseEventArgs mouseEventArgs)
		{
			TreeViewHitTestInfo treeViewHitTestInfo = this.treeView1.HitTest(mouseEventArgs.X, mouseEventArgs.Y);
			if (treeViewHitTestInfo != null && treeViewHitTestInfo.Node.Parent != null)
			{
				IFeature feature = (IFeature)treeViewHitTestInfo.Node.Tag;
				this.listOutBarriers.Items.Add(feature.OID.ToString());
				this.btnStartAnalysis_Click(null, null);
			}
		}

		private void DrawBrokeEdge()
		{
			IMarkerSymbol markerSymbol = new SimpleMarkerSymbol();
			IMarkerSymbol arg_2B_0 = markerSymbol;
			IRgbColor rgbColorClass = new RgbColor();
			rgbColorClass.Red=(255);
			rgbColorClass.Green=(0);
			rgbColorClass.Blue=(255);
			arg_2B_0.Color=(rgbColorClass);
			markerSymbol.Size=(15.0);
			IScreenDisplay screenDisplay = m_iApp.ActiveView.ScreenDisplay;
			screenDisplay.StartDrawing(screenDisplay.hDC, 0);
			screenDisplay.SetSymbol((ISymbol)markerSymbol);
			screenDisplay.DrawPoint(this._nearestEdgeInfo.Location);
			screenDisplay.FinishDrawing();
		}

		private void CreateResultElements()
		{
			IGeometryCollection geometryCollection = new GeometryBag() as IGeometryCollection;
			for (int i = 0; i < this._networkInfo.arrayList_1.Count; i++)
			{
				object missing = Type.Missing;
				IFeature feature = (IFeature)this._networkInfo.arrayList_1[i];
				geometryCollection.AddGeometry(feature.ShapeCopy, ref missing, ref missing);
			}
			if (this._networkInfo.arrayList_1.Count > 0)
			{
				IRgbColor rgbColor = new RgbColor();
				rgbColor.Red=(255);
				rgbColor.Green=(0);
				rgbColor.Blue=(255);
				IGraphicsContainer graphicsContainer = (IGraphicsContainer)m_iApp.ActiveView;
				for (int j = 0; j < geometryCollection.GeometryCount; j++)
				{
					ILineElement lineElement = new LineElement() as ILineElement;
					IElement element = (IElement)lineElement;
					element.Geometry=(geometryCollection.get_Geometry(j));
					ILineElement arg_F5_0 = lineElement;
					ISimpleLineSymbol simpleLineSymbolClass = new SimpleLineSymbol();
					simpleLineSymbolClass.Color=(rgbColor);
					simpleLineSymbolClass.Width=(10.0);
					simpleLineSymbolClass.Style=(0);
					arg_F5_0.Symbol=(simpleLineSymbolClass);
					graphicsContainer.AddElement(element, 0);
				}
				IEnvelope envelope = ((IGeometry)geometryCollection).Envelope;
				envelope.Expand(1.2, 1.2, true);
				m_iApp.ActiveView.Extent=(envelope);
			}
		}

		private void CreateBlockElements()
		{
			IGeometryCollection geometryCollection = new Multipoint() as IGeometryCollection;
			for (int i = 0; i < this._networkInfo.arrayList_0.Count; i++)
			{
				object missing = Type.Missing;
				IFeature feature = (IFeature)this._networkInfo.arrayList_0[i];
				geometryCollection.AddGeometry(feature.ShapeCopy, ref missing, ref missing);
			}
			if (this._networkInfo.arrayList_0.Count > 0)
			{
				IGraphicsContainer graphicsContainer = (IGraphicsContainer)m_iApp.ActiveView;
				IMarkerSymbol markerSymbol = this.GetMarkerSymbol();
				markerSymbol.Size=(26.0);
				for (int j = 0; j < geometryCollection.GeometryCount; j++)
				{
					IMarkerElement markerElement = new MarkerElement() as IMarkerElement;
					IElement element = (IElement)markerElement;
					element.Geometry=(geometryCollection.get_Geometry(j));
					markerElement.Symbol=(markerSymbol);
					graphicsContainer.AddElement(element, 0);
				}
				m_iApp.ActiveView.PartialRefresh((esriViewDrawPhase) 8, null, null);
			}
		}

		private IMarkerSymbol GetMarkerSymbol()
		{
            IMarkerSymbol markerSymbol;
            IStyleGallery serverStyleGalleryClass = new ServerStyleGallery();
            IMarkerSymbol item = null;
            bool flag = false;
            string str = string.Concat(Application.StartupPath, "\\Style\\ESRI.ServerStyle");
            if (!File.Exists(str))
            {
                markerSymbol = null;
            }
            else
            {
                string str1 = str;
                serverStyleGalleryClass.Clear();
                (serverStyleGalleryClass as IStyleGalleryStorage).AddFile(str1);
                IEnumStyleGalleryItem items = serverStyleGalleryClass.Items["Marker Symbols", str1, "Default"];
                items.Reset();
                IStyleGalleryItem styleGalleryItem = items.Next();
                while (true)
                {
                    if (styleGalleryItem == null)
                    {
                        break;
                    }
                    else if (styleGalleryItem.Name == "Star 5")
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        styleGalleryItem = items.Next();
                    }
                }
                if (flag)
                {
                    item = (IMarkerSymbol)styleGalleryItem.Item;
                }
                item.Size = 19;
                markerSymbol = item;
            }
            return markerSymbol;
        }

		private void FillResultToTreeView()
		{
			this.treeView1.Nodes.Clear();
			TreeNode treeNode = this.treeView1.Nodes.Add("爆管的线路");
			TreeNode treeNode2 = treeNode.Nodes.Add(this._networkInfo.LineFeature.OID.ToString());
			treeNode2.Tag = this._networkInfo.LineFeature;
			this.treeView1.SelectedNode = treeNode2;
			treeNode.Expand();
			TreeNode treeNode3 = this.treeView1.Nodes.Add("受影响的阀门");
			for (int i = 0; i < this._networkInfo.arrayList_0.Count; i++)
			{
				IFeature feature = (IFeature)this._networkInfo.arrayList_0[i];
				TreeNode treeNode4 = treeNode3.Nodes.Add(feature.OID.ToString());
				treeNode4.Tag = feature;
			}
			treeNode3.Expand();
			treeNode.EnsureVisible();
		}

		private void btnCancel_Click(object obj, EventArgs eventArgs)
		{
			string str = "";
			for (int i = 0; i < this.listFieldValues.Items.Count; i++)
			{
				str = str + this.listFieldValues.Items[i].ToString() + "/";
			}
			((IMapControlEvents2_Event)m_iApp.MapControl).OnMouseDown-= OnOnMouseDown;
		}

	    private void OnOnMouseDown(int button, int shift, int i, int i1, double mapX, double mapY)
	    {
            if ((button != 1 ? false :m_iApp.MapControl.CurrentTool == null))
            {
                this.SetMousePoint(i, i1);
            }
        }

	    protected override void OnClosing(CancelEventArgs e)
		{
		}

		public  void GetValues(ArrayList Values)
		{
			Values.Clear();
		    if (this._networkInfo.LayerPoint==null) return;
		    IBasicLayerInfo pipePoint =
                m_Config.GetBasicLayerInfo(this._networkInfo.LayerPoint.FeatureClass.AliasName) as IBasicLayerInfo;

            //IPipePoint pipePoint = m_Config.GetSubLayer(this._networkInfo.LayerPoint, enumPipelineDataType.Point) as IPipePoint;

            //string text = CRegOperator.GetRegistryKey().GetValue("节点性质字段值", "").ToString();
		    string text = pipePoint.GetField(PipeConfigWordHelper.PointWords.TZW).DomainValues;
            for (int num = text.IndexOf("/"); num != -1; num = text.IndexOf("/"))
			{
				Values.Add(text.Substring(0, num));
				text = text.Substring(num + 1, text.Length - num - 1);
			}
		}

		private void btnAddThroughValve_CheckedChanged(object obj, EventArgs eventArgs)
		{
		}

		private void timer_0_Tick(object obj, EventArgs eventArgs)
		{
			this.m_nTimerCounter++;
			if (this.m_nTimerCounter > 9)
			{
				this.timer_0.Stop();
				this.m_nTimerCounter = 0;
			}
			else
			{
				this.FlashDstItem();
			}
		}

		public void FlashDstItem()
		{
			IMapControl3 mapControl = m_iApp.MapControl as IMapControl3;
			CRandomColor cRandomColor = new CRandomColor();
			Color randColor = cRandomColor.GetRandColor();
			ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbol();
			IRgbColor rgbColor = new RgbColor();
			rgbColor.Red=((int)randColor.R);
			rgbColor.Green=((int)randColor.G);
			rgbColor.Blue=((int)randColor.B);
			simpleLineSymbol.Color=(rgbColor);
			simpleLineSymbol.Width=(5.0);
			object obj = simpleLineSymbol;
			ISimpleMarkerSymbol simpleMarkerSymbolClass = new SimpleMarkerSymbol();
			simpleMarkerSymbolClass.Color=(rgbColor);
			simpleMarkerSymbolClass.Size=(10.0);
			simpleMarkerSymbolClass.Style=(0);
			object obj2 = simpleMarkerSymbolClass;
			try
			{
				if (this.BuoawIbkuD.GeometryType == (esriGeometryType) 1)
				{
					mapControl.DrawShape(this.BuoawIbkuD, ref obj2);
				}
				if (this.BuoawIbkuD.GeometryType == (esriGeometryType) 3)
				{
					mapControl.DrawShape(this.BuoawIbkuD, ref obj);
				}
			}
			catch
			{
			}
		}

		private void frmBurstReport_FormClosed(object obj, FormClosedEventArgs formClosedEventArgs)
		{
			string str = "";
			for (int i = 0; i < this.listFieldValues.Items.Count; i++)
			{
				str = str + this.listFieldValues.Items[i].ToString() + "/";
			}
			if (this.checkReflushView.Checked)
			{
			    IGraphicsContainer graphicsContainer = (IGraphicsContainer) m_iApp.ActiveView;
				graphicsContainer.DeleteAllElements();
			}
		    m_iApp.MapControl.CurrentTool = null;
			m_iApp.ActiveView.PartialRefresh((esriViewDrawPhase) 8, null, null);
		}

		private void frmBurstReport_HelpRequested(object obj, HelpEventArgs helpEventArgs)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "关阀分析";
			Help.ShowHelp(this, url, HelpNavigator.KeywordIndex, parameter);
		}

		private void btnPickBrokePipe_Click(object obj, EventArgs eventArgs)
		{
            ((IMapControlEvents2_Event)m_iApp.MapControl).OnMouseDown += OnOnMouseDown;
        }

		private void method_8(object obj, IMapControlEvents2_OnMouseDownEvent mapControlEvents2_OnMouseDownEvent)
		{
			if (mapControlEvents2_OnMouseDownEvent.button == 1 && m_iApp.MapControl.CurrentTool == null)
			{
				this.SetMousePoint(mapControlEvents2_OnMouseDownEvent.x, mapControlEvents2_OnMouseDownEvent.y);
			}
		}

		private void ClearGra_Click(object obj, EventArgs eventArgs)
		{
		    IGraphicsContainer graphicsContainer = (IGraphicsContainer) m_iApp.ActiveView;
			graphicsContainer.DeleteAllElements();
		}

		private void btnPickBrokePipe_CheckedChanged(object obj, EventArgs eventArgs)
		{
		}
	}
}

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
using Yutai.Pipeline.Analysis.Classes;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	public partial class frmBurstReport : Form
	{
		private struct Struct0
		{
			public IGeometricNetwork igeometricNetwork_0;

			public int int_0;

			public IPoint ipoint_0;

			public double double_0;
		}

		private struct Struct1
		{
			public IFeatureLayer ifeatureLayer_0;

			public IFeatureLayer ifeatureLayer_1;

			public IFeature ifeature_0;

			public ArrayList arrayList_0;

			public ArrayList arrayList_1;

			public Struct1(IFeatureLayer featureLayer, IFeatureLayer featureLayer2, IFeature feature, ArrayList arrayList, ArrayList arrayList2)
			{
				this.ifeatureLayer_0 = featureLayer;
				this.ifeatureLayer_1 = featureLayer2;
				this.ifeature_0 = feature;
				this.arrayList_0 = arrayList;
				this.arrayList_1 = arrayList2;
			}
		}

		private IContainer icontainer_0 = null;

        
		public IAppContext m_iApp;


		private frmBurstReport.Struct0 struct0_0 = default(frmBurstReport.Struct0);

		private frmBurstReport.Struct1 struct1_0 = new frmBurstReport.Struct1(null, null, null, new ArrayList(), new ArrayList());


		public int m_nTimerCounter;


	public IQueryFilter GetBarrierQuery()
		{
		    IQueryFilter queryFilter = new QueryFilter();
			ArrayList arrayList = new ArrayList();
			frmBurstReport.GetValues(arrayList);
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
			int featureClassID = this.struct1_0.ifeatureLayer_1.FeatureClass.FeatureClassID;
		    ISelectionSetBarriers selectionSetBarriers = new SelectionSetBarriers();
			IFeatureSelection featureSelection = (IFeatureSelection)this.struct1_0.ifeatureLayer_1;
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
			INetElements netElements = (INetElements)this.struct0_0.igeometricNetwork_0.Network;
			int userClassID = 0;
			int userID = 0;
			int userSubID = 0;
			netElements.QueryIDs(this.struct0_0.int_0, (esriElementType) 2, out userClassID, out userID, out userSubID);
		    IEdgeFlag edgeFlag = new EdgeFlag();
			((INetFlag)edgeFlag).UserClassID=(userClassID);
			((INetFlag)edgeFlag).UserID=(userID);
			((INetFlag)edgeFlag).UserSubID=(userSubID);
		    ITraceFlowSolver traceFlowSolver = (ITraceFlowSolver) new TraceFlowSolver();
			((INetSolver)traceFlowSolver).SourceNetwork=(this.struct0_0.igeometricNetwork_0.Network);
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
			string pointTableFieldName = m_iApp.PipeConfig.GetPointTableFieldName("点性");
			this.label1.Text = pointTableFieldName;
			for (int i = 0; i < arrayList.Count; i++)
			{
				IFeatureLayer featureLayer = arrayList[i] as IFeatureLayer;
				if (featureLayer != null && featureLayer.FeatureClass != null && m_iApp.PipeConfig.IsPipePoint(featureLayer.FeatureClass.AliasName))
				{
					IFeatureClass featureClass = featureLayer.FeatureClass;
					//new QueryFilter()
					int num = featureClass.Fields.FindField(pointTableFieldName);
					if (num != -1)
					{
						ArrayList uVByQueryDef = this.GetUVByQueryDef(featureLayer, pointTableFieldName);
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
			this.label1.Text = pointTableFieldName;
			ArrayList arrayList2 = new ArrayList();
			frmBurstReport.GetValues(arrayList2);
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
						this.method_0();
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

		public bool PickBrokePipe(IPoint _pMousePoint)
		{
			this.method_1();
			this.ipoint_0 = _pMousePoint;
			bool flag = false;
			double num = 2147483647.0;
			ArrayList arrayList = new ArrayList();
			frmBurstReport.GetMapVisibleILayers(m_iApp.FocusMap, null, arrayList);
			for (int i = 0; i < arrayList.Count; i++)
			{
				IFeatureLayer featureLayer = arrayList[i] as IFeatureLayer;
				if (featureLayer != null && featureLayer.FeatureClass != null && featureLayer.Visible && m_iApp.PipeConfig.IsPipeLine(featureLayer.FeatureClass.AliasName))
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
						int int_ = 0;
						IPoint point = null;
						double num2 = 0;
						pointToEIDClass.GetNearestEdge(this.ipoint_0, out int_, out point, out num2);
						if (point != null && num > num2)
						{
							num = num2;
							this.struct0_0.igeometricNetwork_0 = geometricNetwork;
							this.struct0_0.double_0 = num2;
							this.struct0_0.int_0 = int_;
							this.struct0_0.ipoint_0 = point;
							flag = true;
						}
					}
				}
			}
			arrayList.Clear();
			if (flag)
			{
				this.method_3();
			}
			return flag;
		}

		private void method_0()
		{
            int num;
            int num1;
            int num2;
            IFeatureClassContainer featureDataset = (IFeatureClassContainer)this.struct0_0.igeometricNetwork_0.FeatureDataset;
            IFeatureClass featureClass = null;
            IFeatureClass featureClass1 = null;
            int num3 = 0;
            while (true)
            {
                if (num3 < featureDataset.ClassCount)
                {
                    IFeatureClass pclass = featureDataset.Class[num3];
                    if ((pclass.ShapeType != esriGeometryType.esriGeometryPoint ? false : !pclass.AliasName.Contains("Junctions")))
                    {
                        featureClass = pclass;
                        break;
                    }
                    else
                    {
                        num3++;
                    }
                }
                else
                {
                    break;
                }
            }
            int num4 = 0;
            while (true)
            {
                if (num4 < featureDataset.ClassCount)
                {
                    IFeatureClass class1 = featureDataset.Class[num4];
                    if (class1.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        featureClass1 = class1;
                        break;
                    }
                    else
                    {
                        num4++;
                    }
                }
                else
                {
                    break;
                }
            }
            this.struct1_0.ifeatureLayer_0 = CMapOperator.GetILayerByAliasName(m_iApp.FocusMap, null, featureClass1.AliasName) as IFeatureLayer;
            this.struct1_0.ifeatureLayer_1 = CMapOperator.GetILayerByAliasName(m_iApp.FocusMap, null, featureClass.AliasName) as IFeatureLayer;
            INetElements network = (INetElements)this.struct0_0.igeometricNetwork_0.Network;
            network.QueryIDs(this.struct0_0.int_0, esriElementType.esriETEdge, out num, out num1, out num2);
            this.struct1_0.ifeature_0 = featureClass1.GetFeature(num1);
            Label label = this.lblPickPipeInfo;
            string[] name = new string[] { this.struct1_0.ifeatureLayer_0.Name, ",", featureClass1.OIDFieldName, "=", num1.ToString() };
            label.Text = string.Concat(name);
            string.Format("\r\n爆管点位置({0:f2},{1:f2},{2:f2})", this.struct0_0.ipoint_0.X, this.struct0_0.ipoint_0.Y, this.struct0_0.ipoint_0.Z);
        }

		private void method_1()
		{
			this.ipoint_0 = null;
			this.struct0_0.igeometricNetwork_0 = null;
			this.struct0_0.ipoint_0 = null;
			this.struct0_0.int_0 = 0;
			this.struct0_0.double_0 = 2147483647.0;
			this.struct1_0.arrayList_1.Clear();
			this.struct1_0.ifeature_0 = null;
			this.struct1_0.ifeatureLayer_0 = null;
			this.struct1_0.ifeatureLayer_1 = null;
			this.struct1_0.arrayList_0.Clear();
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
							frmBurstReport.GetMapVisibleILayers(XMap, layer, Layers);
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

		private void method_2(IFeature feature)
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
							this.method_2(this.struct1_0.ifeature_0);
							this.BuoawIbkuD = this.struct1_0.ifeature_0.Shape;
							this.ScaleToGeo(m_iApp.ActiveView, this.BuoawIbkuD);
							this.timer_0.Start();
							this.m_nTimerCounter = 0;
							m_iApp.ActiveView.Refresh();
						}
						else if (treeViewHitTestInfo.Node.Parent.Text.Trim() == "受影响的阀门")
						{
							for (int i = 0; i <= this.struct1_0.arrayList_0.Count - 1; i++)
							{
								IFeature feature = (IFeature)this.struct1_0.arrayList_0[i];
								if (feature.OID == (int)Convert.ToInt16(treeViewHitTestInfo.Node.Text))
								{
									this.BuoawIbkuD = feature.Shape;
									this.method_2(feature);
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
			pointToEIDClass.GeometricNetwork=(this.struct0_0.igeometricNetwork_0);
			pointToEIDClass.SourceMap=(m_iApp.FocusMap);
			pointToEIDClass.SnapTolerance=(m_iApp.ActiveView.Extent.Width / 200.0);
			int num = 0;
			IPoint point = null;
			pointToEIDClass.GetNearestJunction(_pMousePoint, out num, out point);
			if (point != null)
			{
				INetElements netElements = (INetElements)this.struct0_0.igeometricNetwork_0.Network;
				int num2;
				int num3;
				int num4;
				netElements.QueryIDs(num, (esriElementType) 1, out num2, out num3, out num4);
				IFeature feature = this.struct1_0.ifeatureLayer_1.FeatureClass.GetFeature(num3);
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
			if (this.struct0_0.igeometricNetwork_0 != null)
			{
				this.struct1_0.arrayList_1.Clear();
				this.struct1_0.arrayList_0.Clear();
				INetElements netElements = (INetElements)this.struct0_0.igeometricNetwork_0.Network;
				ITraceFlowSolver traceFlowSolver = this.GetTraceFlowSolver();
				IEnumNetEID enumNetEID;
				IEnumNetEID enumNetEID2;
				traceFlowSolver.FindFlowEndElements(this.GetFindMethod(), 0, out enumNetEID, out enumNetEID2);
				int num;
				int num2;
				int num3;
				for (int i = enumNetEID.Next(); i > 0; i = enumNetEID.Next())
				{
					netElements.QueryIDs(i, (esriElementType) 1, out num, out num2, out num3);
					IFeature feature = this.struct1_0.ifeatureLayer_1.FeatureClass.GetFeature(num2);
					int num4 = feature.Fields.FindField(this.label1.Text);
					if (num4 == -1)
					{
						return;
					}
					if (this.listFieldValues.Items.IndexOf(feature.get_Value(num4)) != -1)
					{
						this.struct1_0.arrayList_0.Add(feature);
					}
				}
				traceFlowSolver.FindFlowElements(this.GetFindMethod(), (esriFlowElements) 1, out enumNetEID, out enumNetEID2);
				for (int i = enumNetEID2.Next(); i > 0; i = enumNetEID2.Next())
				{
					netElements.QueryIDs(i, (esriElementType) 2, out num, out num2, out num3);
					IFeature feature2 = this.struct1_0.ifeatureLayer_0.FeatureClass.GetFeature(num2);
					this.struct1_0.arrayList_1.Add(feature2);
				}
				m_iApp.FocusMap.ClearSelection();
				this.method_4();
				this.method_5();
				this.method_7();
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

		private void method_3()
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
			screenDisplay.DrawPoint(this.struct0_0.ipoint_0);
			screenDisplay.FinishDrawing();
		}

		private void method_4()
		{
			IGeometryCollection geometryCollection = new GeometryBag() as IGeometryCollection;
			for (int i = 0; i < this.struct1_0.arrayList_1.Count; i++)
			{
				object missing = Type.Missing;
				IFeature feature = (IFeature)this.struct1_0.arrayList_1[i];
				geometryCollection.AddGeometry(feature.ShapeCopy, ref missing, ref missing);
			}
			if (this.struct1_0.arrayList_1.Count > 0)
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

		private void method_5()
		{
			IGeometryCollection geometryCollection = new Multipoint() as IGeometryCollection;
			for (int i = 0; i < this.struct1_0.arrayList_0.Count; i++)
			{
				object missing = Type.Missing;
				IFeature feature = (IFeature)this.struct1_0.arrayList_0[i];
				geometryCollection.AddGeometry(feature.ShapeCopy, ref missing, ref missing);
			}
			if (this.struct1_0.arrayList_0.Count > 0)
			{
				IGraphicsContainer graphicsContainer = (IGraphicsContainer)m_iApp.ActiveView;
				IMarkerSymbol markerSymbol = this.method_6();
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

		private IMarkerSymbol method_6()
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

		private void method_7()
		{
			this.treeView1.Nodes.Clear();
			TreeNode treeNode = this.treeView1.Nodes.Add("爆管的线路");
			TreeNode treeNode2 = treeNode.Nodes.Add(this.struct1_0.ifeature_0.OID.ToString());
			treeNode2.Tag = this.struct1_0.ifeature_0;
			this.treeView1.SelectedNode = treeNode2;
			treeNode.Expand();
			TreeNode treeNode3 = this.treeView1.Nodes.Add("受影响的阀门");
			for (int i = 0; i < this.struct1_0.arrayList_0.Count; i++)
			{
				IFeature feature = (IFeature)this.struct1_0.arrayList_0[i];
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

		public static void GetValues(ArrayList Values)
		{
			Values.Clear();
			string text = CRegOperator.GetRegistryKey().GetValue("节点性质字段值", "").ToString();
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

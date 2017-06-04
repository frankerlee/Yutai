using System.ComponentModel;
using System.Drawing;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	public class RepresentationruleListBox : System.Windows.Forms.ListBox
	{
		internal class RepresentationRuleWrap
		{
			private int int_0;

			private string string_0;

			private IRepresentationRule irepresentationRule_0;

			public int RuleID
			{
				get
				{
					return this.int_0;
				}
			}

			public string Name
			{
				get
				{
					return this.string_0;
				}
			}

			public IRepresentationRule RepresentationRule
			{
				get
				{
					return this.irepresentationRule_0;
				}
			}

			internal RepresentationRuleWrap(int int_1, string string_1, IRepresentationRule irepresentationRule_1)
			{
				this.int_0 = int_1;
				this.string_0 = string_1;
				this.irepresentationRule_0 = irepresentationRule_1;
			}
		}

		private IRepresentationRules irepresentationRules_0 = null;

		private Container container_0 = null;

		private esriGeometryType esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;

		public IRepresentationRule SelectRepresentationRule
		{
			get
			{
				IRepresentationRule result;
				if (base.SelectedItem == null)
				{
					result = null;
				}
				else
				{
					result = (base.SelectedItem as RepresentationruleListBox.RepresentationRuleWrap).RepresentationRule;
				}
				return result;
			}
		}

		public int SelectRepresentationRuleID
		{
			get
			{
				int result;
				if (base.SelectedItem == null)
				{
					result = -1;
				}
				else
				{
					result = (base.SelectedItem as RepresentationruleListBox.RepresentationRuleWrap).RuleID;
				}
				return result;
			}
		}

		public string SelectRepresentationRuleName
		{
			get
			{
				string result;
				if (base.SelectedItem == null)
				{
					result = "";
				}
				else
				{
					result = (base.SelectedItem as RepresentationruleListBox.RepresentationRuleWrap).Name;
				}
				return result;
			}
		}

		public esriGeometryType GeometryType
		{
			set
			{
				this.esriGeometryType_0 = value;
			}
		}

		public RepresentationruleListBox()
		{
			this.method_0();
		}

		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

		private void method_0()
		{
			this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RepresentationruleListBox_MouseDown);
			base.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.RepresentationruleListBox_MeasureItem);
			base.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.RepresentationruleListBox_DrawItem);
		}

		public void AddRepresentationRule(IRepresentationRule irepresentationRule_0)
		{
		}

		public void Reset()
		{
			base.Items.Clear();
			this.irepresentationRules_0.Reset();
			int num;
			IRepresentationRule representationRule;
			this.irepresentationRules_0.Next(out num, out representationRule);
			while (representationRule != null)
			{
				string string_ = this.irepresentationRules_0.get_Name(num);
				base.Items.Add(new RepresentationruleListBox.RepresentationRuleWrap(num, string_, representationRule));
				this.irepresentationRules_0.Next(out num, out representationRule);
			}
		}

		public void Init(IRepresentationRules irepresentationRules_1)
		{
			base.Items.Clear();
			this.irepresentationRules_0 = irepresentationRules_1;
			this.irepresentationRules_0.Reset();
			int num;
			IRepresentationRule representationRule;
			this.irepresentationRules_0.Next(out num, out representationRule);
			while (representationRule != null)
			{
				string string_ = this.irepresentationRules_0.get_Name(num);
				base.Items.Add(new RepresentationruleListBox.RepresentationRuleWrap(num, string_, representationRule));
				this.irepresentationRules_0.Next(out num, out representationRule);
			}
		}

		private IGeometry method_1(tagRECT tagRECT_0)
		{
			object value = System.Reflection.Missing.Value;
			IGeometry geometry = new Polygon() as IGeometry;
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			point.PutCoords((double)tagRECT_0.left, (double)tagRECT_0.bottom);
			(geometry as IPointCollection).AddPoint(point, ref value, ref value);
            point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords((double)tagRECT_0.left, (double)tagRECT_0.top);
			(geometry as IPointCollection).AddPoint(point, ref value, ref value);
            point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords((double)tagRECT_0.right, (double)tagRECT_0.top);
			(geometry as IPointCollection).AddPoint(point, ref value, ref value);
            point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords((double)tagRECT_0.right, (double)tagRECT_0.bottom);
			(geometry as IPointCollection).AddPoint(point, ref value, ref value);
            point = new ESRI.ArcGIS.Geometry.Point();
            point.PutCoords((double)tagRECT_0.left, (double)tagRECT_0.bottom);
			(geometry as IPointCollection).AddPoint(point, ref value, ref value);
			return geometry;
		}

		private IGeometry method_2(tagRECT tagRECT_0)
		{
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			object value = System.Reflection.Missing.Value;
			IGeometry geometry = new Polyline() as IGeometry;
			point.PutCoords((double)(tagRECT_0.left + 5), (double)((tagRECT_0.top + tagRECT_0.bottom) / 2));
			(geometry as IPointCollection).AddPoint(point, ref value, ref value);
			point = new ESRI.ArcGIS.Geometry.Point();
			point.PutCoords((double)(tagRECT_0.left + 5), (double)((tagRECT_0.top + tagRECT_0.bottom) / 2));
			(geometry as IPointCollection).AddPoint(point, ref value, ref value);
			return geometry;
		}

		private void RepresentationruleListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			if (base.Items.Count != 0)
			{
				RepresentationruleListBox.RepresentationRuleWrap representationRuleWrap = (RepresentationruleListBox.RepresentationRuleWrap)base.Items[e.Index];
				e.DrawBackground();
				string s = "(" + representationRuleWrap.RuleID.ToString() + ") " + representationRuleWrap.Name;
				Brush brush = new SolidBrush(e.ForeColor);
				e.Graphics.DrawString(s, this.Font, brush, (float)e.Bounds.X, (float)e.Bounds.Y);
				brush.Dispose();
				System.IntPtr hdc = e.Graphics.GetHdc();
				IOutputContext outputContext = new OutputContext();
				IPoint point = new ESRI.ArcGIS.Geometry.Point();
				point.PutCoords((double)(e.Bounds.X + e.Bounds.Width / 2), (double)(e.Bounds.Y + e.Bounds.Height / 2 + 10));
				tagRECT tagRECT_ = default(tagRECT);
				tagRECT_.left = e.Bounds.Left + 10;
				tagRECT_.right = e.Bounds.Right - 10;
				tagRECT_.top = e.Bounds.Top + 10;
				tagRECT_.bottom = e.Bounds.Bottom - 5;
				outputContext.Init(1.0, 1.5, 96.0, 0.0, point, ref tagRECT_, hdc.ToInt32());
				IGeometry geometry;
				if (this.esriGeometryType_0 == esriGeometryType.esriGeometryPolygon)
				{
					geometry = this.method_1(tagRECT_);
				}
				else if (this.esriGeometryType_0 == esriGeometryType.esriGeometryPolyline)
				{
					geometry = this.method_2(tagRECT_);
				}
				else
				{
					geometry = new ESRI.ArcGIS.Geometry.Point();
					(geometry as IPoint).PutCoords((double)((tagRECT_.left + tagRECT_.right) / 2), (double)((tagRECT_.top + tagRECT_.bottom) / 2));
				}
				representationRuleWrap.RepresentationRule.Draw(-1, outputContext, geometry, geometry.Envelope);
				e.Graphics.ReleaseHdc(hdc);
			}
		}

		private void RepresentationruleListBox_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			e.ItemHeight = 60;
		}

		private void RepresentationruleListBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
		}

		protected void DrawSymbol(int int_0, Rectangle rectangle_0, object object_0)
		{
			IDisplayTransformation displayTransformation = new DisplayTransformation() as IDisplayTransformation;
			IEnvelope envelope = new Envelope() as IEnvelope;
			envelope.PutCoords((double)rectangle_0.Left, (double)rectangle_0.Top, (double)rectangle_0.Right, (double)rectangle_0.Bottom);
			tagRECT tagRECT;
			tagRECT.left = rectangle_0.Left;
			tagRECT.right = rectangle_0.Right;
			tagRECT.bottom = rectangle_0.Bottom;
			tagRECT.top = rectangle_0.Top;
			displayTransformation.set_DeviceFrame (ref tagRECT);
			displayTransformation.Bounds = envelope;
			ISymbol symbol = object_0 as ISymbol;
			if (symbol != null)
			{
				symbol.SetupDC(int_0, displayTransformation);
				if (symbol is IMarkerSymbol)
				{
					this.method_3((IMarkerSymbol)symbol, rectangle_0);
				}
				else if (symbol is ILineSymbol)
				{
					this.method_4((ILineSymbol)symbol, rectangle_0);
				}
				else if (symbol is IFillSymbol)
				{
					this.method_5((IFillSymbol)symbol, rectangle_0);
				}
				symbol.ResetDC();
			}
		}

		private void method_3(IMarkerSymbol imarkerSymbol_0, Rectangle rectangle_0)
		{
			if (!(imarkerSymbol_0 is IPictureMarkerSymbol) || ((IPictureMarkerSymbol)imarkerSymbol_0).Picture != null)
			{
				if (imarkerSymbol_0 is IMarker3DSymbol)
				{
					try
					{
						if ((imarkerSymbol_0 as IMarker3DSymbol).MaterialCount == 0)
						{
							return;
						}
					}
					catch
					{
						return;
					}
				}
				IPoint point = new ESRI.ArcGIS.Geometry.Point();
				point.X = (double)((rectangle_0.Left + rectangle_0.Right) / 2);
				point.Y = (double)((rectangle_0.Bottom + rectangle_0.Top) / 2);
				((ISymbol)imarkerSymbol_0).Draw(point);
			}
		}

		private void method_4(ILineSymbol ilineSymbol_0, Rectangle rectangle_0)
		{
			if (ilineSymbol_0 is IPictureLineSymbol)
			{
				if (((IPictureLineSymbol)ilineSymbol_0).Picture == null)
				{
					return;
				}
			}
			else if (ilineSymbol_0 is IMarkerLineSymbol || ilineSymbol_0 is IHashLineSymbol)
			{
				ITemplate template = ((ILineProperties)ilineSymbol_0).Template;
				if (template != null)
				{
					bool flag = false;
					int i = 0;
					while (i < template.PatternElementCount)
					{
						double num;
						double num2;
						template.GetPatternElement(i, out num, out num2);
						if (num + num2 <= 0.0)
						{
							i++;
						}
						else
						{
							flag = true;
							
							if (flag)
							{
							    break;
							}
							return;
						}
					}
					
				}
			}
			
			object value = System.Reflection.Missing.Value;
			IPointCollection pointCollection = new Polyline();
			IPoint point = new ESRI.ArcGIS.Geometry.Point();
			point.PutCoords((double)(rectangle_0.Left + 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
			pointCollection.AddPoint(point, ref value, ref value);
			point.PutCoords((double)(rectangle_0.Right - 3), (double)((rectangle_0.Bottom + rectangle_0.Top) / 2));
			pointCollection.AddPoint(point, ref value, ref value);
			((ISymbol)ilineSymbol_0).Draw((IGeometry)pointCollection);
		}

		private void method_5(IFillSymbol ifillSymbol_0, Rectangle rectangle_0)
		{
			if (!(ifillSymbol_0 is IPictureFillSymbol) || ((IPictureFillSymbol)ifillSymbol_0).Picture != null)
			{
				object value = System.Reflection.Missing.Value;
				IPoint point = new ESRI.ArcGIS.Geometry.Point();
				IPointCollection pointCollection = new Polygon();
				point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
				pointCollection.AddPoint(point, ref value, ref value);
				point.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Top + 3));
				pointCollection.AddPoint(point, ref value, ref value);
				point.PutCoords((double)(rectangle_0.Right - 3), (double)(rectangle_0.Bottom - 3));
				pointCollection.AddPoint(point, ref value, ref value);
				point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Bottom - 3));
				pointCollection.AddPoint(point, ref value, ref value);
				point.PutCoords((double)(rectangle_0.Left + 3), (double)(rectangle_0.Top + 3));
				pointCollection.AddPoint(point, ref value, ref value);
				((ISymbol)ifillSymbol_0).Draw((IGeometry)pointCollection);
			}
		}
	}
}

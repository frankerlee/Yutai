using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Display
{
	public class RotateTrackFeedback : IDisplayFeedback, IRotateTrackerFeedback
	{
		private IGeometryCollection igeometryCollection_0 = new GeometryBag() as IGeometryCollection;

		private ISymbol isymbol_0 = new SimpleMarkerSymbol() as ISymbol;

		private ISymbol isymbol_1 = new SimpleLineSymbol() as ISymbol;

		private ISymbol isymbol_2 = new SimpleFillSymbol() as ISymbol;

		private IScreenDisplay iscreenDisplay_0 = null;

		private IPoint ipoint_0 = null;

		private IPoint ipoint_1 = null;

		private IPoint ipoint_2 = null;

		private double double_0 = 0;

		private IPoint ipoint_3 = null;

		public double Angle
		{
			get
			{
				return this.double_0;
			}
		}

		public IScreenDisplay Display
		{
			set
			{
				this.iscreenDisplay_0 = value;
			}
		}

		public IPoint Origin
		{
			get
			{
				return this.ipoint_1;
			}
			set
			{
				this.ipoint_1 = value;
			}
		}

		public ISymbol Symbol
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public RotateTrackFeedback()
		{
		//	LicenseManager.Validate(typeof(UtilityLicenseProviderCheck), this);
			(this.isymbol_2 as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSNull;
			(this.isymbol_0 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCircle;
			(this.isymbol_0 as ISimpleMarkerSymbol).Size = 5;
		}

		public void AddGeometry(IGeometry igeometry_0)
		{
			object value = Missing.Value;
			this.igeometryCollection_0.AddGeometry(igeometry_0, ref value, ref value);
		}

		public void ClearGeometry()
		{
			this.igeometryCollection_0.RemoveGeometries(0, this.igeometryCollection_0.GeometryCount);
		}

		private void method_0(double double_1)
		{
			int num;
			int num1;
			int num2;
			int num3;
			if (double_1 != 0)
			{
				for (int i = 0; i < this.igeometryCollection_0.GeometryCount; i++)
				{
					IGeometry geometry = this.igeometryCollection_0.Geometry[i];
					ITransform2D transform2D = (geometry as IClone).Clone() as ITransform2D;
					transform2D.Rotate(this.ipoint_1, double_1);
					if (!(geometry.GeometryType == esriGeometryType.esriGeometryMultipoint ? false : geometry.GeometryType != esriGeometryType.esriGeometryPoint))
					{
						if (this.isymbol_0 != null)
						{
							if (this.ipoint_3 == null)
							{
								this.isymbol_0.Draw(transform2D as IGeometry);
								this.ipoint_3 = transform2D as IPoint;
							}
							else
							{
								this.iscreenDisplay_0.DisplayTransformation.FromMapPoint(this.ipoint_3, out num, out num1);
								this.iscreenDisplay_0.DisplayTransformation.FromMapPoint(transform2D as IPoint, out num2, out num3);
								if ((num - num2) * (num - num2) + (num1 - num3) * (num1 - num3) > 4)
								{
									this.isymbol_0.Draw(transform2D as IGeometry);
									this.ipoint_3 = transform2D as IPoint;
								}
							}
						}
					}
					else if (geometry.GeometryType == esriGeometryType.esriGeometryPolyline)
					{
						if (this.isymbol_1 != null)
						{
							this.isymbol_1.Draw(transform2D as IGeometry);
						}
					}
					else if (geometry.GeometryType == esriGeometryType.esriGeometryPolygon && this.isymbol_2 != null)
					{
						this.isymbol_2.Draw(transform2D as IGeometry);
					}
				}
			}
		}

		private void method_1(double double_1, int int_0)
		{
			for (int i = 0; i < this.igeometryCollection_0.GeometryCount; i++)
			{
				IGeometry geometry = this.igeometryCollection_0.Geometry[i];
				ITransform2D transform2D = (geometry as IClone).Clone() as ITransform2D;
				transform2D.Rotate(this.ipoint_1, double_1);
				if (!(geometry.GeometryType == esriGeometryType.esriGeometryMultipoint ? false : geometry.GeometryType != esriGeometryType.esriGeometryPoint))
				{
					if (this.isymbol_0 != null)
					{
						this.isymbol_0.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
						this.isymbol_0.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
						this.isymbol_0.Draw(transform2D as IGeometry);
						this.isymbol_0.ResetDC();
					}
				}
				else if (geometry.GeometryType == esriGeometryType.esriGeometryPolyline)
				{
					if (this.isymbol_1 != null)
					{
						this.isymbol_1.ROP2 = esriRasterOpCode.esriROPXOrPen;
						this.isymbol_1.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
						this.isymbol_1.Draw(transform2D as IGeometry);
						this.isymbol_1.ResetDC();
					}
				}
				else if (geometry.GeometryType == esriGeometryType.esriGeometryPolygon && this.isymbol_1 != null)
				{
					this.isymbol_1.ROP2 = esriRasterOpCode.esriROPXOrPen;
					this.isymbol_1.SetupDC(int_0, this.iscreenDisplay_0.DisplayTransformation);
					this.isymbol_1.Draw(transform2D as IGeometry);
					this.isymbol_1.ResetDC();
				}
			}
		}

		public void MoveTo(IPoint ipoint_4)
		{
			if ((this.ipoint_2 == null ? false : this.ipoint_1 != null))
			{
				if (this.iscreenDisplay_0 != null)
				{
					Graphics graphic = Graphics.FromHwnd(new IntPtr(this.iscreenDisplay_0.hWnd));
					IntPtr hdc = graphic.GetHdc();
					if (this.ipoint_0 != null)
					{
						this.method_1(this.double_0, hdc.ToInt32());
					}
					IConstructAngle geometryEnvironmentClass = new GeometryEnvironment() as IConstructAngle;
					this.double_0 = geometryEnvironmentClass.ConstructThreePoint(this.ipoint_2, this.ipoint_1, ipoint_4);
					this.method_1(this.double_0, hdc.ToInt32());
					graphic.ReleaseHdc(hdc);
					graphic.Dispose();
				}
				this.ipoint_0 = ipoint_4;
			}
		}

		public void Refresh(int int_0)
		{
			if (this.iscreenDisplay_0 != null)
			{
				this.method_1(this.double_0, int_0);
				this.ipoint_0 = null;
			}
		}

		public void Start(IPoint ipoint_4)
		{
			this.ipoint_3 = null;
			this.ipoint_0 = null;
			this.ipoint_2 = ipoint_4;
		}

		public double Stop()
		{
			return this.double_0;
		}
	}
}
using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.ExtendClass
{
	[Guid("F54738E3-CAEE-419d-B109-852E3A76B84E")]
	public class CustomLegend : IBoundsProperties, IClone, IElement, IElementProperties, IElementProperties2, IGraphicElement, ITransform2D, ICustomLegend, IPersistStream, IPersist, IDocumentVersionSupportGEN
	{
		private IGroupElement m_pGroupElement = new GroupElement() as IGroupElement ;

		private IEnvelope m_pEnv = new Envelope() as IEnvelope;

		private string m_LegendInfo = "";

		public bool AutoTransform
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties).AutoTransform;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties).AutoTransform = value;
			}
		}

		public object CustomProperty
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties).CustomProperty;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties).CustomProperty = value;
			}
		}

		public bool FixedAspectRatio
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public bool FixedSize
		{
			get
			{
				return true;
			}
		}

		public IGeometry Geometry
		{
			get
			{
				return (this.m_pGroupElement as IElement).Geometry;
			}
			set
			{
				this.m_pEnv = value.Envelope;
				(this.m_pGroupElement as IElement).Geometry = value;
			}
		}

		public string LegendInfo
		{
			get
			{
				return this.m_LegendInfo;
			}
			set
			{
				this.m_LegendInfo = value;
			}
		}

		public bool Locked
		{
			get
			{
				return (this.m_pGroupElement as IElement).Locked;
			}
			set
			{
				(this.m_pGroupElement as IElement).Locked = value;
			}
		}

		public string Name
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties).Name;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties).Name = value;
			}
		}

		internal IPropertySet PropertySet
		{
			get
			{
				IPropertySet propertySetClass = new PropertySet();
				propertySetClass.SetProperty("Element", this.m_pGroupElement);
				propertySetClass.SetProperty("LegendInfo", this.m_LegendInfo);
				return propertySetClass;
			}
			set
			{
				IPropertySet propertySet = value;
				this.m_pGroupElement = propertySet.GetProperty("Element") as IGroupElement;
				this.m_LegendInfo = propertySet.GetProperty("LegendInfo").ToString();
			}
		}

		public double ReferenceScale
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties2).ReferenceScale;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties2).ReferenceScale = value;
			}
		}

		public ISelectionTracker SelectionTracker
		{
			get
			{
				return (this.m_pGroupElement as IElement).SelectionTracker;
			}
		}

		public ISpatialReference SpatialReference
		{
			get
			{
				return (this.m_pGroupElement as IGraphicElement).SpatialReference;
			}
			set
			{
				(this.m_pGroupElement as IGraphicElement).SpatialReference = value;
			}
		}

		public string Type
		{
			get
			{
				return (this.m_pGroupElement as IElementProperties).Type;
			}
			set
			{
				(this.m_pGroupElement as IElementProperties).Type = value;
			}
		}

		public CustomLegend()
		{
		}

		public void Activate(IDisplay Display)
		{
			(this.m_pGroupElement as IElement).Activate(Display);
		}

		public void Assign(IClone src)
		{
			(this.m_pGroupElement as IClone).Assign(src);
		}

		public bool CanRotate()
		{
			return (this.m_pGroupElement as IElementProperties2).CanRotate();
		}

		public IClone Clone()
		{
			return (this.m_pGroupElement as IClone).Clone();
		}

		public object ConvertToSupportedObject(esriArcGISVersion docVersion)
		{
			return this.m_pGroupElement;
		}

		public void Deactivate()
		{
			(this.m_pGroupElement as IElement).Deactivate();
		}

		public void Draw(IDisplay Display, ITrackCancel TrackCancel)
		{
			(this.m_pGroupElement as IElement).Draw(Display, TrackCancel);
		}

		public void GetClassID(out Guid pClassID)
		{
			pClassID = new Guid("F54738E3-CAEE-419d-B109-852E3A76B84E");
		}

		public void GetSizeMax(out _ULARGE_INTEGER pcbSize)
		{
			(this.PropertySet as IPersistStream).GetSizeMax(out pcbSize);
		}

		public bool HitTest(double x, double y, double Tolerance)
		{
			return (this.m_pGroupElement as IElement).HitTest(x, y, Tolerance);
		}

		public void Init(IActiveView pAv, IPoint pt)
		{
			JLKLegendAssiatant jLKLegendAssiatant = new JLKLegendAssiatant();
			jLKLegendAssiatant.LoadXml(this.m_LegendInfo);
			IEnvelope envelopeClass = new Envelope() as IEnvelope;
			IPoint upperLeft = null;
			if (this.m_pGroupElement != null)
			{
				envelopeClass = this.Geometry.Envelope;
				if (!envelopeClass.IsEmpty)
				{
					upperLeft = envelopeClass.UpperLeft;
				}
			}
			if (upperLeft == null)
			{
				IPoint pointClass = new Point()
				{
				    X = pt.X,
				    Y = pt.Y
				} as IPoint;
				upperLeft = pointClass;
			}
			this.m_pGroupElement = jLKLegendAssiatant.CreateElement(pAv, pt) as IGroupElement;
			envelopeClass = this.Geometry.Envelope;
			if ((envelopeClass.IsEmpty ? false : pt != null))
			{
				IEnvelope envelope = new Envelope() as IEnvelope;
				envelope.PutCoords(upperLeft.X, upperLeft.Y - envelopeClass.Height, upperLeft.X + envelopeClass.Width, upperLeft.Y);
			}
		}

		public void IsDirty()
		{
		}

		public bool IsEqual(IClone other)
		{
			return (this.m_pGroupElement as IClone).IsEqual(other);
		}

		public bool IsIdentical(IClone other)
		{
			return (this.m_pGroupElement as IClone).IsIdentical(other);
		}

		public bool IsSupportedAtVersion(esriArcGISVersion docVersion)
		{
			return (esriArcGISVersion.esriArcGISVersion83 != docVersion ? true : false);
		}

		public void Load(IStream pstm)
		{
			IPropertySet propertySetClass = new PropertySet();
			(propertySetClass as IPersistStream).Load(pstm);
			this.PropertySet = propertySetClass;
		}

		public void Move(double dx, double dy)
		{
			(this.m_pGroupElement as ITransform2D).Move(dx, dy);
		}

		public void MoveVector(ILine v)
		{
			(this.m_pGroupElement as ITransform2D).MoveVector(v);
		}

		public void QueryBounds(IDisplay Display, IEnvelope Bounds)
		{
			(this.m_pGroupElement as IElement).QueryBounds(Display, Bounds);
		}

		public void QueryOutline(IDisplay Display, IPolygon Outline)
		{
			(this.m_pGroupElement as IElement).QueryOutline(Display, Outline);
		}

		public void Rotate(IPoint Origin, double rotationAngle)
		{
			(this.m_pGroupElement as ITransform2D).Rotate(Origin, rotationAngle);
		}

		public void Save(IStream pstm, int fClearDirty)
		{
			(this.PropertySet as IPersistStream).Save(pstm, fClearDirty);
		}

		public void Scale(IPoint Origin, double sx, double sy)
		{
			(this.m_pGroupElement as ITransform2D).Scale(Origin, sx, sy);
		}

		public void Transform(esriTransformDirection direction, ITransformation transformation)
		{
			(this.m_pGroupElement as ITransform2D).Transform(direction, transformation);
		}
	}
}
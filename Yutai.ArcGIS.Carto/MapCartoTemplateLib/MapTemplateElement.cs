using System;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public abstract class MapTemplateElement
    {
        protected IElement m_pElement;
        public string TempleteGuid;

        public MapTemplateElement(MapCartoTemplateLib.MapTemplate pMapTemplate)
        {
            this.m_pElement = null;
            this.MapTemplateGallery = pMapTemplate.MapTemplateGallery;
            this.OID = -1;
            this.MapTemplate = pMapTemplate;
            this.TempleteGuid = pMapTemplate.Guid;
            this.ElementLocation = new MapCartoTemplateLib.ElementLocation();
        }

        public MapTemplateElement(int oid, MapCartoTemplateLib.MapTemplate pMapTemplate)
        {
            this.m_pElement = null;
            this.MapTemplateGallery = pMapTemplate.MapTemplateGallery;
            this.OID = oid;
            this.MapTemplate = pMapTemplate;
            this.ElementLocation = new MapCartoTemplateLib.ElementLocation();
        }

        public void ChangePosition(IPageLayout pPageLayout)
        {
            IPoint lowerLeft;
            double num3;
            double num4;
            double xOffset = this.ElementLocation.XOffset;
            double yOffset = this.ElementLocation.YOffset;
            IPoint position = this.GetPosition(pPageLayout);
            if (this.m_pElement is ITextElement)
            {
                lowerLeft = this.m_pElement.Geometry.Envelope.LowerLeft;
                num3 = lowerLeft.X - position.X;
                num4 = lowerLeft.Y - position.Y;
                xOffset += num3;
                yOffset += num4;
            }
            else if (this.m_pElement is IMapSurroundFrame)
            {
                lowerLeft = this.m_pElement.Geometry.Envelope.LowerLeft;
                num3 = lowerLeft.X - position.X;
                num4 = lowerLeft.Y - position.Y;
                xOffset += num3;
                yOffset += num4;
            }
            else if (this.MapTemplateElementType == MapCartoTemplateLib.MapTemplateElementType.CustomLegendElement)
            {
                lowerLeft = this.m_pElement.Geometry.Envelope.UpperLeft;
                num3 = lowerLeft.X - position.X;
                num4 = lowerLeft.Y - position.Y;
                xOffset += num3;
                yOffset += num4;
            }
            else
            {
                lowerLeft = this.m_pElement.Geometry.Envelope.LowerLeft;
                num3 = lowerLeft.X - position.X;
                num4 = lowerLeft.Y - position.Y;
                xOffset += num3;
                yOffset += num4;
            }
            this.ElementLocation.XOffset = xOffset;
            this.ElementLocation.YOffset = yOffset;
            this.Save();
        }

        public abstract MapTemplateElement Clone(MapCartoTemplateLib.MapTemplate pMapTemplate);

        public virtual void CopyTo(MapTemplateElement pElement)
        {
            pElement.Name = this.Name;
            if (this.Element != null)
            {
                pElement.Element = (this.Element as IClone).Clone() as IElement;
            }
            pElement.ElementLocation = this.ElementLocation.Clone();
            if (this.Style != null)
            {
                if (this.Style is IClone)
                {
                    pElement.Style = (this.Style as IClone).Clone();
                }
                else
                {
                    pElement.Style = this.Style;
                }
            }
        }

        public virtual IElement CreateElement(IPageLayout pPageLayout)
        {
            return null;
        }

        public static MapTemplateElement CreateMapTemplateElement(IPropertySet pPropertySet,
            MapCartoTemplateLib.MapTemplate pMapTemplate)
        {
            string typeName = Convert.ToString(pPropertySet.GetProperty("ElementType"));
            MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery = pMapTemplate.MapTemplateGallery;
            Type type = Type.GetType(typeName);
            try
            {
                MapTemplateElement element =
                    Activator.CreateInstance(type, new object[] {-1, pMapTemplate}) as MapTemplateElement;
                element.Load(pPropertySet);
                return element;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static MapTemplateElement CreateMapTemplateElement(int id,
            MapCartoTemplateLib.MapTemplate pMapTemplate)
        {
            MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery = pMapTemplate.MapTemplateGallery;
            IRow row = null;
            row = mapTemplateGallery.MapTemplateElementTable.GetRow(id);
            int index = row.Fields.FindField("ElementType");
            Type type = Type.GetType(row.get_Value(index).ToString());
            try
            {
                MapTemplateElement element =
                    Activator.CreateInstance(type, new object[] {id, pMapTemplate}) as MapTemplateElement;
                element.Initlize();
                return element;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public void Delete()
        {
            if (this.OID != -1)
            {
                try
                {
                    this.MapTemplateGallery.MapTemplateElementTable.GetRow(this.OID).Delete();
                }
                catch (Exception)
                {
                }
            }
        }

        public IElement FindElementByType(IGroupElement igroupElement, string typeName)
        {
            IEnumElement elements = igroupElement.Elements;
            elements.Reset();
            for (IElement element2 = elements.Next(); element2 != null; element2 = elements.Next())
            {
                if ((element2 is IElementProperties) && ((element2 as IElementProperties).Type == typeName))
                {
                    return element2;
                }
                if (element2 is IGroupElement)
                {
                    IElement element4 = this.FindElementByType(element2 as IGroupElement, typeName);
                    if (element4 != null)
                    {
                        return element4;
                    }
                }
            }
            return null;
        }

        public IElement FindElementByType(IPageLayout iPageLayout, string typeName)
        {
            IGraphicsContainer container = iPageLayout as IGraphicsContainer;
            container.Reset();
            for (IElement element = container.Next(); element != null; element = container.Next())
            {
                if ((element is IElementProperties) && ((element as IElementProperties).Type == typeName))
                {
                    return element;
                }
                if (element is IGroupElement)
                {
                    IElement element3 = this.FindElementByType(element as IGroupElement, typeName);
                    if (element3 != null)
                    {
                        return element3;
                    }
                }
            }
            return null;
        }

        public virtual IElement GetElement(IPageLayout pPageLayout)
        {
            if (this.m_pElement == null)
            {
                this.CreateElement(pPageLayout);
            }
            if (this.m_pElement == null)
            {
                return null;
            }
            IPoint position = this.GetPosition(pPageLayout);
            IEnvelope bounds = new EnvelopeClass();
            if (!(this.m_pElement is ILineElement))
            {
                this.m_pElement.QueryBounds((pPageLayout as IActiveView).ScreenDisplay, bounds);
                if (!bounds.IsEmpty)
                {
                    IEnvelope envelope2 = new EnvelopeClass();
                    envelope2.PutCoords(position.X, position.Y, position.X + bounds.Width, position.Y + bounds.Height);
                    try
                    {
                        this.m_pElement.Geometry = envelope2;
                    }
                    catch
                    {
                    }
                }
            }
            return this.m_pElement;
        }

        protected virtual IPoint GetPosition(IPageLayout pPageLayout)
        {
            IPoint point = new PointClass();
            point.PutCoords(0.0, 0.0);
            try
            {
                IPoint upperLeft;
                IGraphicsContainer container = pPageLayout as IGraphicsContainer;
                IElement element = this.FindElementByType(pPageLayout, "外框");
                IEnvelope bounds = null;
                if (element != null)
                {
                    bounds = new EnvelopeClass();
                    element.QueryBounds((pPageLayout as IActiveView).ScreenDisplay, bounds);
                }
                if (bounds == null)
                {
                    container.Reset();
                    IMapFrame frame = container.FindFrame((pPageLayout as IActiveView).FocusMap) as IMapFrame;
                    bounds = (frame as IElement).Geometry.Envelope;
                }
                double num = 0.0;
                if (this.MapTemplate.BorderSymbol is ILineSymbol)
                {
                    num = this.MapTemplate.OutBorderWidth/2.0;
                }
                switch (this.ElementLocation.LocationType)
                {
                    case LocationType.UpperLeft:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                            (upperLeft.Y + this.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.UpperrCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax)/2.0, bounds.YMax);
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                            (upperLeft.Y + this.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.UpperRight:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                            (upperLeft.Y + this.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.LeftUpper:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) - num,
                            upperLeft.Y + this.ElementLocation.YOffset);
                        return point;

                    case LocationType.RightUpper:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) + num,
                            upperLeft.Y + this.ElementLocation.YOffset);
                        return point;

                    case LocationType.LeftCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMin, (bounds.YMin + bounds.YMax)/2.0);
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) - num,
                            upperLeft.Y + this.ElementLocation.YOffset);
                        return point;

                    case LocationType.RightCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMax, (bounds.YMin + bounds.YMax)/2.0);
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) + num,
                            upperLeft.Y + this.ElementLocation.YOffset);
                        return point;

                    case LocationType.LeftLower:
                        upperLeft = bounds.LowerLeft;
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            break;
                        }
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) - num,
                            upperLeft.Y + (this.ElementLocation.YOffset*this.MapTemplate.BottomLengthScale));
                        return point;

                    case LocationType.RightLower:
                        upperLeft = bounds.LowerRight;
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_03FF;
                        }
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) + num,
                            upperLeft.Y + (this.ElementLocation.YOffset*this.MapTemplate.BottomLengthScale));
                        return point;

                    case LocationType.LowerLeft:
                        upperLeft = bounds.LowerLeft;
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_048B;
                        }
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                            (upperLeft.Y + (this.ElementLocation.YOffset*this.MapTemplate.BottomLengthScale)) - num);
                        return point;

                    case LocationType.LowerCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax)/2.0, bounds.YMin);
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_053A;
                        }
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                            (upperLeft.Y + (this.ElementLocation.YOffset*this.MapTemplate.BottomLengthScale)) - num);
                        return point;

                    case LocationType.LowerRight:
                        upperLeft = bounds.LowerRight;
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_05C3;
                        }
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                            (upperLeft.Y + (this.ElementLocation.YOffset*this.MapTemplate.BottomLengthScale)) - num);
                        return point;

                    default:
                        return point;
                }
                point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) - num,
                    upperLeft.Y + this.ElementLocation.YOffset);
                return point;
                Label_03FF:
                point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) + num,
                    upperLeft.Y + this.ElementLocation.YOffset);
                return point;
                Label_048B:
                point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                    (upperLeft.Y + this.ElementLocation.YOffset) - num);
                return point;
                Label_053A:
                point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                    (upperLeft.Y + this.ElementLocation.YOffset) - num);
                return point;
                Label_05C3:
                point.PutCoords(upperLeft.X + this.ElementLocation.XOffset,
                    (upperLeft.Y + this.ElementLocation.YOffset) - num);
            }
            catch
            {
            }
            return point;
        }

        protected virtual IPoint GetRealPosition(IPageLayout pPageLayout)
        {
            return null;
        }

        public virtual void Init()
        {
        }

        protected virtual void Initlize()
        {
            if (this.OID != -1)
            {
                IRow row = this.MapTemplateGallery.MapTemplateElementTable.GetRow(this.OID);
                this.Name = RowAssisant.GetFieldValue(row, "Name").ToString();
                this.m_pElement = this.method_1(RowAssisant.GetFieldValue(row, "Element"));
                if (this.m_pElement is IMapSurroundFrame)
                {
                    this.Style = (this.m_pElement as IMapSurroundFrame).MapSurround;
                }
                string str = RowAssisant.GetFieldValue(row, "Location").ToString();
                this.ElementLocation = new MapCartoTemplateLib.ElementLocation(str);
                IPropertySet set = this.method_3(RowAssisant.GetFieldValue(row, "Attributes"));
                if (set != null)
                {
                    this.PropertySet = set;
                }
            }
        }

        public virtual void Load()
        {
            this.Initlize();
        }

        public void Load(IPropertySet pPropertySet)
        {
            this.Name = Convert.ToString(pPropertySet.GetProperty("Name"));
            this.m_pElement = pPropertySet.GetProperty("Element") as IElement;
            string str = Convert.ToString(pPropertySet.GetProperty("Location"));
            this.ElementLocation = new MapCartoTemplateLib.ElementLocation(str);
            IPropertySet property = pPropertySet.GetProperty("Attributes") as IPropertySet;
            if (property != null)
            {
                this.PropertySet = property;
            }
        }

        private object CloneElement(IElement pElement)
        {
            IClone clone = pElement as IClone;
            IMemoryBlobStream stream = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass
            {
                Stream = stream
            };
            IPropertySet set = new PropertySetClass();
            IPersistStream stream3 = set as IPersistStream;
            try
            {
                set.SetProperty("Element", clone.Clone());
                stream3.Save(pstm, 0);
            }
            catch
            {
            }
            return stream;
        }

        private IElement method_1(object object_1)
        {
            if (object_1 is DBNull)
            {
                return null;
            }
            IMemoryBlobStream o = object_1 as IMemoryBlobStream;
            IPropertySet set = new PropertySetClass();
            IObjectStream pstm = new ObjectStreamClass
            {
                Stream = o
            };
            IPersistStream stream3 = set as IPersistStream;
            stream3.Load(pstm);
            IElement property = null;
            try
            {
                property = set.GetProperty("Element") as IElement;
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(stream3);
            ComReleaser.ReleaseCOMObject(set);
            ComReleaser.ReleaseCOMObject(o);
            set = null;
            return property;
        }

        private object method_2(IPropertySet pPropertySet)
        {
            IMemoryBlobStream stream = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass
            {
                Stream = stream
            };
            (pPropertySet as IPersistStream).Save(pstm, 0);
            return stream;
        }

        private IPropertySet method_3(object object_1)
        {
            if (object_1 is DBNull)
            {
                return null;
            }
            IMemoryBlobStream o = object_1 as IMemoryBlobStream;
            IPropertySet set2 = new PropertySetClass();
            IObjectStream pstm = new ObjectStreamClass
            {
                Stream = o
            };
            (set2 as IPersistStream).Load(pstm);
            ComReleaser.ReleaseCOMObject(o);
            return set2;
        }

        public virtual void Save()
        {
            IRow row = null;
            if (this.OID == -1)
            {
                row = this.MapTemplateGallery.MapTemplateElementTable.CreateRow();
                this.OID = row.OID;
            }
            else
            {
                row = this.MapTemplateGallery.MapTemplateElementTable.GetRow(this.OID);
            }
            RowAssisant.SetFieldValue(row, "Name", this.Name);
            RowAssisant.SetFieldValue(row, "ElementType", base.GetType().ToString());
            RowAssisant.SetFieldValue(row, "Element", this.CloneElement(this.Element));
            RowAssisant.SetFieldValue(row, "TemplateID", this.MapTemplate.OID);
            RowAssisant.SetFieldValue(row, "Location", this.ElementLocation.ToString());
            RowAssisant.SetFieldValue(row, "TemplateGuid", this.TempleteGuid);
            IPropertySet propertySet = this.PropertySet;
            if (propertySet != null)
            {
                RowAssisant.SetFieldValue(row, "Attributes", this.method_2(propertySet));
            }
            row.Store();
        }

        public void Save(IPropertySetArray pPropertySetArray)
        {
            IPropertySet pPropertySet = new PropertySetClass();
            pPropertySet.SetProperty("Name", this.Name);
            pPropertySet.SetProperty("ElementType", base.GetType().ToString());
            pPropertySet.SetProperty("Element", this.Element);
            pPropertySet.SetProperty("Attributes", this.PropertySet);
            pPropertySet.SetProperty("Location", this.ElementLocation.ToString());
            pPropertySetArray.Add(pPropertySet);
        }

        public abstract void Update(IPageLayout pPageLayout);

        public virtual IElement Element
        {
            get { return this.m_pElement; }
            set { this.m_pElement = value; }
        }

        public MapCartoTemplateLib.ElementLocation ElementLocation { get; set; }


        public MapCartoTemplateLib.MapTemplate MapTemplate { get; set; }


        public MapCartoTemplateLib.MapTemplateElementType MapTemplateElementType { get; set; }

        public MapCartoTemplateLib.MapTemplateGallery MapTemplateGallery { get; set; }


        public string Name { get; set; }


        public int OID { get; set; }

        protected abstract IPropertySet PropertySet { get; set; }

        public object Style { get; set; }
    }
}
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
        [CompilerGenerated]
        private MapCartoTemplateLib.ElementLocation elementLocation_0;
        [CompilerGenerated]
        private int int_0;
        protected IElement m_pElement;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateElementType mapTemplateElementType_0;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery_0;
        [CompilerGenerated]
        private object object_0;
        [CompilerGenerated]
        private string string_0;
        public string TempleteGuid;

        public MapTemplateElement(MapCartoTemplateLib.MapTemplate mapTemplate_1)
        {
            this.m_pElement = null;
            this.MapTemplateGallery = mapTemplate_1.MapTemplateGallery;
            this.OID = -1;
            this.MapTemplate = mapTemplate_1;
            this.TempleteGuid = mapTemplate_1.Guid;
            this.ElementLocation = new MapCartoTemplateLib.ElementLocation();
        }

        public MapTemplateElement(int int_1, MapCartoTemplateLib.MapTemplate mapTemplate_1)
        {
            this.m_pElement = null;
            this.MapTemplateGallery = mapTemplate_1.MapTemplateGallery;
            this.OID = int_1;
            this.MapTemplate = mapTemplate_1;
            this.ElementLocation = new MapCartoTemplateLib.ElementLocation();
        }

        public void ChangePosition(IPageLayout ipageLayout_0)
        {
            IPoint lowerLeft;
            double num3;
            double num4;
            double xOffset = this.ElementLocation.XOffset;
            double yOffset = this.ElementLocation.YOffset;
            IPoint position = this.GetPosition(ipageLayout_0);
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

        public abstract MapTemplateElement Clone(MapCartoTemplateLib.MapTemplate mapTemplate_1);
        public virtual void CopyTo(MapTemplateElement mapTemplateElement_0)
        {
            mapTemplateElement_0.Name = this.Name;
            if (this.Element != null)
            {
                mapTemplateElement_0.Element = (this.Element as IClone).Clone() as IElement;
            }
            mapTemplateElement_0.ElementLocation = this.ElementLocation.Clone();
            if (this.Style != null)
            {
                if (this.Style is IClone)
                {
                    mapTemplateElement_0.Style = (this.Style as IClone).Clone();
                }
                else
                {
                    mapTemplateElement_0.Style = this.Style;
                }
            }
        }

        public virtual IElement CreateElement(IPageLayout ipageLayout_0)
        {
            return null;
        }

        public static MapTemplateElement CreateMapTemplateElement(IPropertySet ipropertySet_0, MapCartoTemplateLib.MapTemplate mapTemplate_1)
        {
            string typeName = Convert.ToString(ipropertySet_0.GetProperty("ElementType"));
            MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery = mapTemplate_1.MapTemplateGallery;
            Type type = Type.GetType(typeName);
            try
            {
                MapTemplateElement element = Activator.CreateInstance(type, new object[] { -1, mapTemplate_1 }) as MapTemplateElement;
                element.Load(ipropertySet_0);
                return element;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static MapTemplateElement CreateMapTemplateElement(int int_1, MapCartoTemplateLib.MapTemplate mapTemplate_1)
        {
            MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery = mapTemplate_1.MapTemplateGallery;
            IRow row = null;
            row = mapTemplateGallery.MapTemplateElementTable.GetRow(int_1);
            int index = row.Fields.FindField("ElementType");
            Type type = Type.GetType(row.get_Value(index).ToString());
            try
            {
                MapTemplateElement element = Activator.CreateInstance(type, new object[] { int_1, mapTemplate_1 }) as MapTemplateElement;
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

        public IElement FindElementByType(IGroupElement igroupElement_0, string string_1)
        {
            IEnumElement elements = igroupElement_0.Elements;
            elements.Reset();
            for (IElement element2 = elements.Next(); element2 != null; element2 = elements.Next())
            {
                if ((element2 is IElementProperties) && ((element2 as IElementProperties).Type == string_1))
                {
                    return element2;
                }
                if (element2 is IGroupElement)
                {
                    IElement element4 = this.FindElementByType(element2 as IGroupElement, string_1);
                    if (element4 != null)
                    {
                        return element4;
                    }
                }
            }
            return null;
        }

        public IElement FindElementByType(IPageLayout ipageLayout_0, string string_1)
        {
            IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
            container.Reset();
            for (IElement element = container.Next(); element != null; element = container.Next())
            {
                if ((element is IElementProperties) && ((element as IElementProperties).Type == string_1))
                {
                    return element;
                }
                if (element is IGroupElement)
                {
                    IElement element3 = this.FindElementByType(element as IGroupElement, string_1);
                    if (element3 != null)
                    {
                        return element3;
                    }
                }
            }
            return null;
        }

        public virtual IElement GetElement(IPageLayout ipageLayout_0)
        {
            if (this.m_pElement == null)
            {
                this.CreateElement(ipageLayout_0);
            }
            if (this.m_pElement == null)
            {
                return null;
            }
            IPoint position = this.GetPosition(ipageLayout_0);
            IEnvelope bounds = new EnvelopeClass();
            if (!(this.m_pElement is ILineElement))
            {
                this.m_pElement.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
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

        protected virtual IPoint GetPosition(IPageLayout ipageLayout_0)
        {
            IPoint point = new PointClass();
            point.PutCoords(0.0, 0.0);
            try
            {
                IPoint upperLeft;
                IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
                IElement element = this.FindElementByType(ipageLayout_0, "外框");
                IEnvelope bounds = null;
                if (element != null)
                {
                    bounds = new EnvelopeClass();
                    element.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                }
                if (bounds == null)
                {
                    container.Reset();
                    IMapFrame frame = container.FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                    bounds = (frame as IElement).Geometry.Envelope;
                }
                double num = 0.0;
                if (this.MapTemplate.BorderSymbol is ILineSymbol)
                {
                    num = this.MapTemplate.OutBorderWidth / 2.0;
                }
                switch (this.ElementLocation.LocationType)
                {
                    case LocationType.UpperLeft:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + this.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.UpperrCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax) / 2.0, bounds.YMax);
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + this.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.UpperRight:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + this.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.LeftUpper:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) - num, upperLeft.Y + this.ElementLocation.YOffset);
                        return point;

                    case LocationType.RightUpper:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) + num, upperLeft.Y + this.ElementLocation.YOffset);
                        return point;

                    case LocationType.LeftCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMin, (bounds.YMin + bounds.YMax) / 2.0);
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) - num, upperLeft.Y + this.ElementLocation.YOffset);
                        return point;

                    case LocationType.RightCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMax, (bounds.YMin + bounds.YMax) / 2.0);
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) + num, upperLeft.Y + this.ElementLocation.YOffset);
                        return point;

                    case LocationType.LeftLower:
                        upperLeft = bounds.LowerLeft;
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            break;
                        }
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) - num, upperLeft.Y + (this.ElementLocation.YOffset * this.MapTemplate.BottomLengthScale));
                        return point;

                    case LocationType.RightLower:
                        upperLeft = bounds.LowerRight;
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_03FF;
                        }
                        point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) + num, upperLeft.Y + (this.ElementLocation.YOffset * this.MapTemplate.BottomLengthScale));
                        return point;

                    case LocationType.LowerLeft:
                        upperLeft = bounds.LowerLeft;
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_048B;
                        }
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + (this.ElementLocation.YOffset * this.MapTemplate.BottomLengthScale)) - num);
                        return point;

                    case LocationType.LowerCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax) / 2.0, bounds.YMin);
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_053A;
                        }
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + (this.ElementLocation.YOffset * this.MapTemplate.BottomLengthScale)) - num);
                        return point;

                    case LocationType.LowerRight:
                        upperLeft = bounds.LowerRight;
                        if (!this.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_05C3;
                        }
                        point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + (this.ElementLocation.YOffset * this.MapTemplate.BottomLengthScale)) - num);
                        return point;

                    default:
                        return point;
                }
                point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) - num, upperLeft.Y + this.ElementLocation.YOffset);
                return point;
            Label_03FF:
                point.PutCoords((upperLeft.X + this.ElementLocation.XOffset) + num, upperLeft.Y + this.ElementLocation.YOffset);
                return point;
            Label_048B:
                point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + this.ElementLocation.YOffset) - num);
                return point;
            Label_053A:
                point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + this.ElementLocation.YOffset) - num);
                return point;
            Label_05C3:
                point.PutCoords(upperLeft.X + this.ElementLocation.XOffset, (upperLeft.Y + this.ElementLocation.YOffset) - num);
            }
            catch
            {
            }
            return point;
        }

        protected virtual IPoint GetRealPosition(IPageLayout ipageLayout_0)
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

        public void Load(IPropertySet ipropertySet_0)
        {
            this.Name = Convert.ToString(ipropertySet_0.GetProperty("Name"));
            this.m_pElement = ipropertySet_0.GetProperty("Element") as IElement;
            string str = Convert.ToString(ipropertySet_0.GetProperty("Location"));
            this.ElementLocation = new MapCartoTemplateLib.ElementLocation(str);
            IPropertySet property = ipropertySet_0.GetProperty("Attributes") as IPropertySet;
            if (property != null)
            {
                this.PropertySet = property;
            }
        }

        private object method_0(IElement ielement_0)
        {
            IClone clone = ielement_0 as IClone;
            IMemoryBlobStream stream = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass {
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
            IObjectStream pstm = new ObjectStreamClass {
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

        private object method_2(IPropertySet ipropertySet_0)
        {
            IMemoryBlobStream stream = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = stream
            };
            (ipropertySet_0 as IPersistStream).Save(pstm, 0);
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
            IObjectStream pstm = new ObjectStreamClass {
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
            RowAssisant.SetFieldValue(row, "Element", this.method_0(this.Element));
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

        public void Save(IPropertySetArray ipropertySetArray_0)
        {
            IPropertySet pPropertySet = new PropertySetClass();
            pPropertySet.SetProperty("Name", this.Name);
            pPropertySet.SetProperty("ElementType", base.GetType().ToString());
            pPropertySet.SetProperty("Element", this.Element);
            pPropertySet.SetProperty("Attributes", this.PropertySet);
            pPropertySet.SetProperty("Location", this.ElementLocation.ToString());
            ipropertySetArray_0.Add(pPropertySet);
        }

        public abstract void Update(IPageLayout ipageLayout_0);

        public virtual IElement Element
        {
            get
            {
                return this.m_pElement;
            }
            set
            {
                this.m_pElement = value;
            }
        }

        public MapCartoTemplateLib.ElementLocation ElementLocation
        {
            [CompilerGenerated]
            get
            {
                return this.elementLocation_0;
            }
            [CompilerGenerated]
            set
            {
                this.elementLocation_0 = value;
            }
        }

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public MapCartoTemplateLib.MapTemplateElementType MapTemplateElementType
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplateElementType_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.mapTemplateElementType_0 = value;
            }
        }

        public MapCartoTemplateLib.MapTemplateGallery MapTemplateGallery
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplateGallery_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplateGallery_0 = value;
            }
        }

        public string Name
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }

        public int OID
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.int_0 = value;
            }
        }

        protected abstract IPropertySet PropertySet { get; set; }

        public object Style
        {
            [CompilerGenerated]
            get
            {
                return this.object_0;
            }
            [CompilerGenerated]
            set
            {
                this.object_0 = value;
            }
        }
    }
}


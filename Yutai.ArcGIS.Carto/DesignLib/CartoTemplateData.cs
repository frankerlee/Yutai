using System;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    internal class CartoTemplateData
    {
        private double double_0;
        private double double_1;
        private double double_2;
        private double double_3;
        private double double_4;
        private double double_5;
        private double double_6;
        private double double_7;
        private double double_8;
        private IRow irow_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;

        public CartoTemplateData(IRow irow_1)
        {
            this.irow_0 = null;
            this.string_0 = "";
            this.double_0 = 0.0;
            this.double_1 = 0.0;
            this.double_2 = 0.0;
            this.string_1 = "";
            this.string_2 = "";
            this.double_3 = 0.1;
            this.double_4 = 0.1;
            this.double_5 = 100.0;
            this.double_6 = 100.0;
            this.double_7 = 0.1;
            this.double_8 = 10.0;
            this.string_3 = "";
            this.irow_0 = irow_1;
            CartoTemplateTableStruct struct2 = new CartoTemplateTableStruct();
            ITable table = irow_1.Table;
            this.string_0 = irow_1.get_Value(table.FindField(struct2.NameFieldName)).ToString();
            this.string_1 = irow_1.get_Value(table.FindField(struct2.DescriptionFieldName)).ToString();
            this.string_2 = this.method_0(irow_1.get_Value(table.FindField(struct2.TuKuoInfoFieldName)));
            try
            {
                this.double_0 = double.Parse(irow_1.get_Value(table.FindField(struct2.ScaleFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_1 = double.Parse(irow_1.get_Value(table.FindField(struct2.WidthFieldName)).ToString());
                this.double_2 = double.Parse(irow_1.get_Value(table.FindField(struct2.HeightFieldName)).ToString());
            }
            catch
            {
            }
            this.string_3 = this.method_0(irow_1.get_Value(table.FindField(struct2.LegendInfoFieldName)));
            try
            {
                this.double_3 = double.Parse(irow_1.get_Value(table.FindField(struct2.InOutDistFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_7 = double.Parse(irow_1.get_Value(table.FindField(struct2.OutBorderWidthFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_8 = double.Parse(irow_1.get_Value(table.FindField(struct2.StartCoodinateMultipleFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_5 = double.Parse(irow_1.get_Value(table.FindField(struct2.XIntervalFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_6 = double.Parse(irow_1.get_Value(table.FindField(struct2.YIntervalFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_4 = double.Parse(irow_1.get_Value(table.FindField(struct2.TitleDistFieldName)).ToString());
            }
            catch
            {
            }
        }

        public CartoTemplateData(IRow irow_1, string string_4)
        {
            this.irow_0 = null;
            this.string_0 = "";
            this.double_0 = 0.0;
            this.double_1 = 0.0;
            this.double_2 = 0.0;
            this.string_1 = "";
            this.string_2 = "";
            this.double_3 = 0.1;
            this.double_4 = 0.1;
            this.double_5 = 100.0;
            this.double_6 = 100.0;
            this.double_7 = 0.1;
            this.double_8 = 10.0;
            this.string_3 = "";
            this.irow_0 = irow_1;
            CartoTemplateTableStruct struct2 = new CartoTemplateTableStruct();
            ITable table = irow_1.Table;
            this.string_0 = irow_1.get_Value(table.FindField(struct2.NameFieldName)).ToString();
            this.string_1 = irow_1.get_Value(table.FindField(struct2.DescriptionFieldName)).ToString();
            this.string_2 = string_4;
            this.string_3 = this.method_0(irow_1.get_Value(table.FindField(struct2.LegendInfoFieldName)));
            try
            {
                this.double_0 = double.Parse(irow_1.get_Value(table.FindField(struct2.ScaleFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_3 = double.Parse(irow_1.get_Value(table.FindField(struct2.InOutDistFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_1 = double.Parse(irow_1.get_Value(table.FindField(struct2.WidthFieldName)).ToString());
                this.double_2 = double.Parse(irow_1.get_Value(table.FindField(struct2.HeightFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_7 = double.Parse(irow_1.get_Value(table.FindField(struct2.OutBorderWidthFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_8 = double.Parse(irow_1.get_Value(table.FindField(struct2.StartCoodinateMultipleFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_5 = double.Parse(irow_1.get_Value(table.FindField(struct2.XIntervalFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_6 = double.Parse(irow_1.get_Value(table.FindField(struct2.YIntervalFieldName)).ToString());
            }
            catch
            {
            }
            try
            {
                this.double_4 = double.Parse(irow_1.get_Value(table.FindField(struct2.TitleDistFieldName)).ToString());
            }
            catch
            {
            }
        }

        public void Delete()
        {
            this.irow_0.Delete();
        }

        public bool GetTK(IPageLayout ipageLayout_0)
        {
            IEnvelope envelope;
            IEnvelope envelope2;
            if (this.string_2.Length == 0)
            {
                return false;
            }
            IMapFrame frame = (ipageLayout_0 as IActiveView).GraphicsContainer.FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
            if ((this.double_1 != 0.0) || (this.double_2 != 0.0))
            {
                ipageLayout_0.Page.PutCustomSize(this.double_1 + 6.0, this.double_2 + 6.0);
                envelope = (frame as IElement).Geometry.Envelope;
                envelope2 = new EnvelopeClass();
                if (this.double_1 == 0.0)
                {
                    this.double_1 = envelope.Width;
                }
                if (this.double_2 == 0.0)
                {
                    this.double_2 = envelope.Height;
                }
                envelope2.PutCoords(envelope.XMin, envelope.YMin, envelope.XMin + this.double_1, envelope.YMin + this.double_2);
                IAffineTransformation2D transformation = new AffineTransformation2DClass();
                transformation.DefineFromEnvelopes(envelope, envelope2);
                ITransform2D transformd = frame as ITransform2D;
                transformd.Transform(esriTransformDirection.esriTransformForward, transformation);
                double dx = 0.0;
                double dy = 0.0;
                if (envelope.XMin < 2.0)
                {
                    dx = 2.0;
                }
                if (envelope.YMin < 2.0)
                {
                    dy = 2.0;
                }
                if ((dx != 0.0) && (dy != 0.0))
                {
                    transformd.Move(dx, dy);
                }
            }
            if (this.double_0 > 0.0)
            {
                envelope = (frame as IElement).Geometry.Envelope;
                double num3 = (envelope.Width * this.double_0) / 100.0;
                double num4 = (envelope.Height * this.double_0) / 100.0;
                IEnvelope extent = (frame.Map as IActiveView).Extent;
                envelope2 = new EnvelopeClass();
                envelope2.PutCoords(extent.XMin, extent.YMin, extent.XMin + num3, extent.YMin + num4);
                (frame.Map as IActiveView).Extent = envelope2;
            }
            frame.Border = null;
            return true;
        }

        private string method_0(object object_0)
        {
            if (object_0 is DBNull)
            {
                return "";
            }
            IMemoryBlobStream o = object_0 as IMemoryBlobStream;
            IPropertySet set = new PropertySetClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = o
            };
            IPersistStream stream3 = set as IPersistStream;
            stream3.Load(pstm);
            string str2 = "";
            try
            {
                str2 = set.GetProperty("TK").ToString();
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(stream3);
            ComReleaser.ReleaseCOMObject(set);
            ComReleaser.ReleaseCOMObject(o);
            set = null;
            return str2;
        }

        public string Description
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public double InOutDist
        {
            get
            {
                return this.double_3;
            }
            set
            {
                this.double_3 = value;
            }
        }

        public string LegendInfo
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public string Name
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

        public int OID
        {
            get
            {
                return this.irow_0.OID;
            }
        }

        public double OutBorderWidth
        {
            get
            {
                return this.double_7;
            }
            set
            {
                this.double_7 = value;
            }
        }

        public IRow Row
        {
            get
            {
                return this.irow_0;
            }
        }

        public double StartCoodinateMultiple
        {
            get
            {
                return this.double_8;
            }
            set
            {
                this.double_8 = value;
            }
        }

        public double TitleDist
        {
            get
            {
                return this.double_4;
            }
            set
            {
                this.double_4 = value;
            }
        }

        public string TuKuoInfo
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public double XInterval
        {
            get
            {
                return this.double_5;
            }
            set
            {
                this.double_5 = value;
            }
        }

        public double YInterval
        {
            get
            {
                return this.double_6;
            }
            set
            {
                this.double_6 = value;
            }
        }
    }
}


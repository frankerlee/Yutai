using System;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateParam
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private DataType dataType_0;
        [CompilerGenerated]
        private int int_0;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplate mapTemplate_0;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery_0;
        [CompilerGenerated]
        private object object_0;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;

        public MapTemplateParam(int int_1, MapCartoTemplateLib.MapTemplate mapTemplate_1)
        {
            this.MapTemplateGallery = mapTemplate_1.MapTemplateGallery;
            this.ParamDataType = DataType.String;
            this.TempleteGuid = mapTemplate_1.Guid;
            this.OID = int_1;
            this.MapTemplate = mapTemplate_1;
        }

        public MapTemplateParam Clone(MapCartoTemplateLib.MapTemplate mapTemplate_1)
        {
            return new MapTemplateParam(-1, mapTemplate_1) { TempleteGuid = this.TempleteGuid, Name = this.Name, AllowNull = this.AllowNull, Description = this.Description, ParamDataType = this.ParamDataType, Value = this.Value };
        }

        public void Delete()
        {
            if (this.OID != -1)
            {
                this.MapTemplateGallery.MapTemplateParamTable.GetRow(this.OID).Delete();
            }
        }

        public void Load()
        {
            if (this.OID != -1)
            {
                IRow row = null;
                row = this.MapTemplateGallery.MapTemplateParamTable.GetRow(this.OID);
                this.Name = RowAssisant.GetFieldValue(row, "Name").ToString();
                this.AllowNull = Convert.ToInt32(RowAssisant.GetFieldValue(row, "AllowNull")) == 1;
                this.ParamDataType = Convert.ToInt32(RowAssisant.GetFieldValue(row, "DataType"));
                this.Description = RowAssisant.GetFieldValue(row, "Description").ToString();
            }
        }

        public void Load(IPropertySet ipropertySet_0)
        {
            this.Name = Convert.ToString(ipropertySet_0.GetProperty("Name"));
            this.Description = Convert.ToString(ipropertySet_0.GetProperty("Description"));
            int num = Convert.ToInt32(ipropertySet_0.GetProperty("AllowNull"));
            this.AllowNull = num == 1;
            this.ParamDataType = Convert.ToInt32(ipropertySet_0.GetProperty("DataType"));
        }

        public void Save()
        {
            IRow row = null;
            if (this.OID == -1)
            {
                row = this.MapTemplateGallery.MapTemplateParamTable.CreateRow();
                RowAssisant.SetFieldValue(row, "MapTemplateOID", this.MapTemplate.OID);
                this.OID = row.OID;
            }
            else
            {
                row = this.MapTemplateGallery.MapTemplateParamTable.GetRow(this.OID);
            }
            RowAssisant.SetFieldValue(row, "Name", this.Name);
            RowAssisant.SetFieldValue(row, "Description", this.Description);
            RowAssisant.SetFieldValue(row, "AllowNull", this.AllowNull ? 1 : 0);
            RowAssisant.SetFieldValue(row, "DataType", (int) this.ParamDataType);
            RowAssisant.SetFieldValue(row, "TemplateGuid", this.TempleteGuid);
            row.Store();
        }

        public void Save(IPropertySetArray ipropertySetArray_0)
        {
            IPropertySet pPropertySet = new PropertySetClass();
            pPropertySet.SetProperty("Name", this.Name);
            pPropertySet.SetProperty("Description", this.Description);
            pPropertySet.SetProperty("AllowNull", this.AllowNull ? 1 : 0);
            pPropertySet.SetProperty("DataType", (int) this.ParamDataType);
            ipropertySetArray_0.Add(pPropertySet);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public bool AllowNull
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public string Description
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
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
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
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
            set
            {
                this.int_0 = value;
            }
        }

        public DataType ParamDataType
        {
            [CompilerGenerated]
            get
            {
                return this.dataType_0;
            }
            [CompilerGenerated]
            set
            {
                this.dataType_0 = value;
            }
        }

        public string TempleteGuid
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

        public object Value
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


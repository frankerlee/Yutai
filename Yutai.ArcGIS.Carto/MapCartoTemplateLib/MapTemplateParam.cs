using System;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateParam
    {
        public MapTemplateParam(int id, MapCartoTemplateLib.MapTemplate pMapTemplate)
        {
            this.MapTemplateGallery = pMapTemplate.MapTemplateGallery;
            this.ParamDataType = DataType.String;
            this.TempleteGuid = pMapTemplate.Guid;
            this.OID = id;
            this.MapTemplate = pMapTemplate;
        }

        public MapTemplateParam Clone(MapCartoTemplateLib.MapTemplate pMapTemplate)
        {
            return new MapTemplateParam(-1, pMapTemplate)
            {
                TempleteGuid = this.TempleteGuid,
                Name = this.Name,
                AllowNull = this.AllowNull,
                Description = this.Description,
                ParamDataType = this.ParamDataType,
                Value = this.Value
            };
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
                this.ParamDataType = (DataType) Convert.ToInt32(RowAssisant.GetFieldValue(row, "DataType"));
                this.Description = RowAssisant.GetFieldValue(row, "Description").ToString();
            }
        }

        public void Load(IPropertySet pPropertySet)
        {
            this.Name = Convert.ToString(pPropertySet.GetProperty("Name"));
            this.Description = Convert.ToString(pPropertySet.GetProperty("Description"));
            int num = Convert.ToInt32(pPropertySet.GetProperty("AllowNull"));
            this.AllowNull = num == 1;
            this.ParamDataType = (DataType) Convert.ToInt32(pPropertySet.GetProperty("DataType"));
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

        public void Save(IPropertySetArray pPropertySetArray)
        {
            IPropertySet pPropertySet = new PropertySetClass();
            pPropertySet.SetProperty("Name", this.Name);
            pPropertySet.SetProperty("Description", this.Description);
            pPropertySet.SetProperty("AllowNull", this.AllowNull ? 1 : 0);
            pPropertySet.SetProperty("DataType", (int) this.ParamDataType);
            pPropertySetArray.Add(pPropertySet);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public bool AllowNull { get; set; }

        public string Description { get; set; }

        public MapCartoTemplateLib.MapTemplate MapTemplate { get; set; }
        public MapCartoTemplateLib.MapTemplateGallery MapTemplateGallery { get; set; }

        public string Name { get; set; }

        public int OID { get; set; }

        public DataType ParamDataType { get; set; }
        public string TempleteGuid { get; set; }

        public object Value { get; set; }
    }
}
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateClass
    {
        [CompilerGenerated]
        private int int_0;
        public List<MapCartoTemplateLib.MapTemplate> mapTemplate;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery_0;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;

        public MapTemplateClass(int int_1, MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery_1)
        {
            this.OID = int_1;
            this.MapTemplateGallery = mapTemplateGallery_1;
            if (int_1 == -1)
            {
                this.Guid = System.Guid.NewGuid().ToString();
            }
            else
            {
                IRow row = null;
                row = this.MapTemplateGallery.MapTemplateClassTable.GetRow(this.OID);
                try
                {
                    this.Guid = RowAssisant.GetFieldValue(row, "Guid").ToString();
                }
                catch
                {
                }
                if (string.IsNullOrEmpty(this.Guid))
                {
                    this.Guid = System.Guid.NewGuid().ToString();
                    RowAssisant.SetFieldValue(row, "Guid", this.Guid);
                    row.Store();
                }
            }
        }

        public void AddMapTemplate(MapCartoTemplateLib.MapTemplate mapTemplate_0)
        {
            if (mapTemplate_0 != null)
            {
                if (this.mapTemplate == null)
                {
                    this.mapTemplate = new List<MapCartoTemplateLib.MapTemplate>();
                }
                if (!this.mapTemplate.Contains(mapTemplate_0))
                {
                    this.mapTemplate.Add(mapTemplate_0);
                }
            }
        }

        public void Delete()
        {
            if (this.OID != -1)
            {
                if (this.mapTemplate != null)
                {
                    foreach (MapCartoTemplateLib.MapTemplate template in this.mapTemplate)
                    {
                        template.Delete();
                    }
                }
                this.MapTemplateGallery.MapTemplateClassTable.GetRow(this.OID).Delete();
                this.RemoveAllMapTemplate();
            }
        }

        public void Load()
        {
            if (this.mapTemplate != null)
            {
                this.mapTemplate.Clear();
            }
            IQueryFilter queryFilter = new QueryFilterClass {
                WhereClause = "ClassID=" + this.OID
            };
            ICursor cursor = this.MapTemplateGallery.MapTemplateTable.Search(queryFilter, false);
            IRow o = cursor.NextRow();
            int index = this.MapTemplateGallery.MapTemplateTable.FindField("Name");
            while (o != null)
            {
                string str = o.get_Value(index).ToString();
                MapCartoTemplateLib.MapTemplate template2 = new MapCartoTemplateLib.MapTemplate(o.OID, this) {
                    Name = str
                };
                this.AddMapTemplate(template2);
                o = cursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(o);
        }

        public void RemoveAllMapTemplate()
        {
            if (this.mapTemplate != null)
            {
                this.mapTemplate.Clear();
            }
        }

        public void RemoveMapTemplate(MapCartoTemplateLib.MapTemplate mapTemplate_0)
        {
            if (((mapTemplate_0 != null) && (this.mapTemplate != null)) && this.mapTemplate.Contains(mapTemplate_0))
            {
                this.mapTemplate.Remove(mapTemplate_0);
            }
        }

        public void Save()
        {
            int index = this.MapTemplateGallery.MapTemplateClassTable.FindField("Name");
            int num2 = this.MapTemplateGallery.MapTemplateClassTable.FindField("Description");
            IRow row = null;
            if (this.OID == -1)
            {
                row = this.MapTemplateGallery.MapTemplateClassTable.CreateRow();
                this.OID = row.OID;
            }
            else
            {
                row = this.MapTemplateGallery.MapTemplateClassTable.GetRow(this.OID);
            }
            row.set_Value(index, this.Name);
            row.set_Value(num2, this.Description);
            row.Store();
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

        public string Guid
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

        public List<MapCartoTemplateLib.MapTemplate> MapTemplate
        {
            get
            {
                if (this.mapTemplate == null)
                {
                    this.mapTemplate = new List<MapCartoTemplateLib.MapTemplate>();
                }
                return this.mapTemplate;
            }
            set
            {
                this.RemoveAllMapTemplate();
                if (value != null)
                {
                    foreach (MapCartoTemplateLib.MapTemplate template in value)
                    {
                        this.AddMapTemplate(template);
                    }
                }
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
    }
}


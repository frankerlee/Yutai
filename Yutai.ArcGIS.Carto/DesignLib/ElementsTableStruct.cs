namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ElementsTableStruct : TableStruct
    {
        public string AttributesFieldName
        {
            get
            {
                return "Attributes";
            }
        }

        public override string DescriptionFieldName
        {
            get
            {
                return "Description";
            }
        }

        public string ElementTypeFieldName
        {
            get
            {
                return "ElementType";
            }
        }

        public string ExtendPropertyFieldName
        {
            get
            {
                return "ExtendProperty";
            }
        }

        public override string IDFieldName
        {
            get
            {
                return "ObjectID";
            }
        }

        public string LocationFieldName
        {
            get
            {
                return "Location";
            }
        }

        public override string NameFieldName
        {
            get
            {
                return "Name";
            }
        }

        public override string OrderIDFieldName
        {
            get
            {
                return "";
            }
        }

        public override string ParentIDFieldName
        {
            get
            {
                return "";
            }
        }

        public override string TableName
        {
            get
            {
                return "Elements";
            }
        }

        public string TemplateIDFieldName
        {
            get
            {
                return "TemplateID";
            }
        }
    }
}


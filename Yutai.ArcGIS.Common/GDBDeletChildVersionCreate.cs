using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common
{
    public class GDBDeletChildVersionCreate : TableCreate
    {
        public GDBDeletChildVersionCreate()
        {
            base.TableName = "GDBDeletChildVersion";
        }

        public override IFields GetFields()
        {
            IObjectClassDescription description = new ObjectClassDescription();
            IFields requiredFields = description.RequiredFields;
            (requiredFields as IFieldsEdit).AddField(base.CreateIntField("GDBOID", "GDBOID"));
            (requiredFields as IFieldsEdit).AddField(base.CreateStringField("VersionName", "VersionName", 150));
            return requiredFields;
        }
    }
}


using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.ZD
{
    public class ZDRegisterTableCreate : ZDTableCreate
    {
        private ZDRegisterTable zdregisterTable_0 = new ZDRegisterTable();

        public ZDRegisterTableCreate()
        {
            base.TableName = this.zdregisterTable_0.TableName;
        }

        public override IFields GetFields()
        {
            IObjectClassDescription objectClassDescription = new ObjectClassDescription();
            IFields requiredFields = objectClassDescription.RequiredFields;
            (requiredFields as IFieldsEdit).AddField(base.CreateStringField(
                this.zdregisterTable_0.FeatureClassNameField, this.zdregisterTable_0.FeatureClassNameField, 150));
            (requiredFields as IFieldsEdit).AddField(base.CreateStringField(this.zdregisterTable_0.ZDBHFieldName,
                this.zdregisterTable_0.ZDBHFieldName, 30));
            (requiredFields as IFieldsEdit).AddField(base.CreateDateField(this.zdregisterTable_0.RegisterDateFieldName,
                this.zdregisterTable_0.RegisterDateFieldName));
            (requiredFields as IFieldsEdit).AddField(base.CreateStringField(this.zdregisterTable_0.GDBConnectInfoName,
                this.zdregisterTable_0.GDBConnectInfoName, 150));
            (requiredFields as IFieldsEdit).AddField(base.CreateStringField(this.zdregisterTable_0.GuidName,
                this.zdregisterTable_0.GuidName, 80));
            (requiredFields as IFieldsEdit).AddField(
                base.CreateStringField(this.zdregisterTable_0.HistoryFeatureClassName,
                    this.zdregisterTable_0.HistoryFeatureClassName, 150));
            return requiredFields;
        }
    }
}
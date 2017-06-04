using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.ZD
{
	public class ZDHistoryTableCreate : ZDTableCreate
	{
		private ZDHistoryTable zdhistoryTable_0 = new ZDHistoryTable();

		public ZDHistoryTableCreate()
		{
			base.TableName = this.zdhistoryTable_0.TableName;
		}

		public override void UpdateStruct(ITable itable_0)
		{
			IFields fields = itable_0.Fields;
			int num = fields.FindField(this.zdhistoryTable_0.HisZDOIDName);
			if (num == -1)
			{
				IField field = base.CreateIntField(this.zdhistoryTable_0.HisZDOIDName, this.zdhistoryTable_0.HisZDOIDName);
				itable_0.AddField(field);
			}
		}

		public override IFields GetFields()
		{
			IObjectClassDescription objectClassDescription = new ObjectClassDescription();
			IFields requiredFields = objectClassDescription.RequiredFields;
			(requiredFields as IFieldsEdit).AddField(base.CreateStringField(this.zdhistoryTable_0.ZDFeatureClassName, this.zdhistoryTable_0.ZDFeatureClassName, 130));
			(requiredFields as IFieldsEdit).AddField(base.CreateStringField(this.zdhistoryTable_0.OrigineZDHFieldName, this.zdhistoryTable_0.OrigineZDHFieldName, 30));
			(requiredFields as IFieldsEdit).AddField(base.CreateStringField(this.zdhistoryTable_0.NewZDHFieldName, this.zdhistoryTable_0.NewZDHFieldName, 30));
			(requiredFields as IFieldsEdit).AddField(base.CreateIntField(this.zdhistoryTable_0.OrigineZDOIDName, this.zdhistoryTable_0.OrigineZDOIDName));
			(requiredFields as IFieldsEdit).AddField(base.CreateIntField(this.zdhistoryTable_0.NewZDOIDName, this.zdhistoryTable_0.NewZDOIDName));
			(requiredFields as IFieldsEdit).AddField(base.CreateIntField(this.zdhistoryTable_0.HisZDOIDName, this.zdhistoryTable_0.HisZDOIDName));
			(requiredFields as IFieldsEdit).AddField(base.CreateIntField(this.zdhistoryTable_0.ChangeTypeFieldName, this.zdhistoryTable_0.ChangeTypeFieldName));
			(requiredFields as IFieldsEdit).AddField(base.CreateDateField(this.zdhistoryTable_0.ChageDateFieldName, this.zdhistoryTable_0.ChageDateFieldName));
			(requiredFields as IFieldsEdit).AddField(base.CreateStringField(this.zdhistoryTable_0.ZDRegisterGuidName, this.zdhistoryTable_0.ZDRegisterGuidName, 80));
			return requiredFields;
		}
	}
}

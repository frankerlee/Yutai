using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Common.ZD
{
	public class ZDChangeHistoryInfo
	{
	
		public bool IsLoad
		{
			get;
			protected set;
		}

		public IFeature Feature
		{
			get;
			set;
		}

		public int OID
		{
			get;
			set;
		}

		public int HisOID
		{
			get;
			set;
		}

		public System.DateTime ChangeDate
		{
			get;
			set;
		}

		public ZDEditType ChageType
		{
			get;
			set;
		}

		public IFeature TopFeat
		{
			get;
			set;
		}

		public System.Collections.Generic.List<ZDChangeHistoryInfo> SouceInfos
		{
			get;
			set;
		}

		public string ChangeTypeDescription
		{
			get
			{
				string result;
				if (this.ChageType == ZDEditType.Create)
				{
					result = "创建";
				}
				else if (this.ChageType == ZDEditType.Attribute)
				{
					result = "属性变更";
				}
				else if (this.ChageType == ZDEditType.BoundChange)
				{
					result = "界址调整";
				}
				else if (this.ChageType == ZDEditType.Split)
				{
					result = "分割";
				}
				else if (this.ChageType == ZDEditType.Union)
				{
					result = "合并";
				}
				else if (this.ChageType == ZDEditType.Unknown)
				{
					result = "未知";
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		public ZDChangeHistoryInfo()
		{
			this.ChageType = ZDEditType.Unknown;
		}

		public void LoadParent()
		{
			this.IsLoad = true;
			try
			{
				ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
				string text = (this.Feature.Class as IDataset).Name;
				string[] array = text.Split(new char[]
				{
					'.'
				});
				text = array[array.Length - 1];
				if (text.Length > 4)
				{
					string a = text.Substring(text.Length - 4).ToLower();
					if (a == "_his")
					{
						text = text.Substring(0, text.Length - 4);
					}
				}
				string historyZDFeatureName = ZDRegister.GetHistoryZDFeatureName(this.Feature.Class as IFeatureClass);
				string registerZDGuid = ZDRegister.GetRegisterZDGuid(this.Feature.Class as IFeatureClass);
				IFeatureClass ifeatureClass_ = ((this.Feature.Class as IDataset).Workspace as IFeatureWorkspace).OpenFeatureClass(historyZDFeatureName);
				string whereClause = string.Format("{0}='{1}' and {2}={3}", new object[]
				{
					zDHistoryTable.ZDRegisterGuidName,
					registerZDGuid,
					zDHistoryTable.NewZDOIDName,
					this.OID
				});
				IQueryFilter queryFilter = new QueryFilter();
				queryFilter.WhereClause = whereClause;
				IWorkspace workspace = AppConfigInfo.GetWorkspace();
				ITable table = (workspace as IFeatureWorkspace).OpenTable(zDHistoryTable.TableName);
				ICursor cursor = table.Search(queryFilter, false);
				for (IRow row = cursor.NextRow(); row != null; row = cursor.NextRow())
				{
					ZDChangeHistoryInfo zDChangeHistoryInfo = new ZDChangeHistoryInfo();
					object fieldValue = RowOperator.GetFieldValue(row, zDHistoryTable.ChangeTypeFieldName);
					if (this.ChageType == ZDEditType.Unknown)
					{
						this.ChageType = (ZDEditType)System.Convert.ToInt32(fieldValue);
					}
					fieldValue = RowOperator.GetFieldValue(row, zDHistoryTable.ChageDateFieldName);
					this.ChangeDate = System.Convert.ToDateTime(fieldValue);
					fieldValue = RowOperator.GetFieldValue(row, zDHistoryTable.OrigineZDOIDName);
					int oID = System.Convert.ToInt32(fieldValue);
					zDChangeHistoryInfo.Feature = ZDChangeHistory.GetHisFeat(ifeatureClass_, oID);
					zDChangeHistoryInfo.OID = oID;
					zDChangeHistoryInfo.TopFeat = this.Feature;
					zDChangeHistoryInfo.SouceInfos = new System.Collections.Generic.List<ZDChangeHistoryInfo>();
					this.SouceInfos.Add(zDChangeHistoryInfo);
				}
				ComReleaser.ReleaseCOMObject(cursor);
			}
			catch (System.Exception)
			{
			}
		}

		public override string ToString()
		{
			if (!this.IsLoad)
			{
				this.LoadParent();
			}
			string result;
			if (this.ChageType == ZDEditType.Create)
			{
				result = string.Format("对象[{0}]于[{1}]生成", this.OID, this.ChangeDate);
			}
			else if (this.ChageType == ZDEditType.Attribute)
			{
				result = string.Format("对象[{0}]于[{1}]由对象{2}属性变更生成", this.OID, this.ChangeDate, this.SouceInfos[0].OID);
			}
			else if (this.ChageType == ZDEditType.BoundChange)
			{
				result = string.Format("对象[{0}]于[{1}]进行界址调整", this.OID, this.ChangeDate);
			}
			else if (this.ChageType == ZDEditType.Split)
			{
				result = string.Format("对象[{0}]于[{1}]由对象[{2}]分割生成", this.OID, this.ChangeDate, this.SouceInfos[0].OID);
			}
			else if (this.ChageType == ZDEditType.Union)
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				foreach (ZDChangeHistoryInfo current in this.SouceInfos)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(current.OID);
				}
				result = string.Format("对象[{0}]于[{1}]由对象[{2}]合并生成", this.OID, this.ChangeDate, stringBuilder);
			}
			else if (this.ChageType == ZDEditType.Unknown && this.TopFeat == null)
			{
				result = string.Format("对象[{0}]无变更信息", this.OID);
			}
			else
			{
				result = "";
			}
			return result;
		}
	}
}

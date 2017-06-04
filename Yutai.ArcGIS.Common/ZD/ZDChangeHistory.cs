using System.Collections.Generic;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Common.ZD
{
	public class ZDChangeHistory
	{
		public static IFeature GetHisFeat(IFeatureClass ifeatureClass_0, int int_0)
		{
			string whereClause = string.Format("OriginOID_={0}", int_0);
			IFeatureCursor featureCursor = ifeatureClass_0.Search(new QueryFilter
			{
				WhereClause = whereClause
			}, false);
			IFeature result = featureCursor.NextFeature();
			ComReleaser.ReleaseCOMObject(featureCursor);
			return result;
		}

		public static ZDChangeHistoryInfo CreateHistory(IFeature ifeature_0)
		{
			ZDChangeHistoryInfo zDChangeHistoryInfo = new ZDChangeHistoryInfo();
			zDChangeHistoryInfo.Feature = ifeature_0;
			zDChangeHistoryInfo.OID = ifeature_0.OID;
			zDChangeHistoryInfo.HisOID = -1;
			zDChangeHistoryInfo.SouceInfos = new System.Collections.Generic.List<ZDChangeHistoryInfo>();
			zDChangeHistoryInfo.LoadParent();
			return zDChangeHistoryInfo;
		}

		public static System.Collections.Generic.List<ZDChangeHistoryInfo2> GetZDChangeHistory(IFeatureClass ifeatureClass_0)
		{
			System.Collections.Generic.List<ZDChangeHistoryInfo2> list = new System.Collections.Generic.List<ZDChangeHistoryInfo2>();
			ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
			string text = (ifeatureClass_0 as IDataset).Name;
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
			string registerZDGuid = ZDRegister.GetRegisterZDGuid(ifeatureClass_0);
			string whereClause = string.Format("{0}='{1}' ", zDHistoryTable.ZDRegisterGuidName, registerZDGuid);
			IQueryFilter queryFilter = new QueryFilter();
			queryFilter.WhereClause = whereClause;
			(queryFilter as IQueryFilterDefinition).PostfixClause = string.Format(" ORDER BY {0} desc ", zDHistoryTable.ChageDateFieldName);
			try
			{
				using (ComReleaser comReleaser = new ComReleaser())
				{
					IWorkspace workspace = AppConfigInfo.GetWorkspace();
					ITable table = (workspace as IFeatureWorkspace).OpenTable(zDHistoryTable.TableName);
					ICursor cursor = table.Search(queryFilter, true);
					comReleaser.ManageLifetime(cursor);
					IRow row = cursor.NextRow();
					SortedList<int, ZDChangeHistoryInfo2> sortedList = new SortedList<int, ZDChangeHistoryInfo2>();
					SortedList<int, ZDChangeHistoryInfo2> sortedList2 = new SortedList<int, ZDChangeHistoryInfo2>();
					while (row != null)
					{
						try
						{
							object fieldValue = RowOperator.GetFieldValue(row, zDHistoryTable.ChangeTypeFieldName);
							ZDEditType zDEditType = (ZDEditType)System.Convert.ToInt32(fieldValue);
							fieldValue = RowOperator.GetFieldValue(row, zDHistoryTable.ChageDateFieldName);
							System.DateTime changeDate = System.Convert.ToDateTime(fieldValue);
							fieldValue = RowOperator.GetFieldValue(row, zDHistoryTable.OrigineZDOIDName);
							int num = System.Convert.ToInt32(fieldValue);
							fieldValue = RowOperator.GetFieldValue(row, zDHistoryTable.NewZDOIDName);
							int num2 = System.Convert.ToInt32(fieldValue);
							fieldValue = RowOperator.GetFieldValue(row, zDHistoryTable.HisZDOIDName);
							int num3 = System.Convert.ToInt32(fieldValue);
							if (num2 != -1)
							{
								ZDChangeHistoryInfo2 zDChangeHistoryInfo;
								if (zDEditType == ZDEditType.Split)
								{
									if (sortedList2.ContainsKey(num))
									{
										zDChangeHistoryInfo = sortedList2[num];
									}
									else
									{
										zDChangeHistoryInfo = new ZDChangeHistoryInfo2();
										sortedList2.Add(num, zDChangeHistoryInfo);
										zDChangeHistoryInfo.Add(num);
										zDChangeHistoryInfo.AddHis(num3);
										zDChangeHistoryInfo.ChangeDate = changeDate;
										zDChangeHistoryInfo.ChageType = zDEditType;
										list.Add(zDChangeHistoryInfo);
									}
									zDChangeHistoryInfo.AddNew(num2);
								}
								else if (zDEditType == ZDEditType.Union)
								{
									if (sortedList.ContainsKey(num2))
									{
										zDChangeHistoryInfo = sortedList[num2];
									}
									else
									{
										zDChangeHistoryInfo = new ZDChangeHistoryInfo2();
										sortedList.Add(num2, zDChangeHistoryInfo);
										zDChangeHistoryInfo.AddNew(num2);
										zDChangeHistoryInfo.ChangeDate = changeDate;
										zDChangeHistoryInfo.ChageType = zDEditType;
										list.Add(zDChangeHistoryInfo);
									}
									zDChangeHistoryInfo.Add(num);
									zDChangeHistoryInfo.AddHis(num3);
								}
								else
								{
									zDChangeHistoryInfo = new ZDChangeHistoryInfo2();
									zDChangeHistoryInfo.Add(num);
									zDChangeHistoryInfo.AddNew(num2);
									zDChangeHistoryInfo.AddHis(num3);
									zDChangeHistoryInfo.ChangeDate = changeDate;
									zDChangeHistoryInfo.ChageType = zDEditType;
									list.Add(zDChangeHistoryInfo);
								}
								zDChangeHistoryInfo.HisOID = num3;
							}
							else if (num != -1 && zDEditType == ZDEditType.Delete)
							{
								ZDChangeHistoryInfo2 zDChangeHistoryInfo = new ZDChangeHistoryInfo2();
								zDChangeHistoryInfo.Add(num);
								zDChangeHistoryInfo.AddNew(num2);
								zDChangeHistoryInfo.AddHis(num3);
								zDChangeHistoryInfo.ChangeDate = changeDate;
								zDChangeHistoryInfo.ChageType = zDEditType;
								list.Add(zDChangeHistoryInfo);
							}
						}
						catch (System.Exception)
						{
						}
						row = cursor.NextRow();
					}
				}
			}
			catch (System.Exception)
			{
			}
			return list;
		}
	}
}

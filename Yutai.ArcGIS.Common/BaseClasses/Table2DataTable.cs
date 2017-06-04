using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.BaseClasses
{
	public class Table2DataTable
	{
		private DataTable dataTable_0 = null;

		private ITable itable_0 = null;

		public Table2DataTable()
		{
			this.dataTable_0 = new DataTable();
			this.dataTable_0.ColumnChanged += new DataColumnChangeEventHandler(this.dataTable_0_ColumnChanged);
		}

		public void CommitTransaction()
		{
			ITransactions workspace = (this.itable_0 as IDataset).Workspace as IWorkspaceEdit as ITransactions;
			if (workspace != null && workspace.InTransaction)
			{
				workspace.CommitTransaction();
			}
		}

		private void dataTable_0_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			IRow proposedValue;
			int num;
			try
			{
				object[] itemArray = e.Row.ItemArray;
				IWorkspaceEdit workspace = (this.itable_0 as IDataset).Workspace as IWorkspaceEdit;
				if (!(itemArray[0] is DBNull))
				{
					int num1 = Convert.ToInt32(itemArray[0]);
					IQueryFilter queryFilterClass = new QueryFilter()
					{
						WhereClause = string.Concat(this.itable_0.OIDFieldName, " = ", num1.ToString())
					};
					ICursor cursor = this.itable_0.Search(queryFilterClass, false);
					proposedValue = cursor.NextRow();
					if (proposedValue != null)
					{
						workspace.StartEditOperation();
						num = proposedValue.Fields.FindField(e.Column.ColumnName);
						if (num != -1)
						{
							proposedValue.Value[num] = e.ProposedValue;
						}
						proposedValue.Store();
						workspace.StopEditOperation();
					}
					ComReleaser.ReleaseCOMObject(cursor);
				}
				else if (!(this.itable_0 is IFeatureClass))
				{
					workspace.StartEditOperation();
					proposedValue = this.itable_0.CreateRow();
					num = proposedValue.Fields.FindField(e.Column.ColumnName);
					if (num != -1)
					{
						proposedValue.Value[num] = e.ProposedValue;
					}
					workspace.StopEditOperation();
					e.Row[this.itable_0.OIDFieldName] = proposedValue.OID;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		~Table2DataTable()
		{
			if (this.itable_0 != null)
			{
				Marshal.ReleaseComObject(this.itable_0);
			}
			this.itable_0 = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public DataSet GetDataSet(ITable itable_1, string string_0)
		{
			DataSet dataSet;
			if (itable_1 != null)
			{
				DataSet dataSet1 = new DataSet();
				dataSet1.Tables.Add(this.ITable2Tatatable(itable_1, string_0));
				dataSet = dataSet1;
			}
			else
			{
				dataSet = null;
			}
			return dataSet;
		}

		public DataTable ITable2Tatatable(ITable itable_1, string string_0)
		{
			DataTable dataTable0;
			if (itable_1 != null)
			{
				this.dataTable_0.Rows.Clear();
				this.dataTable_0.Columns.Clear();
				this.method_0(itable_1.Fields, this.dataTable_0);
				string str = "";
				if (itable_1 is IFeatureClass)
				{
					str = this.method_1(itable_1 as IFeatureClass);
				}
				IQueryFilter queryFilterClass = new QueryFilter()
				{
					WhereClause = string_0
				};
				ICursor cursor = null;
				cursor = (string_0 != "" ? itable_1.Search(queryFilterClass, false) : itable_1.Search(null, false));
				this.itable_0 = itable_1;
				this.method_2(true, this.dataTable_0, cursor, str);
				ComReleaser.ReleaseCOMObject(cursor);
				dataTable0 = this.dataTable_0;
			}
			else
			{
				dataTable0 = null;
			}
			return dataTable0;
		}

		private void method_0(IFields ifields_0, DataTable dataTable_1)
		{
			for (int i = 0; i < ifields_0.FieldCount; i++)
			{
				IField field = ifields_0.Field[i];
				DataColumn dataColumn = new DataColumn(field.Name)
				{
					Caption = field.AliasName
				};
				if (field.Type == esriFieldType.esriFieldTypeBlob)
				{
					dataColumn.ReadOnly = true;
				}
				else if (field.Type != esriFieldType.esriFieldTypeGeometry)
				{
					dataColumn.ReadOnly = !field.Editable;
				}
				else
				{
					dataColumn.ReadOnly = true;
				}
				dataTable_1.Columns.Add(dataColumn);
			}
		}

		private string method_1(IFeatureClass ifeatureClass_0)
		{
			string str;
			if (ifeatureClass_0 != null)
			{
				string str1 = "";
				switch ((int)ifeatureClass_0.ShapeType)
				{
					case (int)esriGeometryType.esriGeometryPoint:
					{
						str1 = "点";
						goto case 8;
					}
					case (int)esriGeometryType.esriGeometryMultipoint:
					{
						str1 = "多点";
						goto case 8;
					}
					case (int)esriGeometryType.esriGeometryPolyline:
					{
						str1 = "线";
						goto case 8;
					}
					case (int)esriGeometryType.esriGeometryPolygon:
					{
						str1 = "多边形";
						goto case 8;
					}
					case (int)esriGeometryType.esriGeometryEnvelope:
					case (int)esriGeometryType.esriGeometryPath:
					case (int)esriGeometryType.esriGeometryAny:
					case 8:
					{
						int num = ifeatureClass_0.Fields.FindField(ifeatureClass_0.ShapeFieldName);
						IGeometryDef geometryDef = ifeatureClass_0.Fields.Field[num].GeometryDef;
						str1 = string.Concat(str1, " ");
						if (geometryDef.HasZ)
						{
							str1 = string.Concat(str1, "Z");
						}
						if (geometryDef.HasM)
						{
							str1 = string.Concat(str1, "M");
						}
						str = str1;
						break;
					}
					case (int)esriGeometryType.esriGeometryMultiPatch:
					{
						str1 = "多面";
						goto case 8;
					}
					default:
					{
						goto case 8;
					}
				}
			}
			else
			{
				str = "";
			}
			return str;
		}

		private void method_2(bool bool_0, DataTable dataTable_1, ICursor icursor_0, string string_0)
		{
			IFields fields = icursor_0.Fields;
			int num = 0;
			IRow row = icursor_0.NextRow();
			object[] string0 = new object[fields.FieldCount];
			while (row != null)
			{
				for (int i = 0; i < fields.FieldCount; i++)
				{
					IField field = fields.Field[i];
					if (field.Type == esriFieldType.esriFieldTypeGeometry)
					{
						string0[i] = string_0;
					}
					else if (field.Type != esriFieldType.esriFieldTypeBlob)
					{
						string0[i] = row.Value[i];
					}
					else
					{
						string0[i] = "二进制数据";
					}
				}
				dataTable_1.Rows.Add(string0);
				num++;
				row = icursor_0.NextRow();
			}
		}

		public void SaveDataTable()
		{
			IWorkspaceEdit workspace = (this.itable_0 as IDataset).Workspace as IWorkspaceEdit;
			if (workspace.IsBeingEdited())
			{
				workspace.StopEditing(true);
			}
		}

		public void StartDataTable()
		{
			IWorkspaceEdit workspace = (this.itable_0 as IDataset).Workspace as IWorkspaceEdit;
			if (!workspace.IsBeingEdited())
			{
				workspace.StartEditing(false);
			}
		}

		public void StartTransaction()
		{
			ITransactions workspace = (this.itable_0 as IDataset).Workspace as IWorkspaceEdit as ITransactions;
			if (workspace != null && !workspace.InTransaction)
			{
				workspace.StartTransaction();
			}
		}
	}
}
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public interface ITask
    {
        string DefaultTool
        {
            get;
        }

        void Excute();

        bool CheckTaskStatue(ITool itool_0);
    }
    public class ExcelReader : IDisposable
	{
		private int[] int_0;

		private string string_0;

		private bool bool_0 = true;

		private bool bool_1 = false;

		private string string_1;

		private string string_2;

		private bool bool_2 = false;

		private OleDbConnection oleDbConnection_0;

		private OleDbCommand oleDbCommand_0;

		private OleDbCommand oleDbCommand_1;

		public string ExcelFilename
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
			}
		}

		public bool Headers
		{
			get
			{
				return this.bool_1;
			}
			set
			{
				this.bool_1 = value;
			}
		}

		public bool KeepConnectionOpen
		{
			get
			{
				return this.bool_2;
			}
			set
			{
				this.bool_2 = value;
			}
		}

		public bool MixedData
		{
			get
			{
				return this.bool_0;
			}
			set
			{
				this.bool_0 = value;
			}
		}

		public int[] PKCols
		{
			get
			{
				return this.int_0;
			}
			set
			{
				this.int_0 = value;
			}
		}

		public string SheetName
		{
			get
			{
				return this.string_1;
			}
			set
			{
				this.string_1 = value;
			}
		}

		public string SheetRange
		{
			get
			{
				return this.string_2;
			}
			set
			{
				if (value.IndexOf(":") == -1)
				{
					throw new Exception("Invalid range length");
				}
				this.string_2 = value;
			}
		}

		public ExcelReader()
		{
		}

		public void Close()
		{
			if (this.oleDbConnection_0 != null)
			{
				if (this.oleDbConnection_0.State != ConnectionState.Closed)
				{
					this.oleDbConnection_0.Close();
				}
				this.oleDbConnection_0.Dispose();
				this.oleDbConnection_0 = null;
			}
		}

		public string ColName(int int_1)
		{
			string str = "";
			if (int_1 >= 26)
			{
				int int1 = int_1 / 26;
				int num = int_1 % 26;
				str = Convert.ToString(Convert.ToChar(Convert.ToByte('A') - 1 + int1));
				str = string.Concat(str, Convert.ToString(Convert.ToChar(Convert.ToByte('A') + num)));
			}
			else
			{
				str = Convert.ToString(Convert.ToChar(Convert.ToByte('A') + int_1));
			}
			return str;
		}

		public int ColNumber(string string_3)
		{
			string_3 = string_3.ToUpper();
			int num = 0;
			if (string_3.Length <= 1)
			{
				num = Convert.ToInt16(Convert.ToByte(string_3[0]) - 65);
			}
			else
			{
				num = Convert.ToInt16(Convert.ToByte(string_3[1]) - 65);
				num = num + Convert.ToInt16(Convert.ToByte(string_3[1]) - 64) * 26;
			}
			return num;
		}

		public void Dispose()
		{
			if (this.oleDbConnection_0 != null)
			{
				this.oleDbConnection_0.Dispose();
				this.oleDbConnection_0 = null;
			}
			if (this.oleDbCommand_0 != null)
			{
				this.oleDbCommand_0.Dispose();
				this.oleDbCommand_0 = null;
			}
		}

		public string[] GetExcelSheetNames()
		{
			string[] strArrays;
			DataTable oleDbSchemaTable = null;
			try
			{
				try
				{
					if (this.oleDbConnection_0 == null)
					{
						this.Open();
					}
					oleDbSchemaTable = this.oleDbConnection_0.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
					if (oleDbSchemaTable != null)
					{
						string[] strArrays1 = new string[oleDbSchemaTable.Rows.Count];
						int num = 0;
						foreach (DataRow row in oleDbSchemaTable.Rows)
						{
							string str = row["TABLE_NAME"].ToString();
							strArrays1[num] = str.Substring(0, str.Length - 1);
							num++;
						}
						strArrays = strArrays1;
					}
					else
					{
						strArrays = null;
					}
				}
				catch (Exception exception)
				{
					strArrays = null;
				}
			}
			finally
			{
				if (!this.KeepConnectionOpen)
				{
					this.Close();
				}
				if (oleDbSchemaTable != null)
				{
					oleDbSchemaTable.Dispose();
					oleDbSchemaTable = null;
				}
			}
			return strArrays;
		}

		public DataTable GetTable()
		{
			return this.GetTable("ExcelTable");
		}

		public DataTable GetTable(string string_3)
		{
			DataTable dataTable;
			try
			{
				if (this.oleDbConnection_0 == null)
				{
					this.Open();
				}
				if (this.oleDbConnection_0.State != ConnectionState.Open)
				{
					throw new Exception("连接不能打开的错误!");
				}
				if (this.method_2())
				{
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter()
					{
						SelectCommand = this.oleDbCommand_0
					};
					DataTable dataTable1 = new DataTable(string_3);
					oleDbDataAdapter.FillSchema(dataTable1, SchemaType.Source);
					oleDbDataAdapter.Fill(dataTable1);
					if (!this.Headers && this.string_2.IndexOf(":") > 0)
					{
						string str = this.string_2.Substring(0, this.string_2.IndexOf(":") - 1);
						int num = this.ColNumber(str);
						for (int i = 0; i < dataTable1.Columns.Count; i++)
						{
							dataTable1.Columns[i].Caption = this.ColName(num + i);
						}
					}
					this.method_7(dataTable1);
					dataTable1.DefaultView.AllowDelete = false;
					this.oleDbCommand_0.Dispose();
					this.oleDbCommand_0 = null;
					oleDbDataAdapter.Dispose();
					oleDbDataAdapter = null;
					if (!this.KeepConnectionOpen)
					{
						this.Close();
					}
					dataTable = dataTable1;
				}
				else
				{
					dataTable = null;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return dataTable;
		}

		public object GetValue(string string_3)
		{
			object obj;
			this.SetSingleCellRange(string_3);
			object obj1 = null;
			if (this.oleDbConnection_0 == null)
			{
				this.Open();
			}
			if (this.oleDbConnection_0.State != ConnectionState.Open)
			{
				throw new Exception("Connection is not open error.");
			}
			if (this.method_2())
			{
				obj1 = this.oleDbCommand_0.ExecuteScalar();
				this.oleDbCommand_0.Dispose();
				this.oleDbCommand_0 = null;
				if (!this.KeepConnectionOpen)
				{
					this.Close();
				}
				obj = obj1;
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		private string method_0()
		{
			string str = "";
			if (this.MixedData)
			{
				str = string.Concat(str, "Imex=2;");
			}
			str = (!this.Headers ? string.Concat(str, "HDR=No;") : string.Concat(str, "HDR=Yes;"));
			return str;
		}

		private string method_1()
		{
			string[] string0 = new string[] { "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", this.string_0, ";Extended Properties=", null, null, null, null };
			char chr = Convert.ToChar(34);
			string0[3] = chr.ToString();
			string0[4] = "Excel 8.0;";
			string0[5] = this.method_0();
			chr = Convert.ToChar(34);
			string0[6] = chr.ToString();
			return string.Concat(string0);
		}

		private bool method_2()
		{
			bool flag;
			try
			{
				if (this.oleDbConnection_0 == null)
				{
					throw new Exception("Connection is unassigned or closed.");
				}
				if (this.string_1.Length == 0)
				{
					throw new Exception("Sheetname was not assigned.");
				}
				string[] string1 = new string[] { "SELECT * FROM [", this.string_1, "$", this.string_2, "]" };
				this.oleDbCommand_0 = new OleDbCommand(string.Concat(string1), this.oleDbConnection_0);
				flag = true;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return flag;
		}

		private string method_3(string string_3, string string_4)
		{
			if (string_3 != "")
			{
				string str = string.Concat(string_3, ", ");
				string_3 = str;
				string_3 = str;
			}
			return string.Concat(string_3, string_4);
		}

		private string method_4(string string_3, string string_4)
		{
			if (string_3 != "")
			{
				string str = string.Concat(string_3, " and ");
				string_3 = str;
				string_3 = str;
			}
			return string.Concat(string_3, string_4);
		}

		private OleDbDataAdapter method_5(DataTable dataTable_0)
		{
			int i;
			int j;
			OleDbDataAdapter oleDbDataAdapter;
			try
			{
				if (this.oleDbConnection_0 == null)
				{
					throw new Exception("Connection is unassigned or closed.");
				}
				if (this.string_1.Length == 0)
				{
					throw new Exception("Sheetname was not assigned.");
				}
				if (this.PKCols == null)
				{
					throw new Exception("Cannot update excel sheet with no primarykey set.");
				}
				if ((int)this.PKCols.Length < 1)
				{
					throw new Exception("Cannot update excel sheet with no primarykey set.");
				}
				OleDbDataAdapter oleDbCommand = new OleDbDataAdapter(this.oleDbCommand_0);
				string str = "";
				string str1 = "";
				string str2 = "";
				string str3 = "";
				for (i = 0; i < (int)this.PKCols.Length; i++)
				{
					str3 = this.method_4(str3, string.Concat(dataTable_0.Columns[i].ColumnName, "=?"));
				}
				str3 = string.Concat(" Where ", str3);
				for (j = 0; j < dataTable_0.Columns.Count; j++)
				{
					str2 = this.method_3(str2, dataTable_0.Columns[j].ColumnName);
					str1 = this.method_3(str1, "?");
					str = string.Concat(this.method_3(str, dataTable_0.Columns[j].ColumnName), "=?");
				}
				string[] sheetName = new string[] { "[", this.SheetName, "$", this.SheetRange, "]" };
				string str4 = string.Concat(sheetName);
				sheetName = new string[] { "INSERT INTO ", str4, "(", str2, ") Values (", str1, ")" };
				str2 = string.Concat(sheetName);
				sheetName = new string[] { "Update ", str4, " Set ", str, str3 };
				str = string.Concat(sheetName);
				oleDbCommand.InsertCommand = new OleDbCommand(str2, this.oleDbConnection_0);
				oleDbCommand.UpdateCommand = new OleDbCommand(str, this.oleDbConnection_0);
				OleDbParameter oleDbParameter = null;
				OleDbParameter columnName = null;
				for (j = 0; j < dataTable_0.Columns.Count; j++)
				{
					oleDbParameter = new OleDbParameter("?", dataTable_0.Columns[j].DataType.ToString());
					columnName = new OleDbParameter("?", dataTable_0.Columns[j].DataType.ToString());
					oleDbParameter.SourceColumn = dataTable_0.Columns[j].ColumnName;
					columnName.SourceColumn = dataTable_0.Columns[j].ColumnName;
					oleDbCommand.InsertCommand.Parameters.Add(oleDbParameter);
					oleDbCommand.UpdateCommand.Parameters.Add(columnName);
					oleDbParameter = null;
					columnName = null;
				}
				for (i = 0; i < (int)this.PKCols.Length; i++)
				{
					columnName = new OleDbParameter("?", dataTable_0.Columns[i].DataType.ToString())
					{
						SourceColumn = dataTable_0.Columns[i].ColumnName,
						SourceVersion = DataRowVersion.Original
					};
					oleDbCommand.UpdateCommand.Parameters.Add(columnName);
				}
				oleDbDataAdapter = oleDbCommand;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return oleDbDataAdapter;
		}

		private bool method_6(string string_3)
		{
			bool flag;
			try
			{
				if (this.oleDbConnection_0 == null)
				{
					throw new Exception("Connection is unassigned or closed.");
				}
				if (this.string_1.Length == 0)
				{
					throw new Exception("Sheetname was not assigned.");
				}
				string[] string1 = new string[] { " Update [", this.string_1, "$", this.string_2, "] set F1=", string_3 };
				this.oleDbCommand_1 = new OleDbCommand(string.Concat(string1), this.oleDbConnection_0);
				flag = true;
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return flag;
		}

		private void method_7(DataTable dataTable_0)
		{
			try
			{
				if (this.PKCols != null && (int)this.PKCols.Length > 0)
				{
					DataColumn[] item = new DataColumn[(int)this.PKCols.Length];
					for (int i = 0; i < (int)this.PKCols.Length; i++)
					{
						item[i] = dataTable_0.Columns[this.PKCols[i]];
					}
					dataTable_0.PrimaryKey = item;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		private void method_8(DataTable dataTable_0)
		{
			if ((int)dataTable_0.PrimaryKey.Length == 0)
			{
				if (this.PKCols == null)
				{
					throw new Exception("Provide an primary key to the datatable");
				}
				this.method_7(dataTable_0);
			}
		}

		public void Open()
		{
			try
			{
				if (this.oleDbConnection_0 != null)
				{
					if (this.oleDbConnection_0.State == ConnectionState.Open)
					{
						this.oleDbConnection_0.Close();
					}
					this.oleDbConnection_0 = null;
				}
				if (!File.Exists(this.string_0))
				{
					throw new Exception(string.Concat("Excel file ", this.string_0, "could not be found."));
				}
				this.oleDbConnection_0 = new OleDbConnection(this.method_1());
				this.oleDbConnection_0.Open();
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public void SetPrimaryKey(int int_1)
		{
			this.int_0 = new int[] { int_1 };
		}

		public void SetSingleCellRange(string string_3)
		{
			this.string_2 = string.Concat(string_3, ":", string_3);
		}

		public DataTable SetTable(DataTable dataTable_0)
		{
			DataTable dataTable0;
			try
			{
				DataTable changes = dataTable_0.GetChanges();
				if (changes == null)
				{
					throw new Exception("There are no changes to be saved!");
				}
				this.method_8(dataTable_0);
				if (this.oleDbConnection_0 == null)
				{
					this.Open();
				}
				if (this.oleDbConnection_0.State != ConnectionState.Open)
				{
					throw new Exception("Connection cannot open error.");
				}
				if (this.method_2())
				{
					OleDbDataAdapter oleDbDataAdapter = this.method_5(changes);
					oleDbDataAdapter.Update(changes);
					this.oleDbCommand_0.Dispose();
					this.oleDbCommand_0 = null;
					oleDbDataAdapter.Dispose();
					oleDbDataAdapter = null;
					if (!this.KeepConnectionOpen)
					{
						this.Close();
					}
					dataTable0 = dataTable_0;
				}
				else
				{
					dataTable0 = null;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return dataTable0;
		}

		public void SetValue(string string_3, object object_0)
		{
			try
			{
				try
				{
					this.SetSingleCellRange(string_3);
					if (this.oleDbConnection_0 == null)
					{
						this.Open();
					}
					if (this.oleDbConnection_0.State != ConnectionState.Open)
					{
						throw new Exception("Connection is not open error.");
					}
					if (this.method_6(object_0.ToString()))
					{
						object_0 = this.oleDbCommand_1.ExecuteNonQuery();
						this.oleDbCommand_1.Dispose();
						this.oleDbCommand_1 = null;
						if (!this.KeepConnectionOpen)
						{
							this.Close();
						}
					}
					else
					{
						return;
					}
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			finally
			{
				if (this.oleDbCommand_1 != null)
				{
					this.oleDbCommand_1.Dispose();
					this.oleDbCommand_1 = null;
				}
			}
		}
	}
}
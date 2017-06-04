using System;
using System.Data;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.Data
{
	public class DBAccess
	{
		private OleDbConnection oleDbConnection_0 = new OleDbConnection();

		private string string_0 = "";

		private bool bool_0 = false;

		public OleDbConnection Connection
		{
			get
			{
				return this.oleDbConnection_0;
			}
			set
			{
				this.oleDbConnection_0 = value;
			}
		}

		public string ConnectString
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

		public bool IsOpen
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

		public DBAccess()
		{
		}

		public void Close()
		{
			try
			{
				if (this.oleDbConnection_0.State == ConnectionState.Open)
				{
					this.oleDbConnection_0.Close();
					this.bool_0 = false;
				}
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
			}
		}

		public bool ExcuteNoQuery(string string_1)
		{
			bool flag = true;
			OleDbCommand oleDbCommand = null;
			try
			{
				try
				{
					oleDbCommand = new OleDbCommand(string_1)
					{
						Connection = this.oleDbConnection_0
					};
					oleDbCommand.ExecuteNonQuery();
				}
				catch (Exception exception)
				{
					flag = false;
				}
			}
			finally
			{
				if (oleDbCommand != null)
				{
					oleDbCommand.Dispose();
				}
			}
			return flag;
		}

		public bool ExcuteNoQueryOra(string string_1)
		{
			bool flag = true;
			OracleCommand oracleCommand = null;
			OracleConnection oracleConnection = new OracleConnection(this.string_0);
			try
			{
				try
				{
					oracleConnection.Open();
					if (oracleConnection.State == ConnectionState.Open)
					{
						oracleCommand = new OracleCommand(string_1)
						{
							Connection = oracleConnection
						};
						oracleCommand.ExecuteNonQuery();
					}
				}
				catch (Exception exception)
				{
					flag = false;
				}
			}
			finally
			{
				if (oracleCommand != null)
				{
					oracleCommand.Dispose();
				}
				if (oracleConnection.State == ConnectionState.Open)
				{
					oracleConnection.Dispose();
				}
			}
			return flag;
		}

		public bool ExcuteNoQueryOra(string string_1, string string_2, byte[] byte_0)
		{
			bool flag = true;
			OracleCommand oracleCommand = null;
			OracleConnection oracleConnection = new OracleConnection(this.string_0);
			int num = 0;
			try
			{
				try
				{
					oracleConnection.Open();
					if (oracleConnection.State == ConnectionState.Open)
					{
						oracleCommand = new OracleCommand(string_1, oracleConnection);
						oracleCommand.Parameters.Add(":img", OracleType.Blob, num, string_2);
						oracleCommand.Parameters[":img"].Value = byte_0;
						oracleCommand.Parameters[":img"].Direction = ParameterDirection.Input;
						oracleCommand.ExecuteNonQuery();
					}
				}
				catch (Exception exception)
				{
					flag = false;
				}
			}
			finally
			{
				if (oracleCommand != null)
				{
					oracleCommand.Dispose();
				}
				if (oracleConnection.State == ConnectionState.Open)
				{
					oracleConnection.Dispose();
				}
			}
			return flag;
		}

		public DataTable ExcuteQurey(string string_1)
		{
			DataTable dataTable = new DataTable();
			OleDbCommand oleDbCommand = null;
			OleDbDataAdapter oleDbDataAdapter = null;
			try
			{
				try
				{
					oleDbCommand = new OleDbCommand(string_1)
					{
						Connection = this.oleDbConnection_0
					};
					oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
					oleDbDataAdapter.Fill(dataTable);
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				if (oleDbCommand != null)
				{
					oleDbCommand.Dispose();
				}
				if (oleDbDataAdapter != null)
				{
					oleDbDataAdapter.Dispose();
				}
			}
			return dataTable;
		}

		public DataTable ExcuteQureyOra(string string_1)
		{
			DataTable dataTable = new DataTable();
			OracleCommand oracleCommand = null;
			OracleDataAdapter oracleDataAdapter = null;
			OracleConnection oracleConnection = new OracleConnection(this.string_0);
			try
			{
				try
				{
					oracleConnection.Open();
					if (oracleConnection.State == ConnectionState.Open)
					{
						oracleCommand = new OracleCommand(string_1)
						{
							Connection = oracleConnection
						};
						oracleDataAdapter = new OracleDataAdapter(oracleCommand);
						oracleDataAdapter.Fill(dataTable);
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
			finally
			{
				if (oracleCommand != null)
				{
					oracleCommand.Dispose();
				}
				if (oracleDataAdapter != null)
				{
					oracleDataAdapter.Dispose();
				}
				if (oracleConnection.State == ConnectionState.Open)
				{
					oracleConnection.Dispose();
				}
			}
			return dataTable;
		}

		~DBAccess()
		{
			try
			{
				if (this.oleDbConnection_0 != null)
				{
					this.oleDbConnection_0.Close();
				}
			}
			catch (Exception exception)
			{
				Debug.WriteLine(exception.Message);
			}
		}

		public bool Open()
		{
			try
			{
				this.bool_0 = true;
				if (this.oleDbConnection_0.State == ConnectionState.Closed)
				{
					this.oleDbConnection_0.ConnectionString = this.string_0;
					this.oleDbConnection_0.Open();
				}
			}
			catch (Exception exception)
			{
				this.bool_0 = false;
			}
			return this.bool_0;
		}

		public bool OpenAccessDB(string string_1, string string_2, string string_3)
		{
			string[] string1 = new string[] { "Provider=Microsoft.Jet.OleDb.4.0;User ID=", string_1, ";Data Source=", string_3, ";Password=", string_2 };
			this.string_0 = string.Concat(string1);
			return this.Open();
		}

		public bool OpenOracleDB(string string_1, string string_2, string string_3)
		{
			string[] string1 = new string[] { "Provider=MSDAORA;User ID=", string_1, ";Data Source=", string_3, ";Password=", string_2 };
			this.string_0 = string.Concat(string1);
			return this.Open();
		}

		public string ReadOracleLOB(string string_1)
		{
			string str;
			OracleConnection oracleConnection = new OracleConnection(this.ConnectString);
			oracleConnection.Open();
			OracleCommand oracleCommand = oracleConnection.CreateCommand();
			oracleCommand.CommandText = string.Concat("SELECT * FROM GRD  where SZTFH ='", string_1, "'");
			OracleDataReader oracleDataReader = oracleCommand.ExecuteReader();
			oracleDataReader.Read();
			if (oracleDataReader.HasRows)
			{
				OracleLob oracleLob = oracleDataReader.GetOracleLob(3);
				int length = (int)oracleLob.Length;
				byte[] numArray = new byte[length];
				oracleLob.Read(numArray, 0, length);
				oracleConnection.Close();
				str = Encoding.Unicode.GetString(numArray);
			}
			else
			{
				MessageBox.Show(string.Concat("数据库中不存在图幅", string_1, "的高程信息，请先生成该图幅高程！"));
				str = null;
			}
			return str;
		}

		public bool TJSave(string string_1, string string_2, string string_3, object object_0)
		{
			bool flag = true;
			OracleCommand oracleCommand = null;
			OracleParameterCollection parameters = null;
			OracleConnection oracleConnection = new OracleConnection(this.string_0);
			string str = "INSERT INTO D_TJXX (TJLX, TJMC,OTFBH,TJ)  Values (:TJLX,:TJMC,:OTFBH,:TJ)";
			try
			{
				try
				{
					oracleConnection.Open();
					if (oracleConnection.State == ConnectionState.Open)
					{
						oracleCommand = new OracleCommand(str, oracleConnection);
						parameters = oracleCommand.Parameters;
						parameters.Add(":TJLX", OracleType.NVarChar);
						parameters[":TJLX"].Direction = ParameterDirection.Input;
						parameters.Add(":TJMC", OracleType.NVarChar);
						parameters[":TJMC"].Direction = ParameterDirection.Input;
						parameters.Add(":OTFBH", OracleType.NVarChar);
						parameters[":OTFBH"].Direction = ParameterDirection.Input;
						parameters.Add(":TJ", OracleType.Blob);
						parameters[":TJ"].Direction = ParameterDirection.Input;
						parameters[":TJLX"].Value = string_1;
						parameters[":TJMC"].Value = string_2;
						parameters[":OTFBH"].Value = string_3;
						parameters[":TJ"].Value = object_0;
						oracleCommand.ExecuteNonQuery();
					}
				}
				catch (Exception exception)
				{
					flag = false;
				}
			}
			finally
			{
				if (oracleCommand != null)
				{
					oracleCommand.Dispose();
				}
				if (oracleConnection.State == ConnectionState.Open)
				{
					oracleConnection.Dispose();
				}
			}
			return flag;
		}

		public bool WriteOracleLOB(string string_1, string string_2)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(string_1);
			OracleConnection oracleConnection = new OracleConnection(this.ConnectString);
			oracleConnection.Open();
			OracleCommand oracleCommand = oracleConnection.CreateCommand();
			oracleCommand.Transaction = oracleConnection.BeginTransaction();
			oracleCommand.CommandText = string.Concat("delete from GRD where SZTFH ='", string_2, "'");
			oracleCommand.ExecuteNonQuery();
			double num = 0;
			oracleCommand.CommandText = "select nvl(max(objectid),0)+1 as MaxValue from GRD ";
			OracleDataReader oracleDataReader = oracleCommand.ExecuteReader();
			oracleDataReader.Read();
			num = double.Parse(oracleDataReader["MAXVALUE"].ToString());
			oracleDataReader = null;
			string[] string2 = new string[] { "insert  into GRD(SZTFH,SZHH,GCXX,objectid) values('", string_2, "','0','temp',", num.ToString(), ")" };
			oracleCommand.CommandText = string.Concat(string2);
			oracleCommand.ExecuteNonQuery();
			oracleCommand.CommandText = string.Concat("SELECT * FROM GRD  where SZTFH ='", string_2, "' FOR UPDATE");
			OracleDataReader oracleDataReader1 = oracleCommand.ExecuteReader();
			oracleDataReader1.Read();
			OracleLob oracleLob = oracleDataReader1.GetOracleLob(3);
			oracleLob.Write(bytes, 0, (int)bytes.Length);
			oracleCommand.Transaction.Commit();
			oracleConnection.Close();
			return true;
		}
	}
}
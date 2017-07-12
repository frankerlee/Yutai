using System;
using System.Collections.Generic;
using System.Linq;
using Yutai.Pipeline.Editor.SqlCe;

namespace Yutai.Pipeline.Editor.Axf
{
    public class AXF_FileInfo
    {
        private int _addCount = -1;
        private List<AXF_TableInfo> _axfTableInfos;
        private int _deleteCount = -1;
        private System.IO.FileInfo _fileInfo;
        private bool _isSelect = false;
        private int _modifyCount = -1;
        private int _noEditCount = -1;
        private SqlCeDb _sqlCeDb;

        public AXF_FileInfo(System.IO.FileInfo fileInfo)
        {
            this.FileInfo = fileInfo;
            this.LoadDatabase(fileInfo.FullName);
        }

        public void Close()
        {
            _sqlCeDb.Close();
            _sqlCeDb.Dispose();
        }

        private void LoadDatabase(string fileName)
        {
            try
            {
                this._axfTableInfos = new List<AXF_TableInfo>();
                _sqlCeDb = new SqlCeDb();

                if (this._sqlCeDb.Open(fileName))
                {
                    SqlCeResultSet set = new SqlCeResultSet("GEOMETRY_COLUMNS", this._sqlCeDb);
                    List<GeometryColumn> list = set.GetGeometryColumns();
                    
                    foreach (GeometryColumn geometryColumn in list)
                    {
                        this._axfTableInfos.Add(new AXF_TableInfo(this._sqlCeDb, geometryColumn.TableName, geometryColumn.GeometryType));
                    }
                }
                Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public int AddCount
        {
            get
            {
                if (this._addCount == -1)
                {
                    this._addCount = Enumerable.Sum<AXF_TableInfo>(this._axfTableInfos, (Func<AXF_TableInfo, int>)(c => c.AddCount));
                }
                return this._addCount;
            }
            set
            {
                this._addCount = value;
            }
        }

        public List<AXF_TableInfo> AxfTableInfos
        {
            get
            {
                return this._axfTableInfos;
            }
            set
            {
                this._axfTableInfos = value;
            }
        }

        public int DeleteCount
        {
            get
            {
                if (this._deleteCount == -1)
                {
                    this._deleteCount = Enumerable.Sum<AXF_TableInfo>(this._axfTableInfos, (Func<AXF_TableInfo, int>)(c => c.DeleteCount));
                }
                return this._deleteCount;
            }
            set
            {
                this._deleteCount = value;
            }
        }

        public string DirectoryName
        {
            get
            {
                if (this.FileInfo.Directory != null)
                {
                    return this.FileInfo.Directory.Name;
                }
                return null;
            }
        }

        public System.IO.FileInfo FileInfo
        {
            get
            {
                return this._fileInfo;
            }
            set
            {
                this._fileInfo = value;
            }
        }

        public bool IsSelect
        {
            get
            {
                return this._isSelect;
            }
            set
            {
                this._isSelect = value;
            }
        }

        public int ModifyCount
        {
            get
            {
                if (this._modifyCount == -1)
                {
                    this._modifyCount = Enumerable.Sum<AXF_TableInfo>(this._axfTableInfos, (Func<AXF_TableInfo, int>)(c => c.ModifyCount));
                }
                return this._modifyCount;
            }
            set
            {
                this._modifyCount = value;
            }
        }

        public int NoEditCount
        {
            get
            {
                if (this._noEditCount == -1)
                {
                    this._noEditCount = Enumerable.Sum<AXF_TableInfo>(this._axfTableInfos, (Func<AXF_TableInfo, int>)(c => c.NoEditCount));
                }
                return this._noEditCount;
            }
            set
            {
                this._noEditCount = value;
            }
        }
    }
}

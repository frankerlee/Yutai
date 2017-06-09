using System;
using System.Collections;
using System.Collections.Generic;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Common
{
    public class StaffHelper
    {
        private string string_0 = "";

        public StaffHelper(string string_1)
        {
            this.string_0 = string_1;
        }

        public List<Staff> Load()
        {
            List<Staff> list = new List<Staff>();
            ITable table = AppConfigInfo.OpenTable(this.string_0);
            if (table != null)
            {
                ICursor o = table.Search(null, false);
                for (IRow row = o.NextRow(); row != null; row = o.NextRow())
                {
                    Staff item = new Staff {
                        StaffID = Convert.ToString(row.get_Value(table.FindField("ID"))),
                        LoginName = Convert.ToString(row.get_Value(table.FindField("NAME"))),
                        RealName = Convert.ToString(row.get_Value(table.FindField("REALNAME"))),
                        Password = Convert.ToString(row.get_Value(table.FindField("PASSWORD_")))
                    };
                    list.Add(item);
                }
                ComReleaser.ReleaseCOMObject(o);
            }
            return list;
        }

        public bool Load(out IList ilist_0, out IList ilist_1, out IList ilist_2)
        {
            ilist_0 = new ArrayList();
            ilist_1 = new ArrayList();
            ilist_2 = new ArrayList();
            new List<Staff>();
            ITable table = AppConfigInfo.OpenTable(this.string_0);
            ICursor o = table.Search(null, false);
            for (IRow row = o.NextRow(); row != null; row = o.NextRow())
            {
                ilist_0.Add(row.get_Value(table.FindField("ID")).ToString());
                ilist_1.Add(row.get_Value(table.FindField("NAME")).ToString());
                ilist_2.Add(row.get_Value(table.FindField("PASSWORD_")).ToString());
            }
            ComReleaser.ReleaseCOMObject(o);
            return true;
        }

        public bool ValidePassword(string string_1, string string_2)
        {
            ITable table = AppConfigInfo.OpenTable(this.string_0);
            if (table == null)
            {
                return false;
            }
            IQueryFilter queryFilter = new QueryFilter {
                WhereClause = "ID='" + string_1 + "' and PASSWORD_ = '" + string_2 + "'"
            };
            if (table.RowCount(queryFilter) == 0)
            {
                return false;
            }
            return true;
        }
    }
}


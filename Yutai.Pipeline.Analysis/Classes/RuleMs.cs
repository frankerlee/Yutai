using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class RuleMsItem
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string MSFS { get; set; }
        public double RxdDepth { get; set; }
        public double CxdDepth { get; set; }

        public string Remark { get; set; }

    }
    public class RuleMs
    {

        public List<RuleMsItem> _rules;

        public RuleMs(IPipelineConfig config)
        {
            ITable table = ((IFeatureWorkspace)config.Workspace).OpenTable("YT_PIPE_DEPTH");
            _rules=new List<RuleMsItem>();
            ICursor cursor = table.Search(null, false);
            int codeIdx = cursor.FindField("PipeCode");
            int nameIdx = cursor.FindField("PipeName");
            int msfsIdx = cursor.FindField("MSFS");
            int rxdIdx = cursor.FindField("RXDDepth");
            int cxdIdx = cursor.FindField("CXDDepth");
            int bzIdx = cursor.FindField("Remark");
            IRow row = cursor.NextRow();
            while (row != null)
            {
                RuleMsItem item=new RuleMsItem()
                {
                    Code= row.get_Value(codeIdx).ToString(),
                    Name= row.get_Value(nameIdx).ToString(),
                    MSFS= row.get_Value(msfsIdx).ToString(),
                    RxdDepth=Convert.ToDouble(row.get_Value(rxdIdx).ToString()),
                    CxdDepth = Convert.ToDouble(row.get_Value(cxdIdx).ToString()),
                    Remark = row.get_Value(bzIdx).ToString()

                };
              _rules.Add(item);
                    row = cursor.NextRow();
             
            }
            Marshal.ReleaseComObject(cursor);
            Marshal.ReleaseComObject(table);
        }

        public double GetRuleMS(string sPipeCode, string sDepthMethod, string sDepPosition)
        {
            double result;
            RuleMsItem item = _rules.FirstOrDefault(c => c.Code == sPipeCode && c.MSFS == sDepthMethod);
            if (item == null)
            {
                item = _rules.FirstOrDefault(c => c.Code == sPipeCode && c.MSFS == "直埋");
            }

            if (item == null)
            {
                item = _rules.FirstOrDefault(c => c.Code == sPipeCode);

            }
            if (item == null) return 0.0;
            if (string.IsNullOrEmpty(sDepPosition))
            {
                return item.RxdDepth > item.CxdDepth ? item.RxdDepth : item.CxdDepth;
            }

            if (sDepPosition.Contains("人"))
            {
                return item.RxdDepth;
            }
            else
            {
                return item.CxdDepth;}
          
        }
    }
}
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class RuleMs
    {
        public TreeView tree = new TreeView();

        public RuleMs(IPipelineConfig config)
        {
            ITable table = ((IFeatureWorkspace)config.Workspace).OpenTable("EMMETAPIPEDEPTH");
            ICursor cursor = table.Search(null, false);
            IRow row = cursor.NextRow();
            while (row != null)
            {
                string text = row.get_Value(1).ToString().ToUpper();
                if (text == "")
                {
                    row = cursor.NextRow();
                }
                else
                {
                    int num = this.tree.Nodes.IndexOfKey(text);
                    if (num == -1)
                    {
                        TreeNode node = this.tree.Nodes.Add(text, text);
                        num = this.tree.Nodes.IndexOf(node);
                    }
                    text = row.get_Value(2).ToString().ToUpper();
                    if (this.tree.Nodes[num].Nodes.IndexOfKey(text) == -1)
                    {
                        TreeNode treeNode = new TreeNode();
                        treeNode.Name = text;
                        treeNode.Text = row.get_Value(3).ToString();
                        treeNode.Tag = row.get_Value(4).ToString();
                        this.tree.Nodes[num].Nodes.Add(treeNode);
                        this.tree.Nodes[num].Tag = ((Convert.ToDouble(row.get_Value(3)) > Convert.ToDouble(row.get_Value(4))) ? row.get_Value(3) : row.get_Value(4));
                    }
                    row = cursor.NextRow();
                }
            }
            Marshal.ReleaseComObject(cursor);
        }

        public double GetRuleMS(string sPipeName, string sDepthMethod, string sDepPosition)
        {
            double result;
            if (!(sPipeName != "") || this.tree.Nodes.IndexOfKey(sPipeName.ToUpper()) == -1)
            {
                result = 0.0;
            }
            else if (!(sDepthMethod != "") || this.tree.Nodes[sPipeName.ToUpper()].Nodes.IndexOfKey(sDepthMethod.ToUpper()) == -1)
            {
                result = Convert.ToDouble(this.tree.Nodes[sPipeName.ToUpper()].Tag);
            }
            else if (sDepPosition == "人行道下埋深")
            {
                result = Convert.ToDouble(this.tree.Nodes[sPipeName.ToUpper()].Nodes[sDepthMethod.ToUpper()].Text);
            }
            else
            {
                result = Convert.ToDouble(this.tree.Nodes[sPipeName.ToUpper()].Nodes[sDepthMethod.ToUpper()].Tag);
            }
            return result;
        }
    }
}
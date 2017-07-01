using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.TableEditor.Functions;

namespace Yutai.Plugins.TableEditor.Controls
{
    public partial class FunctionTreeView : TreeView
    {
        private IFunction _function;

        public FunctionTreeView()
        {
            InitializeComponent();
        }

        public FunctionTreeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void InitControl()
        {
            this.ImageList = imageList;
            this.Nodes.Clear();
            _function = new FunctionBase();
            List<IFunction> list = _function.GetFunctions();
            foreach (IFunction function in list)
            {
                TreeNode parrentNode;
                if (Nodes.ContainsKey(function.Category))
                    parrentNode = this.Nodes[function.Category];
                else
                {
                    parrentNode = this.Nodes.Add(function.Category, function.Category, 0, 2);
                }
                if (parrentNode == null)
                    continue;
                TreeNode node = parrentNode.Nodes.Add(function.Key, function.Caption, 1, 2);

                node.ToolTipText = function.GetDescription();
                node.Tag = function;
            }
            this.ShowNodeToolTips = true;
        }
    }
}
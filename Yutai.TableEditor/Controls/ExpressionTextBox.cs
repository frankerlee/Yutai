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
    public partial class ExpressionTextBox : TextBox
    {
        public ExpressionTextBox()
        {
            InitializeComponent();
        }

        public ExpressionTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        
        public void Add(IFunction function)
        {
            int startIndex = this.SelectionStart;
            if (string.IsNullOrWhiteSpace(this.SelectedText))
            {
                this.Text = this.Text.Insert(startIndex, function.Expression);
            }
            else
            {
                this.SelectedText = function.Expression;
            }
            if (function.Parameters != null && function.Parameters.Count > 0)
            {
                this.SelectionStart = this.Text.IndexOf(function.Parameters[0].Name, startIndex, StringComparison.Ordinal);
                this.SelectionLength = function.Parameters[0].Name.Length;
            }
            else
            {
                this.SelectionStart = this.Text.Last();
            }
        }

        public void Add(string fieldName)
        {
            int startIndex = this.SelectionStart;
            if (string.IsNullOrWhiteSpace(this.SelectedText))
                this.Text = this.Text.Insert(startIndex, fieldName);
            else
                this.SelectedText = fieldName;
            this.SelectionStart = this.Text.Last();
        }
    }
}

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Yutai.Pipeline.Editor.Controls
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
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.Controls
{
    public partial class DataGridViewEx : DataGridView
    {
        private GridPrinter _gridPrinter;
        private PrintDocument _printDocument;
        private string _title;

        public DataGridViewEx()
        {
            InitializeComponent();
        }

        private void _printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = _gridPrinter.DrawDataGridView(e.Graphics);
        }

        public DataGridViewEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        [Browsable(true)]
        [Description("显示标题"), DefaultValue("统计结果")]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public void PrintPreview()
        {
            _printDocument = new PrintDocument();
            _printDocument.DocumentName = _title;
            _printDocument.PrintPage += _printDocument_PrintPage;
            _gridPrinter = new GridPrinter(this, _printDocument, false, true, _title, DefaultFont, BackColor, true);

            PrintPreviewDialogEx dialog = new PrintPreviewDialogEx();
            dialog.Document = _printDocument;
            dialog.ShowDialog();
        }

        public bool ToExcel(bool isShowExcle)
        {
            if (this.Rows.Count == 0)
                return false;
            //建立Excel对象      
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称      
            for (int i = 0; i < this.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = this.Columns[i].HeaderText;
            }
            //填充数据      
            for (int i = 0; i < this.RowCount - 1; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
                {
                    if (this[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" + this[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = this[j, i].Value.ToString();
                    }
                }
            }
            return true;
        }
    }
}

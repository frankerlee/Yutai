using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.Excel
{
    public class DataGridViewListViewHelper
    {
        public static int[] GetColsWidth(DataGridView dataGridView_0)
        {
            int[] numArray = null;
            int columnCount = dataGridView_0.ColumnCount;
            numArray = new int[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                numArray[i] = dataGridView_0.Columns[i].Width;
            }
            return numArray;
        }

        public static int[] GetColsWidth(ListView listView_0)
        {
            int[] numArray = null;
            int count = listView_0.Columns.Count;
            numArray = new int[count];
            for (int i = 0; i < count; i++)
            {
                numArray[i] = listView_0.Columns[i].Width;
            }
            return numArray;
        }

        public static string[,] ToStringArray(DataGridView dataGridView_0, bool bool_0)
        {
            string[,] strArray = null;
            int count = dataGridView_0.Rows.Count;
            int num2 = dataGridView_0.Columns.Count;
            if ((count > 0) && dataGridView_0.Rows[count - 1].IsNewRow)
            {
                count--;
            }
            int num3 = 0;
            if (bool_0)
            {
                count++;
                strArray = new string[count, num2];
                for (num3 = 0; num3 < num2; num3++)
                {
                    strArray[0, num3] = dataGridView_0.Columns[num3].HeaderText;
                }
                num3 = 1;
            }
            else
            {
                strArray = new string[count, num2];
            }
            for (int i = 0; num3 < count; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    strArray[num3, j] = dataGridView_0.Rows[i].Cells[j].Value.ToString();
                }
                num3++;
            }
            return strArray;
        }

        public static string[,] ToStringArray(ListView listView_0, bool bool_0)
        {
            ListView view = listView_0;
            int count = view.Items.Count;
            int num2 = view.Columns.Count;
            if (bool_0)
            {
                count++;
            }
            string[,] strArray = null;
            strArray = new string[count, num2];
            int num3 = 0;
            if (bool_0)
            {
                for (num3 = 0; num3 < num2; num3++)
                {
                    strArray[0, num3] = view.Columns[num3].Text;
                }
                num3 = 1;
            }
            for (int i = 0; num3 < count; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    strArray[num3, j] = view.Items[i].SubItems[j].Text;
                }
                num3++;
            }
            return strArray;
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Catalog.UI
{
   
    internal partial class CatalogComboBox : UserControl
    {
        public delegate void SelectedItemChangedEvent(CatalogComboItem item);
        public event SelectedItemChangedEvent SelectedItemChanged = null;
        private ImageList _imageList;

        public CatalogComboBox()
        {
            InitializeComponent();

            //if (ExplorerImageList.List.CountImages == 0)
            //{
            //    ExplorerImageList.List.AddImages(imageList1);
            //}
            //if (FormExplorer.globalImageList == null) FormExplorer.globalImageList = imageList1;
        }

        public ImageList CatalogImageList
        {
            set { _imageList = value; }
        }

        private void CatalogComboBox_Load(object sender, EventArgs e)
        {

        }

        public void InitComboBox()
        {
            cmbCatalog.Items.Clear();
            


            cmbCatalog.SelectedIndex = 0;
        }

        public ComboBox.ObjectCollection Items
        {
            get { return cmbCatalog.Items; }
        }
        public CatalogComboItem SelectedItem
        {
            get { return cmbCatalog.SelectedItem as CatalogComboItem;}
        }

        public int SelectedIndex
        {
            get { return cmbCatalog.SelectedIndex; }
            set { cmbCatalog.SelectedIndex = value; }
        }
        public void AddChildNode(CatalogComboItem exObject)
        {
            int index = cmbCatalog.SelectedIndex;
            if (index == -1) return;

            int level = ((CatalogComboItem)cmbCatalog.SelectedItem).Level;
            cmbCatalog.Items.Insert(index+1, new CatalogComboItem(exObject.Text, exObject.FullName, exObject.ImageIndex,level + 1) {Tag=exObject.Tag});
          
            cmbCatalog.SelectedIndex = index + 1;
        }

       
        private void cmbCatalog_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= cmbCatalog.Items.Count) return;
            if (!(cmbCatalog.Items[e.Index] is CatalogComboItem)) return;

            //DrawItemState.
            CatalogComboItem item = (CatalogComboItem)cmbCatalog.Items[e.Index];
            int level = item.Level;
            if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
            {
                level = 0;
            }

            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                Rectangle rect = new Rectangle(e.Bounds.X + level * 11 + 18, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected &&
                    (e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit)
                {
                    brush.Color = Color.DarkBlue;
                    e.Graphics.FillRectangle(brush, rect);
                    brush.Color = Color.White;
                }
                else
                {
                    brush.Color = Color.White;
                    e.Graphics.FillRectangle(brush, rect);
                    brush.Color = Color.Black;
                }
                try
                {
                    Image image = _imageList.Images[item.ImageIndex];
                    e.Graphics.DrawImage(image, e.Bounds.X + level * 11 + 3, e.Bounds.Y);
                }
                catch { }
                e.Graphics.DrawString(item.ToString(), cmbCatalog.Font, brush, e.Bounds.X + level * 11 + 20, e.Bounds.Y + 2);
            }
        }

        private void cmbCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItemChanged((CatalogComboItem) cmbCatalog.SelectedItem);
        }
    }

    internal class CatalogComboItem
    {
        private string _text;
        private int _level, _imageIndex;
        object _tag;
        private string _fullName;
        public CatalogComboItem(string text, string fullName, int imageIndex, int level )
        {
            _text = text;
            _level = level;
            _imageIndex = imageIndex;
            _fullName = fullName;
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public int ImageIndex
        {
            get { return _imageIndex; }
            set { _imageIndex = value; }
        }

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public override string ToString()
        {
            return _text;
        }
    }
   
    
}

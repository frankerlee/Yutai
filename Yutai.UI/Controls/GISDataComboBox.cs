using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;

namespace Yutai.UI.Controls
{
    internal partial class GISDataComboBox : UserControl
    {
        public delegate void SelectedItemChangedEvent(GISDataComboItem item);

        public event SelectedItemChangedEvent SelectedItemChanged = null;
        private ImageList _imageList;

        public GISDataComboBox()
        {
            InitializeComponent();

            //if (ExplorerImageList.List.CountImages == 0)
            //{
            //    ExplorerImageList.List.AddImages(imageList1);
            //}
            //if (FormExplorer.globalImageList == null) FormExplorer.globalImageList = imageList1;
        }

        public ImageList GISDataImageList
        {
            set { _imageList = value; }
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

        public GISDataComboItem SelectedItem
        {
            get { return cmbCatalog.SelectedItem as GISDataComboItem; }
        }

        public int SelectedIndex
        {
            get { return cmbCatalog.SelectedIndex; }
            set { cmbCatalog.SelectedIndex = value; }
        }

        public void AddChildNode(GISDataComboItem exObject)
        {
            int index = cmbCatalog.SelectedIndex;
            if (index == -1) index = cmbCatalog.Items.Count - 1;

            int level = ((GISDataComboItem) exObject).Level;
            cmbCatalog.Items.Insert(index + 1,
                new GISDataComboItem(exObject.Text, exObject.FullName, exObject.ImageIndex, level) {Tag = exObject.Tag});

            cmbCatalog.SelectedIndex = index + 1;
        }


        private void cmbCatalog_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= cmbCatalog.Items.Count) return;
            if (!(cmbCatalog.Items[e.Index] is GISDataComboItem)) return;

            //DrawItemState.
            GISDataComboItem item = (GISDataComboItem) cmbCatalog.Items[e.Index];
            int level = item.Level;
            if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
            {
                level = 0;
            }

            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                Rectangle rect = new Rectangle(e.Bounds.X + level*11 + 18, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
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
                    e.Graphics.DrawImage(image, e.Bounds.X + level*11 + 3, e.Bounds.Y, 16, 16);
                }
                catch
                {
                }
                e.Graphics.DrawString(item.ToString(), cmbCatalog.Font, brush, e.Bounds.X + level*11 + 40,
                    e.Bounds.Y + 2);
            }
        }

        private void cmbCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItemChanged((GISDataComboItem) cmbCatalog.SelectedItem);
        }
    }

    internal class GISDataComboItem
    {
        private string _text;
        private int _level, _imageIndex;
        object _tag;
        private string _fullName;

        public GISDataComboItem(string text, string fullName, int imageIndex, int level)
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
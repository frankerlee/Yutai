using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmLayerCheck : Form
    {
        private IContainer icontainer_0 = null;
        [CompilerGenerated]

        public frmLayerCheck()
        {
            this.InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ISet set = new SetClass();
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    object obj2 = this.checkedListBox1.Items[i];
                    set.Add((obj2 as LayerObject).Layer);
                }
            }
            if (set.Count > 0)
            {
                (this.Map as IMapClipOptions).ClipFilter = set;
            }
            else
            {
                (this.Map as IMapClipOptions).ClipFilter = null;
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (!this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void btnSwitchSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                bool itemChecked = this.checkedListBox1.GetItemChecked(i);
                this.checkedListBox1.SetItemChecked(i, !itemChecked);
            }
        }

 private void frmLayerCheck_Load(object sender, EventArgs e)
        {
            ISet clipFilter = (this.Map as IMapClipOptions).ClipFilter;
            for (int i = 0; i < this.Map.LayerCount; i++)
            {
                ILayer unk = this.Map.get_Layer(i);
                bool isChecked = false;
                if (clipFilter != null)
                {
                    isChecked = clipFilter.Find(unk);
                }
                this.checkedListBox1.Items.Add(new LayerObject(unk), isChecked);
            }
        }

 public IMap Map
        {
            [CompilerGenerated]
            get
            {
                return this.imap_0;
            }
            [CompilerGenerated]
            set
            {
                this.imap_0 = value;
            }
        }
    }
}


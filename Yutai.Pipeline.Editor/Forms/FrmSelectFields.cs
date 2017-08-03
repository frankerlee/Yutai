using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Forms
{
    public partial class FrmSelectFields : Form
    {
        public FrmSelectFields(IFields fields)
        {
            InitializeComponent();
            this.ucSelectFields1.Fields = fields;
        }
        public FrmSelectFields(IFields fields, List<IField> selectedFieldList)
        {
            InitializeComponent();
            this.ucSelectFields1.Fields = fields;
            this.ucSelectFields1.SelectedFieldList = selectedFieldList;
        }

        public List<IField> SelectedFieldList
        {
            get { return ucSelectFields1.SelectedFieldList; }
            set { ucSelectFields1.SelectedFieldList = value; }
        }



        public IDictionary<int, IField> SelectedFieldDictionary
        {
            get { return ucSelectFields1.SelectedFieldDictionary; }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            ucSelectFields1.SelectAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ucSelectFields1.SelectClear();
        }
    }
}

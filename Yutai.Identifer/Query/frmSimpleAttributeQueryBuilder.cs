using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Syncfusion.Windows.Forms;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class frmSimpleAttributeQueryBuilder : MetroForm
    {
        private string _whereCaluse = "";

        private Button btnApply;

        private Button btnClose;

        private Panel panel1;

        private UcAttributeQueryBuilder ucBuilder = new UcAttributeQueryBuilder();

        private Button btnClear;

    

        public ILayer CurrentLayer
        {
            set
            {
                this.ucBuilder.CurrentLayer = value;
            }
        }

        public ITable Table
        {
            set
            {
                this.ucBuilder.Table = value;
            }
        }

        public string WhereCaluse
        {
            get
            {
                return this._whereCaluse;
            }
            set
            {
                this.ucBuilder.WhereCaluse = value;
            }
        }

        public frmSimpleAttributeQueryBuilder()
        {
            this.InitializeComponent();
            this.ucBuilder.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.ucBuilder);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            this._whereCaluse = this.ucBuilder.WhereCaluse;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ucBuilder.ClearWhereCaluse();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        

        private void frmAttributeQueryBuilder_Load(object sender, EventArgs e)
        {
        }

       
    }
}


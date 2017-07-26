using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Plugins.Template.Concretes;
using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Forms
{
    public partial class frmEditDataset : Form
    {
        private IObjectDataset _dataset;
        private ITemplateDatabase _database;
        public frmEditDataset()
        {
            InitializeComponent();
        }

        public void SetDatabase(ITemplateDatabase database)
        {
            _database = database;
        }

        public IObjectDataset Dataset
        {
            get { return _dataset; }
            set
            {
                _dataset = value;
                txtName.Text = _dataset.Name;
                txtAliasName.Text = _dataset.AliasName;
                txtBaseName.Text = _dataset.BaseName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtName.EditValue == null || string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("名称不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtBaseName.EditValue == null || string.IsNullOrEmpty(txtBaseName.Text.Trim()))
            {
                MessageBox.Show("基本名称不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _dataset.Name = txtName.EditValue.ToString();
            _dataset.BaseName = txtBaseName.EditValue.ToString();
            _dataset.AliasName = txtAliasName.EditValue.ToString();

            int oid = _database.GetObjectID(_dataset.Name, enumTemplateObjectType.FeatureDataset);
            if (_dataset.ID > 0 )
            {
                if (_dataset.ID != oid)
                {
                    MessageBox.Show("该名称已经存在!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                if (oid > 0)
                {
                    MessageBox.Show("该名称已经存在!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            _database.SaveDataset(_dataset);
            DialogResult = DialogResult.OK;
        }
    }
}

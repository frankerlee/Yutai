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
    public partial class frmEditDomain : Form
    {
        private IYTDomain _domain;
        private ITemplateDatabase _database;
        public frmEditDomain()
        {
            InitializeComponent();
        }

        public void SetDomain(IYTDomain pDomain)
        {
            _domain = pDomain;
            LoadDomain();

        }

        public void SetDatabase(ITemplateDatabase database)
        {
            _database = database;
        }

        private void LoadDomain()
        {
            txtName.Text = _domain.Name;
            txtDescription.Text = _domain.Description;
            dataGridView1.Rows.Clear();
            foreach (YTDomainValue pair in _domain.ValuePairs)
            {
                
                dataGridView1.Rows.Add(new object[] {pair.Value, pair.Description});
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveDomain();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveDomain())
                DialogResult = DialogResult.OK;
        }

        private bool SaveDomain()
        {
            IYTDomain domain=new YTDomain();
            domain.Name = txtName.EditValue == null ? "" : txtName.EditValue.ToString();
            domain.Description = txtDescription.EditValue == null ? "" : txtDescription.EditValue.ToString();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string code = dataGridView1.Rows[i].Cells[0].Value == null
                    ? ""
                    : dataGridView1.Rows[i].Cells[0].Value.ToString();
                string description = dataGridView1.Rows[i].Cells[1].Value == null
                    ? ""
                    : dataGridView1.Rows[i].Cells[1].Value.ToString();
                if (string.IsNullOrEmpty(code)) continue;
                YTDomainValue pair=new YTDomainValue() {Value=code,Description = description};
                domain.ValuePairs.Add(pair);
            }

            int back = _database.GetObjectID(domain.Name, enumTemplateObjectType.Domain);
            if (back > 0 && back != _domain.ID)
            {
                MessageBox.Show("该数据字典已经存在，请改名后保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            domain.ID = _domain.ID;
            return _database.SaveDomain(domain);
            _domain = domain;
        }

        public IYTDomain GetDomain()
        {
            return _domain;
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal partial class RepresentationPropertyPage : UserControl
    {
        private IRepresentation m_pRepentation = null;

        public RepresentationPropertyPage()
        {
            this.InitializeComponent();
        }

        private void RepresentationPropertyPage_Load(object sender, EventArgs e)
        {
            int num;
            IRepresentationRule rule;
            IRepresentationRules representationRules = this.m_pRepentation.RepresentationClass.RepresentationRules;
            representationRules.Reset();
            representationRules.Next(out num, out rule);
            while (rule != null)
            {
                string name = representationRules.get_Name(num);
                this.comboBox1.Items.Add(new RepresentationRuleWrap(num, name, rule));
            }
        }

        public IRepresentation Representation
        {
            get { return this.m_pRepentation; }
            set { this.m_pRepentation = value; }
        }

        internal partial class RepresentationRuleWrap
        {
            private int _id;
            private string _name;
            private IRepresentationRule _RepresentationRule;

            internal RepresentationRuleWrap(int id, string name, IRepresentationRule pRepresentationRule)
            {
                this._id = id;
                this._name = name;
                this._RepresentationRule = pRepresentationRule;
            }

            public string Name
            {
                get { return this._name; }
            }

            public IRepresentationRule RepresentationRule
            {
                get { return this._RepresentationRule; }
            }

            public int RuleID
            {
                get { return this._id; }
            }
        }
    }
}
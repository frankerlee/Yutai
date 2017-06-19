using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Yutai.UI.Controls
{
    public partial class ConfigPanelControl : Panel
    {
        private Label _gradientLabel1;
        public ConfigPanelControl()
        {
            InitializeComponent();
            AddLabel();

            BorderStyle = BorderStyle.None;
        }

        //public ConfigPanelControl(IContainer container)
        //{
        //    container.Add(this);

        //    InitializeComponent();
        //}

        private void AddLabel()
        {
            _gradientLabel1 = new Label
            {
                Dock = DockStyle.Top,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204),
                ForeColor = SystemColors.ControlDarkDark,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Controls.Add(_gradientLabel1);
        }

        public string HeaderText
        {
            get { return _gradientLabel1.Text; }
            set { _gradientLabel1.Text = value; }
        }

        public void ShowCaptionOnly()
        {
            Height = _gradientLabel1.Height;
        }
    }
}

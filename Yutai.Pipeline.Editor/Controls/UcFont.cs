using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.Pipeline.Editor.Classes;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcFont : UserControl, IFontConfig
    {
        private FontDialog _fontDialog;
        private ColorDialog _colorDialog;

        private Font _font;
        private Color _color;

        public UcFont()
        {
            InitializeComponent();
        }

        public Font Font
        {
            get { return _font; }
        }

        public Color Color
        {
            get { return _color; }
        }

        [Browsable(true)]
        [DisplayName(@"组名"), Description("组名"), Category("扩展"), DefaultValue("字体设置")]
        public string GroupHeader
        {
            get { return this.groupBox1.Text; }
            set { this.groupBox1.Text = value; }
        }

        private void btnFontSetting_Click(object sender, EventArgs e)
        {
            if (_fontDialog == null)
                _fontDialog = new FontDialog();
            if (_fontDialog.ShowDialog() == DialogResult.OK)
            {
                _font = _fontDialog.Font;
                this.textBox2.Text = @"测试";
                this.textBox2.Font = _font;
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (_colorDialog == null)
                _colorDialog = new ColorDialog();
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                _color = _colorDialog.Color;
                this.textBox2.ForeColor = _color;
            }
        }
    }
}

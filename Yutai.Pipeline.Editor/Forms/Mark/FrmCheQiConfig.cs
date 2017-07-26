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
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Forms.Mark
{
    public partial class FrmCheQiConfig : Form, ICheQiConfig
    {
        private IFeatureLayer _flagLayer;
        private string _expression;
        private IFeatureLayer _flagLineLayer;
        private IFeatureLayer _flagAnnoLayer;
        private string _fontName;
        private decimal _fontSize;
        private bool _italic;
        private bool _underline;
        private bool _bold;
        private bool _strikethrough;
        private IRgbColor _fontColor;

        public FrmCheQiConfig(IAppContext context)
        {
            InitializeComponent();
            this.cmbFlagLayer.Map = context.FocusMap;
            this.cmbFlagLayer.GeometryTypes = new List<esriGeometryType>()
            {
                esriGeometryType.esriGeometryPoint,
                esriGeometryType.esriGeometryPolyline
            };
            this.cmbFlagAnnoLayer.Map = context.FocusMap;
            this.cmbFlagLineLayer.Map = context.FocusMap;
            this.cmbFlagLineLayer.GeometryType = esriGeometryType.esriGeometryPolyline;

            _fontColor = new RgbColorClass();
        }

        public IFeatureLayer FlagLayer
        {
            get { return _flagLayer; }
        }

        public IFeatureLayer FlagLineLayer
        {
            get { return _flagLineLayer; }
        }

        public IFeatureLayer FlagAnnoLayer
        {
            get { return _flagAnnoLayer; }
        }

        public string Expression
        {
            get { return _expression; }
        }

        public string FontName
        {
            get { return _fontName; }
        }

        public decimal FontSize
        {
            get { return _fontSize; }
        }

        public bool Italic
        {
            get { return _italic; }
        }

        public bool Underline
        {
            get { return _underline; }
        }

        public bool Bold
        {
            get { return _bold; }
        }

        public bool Strikethrough
        {
            get { return _strikethrough; }
        }

        public IRgbColor FontColor
        {
            get { return _fontColor; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _flagLayer = this.cmbFlagLayer.SelectFeatureLayer;
            _flagAnnoLayer = this.cmbFlagAnnoLayer.SelectFeatureLayer;
            _flagLineLayer = this.cmbFlagLineLayer.SelectFeatureLayer;
            _expression = this.TxtExpression.Text;

            _fontName = this.ucFont.Font.Name;
            _fontSize = (decimal) this.ucFont.Font.Size;
            _italic = this.ucFont.Font.Italic;
            _underline = this.ucFont.Font.Underline;
            _bold = this.ucFont.Font.Bold;
            _strikethrough = this.ucFont.Font.Strikeout;
            _fontColor.Red = this.ucFont.Color.R;
            _fontColor.Green = this.ucFont.Color.G;
            _fontColor.Blue = this.ucFont.Color.B;
        }

        private void btnExpression_Click(object sender, EventArgs e)
        {
            FrmAnnotationExpression frm = new FrmAnnotationExpression(this.cmbFlagLayer.SelectFeatureClass.Fields, this.TxtExpression.Text);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.TxtExpression.Text = frm.Expression;
            }
        }

        private void cmbFlagLayer_SelectComplateEvent()
        {
            this.TxtExpression.Clear();
        }
    }
}

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
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Forms.Common
{
    public partial class FrmCode : Form, ICodeSetting
    {
        private IFeatureLayer _featureLayer;

        public FrmCode(IFeatureLayer featureLayer, string codeName)
        {
            InitializeComponent();
            _featureLayer = featureLayer;
            Code = FeatureClassUtil.GetMaxValue(_featureLayer.FeatureClass, codeName);
        }
        
        public string Code
        {
            get { return txtCode.Text; }
            set { txtCode.Text = value; }
        }

        public void Next()
        {
            Code = CommonHelper.GetNext(Code);
        }
    }
}

using System;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.Plugins.Editor.Forms
{
    partial class frmInputValue
    {
		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

	
            InitializeComponent();
		}


		private SimpleButton simpleButton_0;
		private TextEdit textEdit_0;
    }
}
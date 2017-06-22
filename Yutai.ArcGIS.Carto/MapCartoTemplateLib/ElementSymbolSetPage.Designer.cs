using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    partial class ElementSymbolSetPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.styleButton1 = new StyleButton();
            this.label1 = new Label();
            base.SuspendLayout();
            this.styleButton1.Location = new Point(12, 20);
            this.styleButton1.Name = "styleButton1";
            this.styleButton1.Size = new Size(196, 73);
            this.styleButton1.Style = null;
            this.styleButton1.TabIndex = 0;
            this.styleButton1.Click += new EventHandler(this.styleButton1_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择符号";
            base.Controls.Add(this.label1);
            base.Controls.Add(this.styleButton1);
            base.Name = "ElementSymbolSetPage";
            base.Size = new Size(213, 165);
            base.Load += new EventHandler(this.ElementSymbolSetPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private Label label1;
        private MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0;
        private StyleButton styleButton1;
    }
}
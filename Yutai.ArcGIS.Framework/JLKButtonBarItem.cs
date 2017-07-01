using System;
using DevExpress.XtraBars;

namespace Yutai.ArcGIS.Framework
{
    public class JLKButtonBarItem : ButtonBarItem
    {
        private BarItem barItem_0;

        public JLKButtonBarItem(object object_0)
        {
            this.barItem_0 = object_0 as BarItem;
            if (this.barItem_0 == null)
            {
                throw new Exception("该工具栏按钮不是按钮");
            }
        }

        public override string Caption
        {
            set { this.barItem_0.Caption = value; }
        }

        public override object EditValue
        {
            get { return ""; }
            set { }
        }

        public override bool Enabled
        {
            get { return this.barItem_0.Enabled; }
            set { this.barItem_0.Enabled = value; }
        }
    }
}
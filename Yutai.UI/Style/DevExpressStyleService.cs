using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using Yutai.Shared;
using Yutai.UI.Controls;

namespace Yutai.UI.Style
{
    public class DevExpressStyleService : IStyleService
    {
        private readonly Color _metroColor = Color.FromArgb(22, 165, 220);
        private readonly string _skinName;

        public DevExpressStyleService()
        {
           // Logger.Current.Trace("In DevExpress Skin");
        }

        public void ApplyStyle(System.Windows.Forms.Form  form)
        {
            //if (form is XtraForm)
            //{
            //    ((XtraForm) form).AllowFormSkin = true;
            //}
             

            //ApplyStyle(form.Controls);
        }

        private void ApplyStyle(Control.ControlCollection controls)
        {
            //for (int i = controls.Count - 1; i >= 0; i--)
            //{
            //    var control = controls[i];

            //    ApplyStyle(control);
            //}
        }

        public void ApplyStyle(Control control)
        {
            //Logger.Current.Trace("Start ApplyStyle");
         
            //Logger.Current.Trace("End ApplyStyle");
        }

       
        
    }
}
using System;
using System.Windows.Forms;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Framework
{
    internal sealed class UpdateUIHelper : Control
    {
        private IBarManager ibarManager_0 = null;
        internal ITool pCurrentTool = null;

        internal event OnUpdateUICompleteHandler OnUpdateUIComplete;

        public UpdateUIHelper(IBarManager ibarManager_1, ITool itool_0)
        {
            this.CreateHandle();
            base.CreateControl();
            this.ibarManager_0 = ibarManager_1;
            this.pCurrentTool = itool_0;
        }

        public void InvokeMethod()
        {
            try
            {
                if (!(base.IsDisposed || !base.IsHandleCreated))
                {
                    base.Invoke(new MessageHandler(this.UpdateUI));
                }
            }
            catch (Exception)
            {
            }
        }

        private void method_0()
        {
        }

        public void UpdateUI()
        {
            if (this.ibarManager_0 != null)
            {
                this.ibarManager_0.UpdateUI(this.pCurrentTool);
                if (this.OnUpdateUIComplete != null)
                {
                    this.OnUpdateUIComplete();
                }
            }
        }

        internal delegate void MessageHandler();

        internal delegate void OnUpdateUICompleteHandler();
    }
}
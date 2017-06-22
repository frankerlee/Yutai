using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class NewRelationClass_LabelAndNotification : UserControl
    {
        private Container container_0 = null;

        public NewRelationClass_LabelAndNotification()
        {
            this.InitializeComponent();
        }

 private void NewRelationClass_LabelAndNotification_Load(object sender, EventArgs e)
        {
            NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationNone;
            this.txtForwardLabel.Text = NewRelationClassHelper.forwardLabel;
            this.txtBackLabel.Text = NewRelationClassHelper.backwardLabel;
        }

        private void rdoNotificationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.rdoNotificationType.SelectedIndex)
            {
                case 0:
                    NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationForward;
                    break;

                case 1:
                    NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationBackward;
                    break;

                case 2:
                    NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationBoth;
                    break;

                case 3:
                    NewRelationClassHelper.Notification = esriRelNotification.esriRelNotificationNone;
                    break;
            }
        }

        private void txtBackLabel_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.backwardLabel = this.txtBackLabel.Text;
        }

        private void txtForwardLabel_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.forwardLabel = this.txtForwardLabel.Text;
        }
    }
}


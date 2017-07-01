using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using Yutai.Plugins.Mvp;
using Yutai.UI.Properties;

namespace Yutai.UI.Forms
{
    public partial class MainWindowView2 : RibbonForm, IViewInternal
    {
        public event Action OkClicked;

        public virtual ViewStyle Style
        {
            get
            {
                return new ViewStyle()
                {
                    Modal = true,
                    Sizable = false,
                };
            }
        }

        public virtual void BeforeClose()
        {
        }

        protected void Invoke(Action action)
        {
            if (action != null)
            {
                action();
            }
        }

        public virtual void ShowView(IWin32Window parent = null)
        {
            if (!Visible)
            {
                AppViewFactory.Instance.ShowChildView(this, parent);
            }
        }

        public virtual void UpdateView()
        {
        }

        public MainWindowView2()
        {
            InitializeComponent();
            Icon = Resources.MapWindow;
        }

        protected void FireOkClicked()
        {
            Invoke(OkClicked);
        }

        public void StartWait()
        {
            Cursor = Cursors.WaitCursor;

            EnableControls(false);

            Application.DoEvents();
        }

        public void StopWait()
        {
            Cursor = Cursors.Default;

            EnableControls(true);

            Application.DoEvents();
        }

        private void EnableControls(bool state)
        {
            foreach (Control ctrl in Controls)
            {
                ctrl.Enabled = state;
            }
        }

        public Form AsForm
        {
            get { return this; }
        }

        private void MainWindowView2_Load(object sender, EventArgs e)
        {
            // Fixing CORE-160
        }
    }

    public class MainWindowView2<TModel> : MainWindowView2, IViewInternal<TModel>
    {
        protected TModel _model;

        public void InitInternal(TModel model)
        {
            _model = model;
        }

        public TModel Model
        {
            get { return _model; }
        }
    }
}
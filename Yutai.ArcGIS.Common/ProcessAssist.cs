using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common
{
    public class ProcessAssist
    {
        protected System.Windows.Forms.Control m_bindcontrol = null;

        private OnSetMaxValueHandler onSetMaxValueHandler_0;

        private OnSetMessageHandler onSetMessageHandler_0;

        private OnReSetMessageHandler onReSetMessageHandler_0;

        private OnSetPostionHandler onSetPostionHandler_0;

        private OnSetAutoProcessHandler onSetAutoProcessHandler_0;

        private OnIncrementHandler onIncrementHandler_0;

        private OnResetHandler onResetHandler_0;

        public System.Windows.Forms.Form m_frmProgress;

        private int int_0 = 0;

        protected bool m_IsAutoClose = false;

        [System.Runtime.CompilerServices.CompilerGenerated]
        private string string_0;

        [System.Runtime.CompilerServices.CompilerGenerated]
        private bool bool_0;

        public event OnSetMaxValueHandler OnSetMaxValue
        {
            add
            {
                OnSetMaxValueHandler onSetMaxValueHandler = this.onSetMaxValueHandler_0;
                OnSetMaxValueHandler onSetMaxValueHandler2;
                do
                {
                    onSetMaxValueHandler2 = onSetMaxValueHandler;
                    OnSetMaxValueHandler value2 = (OnSetMaxValueHandler)System.Delegate.Combine(onSetMaxValueHandler2, value);
                    onSetMaxValueHandler = System.Threading.Interlocked.CompareExchange<OnSetMaxValueHandler>(ref this.onSetMaxValueHandler_0, value2, onSetMaxValueHandler2);
                }
                while (onSetMaxValueHandler != onSetMaxValueHandler2);
            }
            remove
            {
                OnSetMaxValueHandler onSetMaxValueHandler = this.onSetMaxValueHandler_0;
                OnSetMaxValueHandler onSetMaxValueHandler2;
                do
                {
                    onSetMaxValueHandler2 = onSetMaxValueHandler;
                    OnSetMaxValueHandler value2 = (OnSetMaxValueHandler)System.Delegate.Remove(onSetMaxValueHandler2, value);
                    onSetMaxValueHandler = System.Threading.Interlocked.CompareExchange<OnSetMaxValueHandler>(ref this.onSetMaxValueHandler_0, value2, onSetMaxValueHandler2);
                }
                while (onSetMaxValueHandler != onSetMaxValueHandler2);
            }
        }

        public event OnSetMessageHandler OnSetMessage
        {
            add
            {
                OnSetMessageHandler onSetMessageHandler = this.onSetMessageHandler_0;
                OnSetMessageHandler onSetMessageHandler2;
                do
                {
                    onSetMessageHandler2 = onSetMessageHandler;
                    OnSetMessageHandler value2 = (OnSetMessageHandler)System.Delegate.Combine(onSetMessageHandler2, value);
                    onSetMessageHandler = System.Threading.Interlocked.CompareExchange<OnSetMessageHandler>(ref this.onSetMessageHandler_0, value2, onSetMessageHandler2);
                }
                while (onSetMessageHandler != onSetMessageHandler2);
            }
            remove
            {
                OnSetMessageHandler onSetMessageHandler = this.onSetMessageHandler_0;
                OnSetMessageHandler onSetMessageHandler2;
                do
                {
                    onSetMessageHandler2 = onSetMessageHandler;
                    OnSetMessageHandler value2 = (OnSetMessageHandler)System.Delegate.Remove(onSetMessageHandler2, value);
                    onSetMessageHandler = System.Threading.Interlocked.CompareExchange<OnSetMessageHandler>(ref this.onSetMessageHandler_0, value2, onSetMessageHandler2);
                }
                while (onSetMessageHandler != onSetMessageHandler2);
            }
        }

        public event OnReSetMessageHandler OnReSetMessage
        {
            add
            {
                OnReSetMessageHandler onReSetMessageHandler = this.onReSetMessageHandler_0;
                OnReSetMessageHandler onReSetMessageHandler2;
                do
                {
                    onReSetMessageHandler2 = onReSetMessageHandler;
                    OnReSetMessageHandler value2 = (OnReSetMessageHandler)System.Delegate.Combine(onReSetMessageHandler2, value);
                    onReSetMessageHandler = System.Threading.Interlocked.CompareExchange<OnReSetMessageHandler>(ref this.onReSetMessageHandler_0, value2, onReSetMessageHandler2);
                }
                while (onReSetMessageHandler != onReSetMessageHandler2);
            }
            remove
            {
                OnReSetMessageHandler onReSetMessageHandler = this.onReSetMessageHandler_0;
                OnReSetMessageHandler onReSetMessageHandler2;
                do
                {
                    onReSetMessageHandler2 = onReSetMessageHandler;
                    OnReSetMessageHandler value2 = (OnReSetMessageHandler)System.Delegate.Remove(onReSetMessageHandler2, value);
                    onReSetMessageHandler = System.Threading.Interlocked.CompareExchange<OnReSetMessageHandler>(ref this.onReSetMessageHandler_0, value2, onReSetMessageHandler2);
                }
                while (onReSetMessageHandler != onReSetMessageHandler2);
            }
        }

        public event OnSetPostionHandler OnSetPostion
        {
            add
            {
                OnSetPostionHandler onSetPostionHandler = this.onSetPostionHandler_0;
                OnSetPostionHandler onSetPostionHandler2;
                do
                {
                    onSetPostionHandler2 = onSetPostionHandler;
                    OnSetPostionHandler value2 = (OnSetPostionHandler)System.Delegate.Combine(onSetPostionHandler2, value);
                    onSetPostionHandler = System.Threading.Interlocked.CompareExchange<OnSetPostionHandler>(ref this.onSetPostionHandler_0, value2, onSetPostionHandler2);
                }
                while (onSetPostionHandler != onSetPostionHandler2);
            }
            remove
            {
                OnSetPostionHandler onSetPostionHandler = this.onSetPostionHandler_0;
                OnSetPostionHandler onSetPostionHandler2;
                do
                {
                    onSetPostionHandler2 = onSetPostionHandler;
                    OnSetPostionHandler value2 = (OnSetPostionHandler)System.Delegate.Remove(onSetPostionHandler2, value);
                    onSetPostionHandler = System.Threading.Interlocked.CompareExchange<OnSetPostionHandler>(ref this.onSetPostionHandler_0, value2, onSetPostionHandler2);
                }
                while (onSetPostionHandler != onSetPostionHandler2);
            }
        }

        public event OnSetAutoProcessHandler OnSetAutoProcess
        {
            add
            {
                OnSetAutoProcessHandler onSetAutoProcessHandler = this.onSetAutoProcessHandler_0;
                OnSetAutoProcessHandler onSetAutoProcessHandler2;
                do
                {
                    onSetAutoProcessHandler2 = onSetAutoProcessHandler;
                    OnSetAutoProcessHandler value2 = (OnSetAutoProcessHandler)System.Delegate.Combine(onSetAutoProcessHandler2, value);
                    onSetAutoProcessHandler = System.Threading.Interlocked.CompareExchange<OnSetAutoProcessHandler>(ref this.onSetAutoProcessHandler_0, value2, onSetAutoProcessHandler2);
                }
                while (onSetAutoProcessHandler != onSetAutoProcessHandler2);
            }
            remove
            {
                OnSetAutoProcessHandler onSetAutoProcessHandler = this.onSetAutoProcessHandler_0;
                OnSetAutoProcessHandler onSetAutoProcessHandler2;
                do
                {
                    onSetAutoProcessHandler2 = onSetAutoProcessHandler;
                    OnSetAutoProcessHandler value2 = (OnSetAutoProcessHandler)System.Delegate.Remove(onSetAutoProcessHandler2, value);
                    onSetAutoProcessHandler = System.Threading.Interlocked.CompareExchange<OnSetAutoProcessHandler>(ref this.onSetAutoProcessHandler_0, value2, onSetAutoProcessHandler2);
                }
                while (onSetAutoProcessHandler != onSetAutoProcessHandler2);
            }
        }

        public event OnIncrementHandler OnIncrement
        {
            add
            {
                OnIncrementHandler onIncrementHandler = this.onIncrementHandler_0;
                OnIncrementHandler onIncrementHandler2;
                do
                {
                    onIncrementHandler2 = onIncrementHandler;
                    OnIncrementHandler value2 = (OnIncrementHandler)System.Delegate.Combine(onIncrementHandler2, value);
                    onIncrementHandler = System.Threading.Interlocked.CompareExchange<OnIncrementHandler>(ref this.onIncrementHandler_0, value2, onIncrementHandler2);
                }
                while (onIncrementHandler != onIncrementHandler2);
            }
            remove
            {
                OnIncrementHandler onIncrementHandler = this.onIncrementHandler_0;
                OnIncrementHandler onIncrementHandler2;
                do
                {
                    onIncrementHandler2 = onIncrementHandler;
                    OnIncrementHandler value2 = (OnIncrementHandler)System.Delegate.Remove(onIncrementHandler2, value);
                    onIncrementHandler = System.Threading.Interlocked.CompareExchange<OnIncrementHandler>(ref this.onIncrementHandler_0, value2, onIncrementHandler2);
                }
                while (onIncrementHandler != onIncrementHandler2);
            }
        }

        public event OnResetHandler OnReset
        {
            add
            {
                OnResetHandler onResetHandler = this.onResetHandler_0;
                OnResetHandler onResetHandler2;
                do
                {
                    onResetHandler2 = onResetHandler;
                    OnResetHandler value2 = (OnResetHandler)System.Delegate.Combine(onResetHandler2, value);
                    onResetHandler = System.Threading.Interlocked.CompareExchange<OnResetHandler>(ref this.onResetHandler_0, value2, onResetHandler2);
                }
                while (onResetHandler != onResetHandler2);
            }
            remove
            {
                OnResetHandler onResetHandler = this.onResetHandler_0;
                OnResetHandler onResetHandler2;
                do
                {
                    onResetHandler2 = onResetHandler;
                    OnResetHandler value2 = (OnResetHandler)System.Delegate.Remove(onResetHandler2, value);
                    onResetHandler = System.Threading.Interlocked.CompareExchange<OnResetHandler>(ref this.onResetHandler_0, value2, onResetHandler2);
                }
                while (onResetHandler != onResetHandler2);
            }
        }

        public string Text
        {
            get;
            set;
        }

        public bool IsSuccess
        {
            get;
            set;
        }

        public ProcessAssist(System.Windows.Forms.Control control_0)
        {
            this.m_bindcontrol = control_0;
        }

        public void Reset(bool bool_1)
        {
            if (this.onResetHandler_0 != null)
            {
                this.onResetHandler_0(bool_1);
            }
        }

        public void SetMaxValue(int int_1)
        {
            if (this.onSetMaxValueHandler_0 != null)
            {
                this.onSetMaxValueHandler_0(int_1);
            }
        }

        public void ReSetMessage(string string_1)
        {
            if (this.onReSetMessageHandler_0 != null)
            {
                string text = this.method_0(string_1);
                this.onReSetMessageHandler_0(text);
            }
        }

        private string method_0(string string_1)
        {
            string text = string_1;
            for (int i = 0; i < this.int_0; i++)
            {
                text = "  " + text;
            }
            return text;
        }

        public void SetMessage(string string_1)
        {
            if (this.onSetMessageHandler_0 != null)
            {
                string text = this.method_0(string_1);
                this.onSetMessageHandler_0(text);
            }
        }

        public void SetPostion(int int_1)
        {
            if (this.onSetPostionHandler_0 != null)
            {
                this.onSetPostionHandler_0(int_1);
            }
        }

        public void SetAutoProcess()
        {
            if (this.onSetAutoProcessHandler_0 != null)
            {
                this.onSetAutoProcessHandler_0();
            }
        }

        public void Increment(int int_1)
        {
            if (this.onIncrementHandler_0 != null)
            {
                this.onIncrementHandler_0(int_1);
            }
        }

        public virtual void InitProgress()
        {
            this.m_IsAutoClose = true;
            this.m_frmProgress = new frmProgress2();
            if (this.Text != null)
            {
                this.m_frmProgress.Text = this.Text;
            }
            (this.m_frmProgress as frmProgress2).ProcessAssist = this;
        }

        public virtual void InitProgress2()
        {
        }

        public void AddIndet()
        {
            if (this.int_0 != 10)
            {
                this.int_0++;
            }
        }

        public void SubIndet()
        {
            if (this.int_0 != 0)
            {
                this.int_0--;
            }
        }

        private void method_1(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (this.m_bindcontrol != null)
            {
                if (this.IsSuccess)
                {
                    (this.m_bindcontrol as System.Windows.Forms.Form).Close();
                }
                else
                {
                    this.m_bindcontrol.Visible = true;
                }
            }
        }

        public ProcessAssist()
        {
        }

        public void Start(System.Windows.Forms.IWin32Window iwin32Window_0)
        {
            this.m_frmProgress.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.m_frmProgress.Show(iwin32Window_0);
            if (this.m_bindcontrol != null)
            {
                this.m_bindcontrol.Visible = false;
                this.m_bindcontrol.Enabled = false;
            }
        }

        public void Start()
        {
            this.m_frmProgress.ShowInTaskbar = false;
            this.m_frmProgress.TopLevel = true;
            if (this.m_bindcontrol != null)
            {
                this.m_frmProgress.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.m_frmProgress.Show();
            }
            else
            {
                this.m_frmProgress.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.m_frmProgress.Show();
            }
            if (this.m_bindcontrol != null)
            {
                this.m_bindcontrol.Visible = false;
            }
        }

        public void End()
        {
            if (this.m_bindcontrol != null)
            {
                this.m_bindcontrol.Visible = true;
                this.m_bindcontrol.Enabled = true;
            }
            if (this.m_IsAutoClose)
            {
                this.m_frmProgress.Close();
            }
        }
    }




}

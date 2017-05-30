using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yutai.Shared
{
    public delegate void OnIncrementHandler(int increment);
    public delegate void OnReSetMessageHandler(string msg);
    public delegate void OnResetHandler(bool boolReset);
    public delegate void OnSetAutoProcessHandler();
    public delegate void OnSetMaxValueHandler(int maxValue);
    public delegate void OnSetMessageHandler(string msg);
    public delegate void OnSetPostionHandler(int pos);

    public class ProcessAssist { 

    protected Control m_bindcontrol = null;

    private OnSetMaxValueHandler onSetMaxValueHandler_0;

    private OnSetMessageHandler onSetMessageHandler_0;

    private OnReSetMessageHandler onReSetMessageHandler_0;

    private OnSetPostionHandler onSetPostionHandler_0;

    private OnSetAutoProcessHandler onSetAutoProcessHandler_0;

    private OnIncrementHandler onIncrementHandler_0;

    private OnResetHandler onResetHandler_0;

    public Form m_frmProgress;

    private int int_0 = 0;

    protected bool m_IsAutoClose = false;

    public bool IsSuccess
    {
        get;
        set;
    }

    public string Text
    {
        get;
        set;
    }

    public ProcessAssist(Control control_0)
    {
        this.m_bindcontrol = control_0;
    }

    public ProcessAssist()
    {
    }

    public void AddIndet()
    {
        if (this.int_0 != 10)
        {
            ProcessAssist int0 = this;
            int0.int_0 = int0.int_0 + 1;
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

    private string method_0(string string_1)
    {
        string string1 = string_1;
        for (int i = 0; i < this.int_0; i++)
        {
            string1 = string.Concat("  ", string1);
        }
        return string1;
    }

    private void method_1(object sender, FormClosedEventArgs e)
    {
        if (this.m_bindcontrol != null)
        {
            if (!this.IsSuccess)
            {
                this.m_bindcontrol.Visible = true;
            }
            else
            {
                (this.m_bindcontrol as Form).Close();
            }
        }
    }

    public void Reset(bool bool_1)
    {
        if (this.onResetHandler_0 != null)
        {
            this.onResetHandler_0(bool_1);
        }
    }

    public void ReSetMessage(string string_1)
    {
        if (this.onReSetMessageHandler_0 != null)
        {
            string str = this.method_0(string_1);
            this.onReSetMessageHandler_0(str);
        }
    }

    public void SetAutoProcess()
    {
        if (this.onSetAutoProcessHandler_0 != null)
        {
            this.onSetAutoProcessHandler_0();
        }
    }

    public void SetMaxValue(int int_1)
    {
        if (this.onSetMaxValueHandler_0 != null)
        {
            this.onSetMaxValueHandler_0(int_1);
        }
    }

    public void SetMessage(string string_1)
    {
        if (this.onSetMessageHandler_0 != null)
        {
            string str = this.method_0(string_1);
            this.onSetMessageHandler_0(str);
        }
    }

    public void SetPostion(int int_1)
    {
        if (this.onSetPostionHandler_0 != null)
        {
            this.onSetPostionHandler_0(int_1);
        }
    }

    public void Start(IWin32Window iwin32Window_0)
    {
        this.m_frmProgress.StartPosition = FormStartPosition.CenterScreen;
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
        if (this.m_bindcontrol == null)
        {
            this.m_frmProgress.StartPosition = FormStartPosition.CenterScreen;
            this.m_frmProgress.Show();
        }
        else
        {
            this.m_frmProgress.StartPosition = FormStartPosition.CenterScreen;
            this.m_frmProgress.Show();
        }
        if (this.m_bindcontrol != null)
        {
            this.m_bindcontrol.Visible = false;
        }
    }

    public void SubIndet()
    {
        if (this.int_0 != 0)
        {
            ProcessAssist int0 = this;
            int0.int_0 = int0.int_0 - 1;
        }
    }

    public event OnIncrementHandler OnIncrement
    {
        add
        {
            OnIncrementHandler onIncrementHandler;
            OnIncrementHandler onIncrementHandler0 = this.onIncrementHandler_0;
            do
            {
                onIncrementHandler = onIncrementHandler0;
                OnIncrementHandler onIncrementHandler1 = (OnIncrementHandler)Delegate.Combine(onIncrementHandler, value);
                onIncrementHandler0 = Interlocked.CompareExchange<OnIncrementHandler>(ref this.onIncrementHandler_0, onIncrementHandler1, onIncrementHandler);
            }
            while ((object)onIncrementHandler0 != (object)onIncrementHandler);
        }
        remove
        {
            OnIncrementHandler onIncrementHandler;
            OnIncrementHandler onIncrementHandler0 = this.onIncrementHandler_0;
            do
            {
                onIncrementHandler = onIncrementHandler0;
                OnIncrementHandler onIncrementHandler1 = (OnIncrementHandler)Delegate.Remove(onIncrementHandler, value);
                onIncrementHandler0 = Interlocked.CompareExchange<OnIncrementHandler>(ref this.onIncrementHandler_0, onIncrementHandler1, onIncrementHandler);
            }
            while ((object)onIncrementHandler0 != (object)onIncrementHandler);
        }
    }

    public event OnResetHandler OnReset
    {
        add
        {
            OnResetHandler onResetHandler;
            OnResetHandler onResetHandler0 = this.onResetHandler_0;
            do
            {
                onResetHandler = onResetHandler0;
                OnResetHandler onResetHandler1 = (OnResetHandler)Delegate.Combine(onResetHandler, value);
                onResetHandler0 = Interlocked.CompareExchange<OnResetHandler>(ref this.onResetHandler_0, onResetHandler1, onResetHandler);
            }
            while ((object)onResetHandler0 != (object)onResetHandler);
        }
        remove
        {
            OnResetHandler onResetHandler;
            OnResetHandler onResetHandler0 = this.onResetHandler_0;
            do
            {
                onResetHandler = onResetHandler0;
                OnResetHandler onResetHandler1 = (OnResetHandler)Delegate.Remove(onResetHandler, value);
                onResetHandler0 = Interlocked.CompareExchange<OnResetHandler>(ref this.onResetHandler_0, onResetHandler1, onResetHandler);
            }
            while ((object)onResetHandler0 != (object)onResetHandler);
        }
    }

    public event OnReSetMessageHandler OnReSetMessage
    {
        add
        {
            OnReSetMessageHandler onReSetMessageHandler;
            OnReSetMessageHandler onReSetMessageHandler0 = this.onReSetMessageHandler_0;
            do
            {
                onReSetMessageHandler = onReSetMessageHandler0;
                OnReSetMessageHandler onReSetMessageHandler1 = (OnReSetMessageHandler)Delegate.Combine(onReSetMessageHandler, value);
                onReSetMessageHandler0 = Interlocked.CompareExchange<OnReSetMessageHandler>(ref this.onReSetMessageHandler_0, onReSetMessageHandler1, onReSetMessageHandler);
            }
            while ((object)onReSetMessageHandler0 != (object)onReSetMessageHandler);
        }
        remove
        {
            OnReSetMessageHandler onReSetMessageHandler;
            OnReSetMessageHandler onReSetMessageHandler0 = this.onReSetMessageHandler_0;
            do
            {
                onReSetMessageHandler = onReSetMessageHandler0;
                OnReSetMessageHandler onReSetMessageHandler1 = (OnReSetMessageHandler)Delegate.Remove(onReSetMessageHandler, value);
                onReSetMessageHandler0 = Interlocked.CompareExchange<OnReSetMessageHandler>(ref this.onReSetMessageHandler_0, onReSetMessageHandler1, onReSetMessageHandler);
            }
            while ((object)onReSetMessageHandler0 != (object)onReSetMessageHandler);
        }
    }

    public event OnSetAutoProcessHandler OnSetAutoProcess
    {
        add
        {
            OnSetAutoProcessHandler onSetAutoProcessHandler;
            OnSetAutoProcessHandler onSetAutoProcessHandler0 = this.onSetAutoProcessHandler_0;
            do
            {
                onSetAutoProcessHandler = onSetAutoProcessHandler0;
                OnSetAutoProcessHandler onSetAutoProcessHandler1 = (OnSetAutoProcessHandler)Delegate.Combine(onSetAutoProcessHandler, value);
                onSetAutoProcessHandler0 = Interlocked.CompareExchange<OnSetAutoProcessHandler>(ref this.onSetAutoProcessHandler_0, onSetAutoProcessHandler1, onSetAutoProcessHandler);
            }
            while ((object)onSetAutoProcessHandler0 != (object)onSetAutoProcessHandler);
        }
        remove
        {
            OnSetAutoProcessHandler onSetAutoProcessHandler;
            OnSetAutoProcessHandler onSetAutoProcessHandler0 = this.onSetAutoProcessHandler_0;
            do
            {
                onSetAutoProcessHandler = onSetAutoProcessHandler0;
                OnSetAutoProcessHandler onSetAutoProcessHandler1 = (OnSetAutoProcessHandler)Delegate.Remove(onSetAutoProcessHandler, value);
                onSetAutoProcessHandler0 = Interlocked.CompareExchange<OnSetAutoProcessHandler>(ref this.onSetAutoProcessHandler_0, onSetAutoProcessHandler1, onSetAutoProcessHandler);
            }
            while ((object)onSetAutoProcessHandler0 != (object)onSetAutoProcessHandler);
        }
    }

    public event OnSetMaxValueHandler OnSetMaxValue
    {
        add
        {
            OnSetMaxValueHandler onSetMaxValueHandler;
            OnSetMaxValueHandler onSetMaxValueHandler0 = this.onSetMaxValueHandler_0;
            do
            {
                onSetMaxValueHandler = onSetMaxValueHandler0;
                OnSetMaxValueHandler onSetMaxValueHandler1 = (OnSetMaxValueHandler)Delegate.Combine(onSetMaxValueHandler, value);
                onSetMaxValueHandler0 = Interlocked.CompareExchange<OnSetMaxValueHandler>(ref this.onSetMaxValueHandler_0, onSetMaxValueHandler1, onSetMaxValueHandler);
            }
            while ((object)onSetMaxValueHandler0 != (object)onSetMaxValueHandler);
        }
        remove
        {
            OnSetMaxValueHandler onSetMaxValueHandler;
            OnSetMaxValueHandler onSetMaxValueHandler0 = this.onSetMaxValueHandler_0;
            do
            {
                onSetMaxValueHandler = onSetMaxValueHandler0;
                OnSetMaxValueHandler onSetMaxValueHandler1 = (OnSetMaxValueHandler)Delegate.Remove(onSetMaxValueHandler, value);
                onSetMaxValueHandler0 = Interlocked.CompareExchange<OnSetMaxValueHandler>(ref this.onSetMaxValueHandler_0, onSetMaxValueHandler1, onSetMaxValueHandler);
            }
            while ((object)onSetMaxValueHandler0 != (object)onSetMaxValueHandler);
        }
    }

    public event OnSetMessageHandler OnSetMessage
    {
        add
        {
            OnSetMessageHandler onSetMessageHandler;
            OnSetMessageHandler onSetMessageHandler0 = this.onSetMessageHandler_0;
            do
            {
                onSetMessageHandler = onSetMessageHandler0;
                OnSetMessageHandler onSetMessageHandler1 = (OnSetMessageHandler)Delegate.Combine(onSetMessageHandler, value);
                onSetMessageHandler0 = Interlocked.CompareExchange<OnSetMessageHandler>(ref this.onSetMessageHandler_0, onSetMessageHandler1, onSetMessageHandler);
            }
            while ((object)onSetMessageHandler0 != (object)onSetMessageHandler);
        }
        remove
        {
            OnSetMessageHandler onSetMessageHandler;
            OnSetMessageHandler onSetMessageHandler0 = this.onSetMessageHandler_0;
            do
            {
                onSetMessageHandler = onSetMessageHandler0;
                OnSetMessageHandler onSetMessageHandler1 = (OnSetMessageHandler)Delegate.Remove(onSetMessageHandler, value);
                onSetMessageHandler0 = Interlocked.CompareExchange<OnSetMessageHandler>(ref this.onSetMessageHandler_0, onSetMessageHandler1, onSetMessageHandler);
            }
            while ((object)onSetMessageHandler0 != (object)onSetMessageHandler);
        }
    }

    public event OnSetPostionHandler OnSetPostion
    {
        add
        {
            OnSetPostionHandler onSetPostionHandler;
            OnSetPostionHandler onSetPostionHandler0 = this.onSetPostionHandler_0;
            do
            {
                onSetPostionHandler = onSetPostionHandler0;
                OnSetPostionHandler onSetPostionHandler1 = (OnSetPostionHandler)Delegate.Combine(onSetPostionHandler, value);
                onSetPostionHandler0 = Interlocked.CompareExchange<OnSetPostionHandler>(ref this.onSetPostionHandler_0, onSetPostionHandler1, onSetPostionHandler);
            }
            while ((object)onSetPostionHandler0 != (object)onSetPostionHandler);
        }
        remove
        {
            OnSetPostionHandler onSetPostionHandler;
            OnSetPostionHandler onSetPostionHandler0 = this.onSetPostionHandler_0;
            do
            {
                onSetPostionHandler = onSetPostionHandler0;
                OnSetPostionHandler onSetPostionHandler1 = (OnSetPostionHandler)Delegate.Remove(onSetPostionHandler, value);
                onSetPostionHandler0 = Interlocked.CompareExchange<OnSetPostionHandler>(ref this.onSetPostionHandler_0, onSetPostionHandler1, onSetPostionHandler);
            }
            while ((object)onSetPostionHandler0 != (object)onSetPostionHandler);
        }
    }
}

   
}

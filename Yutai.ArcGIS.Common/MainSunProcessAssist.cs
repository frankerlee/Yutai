using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common
{
    public class MainSunProcessAssist : ProcessAssist
    {
        public delegate void OnSubIncrementHandler(int int_0);

        public delegate void OnSetSubMaxValueHandler(int int_0);

        public delegate void OnSetSubMessageHandler(string string_0);

        public delegate void OnSetSubPostionHandler(int int_0);

        public delegate void OnResetSubInfoHandler();

        public delegate void OnSetSubAutoProcessHandler();

        private MainSunProcessAssist.OnSetSubMaxValueHandler onSetSubMaxValueHandler_0;

        private MainSunProcessAssist.OnSetSubMessageHandler onSetSubMessageHandler_0;

        private MainSunProcessAssist.OnResetSubInfoHandler onResetSubInfoHandler_0;

        private MainSunProcessAssist.OnSetSubPostionHandler onSetSubPostionHandler_0;

        private MainSunProcessAssist.OnSubIncrementHandler onSubIncrementHandler_0;

        public event MainSunProcessAssist.OnSetSubMaxValueHandler OnSetSubMaxValue
        {
            add
            {
                MainSunProcessAssist.OnSetSubMaxValueHandler onSetSubMaxValueHandler = this.onSetSubMaxValueHandler_0;
                MainSunProcessAssist.OnSetSubMaxValueHandler onSetSubMaxValueHandler2;
                do
                {
                    onSetSubMaxValueHandler2 = onSetSubMaxValueHandler;
                    MainSunProcessAssist.OnSetSubMaxValueHandler value2 =
                        (MainSunProcessAssist.OnSetSubMaxValueHandler)
                        System.Delegate.Combine(onSetSubMaxValueHandler2, value);
                    onSetSubMaxValueHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnSetSubMaxValueHandler>(
                            ref this.onSetSubMaxValueHandler_0, value2, onSetSubMaxValueHandler2);
                } while (onSetSubMaxValueHandler != onSetSubMaxValueHandler2);
            }
            remove
            {
                MainSunProcessAssist.OnSetSubMaxValueHandler onSetSubMaxValueHandler = this.onSetSubMaxValueHandler_0;
                MainSunProcessAssist.OnSetSubMaxValueHandler onSetSubMaxValueHandler2;
                do
                {
                    onSetSubMaxValueHandler2 = onSetSubMaxValueHandler;
                    MainSunProcessAssist.OnSetSubMaxValueHandler value2 =
                        (MainSunProcessAssist.OnSetSubMaxValueHandler)
                        System.Delegate.Remove(onSetSubMaxValueHandler2, value);
                    onSetSubMaxValueHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnSetSubMaxValueHandler>(
                            ref this.onSetSubMaxValueHandler_0, value2, onSetSubMaxValueHandler2);
                } while (onSetSubMaxValueHandler != onSetSubMaxValueHandler2);
            }
        }

        public event MainSunProcessAssist.OnSetSubMessageHandler OnSetSubMessage
        {
            add
            {
                MainSunProcessAssist.OnSetSubMessageHandler onSetSubMessageHandler = this.onSetSubMessageHandler_0;
                MainSunProcessAssist.OnSetSubMessageHandler onSetSubMessageHandler2;
                do
                {
                    onSetSubMessageHandler2 = onSetSubMessageHandler;
                    MainSunProcessAssist.OnSetSubMessageHandler value2 =
                        (MainSunProcessAssist.OnSetSubMessageHandler)
                        System.Delegate.Combine(onSetSubMessageHandler2, value);
                    onSetSubMessageHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnSetSubMessageHandler>(
                            ref this.onSetSubMessageHandler_0, value2, onSetSubMessageHandler2);
                } while (onSetSubMessageHandler != onSetSubMessageHandler2);
            }
            remove
            {
                MainSunProcessAssist.OnSetSubMessageHandler onSetSubMessageHandler = this.onSetSubMessageHandler_0;
                MainSunProcessAssist.OnSetSubMessageHandler onSetSubMessageHandler2;
                do
                {
                    onSetSubMessageHandler2 = onSetSubMessageHandler;
                    MainSunProcessAssist.OnSetSubMessageHandler value2 =
                        (MainSunProcessAssist.OnSetSubMessageHandler)
                        System.Delegate.Remove(onSetSubMessageHandler2, value);
                    onSetSubMessageHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnSetSubMessageHandler>(
                            ref this.onSetSubMessageHandler_0, value2, onSetSubMessageHandler2);
                } while (onSetSubMessageHandler != onSetSubMessageHandler2);
            }
        }

        public event MainSunProcessAssist.OnResetSubInfoHandler OnResetSubInfo
        {
            add
            {
                MainSunProcessAssist.OnResetSubInfoHandler onResetSubInfoHandler = this.onResetSubInfoHandler_0;
                MainSunProcessAssist.OnResetSubInfoHandler onResetSubInfoHandler2;
                do
                {
                    onResetSubInfoHandler2 = onResetSubInfoHandler;
                    MainSunProcessAssist.OnResetSubInfoHandler value2 =
                        (MainSunProcessAssist.OnResetSubInfoHandler)
                        System.Delegate.Combine(onResetSubInfoHandler2, value);
                    onResetSubInfoHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnResetSubInfoHandler>(
                            ref this.onResetSubInfoHandler_0, value2, onResetSubInfoHandler2);
                } while (onResetSubInfoHandler != onResetSubInfoHandler2);
            }
            remove
            {
                MainSunProcessAssist.OnResetSubInfoHandler onResetSubInfoHandler = this.onResetSubInfoHandler_0;
                MainSunProcessAssist.OnResetSubInfoHandler onResetSubInfoHandler2;
                do
                {
                    onResetSubInfoHandler2 = onResetSubInfoHandler;
                    MainSunProcessAssist.OnResetSubInfoHandler value2 =
                        (MainSunProcessAssist.OnResetSubInfoHandler)
                        System.Delegate.Remove(onResetSubInfoHandler2, value);
                    onResetSubInfoHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnResetSubInfoHandler>(
                            ref this.onResetSubInfoHandler_0, value2, onResetSubInfoHandler2);
                } while (onResetSubInfoHandler != onResetSubInfoHandler2);
            }
        }

        public event MainSunProcessAssist.OnSetSubPostionHandler OnSetSubPostion
        {
            add
            {
                MainSunProcessAssist.OnSetSubPostionHandler onSetSubPostionHandler = this.onSetSubPostionHandler_0;
                MainSunProcessAssist.OnSetSubPostionHandler onSetSubPostionHandler2;
                do
                {
                    onSetSubPostionHandler2 = onSetSubPostionHandler;
                    MainSunProcessAssist.OnSetSubPostionHandler value2 =
                        (MainSunProcessAssist.OnSetSubPostionHandler)
                        System.Delegate.Combine(onSetSubPostionHandler2, value);
                    onSetSubPostionHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnSetSubPostionHandler>(
                            ref this.onSetSubPostionHandler_0, value2, onSetSubPostionHandler2);
                } while (onSetSubPostionHandler != onSetSubPostionHandler2);
            }
            remove
            {
                MainSunProcessAssist.OnSetSubPostionHandler onSetSubPostionHandler = this.onSetSubPostionHandler_0;
                MainSunProcessAssist.OnSetSubPostionHandler onSetSubPostionHandler2;
                do
                {
                    onSetSubPostionHandler2 = onSetSubPostionHandler;
                    MainSunProcessAssist.OnSetSubPostionHandler value2 =
                        (MainSunProcessAssist.OnSetSubPostionHandler)
                        System.Delegate.Remove(onSetSubPostionHandler2, value);
                    onSetSubPostionHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnSetSubPostionHandler>(
                            ref this.onSetSubPostionHandler_0, value2, onSetSubPostionHandler2);
                } while (onSetSubPostionHandler != onSetSubPostionHandler2);
            }
        }

        public event MainSunProcessAssist.OnSubIncrementHandler OnSubIncrement
        {
            add
            {
                MainSunProcessAssist.OnSubIncrementHandler onSubIncrementHandler = this.onSubIncrementHandler_0;
                MainSunProcessAssist.OnSubIncrementHandler onSubIncrementHandler2;
                do
                {
                    onSubIncrementHandler2 = onSubIncrementHandler;
                    MainSunProcessAssist.OnSubIncrementHandler value2 =
                        (MainSunProcessAssist.OnSubIncrementHandler)
                        System.Delegate.Combine(onSubIncrementHandler2, value);
                    onSubIncrementHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnSubIncrementHandler>(
                            ref this.onSubIncrementHandler_0, value2, onSubIncrementHandler2);
                } while (onSubIncrementHandler != onSubIncrementHandler2);
            }
            remove
            {
                MainSunProcessAssist.OnSubIncrementHandler onSubIncrementHandler = this.onSubIncrementHandler_0;
                MainSunProcessAssist.OnSubIncrementHandler onSubIncrementHandler2;
                do
                {
                    onSubIncrementHandler2 = onSubIncrementHandler;
                    MainSunProcessAssist.OnSubIncrementHandler value2 =
                        (MainSunProcessAssist.OnSubIncrementHandler)
                        System.Delegate.Remove(onSubIncrementHandler2, value);
                    onSubIncrementHandler =
                        System.Threading.Interlocked.CompareExchange<MainSunProcessAssist.OnSubIncrementHandler>(
                            ref this.onSubIncrementHandler_0, value2, onSubIncrementHandler2);
                } while (onSubIncrementHandler != onSubIncrementHandler2);
            }
        }

        public void ResetSubInfo()
        {
            if (this.onResetSubInfoHandler_0 != null)
            {
                this.onResetSubInfoHandler_0();
            }
        }

        public override void InitProgress()
        {
            this.m_frmProgress = new frmMainSubProgress();
            this.m_frmProgress.TopMost = true;
            (this.m_frmProgress as frmMainSubProgress).ProcessAssist = this;
            this.m_IsAutoClose = true;
        }

        public void SetSubMaxValue(int int_1)
        {
            if (this.onSetSubMaxValueHandler_0 != null)
            {
                this.onSetSubMaxValueHandler_0(int_1);
            }
        }

        public void SetSubMessage(string string_1)
        {
            if (this.onSetSubMessageHandler_0 != null)
            {
                this.onSetSubMessageHandler_0(string_1);
            }
        }

        public void SetSubPostion(int int_1)
        {
            if (this.onSetSubPostionHandler_0 != null)
            {
                this.onSetSubPostionHandler_0(int_1);
            }
        }

        public void SubIncrement(int int_1)
        {
            if (this.onSubIncrementHandler_0 != null)
            {
                this.onSubIncrementHandler_0(int_1);
            }
        }
    }
}
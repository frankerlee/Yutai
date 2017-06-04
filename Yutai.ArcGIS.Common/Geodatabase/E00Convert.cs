using System;
using System.Collections;
using System.Threading;
using ESRI.ArcGIS.CoverageTools;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class E00Convert : IConvertEvent, IFeatureProgress_Event
	{
		private IFeatureProgress_StepEventHandler ifeatureProgress_StepEventHandler_0;

		private SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler_0;

		private SetFeatureCountEnventHandler setFeatureCountEnventHandler_0;

		private IList ilist_0 = null;

		private SetMaxValueHandler setMaxValueHandler_0;

		private SetMinValueHandler setMinValueHandler_0;

		private SetPositionHandler setPositionHandler_0;

		private SetMessageHandler setMessageHandler_0;

		private FinishHander finishHander_0;

		public E00Convert()
		{
		}

		public void Convert()
		{
			Export export = new Export();
		}

		public event FinishHander FinishEvent
		{
			add
			{
				FinishHander finishHander;
				FinishHander finishHander0 = this.finishHander_0;
				do
				{
					finishHander = finishHander0;
					FinishHander finishHander1 = (FinishHander)Delegate.Combine(finishHander, value);
					finishHander0 = Interlocked.CompareExchange<FinishHander>(ref this.finishHander_0, finishHander1, finishHander);
				}
				while ((object)finishHander0 != (object)finishHander);
			}
			remove
			{
				FinishHander finishHander;
				FinishHander finishHander0 = this.finishHander_0;
				do
				{
					finishHander = finishHander0;
					FinishHander finishHander1 = (FinishHander)Delegate.Remove(finishHander, value);
					finishHander0 = Interlocked.CompareExchange<FinishHander>(ref this.finishHander_0, finishHander1, finishHander);
				}
				while ((object)finishHander0 != (object)finishHander);
			}
		}

		public event SetFeatureClassNameEnventHandler SetFeatureClassNameEnvent
		{
			add
			{
				SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler;
				SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler0 = this.setFeatureClassNameEnventHandler_0;
				do
				{
					setFeatureClassNameEnventHandler = setFeatureClassNameEnventHandler0;
					SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler1 = (SetFeatureClassNameEnventHandler)Delegate.Combine(setFeatureClassNameEnventHandler, value);
					setFeatureClassNameEnventHandler0 = Interlocked.CompareExchange<SetFeatureClassNameEnventHandler>(ref this.setFeatureClassNameEnventHandler_0, setFeatureClassNameEnventHandler1, setFeatureClassNameEnventHandler);
				}
				while ((object)setFeatureClassNameEnventHandler0 != (object)setFeatureClassNameEnventHandler);
			}
			remove
			{
				SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler;
				SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler0 = this.setFeatureClassNameEnventHandler_0;
				do
				{
					setFeatureClassNameEnventHandler = setFeatureClassNameEnventHandler0;
					SetFeatureClassNameEnventHandler setFeatureClassNameEnventHandler1 = (SetFeatureClassNameEnventHandler)Delegate.Remove(setFeatureClassNameEnventHandler, value);
					setFeatureClassNameEnventHandler0 = Interlocked.CompareExchange<SetFeatureClassNameEnventHandler>(ref this.setFeatureClassNameEnventHandler_0, setFeatureClassNameEnventHandler1, setFeatureClassNameEnventHandler);
				}
				while ((object)setFeatureClassNameEnventHandler0 != (object)setFeatureClassNameEnventHandler);
			}
		}

		public event SetFeatureCountEnventHandler SetFeatureCountEnvent
		{
			add
			{
				SetFeatureCountEnventHandler setFeatureCountEnventHandler;
				SetFeatureCountEnventHandler setFeatureCountEnventHandler0 = this.setFeatureCountEnventHandler_0;
				do
				{
					setFeatureCountEnventHandler = setFeatureCountEnventHandler0;
					SetFeatureCountEnventHandler setFeatureCountEnventHandler1 = (SetFeatureCountEnventHandler)Delegate.Combine(setFeatureCountEnventHandler, value);
					setFeatureCountEnventHandler0 = Interlocked.CompareExchange<SetFeatureCountEnventHandler>(ref this.setFeatureCountEnventHandler_0, setFeatureCountEnventHandler1, setFeatureCountEnventHandler);
				}
				while ((object)setFeatureCountEnventHandler0 != (object)setFeatureCountEnventHandler);
			}
			remove
			{
				SetFeatureCountEnventHandler setFeatureCountEnventHandler;
				SetFeatureCountEnventHandler setFeatureCountEnventHandler0 = this.setFeatureCountEnventHandler_0;
				do
				{
					setFeatureCountEnventHandler = setFeatureCountEnventHandler0;
					SetFeatureCountEnventHandler setFeatureCountEnventHandler1 = (SetFeatureCountEnventHandler)Delegate.Remove(setFeatureCountEnventHandler, value);
					setFeatureCountEnventHandler0 = Interlocked.CompareExchange<SetFeatureCountEnventHandler>(ref this.setFeatureCountEnventHandler_0, setFeatureCountEnventHandler1, setFeatureCountEnventHandler);
				}
				while ((object)setFeatureCountEnventHandler0 != (object)setFeatureCountEnventHandler);
			}
		}

		public event SetMaxValueHandler SetMaxValueEvent
		{
			add
			{
				SetMaxValueHandler setMaxValueHandler;
				SetMaxValueHandler setMaxValueHandler0 = this.setMaxValueHandler_0;
				do
				{
					setMaxValueHandler = setMaxValueHandler0;
					SetMaxValueHandler setMaxValueHandler1 = (SetMaxValueHandler)Delegate.Combine(setMaxValueHandler, value);
					setMaxValueHandler0 = Interlocked.CompareExchange<SetMaxValueHandler>(ref this.setMaxValueHandler_0, setMaxValueHandler1, setMaxValueHandler);
				}
				while ((object)setMaxValueHandler0 != (object)setMaxValueHandler);
			}
			remove
			{
				SetMaxValueHandler setMaxValueHandler;
				SetMaxValueHandler setMaxValueHandler0 = this.setMaxValueHandler_0;
				do
				{
					setMaxValueHandler = setMaxValueHandler0;
					SetMaxValueHandler setMaxValueHandler1 = (SetMaxValueHandler)Delegate.Remove(setMaxValueHandler, value);
					setMaxValueHandler0 = Interlocked.CompareExchange<SetMaxValueHandler>(ref this.setMaxValueHandler_0, setMaxValueHandler1, setMaxValueHandler);
				}
				while ((object)setMaxValueHandler0 != (object)setMaxValueHandler);
			}
		}

		public event SetMessageHandler SetMessageEvent
		{
			add
			{
				SetMessageHandler setMessageHandler;
				SetMessageHandler setMessageHandler0 = this.setMessageHandler_0;
				do
				{
					setMessageHandler = setMessageHandler0;
					SetMessageHandler setMessageHandler1 = (SetMessageHandler)Delegate.Combine(setMessageHandler, value);
					setMessageHandler0 = Interlocked.CompareExchange<SetMessageHandler>(ref this.setMessageHandler_0, setMessageHandler1, setMessageHandler);
				}
				while ((object)setMessageHandler0 != (object)setMessageHandler);
			}
			remove
			{
				SetMessageHandler setMessageHandler;
				SetMessageHandler setMessageHandler0 = this.setMessageHandler_0;
				do
				{
					setMessageHandler = setMessageHandler0;
					SetMessageHandler setMessageHandler1 = (SetMessageHandler)Delegate.Remove(setMessageHandler, value);
					setMessageHandler0 = Interlocked.CompareExchange<SetMessageHandler>(ref this.setMessageHandler_0, setMessageHandler1, setMessageHandler);
				}
				while ((object)setMessageHandler0 != (object)setMessageHandler);
			}
		}

		public event SetMinValueHandler SetMinValueEvent
		{
			add
			{
				SetMinValueHandler setMinValueHandler;
				SetMinValueHandler setMinValueHandler0 = this.setMinValueHandler_0;
				do
				{
					setMinValueHandler = setMinValueHandler0;
					SetMinValueHandler setMinValueHandler1 = (SetMinValueHandler)Delegate.Combine(setMinValueHandler, value);
					setMinValueHandler0 = Interlocked.CompareExchange<SetMinValueHandler>(ref this.setMinValueHandler_0, setMinValueHandler1, setMinValueHandler);
				}
				while ((object)setMinValueHandler0 != (object)setMinValueHandler);
			}
			remove
			{
				SetMinValueHandler setMinValueHandler;
				SetMinValueHandler setMinValueHandler0 = this.setMinValueHandler_0;
				do
				{
					setMinValueHandler = setMinValueHandler0;
					SetMinValueHandler setMinValueHandler1 = (SetMinValueHandler)Delegate.Remove(setMinValueHandler, value);
					setMinValueHandler0 = Interlocked.CompareExchange<SetMinValueHandler>(ref this.setMinValueHandler_0, setMinValueHandler1, setMinValueHandler);
				}
				while ((object)setMinValueHandler0 != (object)setMinValueHandler);
			}
		}

		public event SetPositionHandler SetPositionEvent
		{
			add
			{
				SetPositionHandler setPositionHandler;
				SetPositionHandler setPositionHandler0 = this.setPositionHandler_0;
				do
				{
					setPositionHandler = setPositionHandler0;
					SetPositionHandler setPositionHandler1 = (SetPositionHandler)Delegate.Combine(setPositionHandler, value);
					setPositionHandler0 = Interlocked.CompareExchange<SetPositionHandler>(ref this.setPositionHandler_0, setPositionHandler1, setPositionHandler);
				}
				while ((object)setPositionHandler0 != (object)setPositionHandler);
			}
			remove
			{
				SetPositionHandler setPositionHandler;
				SetPositionHandler setPositionHandler0 = this.setPositionHandler_0;
				do
				{
					setPositionHandler = setPositionHandler0;
					SetPositionHandler setPositionHandler1 = (SetPositionHandler)Delegate.Remove(setPositionHandler, value);
					setPositionHandler0 = Interlocked.CompareExchange<SetPositionHandler>(ref this.setPositionHandler_0, setPositionHandler1, setPositionHandler);
				}
				while ((object)setPositionHandler0 != (object)setPositionHandler);
			}
		}

		public event IFeatureProgress_StepEventHandler Step
		{
			add
			{
				IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
				IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 = this.ifeatureProgress_StepEventHandler_0;
				do
				{
					featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
					IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 = (IFeatureProgress_StepEventHandler)Delegate.Combine(featureProgressStepEventHandler, value);
					ifeatureProgressStepEventHandler0 = Interlocked.CompareExchange<IFeatureProgress_StepEventHandler>(ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1, featureProgressStepEventHandler);
				}
				while ((object)ifeatureProgressStepEventHandler0 != (object)featureProgressStepEventHandler);
			}
			remove
			{
				IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
				IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 = this.ifeatureProgress_StepEventHandler_0;
				do
				{
					featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
					IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 = (IFeatureProgress_StepEventHandler)Delegate.Remove(featureProgressStepEventHandler, value);
					ifeatureProgressStepEventHandler0 = Interlocked.CompareExchange<IFeatureProgress_StepEventHandler>(ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1, featureProgressStepEventHandler);
				}
				while ((object)ifeatureProgressStepEventHandler0 != (object)featureProgressStepEventHandler);
			}
		}
	}
}
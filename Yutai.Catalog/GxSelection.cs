using System;
using System.Threading;

namespace Yutai.Catalog
{
	public class GxSelection : IGxSelection, IGxSelectionEvents
	{
		private IGxObject igxObject_0 = null;

		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private bool bool_0 = false;

		private OnSelectionChangedEventHandler onSelectionChangedEventHandler_0;

		public int Count
		{
			get
			{
				return this.igxObjectArray_0.Count;
			}
		}

		public bool DelayEvents
		{
			get
			{
				return this.bool_0;
			}
			set
			{
				this.bool_0 = false;
			}
		}

		public IGxObject FirstObject
		{
			get
			{
				IGxObject gxObject;
				if (this.igxObjectArray_0.Count != 0)
				{
					gxObject = this.igxObjectArray_0.Item(0);
				}
				else
				{
					gxObject = null;
				}
				return gxObject;
			}
		}

		public IGxObject Location
		{
			get
			{
				return this.igxObject_0;
			}
		}

		public IEnumGxObject SelectedObjects
		{
			get
			{
				return this.igxObjectArray_0 as IEnumGxObject;
			}
		}

		public GxSelection()
		{
		}

		public void Clear(object object_0)
		{
			this.igxObjectArray_0.Empty();
			if (this.onSelectionChangedEventHandler_0 != null)
			{
				this.onSelectionChangedEventHandler_0(this, null);
			}
		}

		public bool IsSelected(IGxObject igxObject_1)
		{
			bool flag;
			int num = 0;
			while (true)
			{
				if (num >= this.igxObjectArray_0.Count)
				{
					flag = false;
					break;
				}
				else if (this.igxObjectArray_0.Item(num) == igxObject_1)
				{
					flag = true;
					break;
				}
				else
				{
					num++;
				}
			}
			return flag;
		}

		public void Select(IGxObject igxObject_1, bool bool_1, object object_0)
		{
			if (!bool_1)
			{
				this.igxObjectArray_0.Empty();
			}
			this.igxObjectArray_0.Insert(-1, igxObject_1);
			if (object_0 != null)
			{
				try
				{
					if (Convert.ToBoolean(object_0) && this.onSelectionChangedEventHandler_0 != null)
					{
						this.onSelectionChangedEventHandler_0(this, igxObject_1);
					}
				}
				catch
				{
				}
			}
			else if (this.onSelectionChangedEventHandler_0 != null)
			{
				this.onSelectionChangedEventHandler_0(this, igxObject_1);
			}
		}

		public void SetLocation(IGxObject igxObject_1, object object_0)
		{
			this.igxObject_0 = igxObject_1;
		}

		public void Unselect(IGxObject igxObject_1, object object_0)
		{
			int num = 0;
			while (true)
			{
				if (num >= this.igxObjectArray_0.Count)
				{
					break;
				}
				else if (this.igxObjectArray_0.Item(num) == igxObject_1)
				{
					this.igxObjectArray_0.Remove(num);
					if (this.onSelectionChangedEventHandler_0 == null)
					{
						break;
					}
					this.onSelectionChangedEventHandler_0(this, igxObject_1);
					break;
				}
				else
				{
					num++;
				}
			}
		}

		public event OnSelectionChangedEventHandler OnSelectionChanged
		{
			add
			{
				OnSelectionChangedEventHandler onSelectionChangedEventHandler;
				OnSelectionChangedEventHandler onSelectionChangedEventHandler0 = this.onSelectionChangedEventHandler_0;
				do
				{
					onSelectionChangedEventHandler = onSelectionChangedEventHandler0;
					OnSelectionChangedEventHandler onSelectionChangedEventHandler1 = (OnSelectionChangedEventHandler)Delegate.Combine(onSelectionChangedEventHandler, value);
					onSelectionChangedEventHandler0 = Interlocked.CompareExchange<OnSelectionChangedEventHandler>(ref this.onSelectionChangedEventHandler_0, onSelectionChangedEventHandler1, onSelectionChangedEventHandler);
				}
				while ((object)onSelectionChangedEventHandler0 != (object)onSelectionChangedEventHandler);
			}
			remove
			{
				OnSelectionChangedEventHandler onSelectionChangedEventHandler;
				OnSelectionChangedEventHandler onSelectionChangedEventHandler0 = this.onSelectionChangedEventHandler_0;
				do
				{
					onSelectionChangedEventHandler = onSelectionChangedEventHandler0;
					OnSelectionChangedEventHandler onSelectionChangedEventHandler1 = (OnSelectionChangedEventHandler)Delegate.Remove(onSelectionChangedEventHandler, value);
					onSelectionChangedEventHandler0 = Interlocked.CompareExchange<OnSelectionChangedEventHandler>(ref this.onSelectionChangedEventHandler_0, onSelectionChangedEventHandler1, onSelectionChangedEventHandler);
				}
				while ((object)onSelectionChangedEventHandler0 != (object)onSelectionChangedEventHandler);
			}
		}
	}
}
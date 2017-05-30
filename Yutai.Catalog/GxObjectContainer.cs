using System;

namespace Yutai.Catalog
{
	public class GxObjectContainer : GxObject, IGxObjectContainer
	{
		protected IGxObjectArray m_GxObjectContainer = new GxObjectArray();

		public bool AreChildrenViewable
		{
			get
			{
				return true;
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				if (this.m_GxObjectContainer.Count == 0)
				{
					this.OpenChild();
				}
				return this.m_GxObjectContainer as IEnumGxObject;
			}
		}

		public bool HasChildren
		{
			get
			{
				return true;
			}
		}

		public GxObjectContainer()
		{
		}

		public IGxObject AddChild(IGxObject igxObject_0)
		{
			IGxObject igxObject0;
			bool flag = igxObject_0 is IGxDiskConnection;
			string upper = igxObject_0.Name.ToUpper();
			if (!flag || upper[0] != '\\')
			{
				int num = 0;
				if (flag)
				{
					int num1 = 0;
					while (num1 < this.m_GxObjectContainer.Count)
					{
						IGxObject gxObject = this.m_GxObjectContainer.Item(num1);
						if (!(gxObject is IGxDiskConnection))
						{
							this.m_GxObjectContainer.Insert(num1, igxObject_0);
							igxObject0 = igxObject_0;
							return igxObject0;
						}
						else
						{
							num = gxObject.Name.ToUpper().CompareTo(upper);
							if (num > 0)
							{
								this.m_GxObjectContainer.Insert(num1, igxObject_0);
								igxObject0 = igxObject_0;
								return igxObject0;
							}
							else if (num == 0)
							{
								igxObject0 = gxObject;
								return igxObject0;
							}
							else
							{
								num1++;
							}
						}
					}
				}
				this.m_GxObjectContainer.Insert(-1, igxObject_0);
				igxObject0 = igxObject_0;
			}
			else
			{
				this.m_GxObjectContainer.Insert(-1, igxObject_0);
				igxObject0 = igxObject_0;
			}
			return igxObject0;
		}

		public void DeleteChild(IGxObject igxObject_0)
		{
			int num = 0;
			while (true)
			{
				if (num >= this.m_GxObjectContainer.Count)
				{
					break;
				}
				else if (this.m_GxObjectContainer.Item(num) == igxObject_0)
				{
					this.m_GxObjectContainer.Remove(num);
					break;
				}
				else
				{
					num++;
				}
			}
		}

		protected virtual void OpenChild()
		{
		}

		public override void Refresh()
		{
			this.m_GxObjectContainer.Empty();
			base.Refresh();
		}

		public void SearchChildren(string string_0, IGxObjectArray igxObjectArray_0)
		{
		}
	}
}
using ESRI.ArcGIS.esriSystem;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Yutai.Catalog
{
	public class GxObject : IGxObject, IGxObjectUI
	{
		protected string m_name = "";

		protected string m_Catalog = "";

		protected string m_fullname = "";

		protected Bitmap m_smallimage = null;

		protected Bitmap m_smallselectedimage = null;

		protected Bitmap m_largeimage = null;

		protected Bitmap m_largeselectedimage = null;

		protected IGxObject m_pParent = null;

		protected IGxCatalog m_pGxCatalog = null;

		public virtual string BaseName
		{
			get
			{
				return this.m_name;
			}
			protected set
			{
			}
		}

		public virtual string Category
		{
			get
			{
				return this.m_Catalog;
			}
			protected set
			{
				this.m_Catalog = value;
			}
		}

		public UID ClassID { get; set; }

	

		public virtual string FullName
		{
			get
			{
				return this.m_fullname;
			}
			protected set
			{
				this.m_fullname = value;
			}
		}

		public IName InternalObjectName
		{
            get; set;
        }

		

		public bool IsValid
		{
            get; set;
        }

	

		public virtual Bitmap LargeImage
		{
			get
			{
				return this.m_largeimage;
			}
			protected set
			{
				this.m_largeimage = value;
			}
		}

		public virtual Bitmap LargeSelectedImage
		{
			get
			{
				return this.m_largeselectedimage;
			}
			protected set
			{
				this.m_largeselectedimage = value;
			}
		}

		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
			protected set
			{
				this.m_name = value;
			}
		}

		public virtual IGxObject Parent
		{
			get
			{
				return this.m_pParent;
			}
		}

		public virtual Bitmap SmallImage
		{
			get
			{
				return this.m_smallimage;
			}
			protected set
			{
				this.m_smallimage = value;
			}
		}

		public virtual Bitmap SmallSelectedImage
		{
			get
			{
				return this.m_smallselectedimage;
			}
			protected set
			{
				this.m_smallselectedimage = value;
			}
		}

		public GxObject()
		{
		}

		public virtual void Attach(IGxObject igxObject_0, IGxCatalog igxCatalog_0)
		{
			this.m_pParent = igxObject_0;
			this.m_pGxCatalog = igxCatalog_0;
			if (this.m_pParent is IGxObjectContainer)
			{
				(this.m_pParent as IGxObjectContainer).AddChild(this);
			}
		}

		public virtual void Detach()
		{
			if (this.m_pParent is IGxObjectContainer)
			{
				(this.m_pParent as IGxObjectContainer).DeleteChild(this);
			}
			if (this.m_pGxCatalog != null)
			{
				this.m_pGxCatalog.ObjectDeleted(this);
			}
			this.m_pParent = null;
			this.m_pGxCatalog = null;
		}

		public virtual void Refresh()
		{
			this.m_pGxCatalog.ObjectRefreshed(this);
		}
	}
}
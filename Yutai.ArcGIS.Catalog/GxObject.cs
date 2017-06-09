using System.Drawing;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog
{
    public class GxObject : IGxObject, IGxObjectUI
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private IName iname_0;
        protected string m_Catalog = "";
        protected string m_fullname = "";
        protected Bitmap m_largeimage = null;
        protected Bitmap m_largeselectedimage = null;
        protected string m_name = "";
        protected IGxCatalog m_pGxCatalog = null;
        protected IGxObject m_pParent = null;
        protected Bitmap m_smallimage = null;
        protected Bitmap m_smallselectedimage = null;
        [CompilerGenerated]
        private UID uid_0;

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

        public UID ClassID
        {
            [CompilerGenerated]
            get
            {
                return this.uid_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.uid_0 = value;
            }
        }

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
            [CompilerGenerated]
            get
            {
                return this.iname_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.iname_0 = value;
            }
        }

        public bool IsValid
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.bool_0 = value;
            }
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
    }
}


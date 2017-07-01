using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.Framework;

namespace Yutai.ArcGIS.Catalog
{
    public class GxGISServersFolder : IGxObject, IGxObjectContainer, IGxObjectUI, IGxContextMenuWap, IGxRemoteContainer,
        IGxGISServersFolder
    {
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;
        private string string_0 = "";

        public GxGISServersFolder()
        {
            this.string_0 = Environment.SystemDirectory.Substring(0, 2) +
                            @"\Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog";
            if (!Directory.Exists(this.string_0))
            {
                try
                {
                    Directory.CreateDirectory(this.string_0);
                }
                catch
                {
                }
            }
        }

        public IGxObject AddChild(IGxObject igxObject_1)
        {
            this.igxObjectArray_0.Insert(-1, igxObject_1);
            return igxObject_1;
        }

        public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
        {
            this.igxObject_0 = igxObject_1;
            this.igxCatalog_0 = igxCatalog_1;
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).AddChild(this);
            }
            if (this.igxObjectArray_0.Count == 0)
            {
                this.method_0();
            }
        }

        public void DeleteChild(IGxObject igxObject_1)
        {
            for (int i = 0; i < this.igxObjectArray_0.Count; i++)
            {
                if (this.igxObjectArray_0.Item(i) == igxObject_1)
                {
                    this.igxObjectArray_0.Remove(i);
                    break;
                }
            }
        }

        public void Detach()
        {
            if (this.igxCatalog_0 != null)
            {
                this.igxCatalog_0.ObjectDeleted(this);
            }
            if (this.igxObject_0 is IGxObjectContainer)
            {
                (this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
            }
            this.igxObject_0 = null;
            this.igxCatalog_0 = null;
        }

        public void Init(object object_0)
        {
            this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
            this.ipopuMenuWrap_0.Clear();
            this.ipopuMenuWrap_0.AddItem("Catalog_Refresh", false);
        }

        private void method_0()
        {
            this.igxObjectArray_0.Empty();
            IGxObject obj2 = new GxAddAGSConnection();
            obj2.Attach(this, this.igxCatalog_0);
            if (Directory.Exists(this.string_0))
            {
                foreach (string str in Directory.GetFiles(this.string_0, "*.ags"))
                {
                    try
                    {
                        obj2 = new GxAGSConnection();
                        (obj2 as IGxAGSConnection).LoadFromFile(str);
                        if ((obj2 as IGxAGSConnection).ConnectionMode >= 0)
                        {
                            obj2.Attach(this, this.igxCatalog_0);
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
            }
        }

        public void Refresh()
        {
            this.igxObjectArray_0.Empty();
            this.method_0();
            this.igxCatalog_0.ObjectRefreshed(this);
        }

        public void SearchChildren(string string_1, IGxObjectArray igxObjectArray_1)
        {
        }

        public override string ToString()
        {
            return "GxGISServersFolder";
        }

        public bool AreChildrenViewable
        {
            get { return true; }
        }

        public string BaseName
        {
            get { return "GIS服务器"; }
        }

        public string Category
        {
            get { return "GIS服务器文件夹"; }
        }

        public IEnumGxObject Children
        {
            get
            {
                if (this.igxObjectArray_0.Count == 0)
                {
                    this.method_0();
                }
                return (this.igxObjectArray_0 as IEnumGxObject);
            }
        }

        public UID ClassID
        {
            get { return null; }
        }

        public UID ContextMenu
        {
            get { return null; }
        }

        public string FullName
        {
            get { return "GIS服务器"; }
        }

        public bool HasChildren
        {
            get { return true; }
        }

        public IName InternalObjectName
        {
            get { return null; }
        }

        public bool IsValid
        {
            get { return false; }
        }

        public Bitmap LargeImage
        {
            get { return ImageLib.GetSmallImage(3); }
        }

        public Bitmap LargeSelectedImage
        {
            get { return ImageLib.GetSmallImage(3); }
        }

        public string Name
        {
            get { return "GIS服务器"; }
        }

        public UID NewMenu
        {
            get { return null; }
        }

        public IGxObject Parent
        {
            get { return this.igxObject_0; }
        }

        public string Path
        {
            get { return this.string_0; }
            set { }
        }

        public Bitmap SmallImage
        {
            get { return ImageLib.GetSmallImage(3); }
        }

        public Bitmap SmallSelectedImage
        {
            get { return ImageLib.GetSmallImage(3); }
        }
    }
}
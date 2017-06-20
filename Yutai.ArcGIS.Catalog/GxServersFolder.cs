using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class GxServersFolder : IGxObject, IGxObjectContainer, IGxObjectUI, IGxContextMenuWap, IGxRemoteContainer, IGxServersFolder
    {
        [CompilerGenerated]
        private IAGSServerConnection2 iagsserverConnection2_0;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private IGxObjectArray igxObjectArray_0 = new GxObjectArray();
        private IPopuMenuWrap ipopuMenuWrap_0 = null;
        private string string_0 = "";
        [CompilerGenerated]
        private string string_1;

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
            try
            {
                if (this.AGSServerConnection != null)
                {
                    IAGSEnumServerObjectName name = this.AGSServerConnection.get_ServerObjectNamesEx(this.FolderName);
                    name.Reset();
                    for (IAGSServerObjectName name2 = name.Next(); name2 != null; name2 = name.Next())
                    {
                        IGxObject obj2;
                        bool flag;
                        if (name2.Type.ToLower() == "mapserver")
                        {
                            obj2 = new GxAGSMap();
                            (obj2 as IGxAGSObject).AGSServerObjectName = name2;
                            flag = true;
                            if (((this.Parent as IGxAGSConnection).ConnectionMode == 0) && ((obj2 as IGxAGSObject).Status != "Started"))
                            {
                                flag = false;
                            }
                            if (flag)
                            {
                                obj2.Attach(this, this.igxCatalog_0);
                            }
                        }
                        else if (name2.Type.ToLower() == "featureserver")
                        {
                            if ((this.Parent as IGxAGSConnection).ConnectionMode == 0)
                            {
                                obj2 = new GxFeatureService();
                                (obj2 as IGxAGSObject).AGSServerObjectName = name2;
                                if ((obj2 as IGxAGSObject).Status == "Started")
                                {
                                    obj2.Attach(this, this.igxCatalog_0);
                                }
                            }
                        }
                        else if (name2.Type.ToLower() == "gpserver")
                        {
                            obj2 = new GxGPServer();
                            (obj2 as IGxAGSObject).AGSServerObjectName = name2;
                            flag = true;
                            if (((this.Parent as IGxAGSConnection).ConnectionMode == 0) && ((obj2 as IGxAGSObject).Status != "Started"))
                            {
                                flag = false;
                            }
                            if (flag)
                            {
                                obj2.Attach(this, this.igxCatalog_0);
                            }
                        }
                        else if (name2.Type.ToLower() == "geometryserver")
                        {
                            if ((this.Parent as IGxAGSConnection).ConnectionMode > 0)
                            {
                                obj2 = new GxGeometryServer();
                                (obj2 as IGxAGSObject).AGSServerObjectName = name2;
                                obj2.Attach(this, this.igxCatalog_0);
                            }
                        }
                        else if (name2.Type.ToLower() == "searchserver")
                        {
                            obj2 = new GxSearchServer();
                            (obj2 as IGxAGSObject).AGSServerObjectName = name2;
                            flag = true;
                            if (((this.Parent as IGxAGSConnection).ConnectionMode == 0) && ((obj2 as IGxAGSObject).Status != "Started"))
                            {
                                flag = false;
                            }
                            if (flag)
                            {
                                obj2.Attach(this, this.igxCatalog_0);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Logger.Current.Error("",exception, "");
            }
        }

        public void Refresh()
        {
            this.igxObjectArray_0.Empty();
            this.method_0();
            this.igxCatalog_0.ObjectRefreshed(this);
        }

        public void SearchChildren(string string_2, IGxObjectArray igxObjectArray_1)
        {
        }

        public override string ToString()
        {
            return "GxServersFolder";
        }

        public IAGSServerConnection2 AGSServerConnection
        {
            [CompilerGenerated]
            get
            {
                return this.iagsserverConnection2_0;
            }
            [CompilerGenerated]
            set
            {
                this.iagsserverConnection2_0 = value;
            }
        }

        public bool AreChildrenViewable
        {
            get
            {
                return true;
            }
        }

        public string BaseName
        {
            get
            {
                return this.FolderName;
            }
        }

        public string Category
        {
            get
            {
                return "ArcGIS Server文件夹";
            }
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
            get
            {
                return null;
            }
        }

        public UID ContextMenu
        {
            get
            {
                return null;
            }
        }

        public string FolderName
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        public string FullName
        {
            get
            {
                return this.FolderName;
            }
        }

        public bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public IName InternalObjectName
        {
            get
            {
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                return false;
            }
        }

        public Bitmap LargeImage
        {
            get
            {
                return ImageLib.GetSmallImage(6);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(7);
            }
        }

        public string Name
        {
            get
            {
                return this.FolderName;
            }
        }

        public UID NewMenu
        {
            get
            {
                return null;
            }
        }

        public IGxObject Parent
        {
            get
            {
                return this.igxObject_0;
            }
        }

        public string Path
        {
            get
            {
                return this.string_0;
            }
            set
            {
            }
        }

        public Bitmap SmallImage
        {
            get
            {
                return ImageLib.GetSmallImage(6);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                return ImageLib.GetSmallImage(7);
            }
        }
    }
}


using System;
using System.IO;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Common.Display
{
    public class SymbolFind
    {
        public SymbolFind()
        {
        }

        public static IStyleGalleryItem FindStyleGalleryItem(string string_0, string string_1, string string_2,
            string string_3)
        {
            IStyleGalleryItem styleGalleryItem;
            IStyleGallery myStyleGallery;
            try
            {
                if (File.Exists(string_1))
                {
                    string lower = Path.GetExtension(string_1).ToLower();
                    if (lower == ".style")
                    {
                        myStyleGallery = new MyStyleGallery();
                    }
                    else if (lower != ".serverstyle")
                    {
                        styleGalleryItem = null;
                        return styleGalleryItem;
                    }
                    else
                    {
                        myStyleGallery = new ServerStyleGallery();
                        for (int i = (myStyleGallery as IStyleGalleryStorage).FileCount - 1; i >= 0; i--)
                        {
                            if (Path.GetExtension((myStyleGallery as IStyleGalleryStorage).File[i]).ToLower() !=
                                ".serverstyle")
                            {
                                (myStyleGallery as IStyleGalleryStorage).RemoveFile(
                                    (myStyleGallery as IStyleGalleryStorage).File[i]);
                            }
                        }
                    }
                    (myStyleGallery as IStyleGalleryStorage).AddFile(string_1);
                    IStyleGalleryItem styleGalleryItem1 = SymbolFind.FindStyleGalleryItem(string_0, myStyleGallery,
                        string_1, string_2, string_3);
                    myStyleGallery = null;
                    GC.Collect();
                    styleGalleryItem = styleGalleryItem1;
                }
                else
                {
                    styleGalleryItem = null;
                }
            }
            catch (Exception exception)
            {
                styleGalleryItem = null;
            }
            return styleGalleryItem;
        }

        public static IStyleGalleryItem FindStyleGalleryItem(string string_0, IStyleGallery istyleGallery_0,
            string string_1, string string_2, string string_3)
        {
            IStyleGalleryItem styleGalleryItem;
            IEnumStyleGalleryItem items;
            if (istyleGallery_0 != null)
            {
                try
                {
                    string_0 = string_0.ToUpper();
                    items = istyleGallery_0.Items[string_2, string_1, string_3];
                    items.Reset();
                    IStyleGalleryItem styleGalleryItem1 = items.Next();
                    while (styleGalleryItem1 != null)
                    {
                        if (styleGalleryItem1.Name.ToUpper() == string_0)
                        {
                            styleGalleryItem = styleGalleryItem1;
                            return styleGalleryItem;
                        }
                        else
                        {
                            styleGalleryItem1 = items.Next();
                        }
                    }
                }
                catch (Exception exception)
                {
                    exception.ToString();
                }
                items = null;
                GC.Collect();
                styleGalleryItem = null;
            }
            else
            {
                styleGalleryItem = null;
            }
            return styleGalleryItem;
        }

        public static IStyleGalleryItem FindStyleGalleryItem(int int_0, IStyleGallery istyleGallery_0, string string_0,
            string string_1, string string_2)
        {
            IStyleGalleryItem styleGalleryItem;
            IEnumStyleGalleryItem items;
            if (istyleGallery_0 == null)
            {
                styleGalleryItem = null;
            }
            else if (string_0 != "")
            {
                try
                {
                    items = istyleGallery_0.Items[string_1, string_0, string_2];
                    items.Reset();
                    IStyleGalleryItem styleGalleryItem1 = items.Next();
                    while (styleGalleryItem1 != null)
                    {
                        if (styleGalleryItem1.ID == int_0)
                        {
                            styleGalleryItem = styleGalleryItem1;
                            return styleGalleryItem;
                        }
                        else
                        {
                            styleGalleryItem1 = items.Next();
                        }
                    }
                }
                catch
                {
                }
                items = null;
                GC.Collect();
                styleGalleryItem = null;
            }
            else
            {
                styleGalleryItem = null;
            }
            return styleGalleryItem;
        }
    }
}
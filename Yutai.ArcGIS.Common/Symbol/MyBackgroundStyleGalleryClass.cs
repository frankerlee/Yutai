using System;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Symbol
{
    public class MyBackgroundStyleGalleryClass : IStyleGalleryClass
    {
        public System.Guid ItemClass
        {
            get
            {
                return default(System.Guid);
            }
        }

        public IEnumBSTR NewObjectTypes
        {
            get
            {
                return null;
            }
        }

        public object get_NewObject(string newType)
        {
            return null;
        }

        public double PreviewRatio
        {
            get
            {
                return 0.0;
            }
        }

        public string Description
        {
            get
            {
                return null;
            }
        }

        public string Name
        {
            get
            {
                return "Backgrounds";
            }
        }

        public object NewObject
        {
            get
            {
                return null;
            }
        }

        public void EditProperties(ref object object_0, IComPropertySheetEvents icomPropertySheetEvents_0, int int_0, out bool bool_0)
        {
            bool_0 = false;
        }

        public void Preview(object object_0, int int_0, ref tagRECT tagRECT_0)
        {
        }
    }
}
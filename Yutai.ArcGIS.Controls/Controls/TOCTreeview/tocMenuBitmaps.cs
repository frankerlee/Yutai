using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Yutai.ArcGIS.Controls.Controls.TOCTreeview
{
    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class tocMenuBitmaps
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal tocMenuBitmaps()
        {
        }

        internal static Bitmap AddData
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("AddData", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static Bitmap PanToSelect
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("PanToSelect", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("JLK.Controls.TOCTreeview.tocMenuBitmaps", typeof(tocMenuBitmaps).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap SwitchSelection
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("SwitchSelection", resourceCulture);
            }
        }

        internal static Bitmap Table
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Table", resourceCulture);
            }
        }

        internal static Bitmap Zoom2SelectFeat
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Zoom2SelectFeat", resourceCulture);
            }
        }

        internal static Bitmap ZoomToLayer
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("ZoomToLayer", resourceCulture);
            }
        }
    }
}


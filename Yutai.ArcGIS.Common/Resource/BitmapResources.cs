using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Yutai.ArcGIS.Common.Resource
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode,
     CompilerGenerated]
    internal class BitmapResources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal BitmapResources()
        {
        }

        internal static Bitmap Anno
        {
            get { return (Bitmap) ResourceManager.GetObject("Anno", resourceCulture); }
        }

        internal static Bitmap Area
        {
            get { return (Bitmap) ResourceManager.GetObject("Area", resourceCulture); }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        internal static Bitmap Line
        {
            get { return (Bitmap) ResourceManager.GetObject("Line", resourceCulture); }
        }

        internal static Bitmap Point
        {
            get { return (Bitmap) ResourceManager.GetObject("Point", resourceCulture); }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager =
                        new System.Resources.ResourceManager("Yutai.ArcGIS.Common.Resource.BitmapResources",
                            typeof(BitmapResources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Yutai.ArcGIS.Framework.Docking
{
    [DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),
     CompilerGenerated]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        internal static Bitmap DockIndicator_PaneDiamond
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PaneDiamond", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PaneDiamond_Bottom
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PaneDiamond_Bottom", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PaneDiamond_Fill
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PaneDiamond_Fill", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PaneDiamond_HotSpot
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PaneDiamond_HotSpot", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PaneDiamond_HotSpotIndex
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("DockIndicator_PaneDiamond_HotSpotIndex", resourceCulture);
            }
        }

        internal static Bitmap DockIndicator_PaneDiamond_Left
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PaneDiamond_Left", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PaneDiamond_Right
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PaneDiamond_Right", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PaneDiamond_Top
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PaneDiamond_Top", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelBottom
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelBottom", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelBottom_Active
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelBottom_Active", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelFill
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelFill", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelFill_Active
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelFill_Active", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelLeft
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelLeft", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelLeft_Active
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelLeft_Active", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelRight
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelRight", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelRight_Active
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelRight_Active", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelTop
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelTop", resourceCulture); }
        }

        internal static Bitmap DockIndicator_PanelTop_Active
        {
            get { return (Bitmap) ResourceManager.GetObject("DockIndicator_PanelTop_Active", resourceCulture); }
        }

        internal static Bitmap DockPane_AutoHide
        {
            get { return (Bitmap) ResourceManager.GetObject("DockPane_AutoHide", resourceCulture); }
        }

        internal static Bitmap DockPane_Close
        {
            get { return (Bitmap) ResourceManager.GetObject("DockPane_Close", resourceCulture); }
        }

        internal static Bitmap DockPane_Dock
        {
            get { return (Bitmap) ResourceManager.GetObject("DockPane_Dock", resourceCulture); }
        }

        internal static Bitmap DockPane_Option
        {
            get { return (Bitmap) ResourceManager.GetObject("DockPane_Option", resourceCulture); }
        }

        internal static Bitmap DockPane_OptionOverflow
        {
            get { return (Bitmap) ResourceManager.GetObject("DockPane_OptionOverflow", resourceCulture); }
        }

        internal static Bitmap DockPanel
        {
            get { return (Bitmap) ResourceManager.GetObject("DockPanel", resourceCulture); }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager =
                        new System.Resources.ResourceManager("JLK.WinFormsUI.Docking.Resources",
                            typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}
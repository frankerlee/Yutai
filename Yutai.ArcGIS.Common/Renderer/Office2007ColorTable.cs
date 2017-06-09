using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.Renderer
{
    public class Office2007ColorTable : ProfessionalColorTable
    {
        private static Color _buttonBorder;
        private static Color _buttonPressedBegin;
        private static Color _buttonPressedEnd;
        private static Color _buttonPressedMiddle;
        private static Color _buttonSelectedBegin;
        private static Color _buttonSelectedEnd;
        private static Color _buttonSelectedMiddle;
        private static Color _checkBack;
        private static Color _contextMenuBack;
        private static Color _gripDark;
        private static Color _gripLight;
        private static Color _imageMargin;
        private static Color _menuBorder;
        private static Color _menuItemSelectedBegin;
        private static Color _menuItemSelectedEnd;
        private static Color _menuToolBack;
        private static Color _overflowBegin;
        private static Color _overflowEnd;
        private static Color _overflowMiddle;
        private static Color _separatorDark;
        private static Color _separatorLight;
        private static Color _statusStripDark;
        private static Color _statusStripLight;
        private static Color _toolStripBegin;
        private static Color _toolStripBorder;
        private static Color _toolStripContentEnd;
        private static Color _toolStripEnd;
        private static Color _toolStripMiddle;

        static Office2007ColorTable()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            _contextMenuBack = Color.FromArgb(250, 250, 250);
            _buttonPressedBegin = Color.FromArgb(0xf8, 0xb5, 0x6a);
            _buttonPressedEnd = Color.FromArgb(0xff, 0xd0, 0x86);
            _buttonPressedMiddle = Color.FromArgb(0xfb, 140, 60);
            _buttonSelectedBegin = Color.FromArgb(0xff, 0xff, 0xde);
            _buttonSelectedEnd = Color.FromArgb(0xff, 0xcb, 0x88);
            _buttonSelectedMiddle = Color.FromArgb(0xff, 0xe1, 0xac);
            _menuItemSelectedBegin = Color.FromArgb(0xff, 0xd5, 0x67);
            _menuItemSelectedEnd = Color.FromArgb(0xff, 0xe4, 0x91);
            _checkBack = Color.FromArgb(0xff, 0xe3, 0x95);
            _gripDark = Color.FromArgb(0x6f, 0x9d, 0xd9);
            _gripLight = Color.FromArgb(0xff, 0xff, 0xff);
            _imageMargin = Color.FromArgb(0xe9, 0xee, 0xee);
            _menuBorder = Color.FromArgb(0x86, 0x86, 0x86);
            _overflowBegin = Color.FromArgb(0xa7, 0xcc, 0xfb);
            _overflowEnd = Color.FromArgb(0x65, 0x93, 0xcf);
            _overflowMiddle = Color.FromArgb(0xa7, 0xcc, 0xfb);
            _menuToolBack = Color.FromArgb(0xbf, 0xdb, 0xff);
            _separatorDark = Color.FromArgb(0x9a, 0xc6, 0xff);
            _separatorLight = Color.FromArgb(0xff, 0xff, 0xff);
            _statusStripLight = Color.FromArgb(0xd7, 0xe5, 0xf7);
            _statusStripDark = Color.FromArgb(0xac, 0xc9, 0xee);
            _toolStripBorder = Color.FromArgb(0x6f, 0x9d, 0xd9);
            _toolStripContentEnd = Color.FromArgb(0xa4, 0xc3, 0xeb);
            _toolStripBegin = Color.FromArgb(0xe3, 0xef, 0xff);
            _toolStripEnd = Color.FromArgb(0x98, 0xba, 230);
            _toolStripMiddle = Color.FromArgb(0xde, 0xec, 0xff);
            _buttonBorder = Color.FromArgb(0x79, 0x99, 0xc2);
        }

        public override Color ButtonPressedGradientBegin
        {
            get
            {
                return _buttonPressedBegin;
            }
        }

        public override Color ButtonPressedGradientEnd
        {
            get
            {
                return _buttonPressedEnd;
            }
        }

        public override Color ButtonPressedGradientMiddle
        {
            get
            {
                return _buttonPressedMiddle;
            }
        }

        public override Color ButtonSelectedGradientBegin
        {
            get
            {
                return _buttonSelectedBegin;
            }
        }

        public override Color ButtonSelectedGradientEnd
        {
            get
            {
                return _buttonSelectedEnd;
            }
        }

        public override Color ButtonSelectedGradientMiddle
        {
            get
            {
                return _buttonSelectedMiddle;
            }
        }

        public override Color ButtonSelectedHighlightBorder
        {
            get
            {
                return _buttonBorder;
            }
        }

        public override Color CheckBackground
        {
            get
            {
                return _checkBack;
            }
        }

        public override Color GripDark
        {
            get
            {
                return _gripDark;
            }
        }

        public override Color GripLight
        {
            get
            {
                return _gripLight;
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return _imageMargin;
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return _menuBorder;
            }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return _toolStripBegin;
            }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return _toolStripEnd;
            }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get
            {
                return _toolStripMiddle;
            }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return _menuItemSelectedBegin;
            }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return _menuItemSelectedEnd;
            }
        }

        public override Color MenuStripGradientBegin
        {
            get
            {
                return _menuToolBack;
            }
        }

        public override Color MenuStripGradientEnd
        {
            get
            {
                return _menuToolBack;
            }
        }

        public override Color OverflowButtonGradientBegin
        {
            get
            {
                return _overflowBegin;
            }
        }

        public override Color OverflowButtonGradientEnd
        {
            get
            {
                return _overflowEnd;
            }
        }

        public override Color OverflowButtonGradientMiddle
        {
            get
            {
                return _overflowMiddle;
            }
        }

        public override Color RaftingContainerGradientBegin
        {
            get
            {
                return _menuToolBack;
            }
        }

        public override Color RaftingContainerGradientEnd
        {
            get
            {
                return _menuToolBack;
            }
        }

        public override Color SeparatorDark
        {
            get
            {
                return _separatorDark;
            }
        }

        public override Color SeparatorLight
        {
            get
            {
                return _separatorLight;
            }
        }

        public override Color StatusStripGradientBegin
        {
            get
            {
                return _statusStripLight;
            }
        }

        public override Color StatusStripGradientEnd
        {
            get
            {
                return _statusStripDark;
            }
        }

        public override Color ToolStripBorder
        {
            get
            {
                return _toolStripBorder;
            }
        }

        public override Color ToolStripContentPanelGradientBegin
        {
            get
            {
                return _toolStripContentEnd;
            }
        }

        public override Color ToolStripContentPanelGradientEnd
        {
            get
            {
                return _menuToolBack;
            }
        }

        public override Color ToolStripDropDownBackground
        {
            get
            {
                return _contextMenuBack;
            }
        }

        public override Color ToolStripGradientBegin
        {
            get
            {
                return _toolStripBegin;
            }
        }

        public override Color ToolStripGradientEnd
        {
            get
            {
                return _toolStripEnd;
            }
        }

        public override Color ToolStripGradientMiddle
        {
            get
            {
                return _toolStripMiddle;
            }
        }

        public override Color ToolStripPanelGradientBegin
        {
            get
            {
                return _menuToolBack;
            }
        }

        public override Color ToolStripPanelGradientEnd
        {
            get
            {
                return _menuToolBack;
            }
        }
    }
}


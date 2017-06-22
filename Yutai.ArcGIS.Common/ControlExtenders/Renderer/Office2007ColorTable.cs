using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders.Renderer
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
            _buttonPressedBegin = Color.FromArgb(248, 181, 106);
            _buttonPressedEnd = Color.FromArgb(255, 208, 134);
            _buttonPressedMiddle = Color.FromArgb(251, 140, 60);
            _buttonSelectedBegin = Color.FromArgb(255, 255, 222);
            _buttonSelectedEnd = Color.FromArgb(255, 203, 136);
            _buttonSelectedMiddle = Color.FromArgb(255, 225, 172);
            _menuItemSelectedBegin = Color.FromArgb(255, 213, 103);
            _menuItemSelectedEnd = Color.FromArgb(255, 228, 145);
            _checkBack = Color.FromArgb(255, 227, 149);
            _gripDark = Color.FromArgb(111, 157, 217);
            _gripLight = Color.FromArgb(255, 255, 255);
            _imageMargin = Color.FromArgb(233, 238, 238);
            _menuBorder = Color.FromArgb(134, 134, 134);
            _overflowBegin = Color.FromArgb(167, 204, 251);
            _overflowEnd = Color.FromArgb(101, 147, 207);
            _overflowMiddle = Color.FromArgb(167, 204, 251);
            _menuToolBack = Color.FromArgb(191, 219, 255);
            _separatorDark = Color.FromArgb(154, 198, 255);
            _separatorLight = Color.FromArgb(255, 255, 255);
            _statusStripLight = Color.FromArgb(215, 229, 247);
            _statusStripDark = Color.FromArgb(172, 201, 238);
            _toolStripBorder = Color.FromArgb(111, 157, 217);
            _toolStripContentEnd = Color.FromArgb(164, 195, 235);
            _toolStripBegin = Color.FromArgb(227, 239, 255);
            _toolStripEnd = Color.FromArgb(152, 186, 230);
            _toolStripMiddle = Color.FromArgb(222, 236, 255);
            _buttonBorder = Color.FromArgb(121, 153, 194);
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


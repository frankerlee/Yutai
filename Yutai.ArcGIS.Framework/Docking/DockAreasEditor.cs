using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal class DockAreasEditor : UITypeEditor
    {
        private DockAreasEditorControl m_ui = null;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider sp, object value)
        {
            if (this.m_ui == null)
            {
                this.m_ui = new DockAreasEditorControl();
            }
            this.m_ui.SetStates((DockAreas) value);
            ((IWindowsFormsEditorService) sp.GetService(typeof(IWindowsFormsEditorService))).DropDownControl(this.m_ui);
            return this.m_ui.DockAreas;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        private class DockAreasEditorControl : UserControl
        {
            private CheckBox checkBoxDockBottom = new CheckBox();
            private CheckBox checkBoxDockFill = new CheckBox();
            private CheckBox checkBoxDockLeft = new CheckBox();
            private CheckBox checkBoxDockRight = new CheckBox();
            private CheckBox checkBoxDockTop = new CheckBox();
            private CheckBox checkBoxFloat = new CheckBox();
            private DockAreas m_oldDockAreas;

            public DockAreasEditorControl()
            {
                base.SuspendLayout();
                this.checkBoxFloat.Appearance = Appearance.Button;
                this.checkBoxFloat.Dock = DockStyle.Top;
                this.checkBoxFloat.Height = 24;
                this.checkBoxFloat.Text = Strings.DockAreaEditor_FloatCheckBoxText;
                this.checkBoxFloat.TextAlign = ContentAlignment.MiddleCenter;
                this.checkBoxFloat.FlatStyle = FlatStyle.System;
                this.checkBoxDockLeft.Appearance = Appearance.Button;
                this.checkBoxDockLeft.Dock = DockStyle.Left;
                this.checkBoxDockLeft.Width = 24;
                this.checkBoxDockLeft.FlatStyle = FlatStyle.System;
                this.checkBoxDockRight.Appearance = Appearance.Button;
                this.checkBoxDockRight.Dock = DockStyle.Right;
                this.checkBoxDockRight.Width = 24;
                this.checkBoxDockRight.FlatStyle = FlatStyle.System;
                this.checkBoxDockTop.Appearance = Appearance.Button;
                this.checkBoxDockTop.Dock = DockStyle.Top;
                this.checkBoxDockTop.Height = 24;
                this.checkBoxDockTop.FlatStyle = FlatStyle.System;
                this.checkBoxDockBottom.Appearance = Appearance.Button;
                this.checkBoxDockBottom.Dock = DockStyle.Bottom;
                this.checkBoxDockBottom.Height = 24;
                this.checkBoxDockBottom.FlatStyle = FlatStyle.System;
                this.checkBoxDockFill.Appearance = Appearance.Button;
                this.checkBoxDockFill.Dock = DockStyle.Fill;
                this.checkBoxDockFill.FlatStyle = FlatStyle.System;
                base.Controls.AddRange(new Control[] { this.checkBoxDockFill, this.checkBoxDockBottom, this.checkBoxDockTop, this.checkBoxDockRight, this.checkBoxDockLeft, this.checkBoxFloat });
                base.Size = new Size(160, 144);
                this.BackColor = SystemColors.Control;
                base.ResumeLayout();
            }

            public void SetStates(DockAreas dockAreas)
            {
                this.m_oldDockAreas = dockAreas;
                if ((dockAreas & DockAreas.DockLeft) != 0)
                {
                    this.checkBoxDockLeft.Checked = true;
                }
                if ((dockAreas & DockAreas.DockRight) != 0)
                {
                    this.checkBoxDockRight.Checked = true;
                }
                if ((dockAreas & DockAreas.DockTop) != 0)
                {
                    this.checkBoxDockTop.Checked = true;
                }
                if ((dockAreas & DockAreas.DockTop) != 0)
                {
                    this.checkBoxDockTop.Checked = true;
                }
                if ((dockAreas & DockAreas.DockBottom) != 0)
                {
                    this.checkBoxDockBottom.Checked = true;
                }
                if ((dockAreas & DockAreas.Document) != 0)
                {
                    this.checkBoxDockFill.Checked = true;
                }
                if ((dockAreas & DockAreas.Float) != 0)
                {
                    this.checkBoxFloat.Checked = true;
                }
            }

            public DockAreas DockAreas
            {
                get
                {
                    DockAreas areas = 0;
                    if (this.checkBoxFloat.Checked)
                    {
                        areas |= DockAreas.Float;
                    }
                    if (this.checkBoxDockLeft.Checked)
                    {
                        areas |= DockAreas.DockLeft;
                    }
                    if (this.checkBoxDockRight.Checked)
                    {
                        areas |= DockAreas.DockRight;
                    }
                    if (this.checkBoxDockTop.Checked)
                    {
                        areas |= DockAreas.DockTop;
                    }
                    if (this.checkBoxDockBottom.Checked)
                    {
                        areas |= DockAreas.DockBottom;
                    }
                    if (this.checkBoxDockFill.Checked)
                    {
                        areas |= DockAreas.Document;
                    }
                    if (areas == 0)
                    {
                        return this.m_oldDockAreas;
                    }
                    return areas;
                }
            }
        }
    }
}


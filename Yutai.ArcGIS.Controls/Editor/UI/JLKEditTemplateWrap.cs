namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class JLKEditTemplateWrap
    {
        internal JLKEditTemplateWrap(JLKEditTemplate t)
        {
            this.EditTemplate = t;
            this.IsUse = true;
        }

        internal JLKEditTemplate EditTemplate { get; set; }

        internal bool IsUse { get; set; }
    }
}


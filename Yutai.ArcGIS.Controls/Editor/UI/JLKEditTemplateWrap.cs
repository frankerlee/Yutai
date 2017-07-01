using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class YTEditTemplateWrap
    {
        internal YTEditTemplateWrap(YTEditTemplate t)
        {
            this.EditTemplate = t;
            this.IsUse = true;
        }

        internal YTEditTemplate EditTemplate { get; set; }

        internal bool IsUse { get; set; }
    }
}
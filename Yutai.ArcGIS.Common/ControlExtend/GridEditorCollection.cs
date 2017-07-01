using System.Collections;
using DevExpress.XtraEditors.Repository;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    internal class GridEditorCollection : ArrayList
    {
        public void Add(RepositoryItem repositoryItem_0, string string_0, object object_0)
        {
            base.Add(new GridEditorItem(repositoryItem_0, string_0, object_0));
        }

        public void Add(RepositoryItem repositoryItem_0, string string_0, object object_0, object object_1)
        {
            base.Add(new GridEditorItem(repositoryItem_0, string_0, object_0, object_1));
        }

        public GridEditorItem this[int int_0]
        {
            get { return (base[int_0] as GridEditorItem); }
        }
    }
}
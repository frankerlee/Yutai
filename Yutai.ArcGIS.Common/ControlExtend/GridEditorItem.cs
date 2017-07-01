using DevExpress.XtraEditors.Repository;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class GridEditorItem
    {
        private bool bool_0;
        private object object_0;
        private object object_1;
        private RepositoryItem repositoryItem_0;
        private string string_0;

        public GridEditorItem(RepositoryItem repositoryItem_1, string string_1, object object_2)
        {
            this.object_1 = null;
            this.bool_0 = true;
            this.repositoryItem_0 = repositoryItem_1;
            this.string_0 = string_1;
            this.object_0 = object_2;
        }

        public GridEditorItem(RepositoryItem repositoryItem_1, string string_1, object object_2, object object_3)
        {
            this.object_1 = null;
            this.bool_0 = true;
            this.repositoryItem_0 = repositoryItem_1;
            this.string_0 = string_1;
            this.object_0 = object_2;
            this.object_1 = object_3;
        }

        public bool Check
        {
            get { return this.bool_0; }
            set { this.bool_0 = value; }
        }

        public string Name
        {
            get { return this.string_0; }
        }

        public RepositoryItem RepositoryItem
        {
            get { return this.repositoryItem_0; }
            set { this.repositoryItem_0 = value; }
        }

        public object Tag
        {
            get { return this.object_1; }
            set { this.object_1 = value; }
        }

        public object Value
        {
            get { return this.object_0; }
            set { this.object_0 = value; }
        }
    }
}
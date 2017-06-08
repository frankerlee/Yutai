using System.Collections;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal class GraphicHelper
    {
        public object DataSource = null;
        public string HorFieldName = "";
        private IList m_pFiledName = new ArrayList();
        public static GraphicHelper pGraphicHelper = null;
        public string Title = "";

        public void Add(string s)
        {
            this.m_pFiledName.Add(s);
        }

        public void Clear()
        {
            this.m_pFiledName.Clear();
        }

        public static void Init()
        {
            pGraphicHelper = new GraphicHelper();
        }

        public bool Show()
        {
            return true;
        }

        public ICursor Cursor
        {
            get
            {
                if (this.DataSource != null)
                {
                    if (this.DataSource is ISelectionSet)
                    {
                        ICursor cursor;
                        (this.DataSource as ISelectionSet).Search(null, false, out cursor);
                        return cursor;
                    }
                    if (this.DataSource is ITable)
                    {
                        return (this.DataSource as ITable).Search(null, false);
                    }
                }
                return null;
            }
        }

        public IList FiledNames
        {
            get
            {
                return this.m_pFiledName;
            }
        }
    }
}


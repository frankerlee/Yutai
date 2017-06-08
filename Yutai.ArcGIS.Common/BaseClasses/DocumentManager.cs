using System.Collections.Generic;
using System.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class DocumentManager
    {
        private static IList<MxDocument> m_pList;

        static DocumentManager()
        {
            old_acctor_mc();
        }

        public static void DocumentChanged(object object_0)
        {
            try
            {
                MxDocument document;
                if (object_0 is IAppContext)
                {
                    object_0 = (object_0 as IAppContext);
                }
                for (int i = 0; i < m_pList.Count; i++)
                {
                    document = m_pList[i];
                    if (document.Hook == object_0)
                    {
                        goto Label_004F;
                    }
                }
                return;
            Label_004F:
                document.IsDocumentChange = true;
            }
            catch
            {
            }
        }

        public static bool DocumentIsChange(object object_0)
        {
            if (object_0 is IAppContext)
            {
                object_0 = (object_0 as IAppContext);
            }
            for (int i = 0; i < m_pList.Count; i++)
            {
                MxDocument document = m_pList[i];
                if (document.Hook == object_0)
                {
                    return document.IsDocumentChange;
                }
            }
            return false;
        }

        public static MxDocument GetDocument(object object_0)
        {
            if (object_0 is IAppContext)
            {
                object_0 = (object_0 as IAppContext);
            }
            for (int i = 0; i < m_pList.Count; i++)
            {
                MxDocument document = m_pList[i];
                if (document.Hook == object_0)
                {
                    return document;
                }
            }
            return null;
        }

        public static bool NewDocument(object object_0)
        {
            if (object_0 is IAppContext)
            {
                object_0 = (object_0 as IAppContext);
            }
            for (int i = 0; i < m_pList.Count; i++)
            {
                MxDocument document = m_pList[i];
                if (document.Hook == object_0)
                {
                    return document.NewDocument();
                }
            }
            return false;
        }

        private static void old_acctor_mc()
        {
            m_pList = new List<MxDocument>();
        }

        public static bool OpenDocument(object object_0)
        {
            if (object_0 is IAppContext)
            {
                object_0 = (object_0 as IAppContext);
            }
            MxDocument document = null;
            for (int i = 0; i < m_pList.Count; i++)
            {
                document = m_pList[i];
                if (document.Hook == object_0)
                {
                    break;
                }
            }
            OpenFileDialog dialog = new OpenFileDialog {
                CheckFileExists = true,
                Multiselect = false,
                RestoreDirectory = true,
                Filter = "ArcMap Document (*.mxd)|*.mxd|ArcMap Template (*.mxt)|*.mxt|Published Maps (*.pmf)|*.pmf|所有支持Map格式|*.mxd;*.mxt;*.pmf"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                if (document == null)
                {
                    document = new MxDocument(object_0);
                    return document.Open(fileName);
                }
                return document.Open(fileName);
            }
            return false;
        }

        public static void Register(object object_0)
        {
            m_pList.Add(new MxDocument(object_0));
        }

        public static bool SaveDocument(object object_0, string string_0)
        {
            if (object_0 is IAppContext)
            {
                object_0 = (object_0 as IAppContext);
            }
            for (int i = 0; i < m_pList.Count; i++)
            {
                MxDocument document = m_pList[i];
                if (document.Hook == object_0)
                {
                    return document.Save(string_0);
                }
            }
            return false;
        }

        public static bool SaveDocumentAs(object object_0)
        {
            if (object_0 is IAppContext)
            {
                object_0 = (object_0 as IAppContext);
            }
            MxDocument document = null;
            for (int i = 0; i < m_pList.Count; i++)
            {
                document = m_pList[i];
                if (document.Hook == object_0)
                {
                    break;
                }
            }
            if (document != null)
            {
                SaveFileDialog dialog = new SaveFileDialog {
                    OverwritePrompt = true,
                    Title = "保存为",
                    Filter = "ArcMap Document (*.mxd)|*.mxd|ArcMap Template (*.mxt)|*.mxt|Published Maps (*.pmf)|*.pmf|所有支持Map格式|*.mxd;*.mxt;*.pmf",
                    FilterIndex = 0,
                    RestoreDirectory = true,
                    FileName = document.DocumentFilename
                };
                switch (dialog.ShowDialog())
                {
                    case DialogResult.Cancel:
                        return true;

                    case DialogResult.OK:
                        document.Save(dialog.FileName);
                        break;
                }
            }
            return false;
        }

        public static bool UnRegister(object object_0)
        {
            if (object_0 is IAppContext)
            {
                object_0 = (object_0 as IAppContext);
            }
            for (int i = 0; i < m_pList.Count; i++)
            {
                MxDocument document = m_pList[i];
                if (document.Hook == object_0)
                {
                    if (document.Close())
                    {
                        return true;
                    }
                    m_pList.RemoveAt(i);
                    return false;
                }
            }
            return false;
        }

        public static bool IsCancel
        {
            get
            {
                return (m_pList.Count > 0);
            }
        }
    }
}


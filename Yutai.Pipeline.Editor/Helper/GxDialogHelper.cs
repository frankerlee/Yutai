using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Helper
{
    public class GxDialogHelper
    {
        public static IWorkspace SelectPersonalWorkspaceDialog()
        {
            IGxObjectFilter pGxObjectFilter = new GxFilterPersonalGeodatabasesClass();
            IGxDialog pGxDialog = new GxDialogClass();
            pGxDialog.ObjectFilter = pGxObjectFilter;
            pGxDialog.AllowMultiSelect = false;
            pGxDialog.ButtonCaption = "选择";
            pGxDialog.RememberLocation = true;
            pGxDialog.Title = "选择地理数据库";
            IEnumGxObject pEnumGxObject = null;
            if (pGxDialog.DoModalOpen(0, out pEnumGxObject))
            {
                IGxObject pSelectGxObject = pEnumGxObject.Next();
                if (pSelectGxObject is IGxDatabase)
                {
                    IGxDatabase pGxDatabase = pSelectGxObject as IGxDatabase;
                    return pGxDatabase.Workspace;
                }
            }
            return null;
        }

        public static IWorkspace SelectWorkspaceDialog()
        {
            IGxObjectFilter pGxObjectFilter = new GxFilterWorkspacesClass();
            IGxDialog pGxDialog = new GxDialogClass();
            pGxDialog.ObjectFilter = pGxObjectFilter;
            pGxDialog.AllowMultiSelect = false;
            pGxDialog.ButtonCaption = "选择";
            pGxDialog.RememberLocation = true;
            pGxDialog.Title = "选择地理数据库";
            IEnumGxObject pEnumGxObject = null;
            if (pGxDialog.DoModalOpen(0, out pEnumGxObject))
            {
                IGxObject pSelectGxObject = pEnumGxObject.Next();
                if (pSelectGxObject is IGxDatabase)
                {
                    IGxDatabase pGxDatabase = pSelectGxObject as IGxDatabase;
                    return pGxDatabase.Workspace;
                }
            }
            return null;
        }

        public static IGxObject SelectWorkspaceAndDatasetDialog()
        {
            IGxDialog pGxDialog = new GxDialogClass();
            IGxObjectFilterCollection pCollection = pGxDialog as IGxObjectFilterCollection;
            pCollection.AddFilter(new GxFilterWorkspacesClass(), true);
            pCollection.AddFilter(new GxFilterDatasetsClass(), false);
            IEnumGxObject pEnumGxObject;
            pGxDialog.Title = "选择数据";
            pGxDialog.AllowMultiSelect = false;
            if (pGxDialog.DoModalOpen(0, out pEnumGxObject))
            {
                IGxObject pGxObject = pEnumGxObject.Next();
                if (pGxDialog != null)
                {
                    return pGxObject;
                }
            }
            return null;
        }

        public static List<IGxObject> SelectGxObject()
        {
            List<IGxObject> list = new List<IGxObject>();
            IGxDialog gxDialog = new GxDialogClass();
            IGxObjectFilterCollection gxObjectFilterCollection = gxDialog as IGxObjectFilterCollection;
            gxObjectFilterCollection.AddFilter(new GxFilterWorkspacesClass(), true);
            gxObjectFilterCollection.AddFilter(new GxFilterShapefilesClass(), false);
            gxDialog.AllowMultiSelect = true;
            gxDialog.ButtonCaption = "添加";
            gxDialog.RememberLocation = true;
            IEnumGxObject enumGxObject = null;
            if (gxDialog.DoModalOpen(0, out enumGxObject))
            {
                enumGxObject.Reset();
                IGxObject gxObject;
                while ((gxObject = enumGxObject.Next()) != null)
                {
                    list.Add(gxObject);
                }
            }
            return list;
        }

        public static string SelectFolderDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string folderPath = dialog.SelectedPath;
                return folderPath;
            }
            return null;
        }

        public static IFeatureClass SelectFeatureClassDialog()
        {
            IGxDialog pGxDialog = new GxDialogClass();
            pGxDialog.ObjectFilter = new GxFilterFeatureClassesClass();
            pGxDialog.AllowMultiSelect = false;
            pGxDialog.RememberLocation = true;
            IEnumGxObject pEnumGxObject;
            if (pGxDialog.DoModalOpen(0, out pEnumGxObject))
            {
                IGxObject pSelectGxObject = pEnumGxObject.Next();
                IGxDataset pGxDataset = (IGxDataset)pSelectGxObject;
                return pGxDataset.Dataset as IFeatureClass;
            }
            return null;
        }

        public static IFeatureClass SelectFeatureClassDialog(List<IGxObjectFilter> gxFilters)
        {
            IGxDialog pGxDialog = new GxDialogClass();
            IGxObjectFilterCollection pFilterCol = pGxDialog as IGxObjectFilterCollection;
            foreach (IGxObjectFilter gxObjectFilter in gxFilters)
            {
                pFilterCol.AddFilter(gxObjectFilter, true);
            }
            pGxDialog.AllowMultiSelect = false;
            pGxDialog.RememberLocation = true;
            IEnumGxObject pEnumGxObject;
            if (pGxDialog.DoModalOpen(0, out pEnumGxObject))
            {
                IGxObject pSelectGxObject = pEnumGxObject.Next();
                IGxDataset pGxDataset = (IGxDataset)pSelectGxObject;
                return pGxDataset.Dataset as IFeatureClass;
            }
            return null;
        }

        public static List<IFeatureClass> SelectFeatureClassesDialog()
        {
            List<IFeatureClass> list = new List<IFeatureClass>();
            IGxDialog pGxDialog = new GxDialogClass();
            pGxDialog.ObjectFilter = new GxFilterFeatureClassesClass();
            pGxDialog.AllowMultiSelect = true;
            pGxDialog.RememberLocation = true;
            IEnumGxObject pEnumGxObject;
            if (pGxDialog.DoModalOpen(0, out pEnumGxObject))
            {
                IGxObject pSelectGxObject;
                while ((pSelectGxObject = pEnumGxObject.Next()) != null)
                {
                    IGxDataset pGxDataset = (IGxDataset)pSelectGxObject;
                    list.Add(pGxDataset.Dataset as IFeatureClass);
                }
            }

            return list;
        }

        public static string[] SelectTxtFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileNames;
            }
            return null;
        }
    }
}

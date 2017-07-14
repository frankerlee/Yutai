using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcExportPath : UserControl
    {
        private EnumSaveType _saveType;
        private IWorkspace _workspace;
        private IDataset _dataset;

        public UcExportPath()
        {
            InitializeComponent();
        }

        public EnumExportType ExportType
        {
            get
            {
                int type = (int)radioGroup1.EditValue;
                switch (type)
                {
                    case 0:
                        return EnumExportType.FeatureClass;
                    case 1:
                        return EnumExportType.Shapefile;
                    default:
                        return EnumExportType.FeatureClass;
                }
            }
        }

        public EnumSaveType SaveType => _saveType;

        public IWorkspace Workspace => _workspace;

        public IDataset Dataset => _dataset;

        public string Path => this.txtPath.Text;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            switch (ExportType)
            {
                case EnumExportType.FeatureClass:
                    {
                        IGxDialog dialog = new GxDialogClass();
                        IGxObjectFilterCollection filterCollection = dialog as IGxObjectFilterCollection;
                        filterCollection.AddFilter(new GxFilterWorkspacesClass(), true);
                        filterCollection.AddFilter(new GxFilterDatasetsClass(), false);
                        dialog.AllowMultiSelect = false;
                        dialog.ButtonCaption = "选择";
                        dialog.RememberLocation = true;
                        dialog.Title = "选择输出位置";
                        IEnumGxObject selection = null;
                        if (dialog.DoModalOpen(0, out selection))
                        {
                            if (selection == null)
                                return;
                            IGxObject gxObject = selection.Next();
                            if (gxObject is IGxDatabase)
                            {
                                IGxDatabase gxDatabase = gxObject as IGxDatabase;
                                _dataset = null;
                                _workspace = gxDatabase.Workspace;
                                _saveType = EnumSaveType.Workspace;
                            }
                            else if (gxObject is IGxDataset)
                            {
                                IGxDataset gxDataset = gxObject as IGxDataset;
                                _dataset = gxDataset.Dataset;
                                _workspace = _dataset.Workspace;
                                _saveType = EnumSaveType.Dataset;
                            }
                            txtPath.Text = gxObject.FullName;
                        }
                    }
                    break;
                case EnumExportType.Shapefile:
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        dialog.ShowNewFolderButton = true;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            this.txtPath.Text = dialog.SelectedPath;

                            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory");
                            IWorkspaceFactory shapefileWorkspaceFactory = Activator.CreateInstance(factoryType) as IWorkspaceFactory;
                            if (shapefileWorkspaceFactory == null)
                                return;
                            _dataset = null;
                            _workspace = shapefileWorkspaceFactory.OpenFromFile(dialog.SelectedPath, 0);
                            _saveType = EnumSaveType.Workspace;
                        }
                    }
                    break;
            }
        }
    }

    public enum EnumExportType
    {
        FeatureClass = 0,
        Shapefile = 1
    }

    public enum EnumSaveType
    {
        Workspace = 0,
        Dataset = 1
    }
}

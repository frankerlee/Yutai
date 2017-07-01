using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    internal class RepresentationAssist
    {
        public static IRepresentation GetRepresentation(IFeature pf)
        {
            return null;
        }

        public static IRepresentationWorkspaceExtension GetRepWSExt(IWorkspace pWorkspace)
        {
            try
            {
                IWorkspaceExtensionManager manager = pWorkspace as IWorkspaceExtensionManager;
                UID gUID = new UIDClass
                {
                    Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                };
                return (manager.FindExtension(gUID) as IRepresentationWorkspaceExtension);
            }
            catch
            {
            }
            return null;
        }

        public static IRepresentationWorkspaceExtension GetRepWSExtFromFClass(IFeatureClass pfclass)
        {
            try
            {
                IDataset dataset = pfclass as IDataset;
                IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                UID gUID = new UIDClass
                {
                    Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                };
                return (workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension);
            }
            catch
            {
            }
            return null;
        }

        public static bool HasRepresentation(IFeature pf)
        {
            IFeatureClass featureClass = pf.Class as IFeatureClass;
            if (featureClass != null)
            {
                try
                {
                    IDataset dataset = featureClass as IDataset;
                    IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                    UID gUID = new UIDClass
                    {
                        Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                    };
                    IRepresentationWorkspaceExtension extension =
                        workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension;
                    if (extension == null)
                    {
                        return false;
                    }
                    return extension.get_FeatureClassHasRepresentations(featureClass);
                }
                catch
                {
                }
            }
            return false;
        }

        public static bool HasRepresentation(IFeatureClass pfclass)
        {
            if (pfclass != null)
            {
                try
                {
                    IDataset dataset = pfclass as IDataset;
                    IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                    UID gUID = new UIDClass
                    {
                        Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                    };
                    IRepresentationWorkspaceExtension extension =
                        workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension;
                    if (extension == null)
                    {
                        return false;
                    }
                    return extension.get_FeatureClassHasRepresentations(pfclass);
                }
                catch
                {
                }
            }
            return false;
        }
    }
}
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto
{
    public class RepresentationAssist
    {
        internal static IBasicSymbol CreateBasicSymbol(IFeatureClass ifeatureClass_0)
        {
            IBasicSymbol symbol = null;
            if ((ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryMultipoint) || (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint))
            {
                return new BasicMarkerSymbolClass();
            }
            if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                return new BasicLineSymbolClass();
            }
            if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                symbol = new BasicFillSymbolClass();
            }
            return symbol;
        }

        public IRepresentationClass CreateRepClass(IFeatureClass ifeatureClass_0)
        {
            IDataset dataset = ifeatureClass_0 as IDataset;
            IRepresentationWorkspaceExtension repWSExt = GetRepWSExt(dataset.Workspace);
            if (repWSExt != null)
            {
                IRepresentationRules rules = new RepresentationRulesClass();
                return repWSExt.CreateRepresentationClass(ifeatureClass_0, ifeatureClass_0.AliasName + "_Rep", "My_RuleID", "My_Override", false, rules, null);
            }
            return null;
        }

        public static IRepresentationRule CreateRepresentationRule(IFeatureClass ifeatureClass_0)
        {
            IBasicSymbol symbol = CreateBasicSymbol(ifeatureClass_0);
            IRepresentationRule rule = new RepresentationRuleClass();
            rule.InsertLayer(0, symbol);
            return rule;
        }

        public static IRepresentationWorkspaceExtension GetRepWSExt(IWorkspace iworkspace_0)
        {
            try
            {
                IWorkspaceExtensionManager manager = iworkspace_0 as IWorkspaceExtensionManager;
                UID gUID = new UIDClass {
                    Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                };
                return (manager.FindExtension(gUID) as IRepresentationWorkspaceExtension);
            }
            catch
            {
            }
            return null;
        }

        public static IRepresentationWorkspaceExtension GetRepWSExtFromFClass(IFeatureClass ifeatureClass_0)
        {
            try
            {
                IDataset dataset = ifeatureClass_0 as IDataset;
                IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                UID gUID = new UIDClass {
                    Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                };
                return (workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension);
            }
            catch
            {
            }
            return null;
        }

        public static bool HasRepresentation(IFeatureClass ifeatureClass_0)
        {
            if (ifeatureClass_0 != null)
            {
                try
                {
                    IDataset dataset = ifeatureClass_0 as IDataset;
                    IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                    UID gUID = new UIDClass {
                        Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                    };
                    IRepresentationWorkspaceExtension extension = workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension;
                    if (extension == null)
                    {
                        return false;
                    }
                    return extension.get_FeatureClassHasRepresentations(ifeatureClass_0);
                }
                catch
                {
                }
            }
            return false;
        }
    }
}


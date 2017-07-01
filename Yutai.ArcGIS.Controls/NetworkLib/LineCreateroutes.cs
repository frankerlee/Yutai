using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class LineCreateroutes
    {
        public void CreateRoutes(IFeatureClass pLineFC, string InputRouteIDFieldName, string fromMeasureField,
            string toMeasureField)
        {
            try
            {
                IWorkspace selectionContainer = (pLineFC as IDataset).Workspace;
                IFeatureClassName outputFClassName = new FeatureClassNameClass();
                IDataset dataset = (IDataset) selectionContainer;
                IWorkspaceName fullName = (IWorkspaceName) dataset.FullName;
                IDatasetName name3 = (IDatasetName) outputFClassName;
                name3.WorkspaceName = fullName;
                name3.Name = "CreateRoutes";
                IFields fields = pLineFC.Fields;
                int index = fields.FindField(pLineFC.ShapeFieldName);
                IClone geometryDef = (IClone) fields.get_Field(index).GeometryDef;
                IGeometryDef outputGeometryDef = (IGeometryDef) geometryDef.Clone();
                ((ISpatialReference2) outputGeometryDef.SpatialReference).SetMFalseOriginAndUnits(-1000.0, 1000.0);
                IQueryFilter queryFilter = new QueryFilterClass
                {
                    WhereClause = "[ROUTE1] <> 0"
                };
                ISelectionSet2 set =
                    (ISelectionSet2)
                    pLineFC.Select(queryFilter, esriSelectionType.esriSelectionTypeIDSet,
                        esriSelectionOption.esriSelectionOptionNormal, selectionContainer);
                IRouteMeasureCreator creator = new RouteMeasureCreatorClass
                {
                    InputFeatureSelection = set,
                    InputRouteIDFieldName = InputRouteIDFieldName
                };
                IEnumBSTR mbstr = creator.CreateUsing2Fields(fromMeasureField, toMeasureField, outputFClassName,
                    outputGeometryDef, "", null);
                for (string str = mbstr.Next(); str != null; str = mbstr.Next())
                {
                }
            }
            catch (COMException exception)
            {
                MessageBox.Show(exception.Message, "COM Error: " + exception.ErrorCode.ToString(), MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (Exception exception2)
            {
                MessageBox.Show(exception2.Message, ".NET Error: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
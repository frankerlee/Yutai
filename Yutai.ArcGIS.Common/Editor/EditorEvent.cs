using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Editor
{
    public class EditorEvent
    {
        public EditorEvent()
        {
        }

        public static void AddFeature(ILayer ilayer_0, IFeature ifeature_0)
        {
            if (EditorEvent.OnAddFeature != null)
            {
                EditorEvent.OnAddFeature(ilayer_0, ifeature_0);
            }
        }

        public static void AfterNewRow(IRow irow_0)
        {
            if (EditorEvent.OnAfterNewRow != null)
            {
                EditorEvent.OnAfterNewRow(irow_0);
            }
        }

        public static void BeginSaveEditing()
        {
            if (EditorEvent.OnBeginSaveEditing != null)
            {
                EditorEvent.OnBeginSaveEditing();
            }
        }

        public static void BeginStopEditing()
        {
            if (EditorEvent.OnBeginStopEditing != null)
            {
                EditorEvent.OnBeginStopEditing();
            }
        }

        public static void CancelEditing()
        {
            if (EditorEvent.OnCancelEditing != null)
            {
                EditorEvent.OnCancelEditing();
            }
        }

        public static void DeleteFeature(ILayer ilayer_0, int int_0)
        {
            if (EditorEvent.OnDeleteFeature != null)
            {
                EditorEvent.OnDeleteFeature(ilayer_0, int_0);
            }
        }

        public static void EditLayerChange(IFeatureLayer ifeatureLayer_0)
        {
            if (EditorEvent.OnEditLayerChange != null)
            {
                EditorEvent.OnEditLayerChange(ifeatureLayer_0);
            }
        }

        public static void EditTempalateChange(YTEditTemplate jlkeditTemplate_0)
        {
            if (EditorEvent.OnEditTemplateChange != null)
            {
                EditorEvent.OnEditTemplateChange(jlkeditTemplate_0);
            }
        }

        public static void FeatureGeometryChanged(IFeature ifeature_0)
        {
            if (EditorEvent.OnFeatureGeometryChanged != null)
            {
                EditorEvent.OnFeatureGeometryChanged(ifeature_0);
            }
        }

        public static void NewRow(IRow irow_0)
        {
            if (EditorEvent.OnNewRow != null)
            {
                EditorEvent.OnNewRow(irow_0);
            }
        }

        public static void SaveEditing()
        {
            if (EditorEvent.OnSaveEditing != null)
            {
                EditorEvent.OnSaveEditing();
            }
        }

        public static void StartEditing()
        {
            if (EditorEvent.OnStartEditing != null)
            {
                EditorEvent.OnStartEditing();
            }
        }

        public static void StopEditing()
        {
            if (EditorEvent.OnStopEditing != null)
            {
                EditorEvent.OnStopEditing();
            }
        }

        public static event EditorEvent.OnAddFeatureHandler OnAddFeature;

        public static event EditorEvent.OnAfterNewRowHandler OnAfterNewRow;

        public static event EditorEvent.OnBeginSaveEditingHandler OnBeginSaveEditing;

        public static event EditorEvent.OnBeginStopEditingHandler OnBeginStopEditing;

        public static event EditorEvent.OnCancelEditingHandler OnCancelEditing;

        public static event EditorEvent.OnDeleteFeatureHandler OnDeleteFeature;

        public static event EditorEvent.OnEditLayerChangeHandler OnEditLayerChange;

        public static event EditorEvent.OnEditTemplateChangeHandler OnEditTemplateChange;

        public static event EditorEvent.OnFeatureGeometryChangedHandler OnFeatureGeometryChanged;

        public static event EditorEvent.OnNewRowHandler OnNewRow;

        public static event EditorEvent.OnSaveEditingHandler OnSaveEditing;

        public static event EditorEvent.OnStartEditingHandler OnStartEditing;

        public static event EditorEvent.OnStopEditingHandler OnStopEditing;

        public delegate void OnAddFeatureHandler(ILayer ilayer_0, IFeature ifeature_0);

        public delegate void OnAfterNewRowHandler(IRow irow_0);

        public delegate void OnBeginSaveEditingHandler();

        public delegate void OnBeginStopEditingHandler();

        public delegate void OnCancelEditingHandler();

        public delegate void OnDeleteFeatureHandler(ILayer ilayer_0, int int_0);

        public delegate void OnEditLayerChangeHandler(IFeatureLayer ifeatureLayer_0);

        public delegate void OnEditTemplateChangeHandler(YTEditTemplate jlkeditTemplate_0);

        public delegate void OnFeatureGeometryChangedHandler(IFeature ifeature_0);

        public delegate void OnNewRowHandler(IRow irow_0);

        public delegate void OnSaveEditingHandler();

        public delegate void OnStartEditingHandler();

        public delegate void OnStopEditingHandler();
    }
}
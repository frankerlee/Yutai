using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common
{
    public class SnapEnvironment : IEngineSnapEnvironment, ISnapEnvironment
    {
        private IArray iarray_0 = new Array();

        private double double_0 = 10.0;

        private esriEngineSnapToleranceUnits esriEngineSnapToleranceUnits_0 = esriEngineSnapToleranceUnits.esriEngineSnapToleranceMapUnits;

        private IAppContext _appContext = null;

        private bool bool_0 = true;

        public double SnapTolerance
        {
            get
            {
                return this.double_0;
            }
            set
            {
                this.double_0 = value;
            }
        }

        public double MapUnitTolerance
        {
            get
            {
                double result;
                if (this._appContext != null)
                {
                    if (this._appContext.MapControl.Map != null)
                    {
                        if (this.esriEngineSnapToleranceUnits_0 == esriEngineSnapToleranceUnits.esriEngineSnapToleranceMapUnits)
                        {
                            result = this.double_0;
                        }
                        else
                        {
                            result = this.method_0(this._appContext.MapControl.Map as IActiveView, this.double_0);
                        }
                    }
                    else
                    {
                        result = this.double_0;
                    }
                }
                else
                {
                    result = this.double_0;
                }
                return result;
            }
        }

        public bool UseSnap
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public int SnapAgentCount
        {
            get
            {
                return this.iarray_0.Count;
            }
        }

        public IEngineSnapAgent get_SnapAgent(int index)
        {
            return (IEngineSnapAgent)this.iarray_0.Element[index];
        }

        public esriEngineSnapToleranceUnits SnapToleranceUnits
        {
            get
            {
                return this.esriEngineSnapToleranceUnits_0;
            }
            set
            {
                this.esriEngineSnapToleranceUnits_0 = value;
            }
        }

        public IEngineSnapAgent this[int int_0]
        {
            get
            {
                return (IEngineSnapAgent)this.iarray_0.Element[int_0];
            }
        }

        public SnapEnvironment(IAppContext iapplication_1)
        {
            this._appContext = iapplication_1;
        }

        public void AddSnapAgent(IEngineSnapAgent iengineSnapAgent_0)
        {
            this.iarray_0.Add(iengineSnapAgent_0);
        }

        public void ClearSnapAgents()
        {
            this.iarray_0.RemoveAll();
        }

        public void RemoveSnapAgent(int int_0)
        {
            this.iarray_0.Remove(int_0);
        }

        public bool SnapPoint(IPoint ipoint_0)
        {
            bool result;
            if (!this.bool_0)
            {
                result = false;
            }
            else
            {
                double num = this.double_0;
                if (this._appContext != null && this.esriEngineSnapToleranceUnits_0 == esriEngineSnapToleranceUnits.esriEngineSnapTolerancePixels)
                {
                    num = this.method_0(this._appContext.MapControl.Map as IActiveView, this.double_0);
                    if (num == 0.0)
                    {
                        num = this.double_0;
                    }
                }
                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    IEngineSnapAgent engineSnapAgent = (IEngineSnapAgent)this.iarray_0.get_Element(i);
                    IFeatureCache featureCache = null;
                    if (engineSnapAgent is IFeatureSnapAgent)
                    {
                        IFeatureClass featureClass = (engineSnapAgent as IFeatureSnapAgent).FeatureClass;
                        try
                        {
                            featureCache = (hashtable[featureClass] as IFeatureCache);
                            goto IL_10F;
                        }
                        catch
                        {
                            goto IL_10F;
                        }
                        goto IL_C6;
                        IL_E9:
                        (engineSnapAgent as IFeatureSnapAgent).FeatureCache = featureCache;
                        goto IL_F7;
                        IL_10F:
                        if (featureCache != null)
                        {
                            goto IL_E9;
                        }
                        IL_C6:
                        featureCache = new FeatureCache();
                        featureCache.Initialize(ipoint_0, num);
                        featureCache.AddFeatures(featureClass);
                        hashtable.Add(featureClass, featureCache);
                        goto IL_E9;
                    }
                    IL_F7:
                    if (engineSnapAgent.Snap(null, ipoint_0, num))
                    {
                        result = true;
                        return result;
                    }
                }
                hashtable.Clear();
                result = false;
            }
            return result;
        }

        private double method_0(IActiveView iactiveView_0, double double_1)
        {
            int num = iactiveView_0.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right - iactiveView_0.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;
            double result;
            if (num == 0)
            {
                result = double_1;
            }
            else
            {
                double width = iactiveView_0.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
                double num2 = width / (double)num;
                result = double_1 * num2;
            }
            return result;
        }

        public bool SnapPoint(IPoint ipoint_0, IPoint ipoint_1)
        {
            bool result;
            if (!this.bool_0)
            {
                result = false;
            }
            else
            {
                double num = this.double_0;
                if (this._appContext != null && this.esriEngineSnapToleranceUnits_0 == esriEngineSnapToleranceUnits.esriEngineSnapTolerancePixels)
                {
                    num = this.method_0(this._appContext.MapControl.Map as IActiveView, this.double_0);
                    if (num == 0.0)
                    {
                        num = this.double_0;
                    }
                }
                System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
                for (int i = 0; i < this.iarray_0.Count; i++)
                {
                    IEngineSnapAgent engineSnapAgent = (IEngineSnapAgent)this.iarray_0.get_Element(i);
                    IFeatureCache featureCache = null;
                    if (engineSnapAgent is IFeatureSnapAgent)
                    {
                        IFeatureClass featureClass = (engineSnapAgent as IFeatureSnapAgent).FeatureClass;
                        try
                        {
                            featureCache = (hashtable[featureClass] as IFeatureCache);
                            goto IL_10F;
                        }
                        catch
                        {
                            goto IL_10F;
                        }
                        goto IL_C6;
                        IL_E9:
                        (engineSnapAgent as IFeatureSnapAgent).FeatureCache = featureCache;
                        goto IL_F7;
                        IL_10F:
                        if (featureCache != null)
                        {
                            goto IL_E9;
                        }
                        IL_C6:
                        featureCache = new FeatureCache();
                        featureCache.Initialize(ipoint_1, num);
                        featureCache.AddFeatures(featureClass);
                        hashtable.Add(featureClass, featureCache);
                        goto IL_E9;
                    }
                    IL_F7:
                    if (engineSnapAgent.Snap(ipoint_0, ipoint_1, num))
                    {
                        result = true;
                        return result;
                    }
                }
                hashtable.Clear();
                result = false;
            }
            return result;
        }
    }
}
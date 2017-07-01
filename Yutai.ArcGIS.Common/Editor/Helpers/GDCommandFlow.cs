using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class GDCommandFlow : ICommandFlow
    {
        private string string_0 = "下一点:";

        private int int_0 = 0;

        private ICommandFlow icommandFlow_0 = null;

        private IAppContext _appContext = null;

        private bool bool_0 = false;


        public IAppContext AppContext
        {
            set { _appContext = value; }
        }

        public string CurrentCommandInfo
        {
            get { return this.string_0; }
        }

        public bool IsFinished
        {
            get { return this.bool_0; }
        }

        public GDCommandFlow()
        {
        }

        public GDCommandFlow(ICommandFlow icommandFlow_1)
        {
            this.icommandFlow_0 = icommandFlow_1;
        }

        public bool HandleCommand(string string_1)
        {
            bool flag;
            if (!this.bool_0)
            {
                try
                {
                    string_1 = string_1.Trim();
                    if (string_1.Length != 0)
                    {
                        string[] strArrays = string_1.Split(new char[] {','});
                        double num = 0;
                        double num1 = 0;
                        IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
                        if ((int) strArrays.Length < 2)
                        {
                            this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                            flag = true;
                            return flag;
                        }
                        else
                        {
                            num = Convert.ToDouble(strArrays[0]);
                            pointClass.PutCoords(num, Convert.ToDouble(strArrays[1]));
                            if ((int) strArrays.Length == 3)
                            {
                                num1 = Convert.ToDouble(strArrays[2]);
                                pointClass.Z = num1;
                            }
                            this.int_0 = 1;
                            this.bool_0 = true;
                            if (SketchShareEx.LastPoint != null)
                            {
                                int pointCount = SketchToolAssist.m_pPointColn.PointCount;
                                IPoint point = SketchToolAssist.m_pPointColn.Point[pointCount - 2];
                                IPoint point1 = SketchToolAssist.m_pPointColn.Point[pointCount - 1];
                                IPoint gD = CommonHelper.GetGD(point, point1, pointClass);
                                gD.Z = num1;
                                SketchShareEx.SketchMouseDown(gD, this._appContext.MapControl.Map as IActiveView,
                                    Editor.CurrentEditTemplate.FeatureLayer);
                            }
                            SketchShareEx.SketchMouseDown(pointClass, this._appContext.MapControl.Map as IActiveView,
                                Editor.CurrentEditTemplate.FeatureLayer);
                        }
                    }
                    else
                    {
                        flag = false;
                        return flag;
                    }
                }
                catch
                {
                    this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
                    flag = true;
                    return flag;
                }
                if (this.icommandFlow_0 == null)
                {
                    this.ShowCommandLine();
                }
                else
                {
                    this.icommandFlow_0.ShowCommandLine();
                }
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public void Reset()
        {
            this.int_0 = 0;
        }

        public void ShowCommandLine()
        {
            switch (this.int_0)
            {
                case 0:
                {
                    this._appContext.ShowCommandString("隔点:", CommandTipsType.CTTCommandTip);
                    return;
                }
                case 1:
                {
                    this._appContext.ShowCommandString(
                        "下一点或[闭合(C)/撤销(U)/重做(R)/三点弧(A)/切线弧(T)/隔一点(J)/隔点闭合(G)/结束(F)/线反向(D)]:",
                        CommandTipsType.CTTCommandTip);
                    return;
                }
                case 2:
                {
                    return;
                }
                default:
                {
                    return;
                }
            }
        }
    }
}
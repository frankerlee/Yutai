using System.Collections;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
    public class AlignElementsOperation : IAlignElementsOperation, IOperation
    {
        private IEnumElement ienumElement_0 = null;

        private IEnvelope ienvelope_0 = null;

        private IActiveView iactiveView_0 = null;

        private IGraphicsContainer igraphicsContainer_0 = null;

        private IList ilist_0 = new ArrayList();

        private enumAlignType enumAlignType_0 = enumAlignType.esriBottom;

        public IActiveView ActiveView
        {
            set
            {
                this.iactiveView_0 = value;
                this.igraphicsContainer_0 = this.iactiveView_0.GraphicsContainer;
            }
        }

        public IEnvelope AlignEnvelope
        {
            set { this.ienvelope_0 = value; }
        }

        public enumAlignType AlignType
        {
            set { this.enumAlignType_0 = value; }
        }

        public bool CanRedo
        {
            get { return true; }
        }

        public bool CanUndo
        {
            get { return true; }
        }

        public IEnumElement Elements
        {
            set { this.ienumElement_0 = value; }
        }

        public string MenuString
        {
            get { return "对齐"; }
        }

        public AlignElementsOperation()
        {
        }

        public void Do()
        {
            this.method_0();
        }

        private void method_0()
        {
            IPoint point;
            if (CartoLicenseProviderCheck.Check())
            {
                this.ilist_0.Clear();
                IEnvelope envelopeClass = new Envelope() as IEnvelope;
                this.ienumElement_0.Reset();
                for (IElement i = this.ienumElement_0.Next(); i != null; i = this.ienumElement_0.Next())
                {
                    if (this.method_6(i))
                    {
                        i.QueryBounds(this.iactiveView_0.ScreenDisplay, envelopeClass);
                        switch (this.enumAlignType_0)
                        {
                            case enumAlignType.esriTop:
                            {
                                point = this.method_2(envelopeClass);
                                break;
                            }
                            case enumAlignType.esriBottom:
                            {
                                point = this.method_3(envelopeClass);
                                break;
                            }
                            case enumAlignType.esriLeft:
                            {
                                point = this.method_4(envelopeClass);
                                break;
                            }
                            case enumAlignType.esriRight:
                            {
                                point = this.method_5(envelopeClass);
                                break;
                            }
                            default:
                            {
                                return;
                            }
                        }
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, i, null);
                        (i as ITransform2D).Move(point.X, point.Y);
                        this.igraphicsContainer_0.UpdateElement(i);
                        this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, i, null);
                        this.ilist_0.Add(point);
                        ElementChangeEvent.ElementPositionChange(i);
                    }
                }
            }
        }

        private void method_1()
        {
            int num = 0;
            this.ienumElement_0.Reset();
            for (IElement i = this.ienumElement_0.Next(); i != null; i = this.ienumElement_0.Next())
            {
                if (this.method_6(i))
                {
                    IPoint item = this.ilist_0[num] as IPoint;
                    num++;
                    this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, i, null);
                    ITransform2D transform2D = i as ITransform2D;
                    transform2D.Move(-item.X, -item.Y);
                    this.igraphicsContainer_0.UpdateElement(i);
                    this.iactiveView_0.PartialRefresh(esriViewDrawPhase.esriViewGraphics, i, null);
                }
                ElementChangeEvent.ElementPositionChange(i);
            }
        }

        private IPoint method_2(IEnvelope ienvelope_1)
        {
            IPoint pointClass = new Point()
            {
                X = 0
            };
            try
            {
                if (ienvelope_1.IsEmpty)
                {
                    pointClass.Y = 0;
                }
                else
                {
                    pointClass.Y = this.ienvelope_0.YMax - ienvelope_1.YMax;
                }
            }
            catch
            {
                pointClass.Y = 0;
            }
            return pointClass;
        }

        private IPoint method_3(IEnvelope ienvelope_1)
        {
            IPoint pointClass = new Point()
            {
                X = 0
            };
            try
            {
                if (ienvelope_1.IsEmpty)
                {
                    pointClass.Y = 0;
                }
                else
                {
                    pointClass.Y = this.ienvelope_0.YMin - ienvelope_1.YMin;
                }
            }
            catch
            {
                pointClass.Y = 0;
            }
            return pointClass;
        }

        private IPoint method_4(IEnvelope ienvelope_1)
        {
            IPoint pointClass = new Point();
            try
            {
                if (ienvelope_1.IsEmpty)
                {
                    pointClass.X = 0;
                }
                else
                {
                    pointClass.X = this.ienvelope_0.XMin - ienvelope_1.XMin;
                }
            }
            catch
            {
                pointClass.X = 0;
            }
            pointClass.Y = 0;
            return pointClass;
        }

        private IPoint method_5(IEnvelope ienvelope_1)
        {
            IPoint pointClass = new Point();
            try
            {
                if (ienvelope_1.IsEmpty)
                {
                    pointClass.X = 0;
                }
                else
                {
                    pointClass.X = this.ienvelope_0.XMax - ienvelope_1.XMax;
                }
            }
            catch
            {
                pointClass.X = 0;
            }
            pointClass.Y = 0;
            return pointClass;
        }

        private bool method_6(IElement ielement_0)
        {
            bool flag;
            if (ielement_0 is IAnnotationElement)
            {
                IFeature feature = (ielement_0 as IAnnotationElement).Feature;
                if (feature != null)
                {
                    IDataset table = feature.Table as IDataset;
                    if (table != null)
                    {
                        IWorkspaceEdit workspace = table.Workspace as IWorkspaceEdit;
                        if (workspace != null)
                        {
                            flag = (workspace.IsBeingEdited() ? true : false);
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public void Redo()
        {
            this.method_0();
        }

        public void Undo()
        {
            this.method_1();
        }
    }
}
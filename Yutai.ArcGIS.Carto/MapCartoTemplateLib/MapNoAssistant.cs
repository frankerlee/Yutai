using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public abstract class MapNoAssistant
    {
        [CompilerGenerated]
        private string string_0;

        public MapNoAssistant(string string_1)
        {
            this.MapNo = string_1.ToUpper();
        }

        protected IPoint ConstructProjectionPoint(IProjectedCoordinateSystem iprojectedCoordinateSystem_0, double double_0, double double_1)
        {
            IPoint point = new PointClass();
            point.PutCoords(double_0, double_1);
            point.SpatialReference = iprojectedCoordinateSystem_0.GeographicCoordinateSystem;
            point.Project(iprojectedCoordinateSystem_0);
            return point;
        }

        public abstract bool GetBLInfo(out double double_0, out double double_1, out double double_2, out double double_3);
        public virtual List<IPoint> GetProjectCoord(IProjectedCoordinateSystem iprojectedCoordinateSystem_0)
        {
            double num;
            double num2;
            double num3;
            double num4;
            List<IPoint> list = new List<IPoint>();
            this.GetBLInfo(out num, out num2, out num3, out num4);
            list.Add(this.ConstructProjectionPoint(iprojectedCoordinateSystem_0, num2, num));
            list.Add(this.ConstructProjectionPoint(iprojectedCoordinateSystem_0, num2, num + num3));
            list.Add(this.ConstructProjectionPoint(iprojectedCoordinateSystem_0, num2 + num4, num + num3));
            list.Add(this.ConstructProjectionPoint(iprojectedCoordinateSystem_0, num2 + num4, num));
            return list;
        }

        public abstract int GetScale();
        public abstract bool Validate();

        public string MapNo
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.string_0 = value;
            }
        }
    }
}


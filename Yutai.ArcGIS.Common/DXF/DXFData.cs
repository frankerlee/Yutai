using System;
using System.Collections;

namespace Yutai.ArcGIS.Common.DXF
{
    public class DXFData : ICloneable
    {
        public byte bold;

        public int color;

        public int count;

        public float endAngle;

        public int flags;

        public float fScale;

        public float globalWidth;

        public byte hAlign;

        public float height;

        public byte italic;

        public string name;

        public DXFPoint point;

        public DXFPoint point1;

        public DXFPoint point2;

        public DXFPoint point3;

        public ArrayList points;

        public float radius;

        public float rHeight;

        public float rotation;

        public float rWidth;

        public DXFPoint scale;

        public byte selfType;

        public float startAngle;

        public byte style;

        public int tag;

        public string text;

        public float thickness;

        public byte vAlign;

        public DXFData()
        {
            this.text = "";
            this.point = new DXFPoint();
            this.point1 = new DXFPoint();
            this.point2 = new DXFPoint();
            this.point3 = new DXFPoint();
            this.scale = new DXFPoint();
            this.points = new ArrayList();
            this.name = "";
        }

        public void Clear()
        {
            this.tag = 0;
            this.count = 0;
            this.flags = 0;
            this.style = 0;
            this.color = 0;
            this.thickness = 0f;
            this.rotation = 0f;
            this.text = "";
            this.point.X = 0f;
            this.point.Y = 0f;
            this.point.Z = 0f;
            this.point1.X = 0f;
            this.point1.Y = 0f;
            this.point1.Z = 0f;
            this.point2.X = 0f;
            this.point2.Y = 0f;
            this.point2.Z = 0f;
            this.point3.X = 0f;
            this.point3.Y = 0f;
            this.point3.Z = 0f;
            this.radius = 0f;
            this.startAngle = 0f;
            this.endAngle = 0f;
            this.scale.X = 0f;
            this.scale.Y = 0f;
            this.scale.Z = 0f;
            this.hAlign = 0;
            this.vAlign = 0;
            this.rWidth = 0f;
            this.rHeight = 0f;
            this.height = 0f;
            this.fScale = 0f;
            this.points.Clear();
            this.bold = 0;
            this.italic = 0;
            this.name = "";
            this.globalWidth = 0f;
        }

        public object Clone()
        {
            DXFData dXFDatum = new DXFData()
            {
                tag = this.tag,
                count = this.count,
                flags = this.flags,
                style = this.style,
                selfType = this.selfType,
                color = this.color,
                thickness = this.thickness,
                rotation = this.rotation,
                text = (string) this.text.Clone(),
                point = (DXFPoint) this.point.Clone(),
                point1 = (DXFPoint) this.point1.Clone(),
                point2 = (DXFPoint) this.point2.Clone(),
                point3 = (DXFPoint) this.point3.Clone(),
                radius = this.radius,
                startAngle = this.startAngle,
                endAngle = this.endAngle,
                scale = (DXFPoint) this.scale.Clone(),
                hAlign = this.hAlign,
                vAlign = this.vAlign,
                rWidth = this.rWidth,
                rHeight = this.rHeight,
                height = this.height,
                fScale = this.fScale,
                points = new ArrayList(this.points),
                bold = this.bold,
                italic = this.italic,
                name = (string) this.name.Clone(),
                globalWidth = this.globalWidth
            };
            return dXFDatum;
        }
    }
}
namespace JLK.Catalog.VCT
{
    using System;
    using System.Drawing;

    internal class CoAnnotationFeature : AbsCoFeature, ICoAnnotationFeature
    {
        private System.Drawing.Color color_0;
        private CoPointCollection coPointCollection_0;
        private double double_0;
        private System.Drawing.Font font_0;
        private string string_0;

        public CoAnnotationFeature(ICoLayer icoLayer_1) : base(icoLayer_1, CoFeatureType.Annotation)
        {
            this.double_0 = 0.0;
            this.color_0 = System.Drawing.Color.Black;
            this.font_0 = SystemFonts.DefaultFont;
            this.coPointCollection_0 = new CoPointCollection();
        }

        public double Angle
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

        public System.Drawing.Color Color
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
            }
        }

        public System.Drawing.Font Font
        {
            get
            {
                return this.font_0;
            }
            set
            {
                this.font_0 = value;
            }
        }

        public CoPointCollection Point
        {
            get
            {
                return this.coPointCollection_0;
            }
            set
            {
                this.coPointCollection_0 = value;
            }
        }

        public string Text
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}


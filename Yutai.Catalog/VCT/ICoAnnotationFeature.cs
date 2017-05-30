namespace Yutai.Catalog.VCT
{
    using System;
    using System.Drawing;

    public interface ICoAnnotationFeature
    {
        double Angle { get; set; }

        System.Drawing.Color Color { get; set; }

        System.Drawing.Font Font { get; set; }

        CoPointCollection Point { get; set; }

        string Text { get; set; }
    }
}


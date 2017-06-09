namespace Yutai.ArcGIS.Catalog.VCT
{
    public interface ICoAnnotationFeature
    {
        double Angle { get; set; }

        System.Drawing.Color Color { get; set; }

        System.Drawing.Font Font { get; set; }

        CoPointCollection Point { get; set; }

        string Text { get; set; }
    }
}


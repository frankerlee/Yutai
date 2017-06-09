using System.Windows.Forms;

namespace Yutai.ArcGIS.Catalog.VCT
{
    public class ConvertHander
    {
        private CoLayerMapper coLayerMapper_0;

        public void Convert(ICoConvert icoConvert_0, ICoConvert icoConvert_1, ProgressBar progressBar_0)
        {
            if (((icoConvert_0 != null) && (icoConvert_1 != null)) && (icoConvert_0.FeatureCount > 0))
            {
                int num = 0;
                int num2 = 200;
                icoConvert_0.DestConvert = icoConvert_1;
                while (icoConvert_0.NextFeature() != -1)
                {
                    num++;
                    if ((num % num2) == 0)
                    {
                        icoConvert_0.ConvertFlush(this.coLayerMapper_0);
                        icoConvert_0.XpgisLayer.RemoveAllFeature();
                        if (progressBar_0 != null)
                        {
                            if ((progressBar_0.Value + num2) < progressBar_0.Maximum)
                            {
                                progressBar_0.Value += num2;
                            }
                            else
                            {
                                progressBar_0.Value = progressBar_0.Maximum;
                            }
                        }
                        Application.DoEvents();
                    }
                }
                icoConvert_0.ConvertFlush(this.coLayerMapper_0);
                if (progressBar_0 != null)
                {
                    progressBar_0.Value = progressBar_0.Maximum;
                }
                icoConvert_0.XpgisLayer.RemoveAllFeature();
            }
        }

        public CoLayerMapper Mapper
        {
            set
            {
                this.coLayerMapper_0 = value;
            }
        }
    }
}


using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Editor
{
    public class YTEditTemplateFactory
    {
        public YTEditTemplateFactory()
        {
        }

        public static List<YTEditTemplate> Create(IFeatureLayer ifeatureLayer_0)
        {
            YTEditTemplate jLKEditTemplate;
            int i;
            string field;
            List<YTEditTemplate> jLKEditTemplates;
            List<YTEditTemplate> jLKEditTemplates1 = new List<YTEditTemplate>();
            if (ifeatureLayer_0 is IGeoFeatureLayer)
            {
                IGeoFeatureLayer ifeatureLayer0 = ifeatureLayer_0 as IGeoFeatureLayer;
                if (ifeatureLayer0.Renderer is ISimpleRenderer)
                {
                    ISimpleRenderer renderer = ifeatureLayer0.Renderer as ISimpleRenderer;
                    jLKEditTemplate = new YTEditTemplate();
                    jLKEditTemplate.Init(ifeatureLayer_0);
                    jLKEditTemplate.Symbol = (renderer.Symbol as IClone).Clone() as ISymbol;
                    jLKEditTemplate.InitBitmap();
                    jLKEditTemplates1.Add(jLKEditTemplate);
                }
                else if (!(ifeatureLayer0.Renderer is IUniqueValueRenderer))
                {
                    jLKEditTemplate = new YTEditTemplate();
                    jLKEditTemplate.Init(ifeatureLayer_0);
                    jLKEditTemplate.InitBitmap();
                    jLKEditTemplates1.Add(jLKEditTemplate);
                }
                else
                {
                    EditTemplateSchems editTemplateSchem = new EditTemplateSchems();
                    IUniqueValueRenderer uniqueValueRenderer = ifeatureLayer0.Renderer as IUniqueValueRenderer;
                    for (i = 0; i < uniqueValueRenderer.FieldCount; i++)
                    {
                        field = uniqueValueRenderer.Field[i];
                        editTemplateSchem.AddField(field);
                    }
                    EditTemplateSchem symbol = new EditTemplateSchem();
                    for (int j = 0; j < uniqueValueRenderer.ValueCount; j++)
                    {
                        symbol = new EditTemplateSchem();
                        string value = uniqueValueRenderer.Value[j];
                        jLKEditTemplate = new YTEditTemplate();
                        jLKEditTemplate.Init(ifeatureLayer_0);
                        jLKEditTemplate.Symbol = (uniqueValueRenderer.Symbol[value] as IClone).Clone() as ISymbol;
                        jLKEditTemplate.Name = uniqueValueRenderer.Heading[value];
                        symbol.Symbol = jLKEditTemplate.Symbol;
                        symbol.Value = value;
                        symbol.Label = uniqueValueRenderer.Label[value];
                        symbol.Description = uniqueValueRenderer.Description[value];
                        if (uniqueValueRenderer.FieldCount != 1)
                        {
                            string[] strArrays = value.Split(uniqueValueRenderer.FieldDelimiter.ToCharArray());
                            for (i = 0; i < uniqueValueRenderer.FieldCount; i++)
                            {
                                field = uniqueValueRenderer.Field[i];
                                string str = strArrays[i].Trim();
                                if (str.Length > 0)
                                {
                                    jLKEditTemplate.SetFieldValue(field, str);
                                }
                                symbol.Add(field, str);
                            }
                        }
                        else
                        {
                            string field1 = uniqueValueRenderer.Value[0];
                            jLKEditTemplate.SetFieldValue(field1, value);
                            symbol.Add(field1, value);
                        }
                        editTemplateSchem.Add(symbol);
                        jLKEditTemplate.InitBitmap();
                        jLKEditTemplate.EditTemplateSchems = editTemplateSchem;
                        jLKEditTemplates1.Add(jLKEditTemplate);
                    }
                }
            }
            else if (ifeatureLayer_0.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
            {
                IAnnoClass extension = ifeatureLayer_0.FeatureClass.Extension as IAnnoClass;
                ISymbolCollection symbolCollection = extension.SymbolCollection;
                IAnnotateLayerPropertiesCollection2 annoProperties =
                    extension.AnnoProperties as IAnnotateLayerPropertiesCollection2;
                symbolCollection.Reset();
                for (ISymbolIdentifier k = symbolCollection.Next(); k != null; k = symbolCollection.Next())
                {
                    jLKEditTemplate = new YTEditTemplate();
                    jLKEditTemplate.Init(ifeatureLayer_0);
                    jLKEditTemplate.Symbol = (k.Symbol as IClone).Clone() as ISymbol;
                    jLKEditTemplate.Name = annoProperties.Properties[k.ID].Class;
                    jLKEditTemplate.SetFieldValue("SymbolID", k.ID.ToString());
                    jLKEditTemplate.InitBitmap();
                    jLKEditTemplates1.Add(jLKEditTemplate);
                }
            }
            if (jLKEditTemplates1.Count <= 0)
            {
                jLKEditTemplates = null;
            }
            else
            {
                jLKEditTemplates = jLKEditTemplates1;
            }
            return jLKEditTemplates;
        }
    }
}
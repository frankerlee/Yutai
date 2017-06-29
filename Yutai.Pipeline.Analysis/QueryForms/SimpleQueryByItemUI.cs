using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.Pipeline.Config.Helpers;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class SimpleQueryByItemUI : Form
	{
		public IAppContext m_context;

		public IMapControl3 MapControl;

		public IPipelineConfig pPipeCfg;

        private PipelineAnalysisPlugin _plugin;
        public PipelineAnalysisPlugin Plugin
        {
            set
            {
                _plugin = value;
            }
        }
        
		public SimpleQueryByItemUI()
		{
			this.InitializeComponent();
		}

		private void SimpleQueryByItemUI_Load(object sender, EventArgs e)
		{
			this.AutoFlash();
		}

		public void AutoFlash()
		{
			int layerCount = m_context.FocusMap.LayerCount;
			for (int i = 0; i < layerCount; i++)
			{
				ILayer ipLay = m_context.FocusMap.Layer[i];
			    IFunctionLayer functionLayer = pPipeCfg.GetFunctionLayer(enumFunctionLayerType.Item);
                if (functionLayer == null)
                    break;
			    if (this.GetLayer(functionLayer.AutoNames, ipLay))
			        break;
			}
			if (this.SelectLayer == null)
			{
				base.Close();
			}
			else
			{
				this.FillLayerBox();
			}
		}

		private bool GetLayer(string name, ILayer ipLay)
		{
			bool result;
			if (ipLay is IFeatureLayer)
			{
				if (ipLay.Name == name)
				{
					this.SelectLayer = (ipLay as IFeatureLayer);
					result = true;
					return result;
				}
			}
			else if (ipLay is IGroupLayer)
			{
				result = this.GetGroupLayer(pPipeCfg.GetFunctionLayer(enumFunctionLayerType.Item).AutoNames, (IGroupLayer)ipLay);
				return result;
			}
			result = false;
			return result;
		}

		private bool GetGroupLayer(string name, ILayer iGLayer)
		{
			ICompositeLayer compositeLayer = (ICompositeLayer)iGLayer;
			bool result;
			if (compositeLayer == null)
			{
				result = false;
			}
			else
			{
				int count = compositeLayer.Count;
				for (int i = 0; i < count; i++)
				{
					ILayer ipLay = compositeLayer.Layer[i];
					if (this.GetLayer(name, ipLay))
					{
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}

		private void FillLayerBox()
		{
			if (this.SelectLayer == null)
			{
				MessageBox.Show(@"此图层不存在");
			}
			else
			{
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
			    IFunctionLayer layerInfo = pPipeCfg.GetFunctionLayer(featureClass);
				this.comboBox1.Items.Clear();
				string text;
				if (this.radioButton1.Checked)
				{
					text = layerInfo.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.XMMC);
				}
				else
				{
					text = layerInfo.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.XMDW);
                }
				int num = featureClass.Fields.FindField(text);
				if (num == -1)
				{
					MessageBox.Show(@"没有找到字段！");
				}
				else
				{
                    List<string> values = new List<string>();
                    CommonHelper.GetUniqueValues((ITable)featureClass, text, values);
                    this.comboBox1.Items.AddRange(values.ToArray());
                    if (this.comboBox1.Items.Count > 0)
                    {
                        this.comboBox1.SelectedIndex = 0;
                    }
                }
			}
		}
        
		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.FillLayerBox();
		}
        
		private void button1_Click(object sender, EventArgs e)
		{
			if (this.SelectLayer == null)
			{
				MessageBox.Show(@"项目地界层不存在!");
			}
			else
			{
				IFeatureClass featureClass = this.SelectLayer.FeatureClass;
                IFunctionLayer layerInfo = pPipeCfg.GetFunctionLayer(featureClass);
                ISpatialFilter spatialFilter = new SpatialFilter();
				string text = this.comboBox1.Text;
				string text2;
				if (!this.checkBox1.Checked)
				{
					if (this.radioButton1.Checked)
					{
						text2 = $"{layerInfo.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.XMMC)} = ";
					}
					else
					{
						text2 = $"{layerInfo.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.XMDW)} = ";
					}
					text2 += " '";
					text2 += text;
					text2 += "'";
				}
				else
				{
					if (this.radioButton1.Checked)
					{
						text2 = $"{layerInfo.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.XMMC)} LIKE ";
					}
					else
					{
						text2 = $"{layerInfo.GetFieldName(PipeConfigWordHelper.FunctionLayerWorkds.XMDW)} LIKE ";
					}
					IDataset dataset = this.SelectLayer.FeatureClass as IDataset;
					if (dataset.Workspace.Type == (esriWorkspaceType) 2)
					{
						text2 += " '%";
						text2 += text;
						text2 += "%'";
					}
					else
					{
						text2 += " '*";
						text2 += text;
						text2 += "*'";
					}
				}
				if (this.comboBox1.Text == "")
				{
					text2 = "";
				}
				spatialFilter.WhereClause=text2;
				IFeatureCursor pCursor = featureClass.Search(spatialFilter, false);
                //修改为插件事件，因为结果显示窗体为插件拥有。
                _plugin.FireQueryResultChanged(new QueryResultArgs(pCursor, (IFeatureSelection)this.SelectLayer));
            }
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}


    }
}
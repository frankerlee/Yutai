
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.PipeConfig;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public partial class SimpleQueryByCmisUI : Form
	{








		public IGeometry m_Geo;

		public IMapControl3 MapControl;

		public IAppContext m_context;

		public IPipeConfig pPipeCfg;

	public SimpleQueryByCmisUI()
		{
			this.InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.textBox1.Text = null;
			base.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.textBox1.Text == null)
			{
				MessageBox.Show("坐标值不能为空:请依次输入坐标的XY值，其间用逗号隔开");
			}
			else
			{
				string text = this.textBox1.Text;
				int num = text.IndexOf(",");
				if (num < 0)
				{
					MessageBox.Show("请输入分隔符 , !");
				}
				else
				{
					string[] array = text.Split(new char[]
					{
						','
					});
					if (array[0] == string.Empty)
					{
						MessageBox.Show("X坐标不能为空!");
					}
					else
					{
						string value = array[0];
						if (array[1] == string.Empty)
						{
							MessageBox.Show("Y坐标不能为空!");
						}
						else
						{
							string value2 = array[1];
							double num2 = 0.0;
							double num3 = 0.0;
							try
							{
								num2 = Convert.ToDouble(value);
								num3 = Convert.ToDouble(value2);
							}
							catch (Exception)
							{
								MessageBox.Show("输入的坐标值有误,请检查!");
								return;
							}
							IPoint point = new ESRI.ArcGIS.Geometry.Point();
							point.X=(num2);
							point.Y=(num3);
							IEnvelope envelope = new Envelope() as IEnvelope;
							decimal value3 = this.numericUpDown1.Value / 2m;
							double num4 = Convert.ToDouble(value3);
						    double num5 = m_context.ActiveView.Extent.Height/m_context.ActiveView.Extent.Width;
                            envelope.PutCoords(num2 - num4, num3 - num4 * num5, num2 + num4, num3 + num4 * num5);
							m_context.ActiveView.Extent=(envelope);
							m_context.ActiveView.PartialRefresh((esriViewDrawPhase) 8, null, null);
						}
					}
				}
			}
		}

		private void SimpleQueryByCmisUI_Load(object sender, EventArgs e)
		{
		}

		private void SimpleQueryByCmisUI_HelpRequested(object sender, HelpEventArgs hlpevent)
		{
			string url = Application.StartupPath + "\\帮助.chm";
			string parameter = "坐标定位";
			HelpNavigator command = HelpNavigator.KeywordIndex;
			Help.ShowHelp(this, url, command, parameter);
		}
	}
}

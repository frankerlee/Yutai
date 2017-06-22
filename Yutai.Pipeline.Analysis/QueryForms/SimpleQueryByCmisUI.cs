using ApplicationData;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using PipeConfig;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.QueryForms
{
	public class SimpleQueryByCmisUI : Form
	{
		private IContainer components = null;

		private TextBox textBox1;

		private Label label1;

		private Button button1;

		private Button button2;

		private Label label2;

		private NumericUpDown numericUpDown1;

		private Label label3;

		public IGeometry m_Geo;

		public IMapControl3 MapControl;

		public IAppContext m_context;

		public IPipeConfig pPipeCfg;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.textBox1 = new TextBox();
			this.label1 = new Label();
			this.button1 = new Button();
			this.button2 = new Button();
			this.label2 = new Label();
			this.numericUpDown1 = new NumericUpDown();
			this.label3 = new Label();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			base.SuspendLayout();
			this.textBox1.Location = new System.Drawing.Point(77, 8);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(211, 21);
			this.textBox1.TabIndex = 2;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 11);
			this.label1.Name = "label1";
			this.label1.Size = new Size(59, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "坐标值X,Y";
			this.button1.Location = new System.Drawing.Point(58, 59);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "查询";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new System.Drawing.Point(184, 59);
			this.button2.Name = "button2";
			this.button2.Size = new Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "取消";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 36);
			this.label2.Name = "label2";
			this.label2.Size = new Size(53, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "视口尺寸";
			int[] array = new int[4];
			array[0] = 10;
			this.numericUpDown1.Increment = new decimal(array);
			this.numericUpDown1.Location = new System.Drawing.Point(80, 33);
			int[] array2 = new int[4];
			array2[0] = 200;
			this.numericUpDown1.Maximum = new decimal(array2);
			int[] array3 = new int[4];
			array3[0] = 10;
			this.numericUpDown1.Minimum = new decimal(array3);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new Size(53, 21);
			this.numericUpDown1.TabIndex = 6;
			this.numericUpDown1.TextAlign = HorizontalAlignment.Center;
			int[] array4 = new int[4];
			array4[0] = 50;
			this.numericUpDown1.Value = new decimal(array4);
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(139, 36);
			this.label3.Name = "label3";
			this.label3.Size = new Size(131, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "坐标值X,Y间用逗号隔开";
			base.AcceptButton = this.button1;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(292, 90);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.numericUpDown1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.Name = "SimpleQueryByCmisUI";
			base.ShowIcon = false;
			this.Text = "按坐标点查询";
			base.Load += new EventHandler(this.SimpleQueryByCmisUI_Load);
			base.HelpRequested += new HelpEventHandler(this.SimpleQueryByCmisUI_HelpRequested);
			((ISupportInitialize)this.numericUpDown1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

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

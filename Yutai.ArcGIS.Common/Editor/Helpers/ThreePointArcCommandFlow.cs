using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
	public class ThreePointArcCommandFlow : ICommandFlow
	{
		private IAppContext _appContext = null;

		private string string_0 = "第一点";

		private int int_0 = 0;

		private ICommandFlow icommandFlow_0 = null;

		private bool bool_0 = false;

		public IAppContext AppContext
		{
			set
			{
				this._appContext = value;
			}
		}

		public string CurrentCommandInfo
		{
			get
			{
				return this.string_0;
			}
		}

		public bool IsFinished
		{
			get
			{
				return this.bool_0;
			}
		}

		public ThreePointArcCommandFlow()
		{
		}

		public ThreePointArcCommandFlow(ICommandFlow icommandFlow_1)
		{
			this.icommandFlow_0 = icommandFlow_1;
		}

		public bool HandleCommand(string string_1)
		{
			bool flag;
			if (!this.bool_0)
			{
				try
				{
					string_1 = string_1.Trim();
					if (string_1.Length != 0)
					{
						string[] strArrays = string_1.Split(new char[] { ',' });
						double num = 0;
						IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
						if ((int)strArrays.Length < 2)
						{
							this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
							flag = true;
							return flag;
						}
						else
						{
							num = Convert.ToDouble(strArrays[0]);
							pointClass.PutCoords(num, Convert.ToDouble(strArrays[1]));
							if ((int)strArrays.Length == 3)
							{
								pointClass.Z = Convert.ToDouble(strArrays[2]);
							}
							if (this.int_0 == 1)
							{
								this.bool_0 = true;
							}
							SketchShareEx.SketchMouseDown(pointClass, this._appContext.MapControl.Map as IActiveView, Editor.CurrentEditTemplate.FeatureLayer);
							ThreePointArcCommandFlow int0 = this;
							int0.int_0 = int0.int_0 + 1;
						}
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				catch
				{
					this._appContext.ShowCommandString("输入不正确", CommandTipsType.CTTLog);
					flag = true;
					return flag;
				}
				this.ShowCommandLine();
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public void Reset()
		{
			this.int_0 = 0;
		}

		public void ShowCommandLine()
		{
			if (!this.IsFinished)
			{
				switch (this.int_0)
				{
					case 0:
					{
						if (SketchShareEx.LastPoint == null)
						{
							break;
						}
						this._appContext.ShowCommandString("弧终点:", CommandTipsType.CTTCommandTip);
						break;
					}
					case 1:
					{
						this._appContext.ShowCommandString("弧段中间点:", CommandTipsType.CTTCommandTip);
						break;
					}
					default:
					{
						this._appContext.ShowCommandString("下一点或[闭合(C)/撤销(U)/重做(R)/三点弧(A)/切线弧(T)/隔一点(J)/隔点闭合(G)/结束(F)/线反向(D)]:", CommandTipsType.CTTCommandTip);
						break;
					}
				}
			}
			else
			{
				this._appContext.ShowCommandString("下一点或[闭合(C)/撤销(U)/重做(R)/三点弧(A)/切线弧(T)/隔一点(J)/隔点闭合(G)/结束(F)/线反向(D)]:", CommandTipsType.CTTCommandTip);
			}
		}
	}
}
using System;
using System.Collections;
using System.Drawing;

namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFMText : DXFFigure
	{
		public string Text
		{
			get
			{
				return this.data.text;
			}
			set
			{
				this.data.text = (string)value.Clone();
			}
		}

		public DXFMText()
		{
		}

		public DXFMText(DXFData aData) : base(aData)
		{
		}

		public override void ExportAsDXF(DXFExport ADXFExport)
		{
			ADXFExport.AddName("MTEXT", "AcDbMText");
			ADXFExport.AddColor(this.data);
			ADXFExport.Add3DPoint(10, this.data.point);
			ADXFExport.AddFloat(40, ADXFExport.MM(this.data.height));
			if (this.data.rotation != 0f)
			{
				ADXFExport.AddFloat(50, this.data.rotation);
			}
			if (this.data.hAlign != 0)
			{
				ADXFExport.AddInt(71, this.data.hAlign + 1);
			}
			ADXFExport.current.Add("  1");
			ADXFExport.current.Add(this.data.text);
		}

		public override bool IntersecRect(Rect aRect)
		{
			Rect x1 = new Rect();
			if (this.data.rotation == 0f)
			{
				x1.X1 = (int)Math.Round((double)this.data.point.X);
				x1.Y1 = (int)Math.Round((double)this.data.point.Y);
				x1.X2 = x1.X1 + (int)Math.Round((double)this.data.rWidth);
				x1.Y2 = x1.Y1 - (int)Math.Round((double)this.data.height);
			}
			if (this.data.rotation == 90f)
			{
				x1.X1 = (int)Math.Round((double)this.data.point.X);
				x1.Y1 = (int)Math.Round((double)this.data.point.Y);
				x1.X2 = x1.X1 + (int)Math.Round((double)this.data.height);
				x1.Y2 = x1.Y1 + (int)Math.Round((double)this.data.rWidth);
			}
			if (this.data.rotation == 270f)
			{
				x1.X1 = (int)Math.Round((double)this.data.point.X);
				x1.Y1 = (int)Math.Round((double)this.data.point.Y);
				x1.X2 = x1.X1 - (int)Math.Round((double)this.data.height);
				x1.Y2 = x1.Y1 - (int)Math.Round((double)this.data.rWidth);
			}
			if (x1.X2 < x1.X1)
			{
				int x2 = x1.X2;
				x1.X2 = x1.X1;
				x1.X1 = x2;
			}
			if (x1.Y2 < x1.Y1)
			{
				int y2 = x1.Y2;
				x1.Y2 = x1.Y1;
				x1.Y1 = y2;
			}
			Rectangle rectangle = new Rectangle(0, 0, 0, 0);
			rectangle = Rectangle.Intersect(new Rectangle(aRect.X1, aRect.Y1, aRect.X2, aRect.Y2), new Rectangle(x1.X1, x1.Y1, x1.X2, x1.Y2));
			if (rectangle.X <= 0 && rectangle.Y <= 0 && rectangle.Width <= 0 && rectangle.Height <= 0)
			{
				return false;
			}
			return true;
		}

		public void ParseToTexts(Rect aRect, ArrayList NewElemes)
		{
			int num;
			float x;
			string pourStr = "";
			string str = "";
			string str1 = "";
			DXFData dXFDatum = new DXFData();
			if (this.data.rotation == 0f)
			{
				dXFDatum = (DXFData)this.data.Clone();
				if ((float)aRect.X1 > dXFDatum.point.X)
				{
					pourStr = DXFExport.GetPourStr(dXFDatum.text);
					str1 = dXFDatum.text.Substring(0, dXFDatum.text.Length - pourStr.Length);
					num = (int)Math.Round((double)((float)pourStr.Length * (((float)aRect.X1 - dXFDatum.point.X) / dXFDatum.rWidth)));
					for (int i = 0; i < num; i++)
					{
						str = string.Concat(str, pourStr[i]);
					}
					pourStr = string.Concat(str1, str);
					dXFDatum.rWidth = this.data.rWidth * (float)num / (float)pourStr.Length;
					dXFDatum.text = pourStr;
					NewElemes.Add(new DXFMText(dXFDatum));
				}
				dXFDatum = (DXFData)this.data.Clone();
				str = "";
				if ((float)aRect.X2 < dXFDatum.point.X + dXFDatum.rWidth)
				{
					pourStr = DXFExport.GetPourStr(dXFDatum.text);
					str1 = dXFDatum.text.Substring(0, dXFDatum.text.Length - pourStr.Length);
					x = (dXFDatum.point.X + dXFDatum.rWidth - (float)aRect.X2) / dXFDatum.rWidth;
					num = (int)Math.Round((double)((float)pourStr.Length * x));
					DXFPoint dXFPoint = dXFDatum.point;
					dXFPoint.X = dXFPoint.X + (dXFDatum.rWidth - dXFDatum.rWidth * x);
					for (int j = pourStr.Length; j < pourStr.Length - num + 1; j--)
					{
						str = string.Concat(pourStr[j], str);
					}
					pourStr = string.Concat(str1, str);
					dXFDatum.rWidth = dXFDatum.rWidth * (float)num / (float)pourStr.Length;
					dXFDatum.text = pourStr;
					NewElemes.Add(new DXFMText(dXFDatum));
				}
			}
			if (this.data.rotation == 90f)
			{
				dXFDatum = (DXFData)this.data.Clone();
				if ((float)aRect.X1 > dXFDatum.point.X)
				{
					pourStr = DXFExport.GetPourStr(dXFDatum.text);
					str1 = dXFDatum.text.Substring(0, dXFDatum.text.Length - pourStr.Length);
					num = (int)Math.Round((double)((float)pourStr.Length * (((float)aRect.X1 - dXFDatum.point.X) / dXFDatum.rWidth)));
					for (int k = 0; k < num; k++)
					{
						str = string.Concat(str, pourStr[k]);
					}
					pourStr = string.Concat(str1, str);
					dXFDatum.rWidth = dXFDatum.rWidth * (float)num / (float)pourStr.Length;
					dXFDatum.text = pourStr;
					NewElemes.Add(new DXFMText(dXFDatum));
				}
				dXFDatum = (DXFData)this.data.Clone();
				str = "";
				if ((float)aRect.X2 < dXFDatum.point.X + dXFDatum.rWidth)
				{
					pourStr = DXFExport.GetPourStr(dXFDatum.text);
					str1 = dXFDatum.text.Substring(0, dXFDatum.text.Length - pourStr.Length);
					x = (dXFDatum.point.X + dXFDatum.rWidth - (float)aRect.X2) / dXFDatum.rWidth;
					num = (int)Math.Round((double)((float)pourStr.Length * x));
					DXFPoint x1 = dXFDatum.point;
					x1.X = x1.X + (dXFDatum.rWidth - dXFDatum.rWidth * x);
					DXFPoint y = dXFDatum.point;
					y.Y = y.Y + (dXFDatum.rWidth - dXFDatum.rWidth * x);
					for (int l = pourStr.Length; l < pourStr.Length - num + 1; l--)
					{
						str = string.Concat(pourStr[l], str);
					}
					pourStr = string.Concat(str1, str);
					dXFDatum.rWidth = dXFDatum.rWidth * (float)num / (float)pourStr.Length;
					dXFDatum.text = pourStr;
					NewElemes.Add(new DXFMText(dXFDatum));
				}
			}
			if (this.data.rotation == 270f)
			{
				dXFDatum = (DXFData)this.data.Clone();
				if ((float)aRect.Y2 < dXFDatum.point.Y)
				{
					pourStr = DXFExport.GetPourStr(dXFDatum.text);
					str1 = dXFDatum.text.Substring(0, dXFDatum.text.Length - pourStr.Length);
					num = (int)Math.Round((double)((float)pourStr.Length * ((dXFDatum.point.Y - (float)aRect.Y2) / dXFDatum.rWidth)));
					for (int m = 0; m < num; m++)
					{
						str = string.Concat(str, pourStr[m]);
					}
					dXFDatum.rWidth = dXFDatum.rWidth * (float)num / (float)pourStr.Length;
					pourStr = string.Concat(str1, str);
					dXFDatum.text = pourStr;
					NewElemes.Add(new DXFMText(dXFDatum));
				}
				dXFDatum = this.data;
				str = "";
				if ((float)aRect.Y1 > dXFDatum.point.Y - dXFDatum.rWidth)
				{
					pourStr = DXFExport.GetPourStr(dXFDatum.text);
					str1 = dXFDatum.text.Substring(0, dXFDatum.text.Length - pourStr.Length);
					x = (dXFDatum.rWidth - dXFDatum.point.Y + (float)aRect.Y1) / dXFDatum.rWidth;
					num = (int)Math.Round((double)((float)pourStr.Length * x));
					DXFPoint y1 = dXFDatum.point;
					y1.Y = y1.Y - (dXFDatum.rWidth - dXFDatum.rWidth * x);
					for (int n = pourStr.Length; n < pourStr.Length - num + 1; n--)
					{
						str = string.Concat(pourStr[n], str);
					}
					dXFDatum.rWidth = dXFDatum.rWidth * (float)num / (float)pourStr.Length;
					pourStr = string.Concat(str1, str);
					dXFDatum.text = pourStr;
					NewElemes.Add(new DXFMText(dXFDatum));
				}
			}
		}
	}
}
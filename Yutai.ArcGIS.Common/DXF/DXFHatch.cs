using System.Collections;

namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFHatch : DXFFigure
	{
		protected int bndAmount;

		protected ArrayList boundaries;

		protected HatchBoundaryType boundaryType;

		protected string patternName;

		private HatchStyle style;

		public int BndAmount
		{
			get
			{
				return this.boundaries.Count;
			}
		}

		public string PatternName
		{
			get
			{
				return this.PatternName;
			}
			set
			{
				this.patternName = value;
			}
		}

		public HatchStyle Style
		{
			get
			{
				return this.style;
			}
		}

		public DXFHatch()
		{
			this.boundaries = new ArrayList();
			this.patternName = "";
		}

		public DXFHatch(DXFData aData) : base(aData)
		{
			int j;
			this.boundaries = new ArrayList();
			this.patternName = "";
			this.boundaryType = (HatchBoundaryType)aData.selfType;
			DXFPoint dXFPoint = new DXFPoint();
			base.Layer = "0";
			this.SetStyle(aData);
			this.boundaries.Clear();
			switch (this.boundaryType)
			{
				case HatchBoundaryType.hbtPolyPolyline:
				{
					if (aData.count > 0)
					{
						for (int i = 0; i < aData.points.Count; i++)
						{
							if (((ArrayList)aData.points[i]).Count >= 3)
							{
								this.boundaries.Add(new ArrayList());
								for (j = 0; j < ((ArrayList)aData.points[i]).Count; j++)
								{
									dXFPoint = (DXFPoint)((ArrayList)aData.points[i])[j];
									if (j < 2 || dXFPoint.X != ((DXFPoint)((ArrayList)aData.points[i])[j - 2]).X || dXFPoint.Y != ((DXFPoint)((ArrayList)aData.points[i])[j - 2]).Y)
									{
										((ArrayList)this.boundaries[this.boundaries.Count - 1]).Add(dXFPoint.Clone());
									}
									else
									{
										dXFPoint = null;
									}
								}
								if (((ArrayList)this.boundaries[this.boundaries.Count - 1]).Count < 3)
								{
									for (j = 0; j < ((ArrayList)this.boundaries[this.boundaries.Count - 1]).Count - 1; j++)
									{
										((ArrayList)this.boundaries[this.boundaries.Count - 1])[j] = null;
									}
									this.boundaries.Remove((ArrayList)this.boundaries[this.boundaries.Count - 1]);
								}
							}
						}
					}
					return;
				}
				case HatchBoundaryType.hbtCircle:
				{
					this.bndAmount = 1;
					return;
				}
				case HatchBoundaryType.hbtEllipse:
				{
					this.bndAmount = 1;
					return;
				}
			}
			this.bndAmount = 0;
		}

		private void AddBoundaryPathData(DXFExport ADXFExport)
		{
			switch (this.boundaryType)
			{
				case HatchBoundaryType.hbtPolyPolyline:
				{
					ADXFExport.AddInt(91, this.boundaries.Count);
					for (int i = 0; i < this.boundaries.Count; i++)
					{
						ADXFExport.AddInt(92, 1);
						ADXFExport.AddInt(93, ((ArrayList)this.boundaries[i]).Count);
						for (int j = 1; j < ((ArrayList)this.boundaries[i]).Count; j++)
						{
							ADXFExport.AddInt(72, 1);
							ADXFExport.Add3DPoint(10, (DXFPoint)((ArrayList)this.boundaries[i])[j - 1]);
							ADXFExport.Add3DPoint(11, (DXFPoint)((ArrayList)this.boundaries[i])[j]);
						}
						ADXFExport.AddInt(72, 1);
						ADXFExport.Add3DPoint(10, (DXFPoint)((ArrayList)this.boundaries[i])[((ArrayList)this.boundaries[i]).Count - 1]);
						ADXFExport.Add3DPoint(11, (DXFPoint)((ArrayList)this.boundaries[i])[0]);
						ADXFExport.AddInt(97, 0);
					}
					return;
				}
				case HatchBoundaryType.hbtCircle:
				{
					ADXFExport.AddInt(91, 1);
					ADXFExport.AddInt(92, 1);
					ADXFExport.AddInt(93, 1);
					ADXFExport.AddInt(72, 2);
					ADXFExport.Add3DPoint(10, this.data.point);
					ADXFExport.AddFloat(40, ADXFExport.MM(this.data.radius));
					ADXFExport.AddFloat(50, this.data.startAngle);
					ADXFExport.AddFloat(51, this.data.endAngle);
					ADXFExport.AddInt(73, 1);
					ADXFExport.AddInt(97, 0);
					return;
				}
				case HatchBoundaryType.hbtEllipse:
				{
					ADXFExport.AddInt(91, 1);
					ADXFExport.AddInt(92, 1);
					ADXFExport.AddInt(93, 1);
					ADXFExport.AddInt(72, 3);
					ADXFExport.Add3DPoint(10, this.data.point);
					ADXFExport.Add3DPoint(11, this.data.point1);
					ADXFExport.AddFloat(40, this.data.radius);
					ADXFExport.AddFloat(50, this.data.startAngle);
					ADXFExport.AddFloat(51, this.data.endAngle);
					ADXFExport.AddInt(73, 1);
					ADXFExport.AddInt(97, 0);
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void AddPatternData(float offset, DXFExport aDXFExport)
		{
			DXFHatchPatternData[] dXFHatchPatternDataArray = new DXFHatchPatternData[2];
			int num = 1;
			dXFHatchPatternDataArray[0].basePointX = 0f;
			dXFHatchPatternDataArray[0].basePointY = 0f;
			dXFHatchPatternDataArray[0].offsetX = -offset;
			dXFHatchPatternDataArray[0].offsetY = offset;
			dXFHatchPatternDataArray[1].basePointX = 0f;
			dXFHatchPatternDataArray[1].basePointY = 0f;
			dXFHatchPatternDataArray[1].offsetX = -offset;
			dXFHatchPatternDataArray[1].offsetY = offset;
			switch (this.Style)
			{
				case HatchStyle.hsHorizontal:
				{
					dXFHatchPatternDataArray[0].angle = 0f;
					dXFHatchPatternDataArray[0].offsetX = 0f;
					break;
				}
				case HatchStyle.hsVertical:
				{
					dXFHatchPatternDataArray[0].angle = 90f;
					dXFHatchPatternDataArray[0].offsetY = 0f;
					break;
				}
				case HatchStyle.hsFDiagonal:
				{
					dXFHatchPatternDataArray[0].angle = 135f;
					dXFHatchPatternDataArray[0].offsetY = -dXFHatchPatternDataArray[0].offsetY;
					break;
				}
				case HatchStyle.hsBDiagonal:
				{
					dXFHatchPatternDataArray[0].angle = 45f;
					break;
				}
				case HatchStyle.hsCross:
				{
					num = 2;
					dXFHatchPatternDataArray[0].angle = 0f;
					dXFHatchPatternDataArray[0].offsetX = 0f;
					dXFHatchPatternDataArray[1].angle = 90f;
					break;
				}
				case HatchStyle.hsDiagCross:
				{
					num = 2;
					dXFHatchPatternDataArray[0].angle = 45f;
					dXFHatchPatternDataArray[1].angle = 135f;
					dXFHatchPatternDataArray[1].offsetY = -dXFHatchPatternDataArray[0].offsetY;
					break;
				}
			}
			aDXFExport.AddFloat(52, 0f);
			aDXFExport.AddFloat(41, 1f);
			aDXFExport.AddInt(77, 0);
			aDXFExport.AddInt(78, num);
			for (int i = 0; i < num; i++)
			{
				aDXFExport.AddFloat(53, dXFHatchPatternDataArray[i].angle);
				aDXFExport.AddFloat(43, dXFHatchPatternDataArray[i].basePointX);
				aDXFExport.AddFloat(44, dXFHatchPatternDataArray[i].basePointY);
				aDXFExport.AddFloat(45, dXFHatchPatternDataArray[i].offsetX);
				aDXFExport.AddFloat(46, dXFHatchPatternDataArray[i].offsetY);
				aDXFExport.AddInt(79, 0);
			}
		}

		public override void ExportAsDXF(DXFExport ADXFExport)
		{
			DXFPoint dXFPoint = new DXFPoint();
			if ((this.boundaries != null || this.boundaryType != HatchBoundaryType.hbtPolyPolyline) && (this.boundaries.Count != 0 || this.boundaryType != HatchBoundaryType.hbtPolyPolyline))
			{
				dXFPoint.X = 0f;
				dXFPoint.Y = 0f;
				float aDXFExport = 0.05f;
				if (!DXFExport.use01MM)
				{
					aDXFExport = aDXFExport * ADXFExport.fOffset;
				}
				ADXFExport.AddName(DXFTables.sHatchEntity, "AcDbHatch");
				ADXFExport.AddColor(this.data);
				ADXFExport.Add3DPoint(10, dXFPoint);
				ADXFExport.AddFloat(30, 0f);
				ADXFExport.AddFloat(210, 0f);
				ADXFExport.AddFloat(220, 0f);
				ADXFExport.AddFloat(230, 1f);
				ADXFExport.AddString(2, this.patternName);
				ADXFExport.AddInt(70, this.data.flags);
				ADXFExport.AddInt(71, 0);
				this.AddBoundaryPathData(ADXFExport);
				ADXFExport.AddInt(75, 0);
				ADXFExport.AddInt(76, 1);
				if (this.data.flags == 0)
				{
					this.AddPatternData(aDXFExport, ADXFExport);
				}
				ADXFExport.AddInt(98, 1);
				ADXFExport.AddPoint(10, dXFPoint);
			}
		}

		public DXFPoint GetPoint(int bndIndex, int aIndex)
		{
			return new DXFPoint(((DXFPoint)((ArrayList)this.boundaries[bndIndex])[aIndex]).X, ((DXFPoint)((ArrayList)this.boundaries[bndIndex])[aIndex]).Y, ((DXFPoint)((ArrayList)this.boundaries[bndIndex])[aIndex]).Z);
		}

		public int GetPointsNumber(int bndIndex)
		{
			return ((ArrayList)this.boundaries[bndIndex]).Count;
		}

		protected void SetStyle(DXFData Data)
		{
			this.style = (HatchStyle)this.data.style;
			this.patternName = DXFTables.sPatternSOLID;
			this.data.flags = 1;
			if (this.style != HatchStyle.hsSolid)
			{
				this.data.flags = 0;
				switch (this.style)
				{
					case HatchStyle.hsHorizontal:
					{
						this.PatternName = DXFTables.sPatternLINE;
						return;
					}
					case HatchStyle.hsVertical:
					{
						this.PatternName = DXFTables.sPatternLINE;
						return;
					}
					case HatchStyle.hsFDiagonal:
					{
						this.PatternName = DXFTables.sPatternANSI31;
						return;
					}
					case HatchStyle.hsBDiagonal:
					{
						this.PatternName = DXFTables.sPatternANSI31;
						return;
					}
					case HatchStyle.hsCross:
					{
						this.PatternName = DXFTables.sPatternNET;
						return;
					}
					case HatchStyle.hsDiagCross:
					{
						this.PatternName = DXFTables.sPatternANSI37;
						break;
					}
					default:
					{
						return;
					}
				}
			}
		}
	}
}
using System;

namespace Yutai.ArcGIS.Common.DXF
{
	public class DXFLayer : DXFFigure, ICloneable
	{
		protected int color;

		protected byte flags;

		private string handle;

		protected string hardPointer;

		protected string lineTypeName;

		protected int lineWeight;

		protected string name;

		private byte plottingFlag;

		public int DXFColor
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		public byte Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		public string Handle
		{
			get
			{
				return this.handle;
			}
			set
			{
				this.handle = value;
			}
		}

		public string HardPointer
		{
			get
			{
				return this.hardPointer;
			}
			set
			{
				this.hardPointer = value;
			}
		}

		public string LinetypeName
		{
			get
			{
				return this.lineTypeName;
			}
			set
			{
				this.lineTypeName = value;
			}
		}

		public int LineWeight
		{
			get
			{
				return this.lineWeight;
			}
			set
			{
				this.lineWeight = value;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public byte PlottingFlag
		{
			get
			{
				return this.plottingFlag;
			}
			set
			{
				this.plottingFlag = value;
			}
		}

		public DXFLayer()
		{
		}

		public DXFLayer(string aName)
		{
			this.name = aName;
			this.flags = 0;
			this.color = 7;
			this.lineTypeName = "Continuous";
			this.lineWeight = -3;
		}

		public object Clone()
		{
			DXFLayer dXFLayer = new DXFLayer()
			{
				name = this.name,
				handle = this.handle,
				color = this.color,
				plottingFlag = this.plottingFlag,
				lineTypeName = this.lineTypeName,
				hardPointer = this.hardPointer,
				flags = this.flags,
				lineWeight = this.lineWeight
			};
			return dXFLayer;
		}

		public override void ExportAsDXF(DXFExport ADXFExport)
		{
			ADXFExport.AddString(0, "LAYER");
			ADXFExport.AddHandle();
			ADXFExport.AddString(330, DXFTables.STablesLAYER[7]);
			ADXFExport.AddString(100, "AcDbSymbolTableRecord");
			ADXFExport.AddString(100, "AcDbLayerTableRecord");
			ADXFExport.AddString(2, this.name);
			ADXFExport.AddInt(70, (int)this.flags);
			ADXFExport.AddInt(62, this.color);
			ADXFExport.AddString(6, this.lineTypeName);
			if (DXFExport.autoCADVer != AutoCADVersion.R14)
			{
				ADXFExport.AddInt(370, this.lineWeight);
				ADXFExport.AddString(390, DXFTables.SObjects_R2000[95]);
			}
		}
	}
}
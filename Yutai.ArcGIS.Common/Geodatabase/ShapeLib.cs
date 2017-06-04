using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Yutai.ArcGIS.Common.Geodatabase
{
	public class ShapeLib
	{
		private ShapeLib()
		{
		}

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFAddField(IntPtr intptr_0, string string_0, ShapeLib.DBFFieldType dbffieldType_0, int int_0, int int_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr DBFCloneEmpty(IntPtr intptr_0, string string_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern void DBFClose(IntPtr intptr_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr DBFCreate(string string_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFGetFieldCount(IntPtr intptr_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFGetFieldIndex(IntPtr intptr_0, string string_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern ShapeLib.DBFFieldType DBFGetFieldInfo(IntPtr intptr_0, int int_0, StringBuilder stringBuilder_0, ref int int_1, ref int int_2);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern sbyte DBFGetNativeFieldType(IntPtr intptr_0, int int_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFGetRecordCount(IntPtr intptr_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFIsAttributeNULL(IntPtr intptr_0, int int_0, int int_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr DBFOpen(string string_0, string string_1);

		public static DateTime DBFReadDateAttribute(IntPtr intptr_0, int int_0, int int_1)
		{
			DateTime dateTime;
			int num = ShapeLib.DBFReadDateAttribute_1(intptr_0, int_0, int_1);
			string str = num.ToString();
			try
			{
				DateTime dateTime1 = new DateTime(int.Parse(str.Substring(0, 4)), int.Parse(str.Substring(4, 2)), int.Parse(str.Substring(6, 2)));
				dateTime = dateTime1;
			}
			catch
			{
				dateTime = new DateTime((long)0);
			}
			return dateTime;
		}

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, EntryPoint="DBFReadDateAttribute", ExactSpelling=false)]
		private static extern int DBFReadDateAttribute_1(IntPtr intptr_0, int int_0, int int_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern double DBFReadDoubleAttribute(IntPtr intptr_0, int int_0, int int_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFReadIntegerAttribute(IntPtr intptr_0, int int_0, int int_1);

		public static bool DBFReadLogicalAttribute(IntPtr intptr_0, int int_0, int int_1)
		{
			return ShapeLib.DBFReadLogicalAttribute_1(intptr_0, int_0, int_1) == "T";
		}

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, EntryPoint="DBFReadLogicalAttribute", ExactSpelling=false)]
		private static extern string DBFReadLogicalAttribute_1(IntPtr intptr_0, int int_0, int int_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern string DBFReadStringAttribute(IntPtr intptr_0, int int_0, int int_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr DBFReadTuple(IntPtr intptr_0, int int_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFWriteAttributeDirectly(IntPtr intptr_0, int int_0, int int_1, IntPtr intptr_1);

		public static int DBFWriteDateAttribute(IntPtr intptr_0, int int_0, int int_1, DateTime dateTime_0)
		{
			int num = ShapeLib.DBFWriteDateAttribute_1(intptr_0, int_0, int_1, int.Parse(dateTime_0.ToString("yyyyMMdd")));
			return num;
		}

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, EntryPoint="DBFWriteDateAttribute", ExactSpelling=false)]
		private static extern int DBFWriteDateAttribute_1(IntPtr intptr_0, int int_0, int int_1, int int_2);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFWriteDoubleAttribute(IntPtr intptr_0, int int_0, int int_1, double double_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFWriteIntegerAttribute(IntPtr intptr_0, int int_0, int int_1, int int_2);

		public static int DBFWriteLogicalAttribute(IntPtr intptr_0, int int_0, int int_1, bool bool_0)
		{
			int num;
			num = (!bool_0 ? ShapeLib.DBFWriteLogicalAttribute_1(intptr_0, int_0, int_1, 'F') : ShapeLib.DBFWriteLogicalAttribute_1(intptr_0, int_0, int_1, 'T'));
			return num;
		}

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, EntryPoint="DBFWriteLogicalAttribute", ExactSpelling=false)]
		private static extern int DBFWriteLogicalAttribute_1(IntPtr intptr_0, int int_0, int int_1, char char_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFWriteNULLAttribute(IntPtr intptr_0, int int_0, int int_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFWriteStringAttribute(IntPtr intptr_0, int int_0, int int_1, string string_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int DBFWriteTuple(IntPtr intptr_0, int int_0, IntPtr intptr_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int SHPCheckBoundsOverlap(double[] double_0, double[] double_1, double[] double_2, double[] double_3, int int_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern void SHPClose(IntPtr intptr_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern void SHPComputeExtents(IntPtr intptr_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr SHPCreate(string string_0, ShapeLib.ShapeType shapeType_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr SHPCreateObject(ShapeLib.ShapeType shapeType_0, int int_0, int int_1, int[] int_2, ShapeLib.PartType[] partType_0, int int_3, double[] double_0, double[] double_1, double[] double_2, double[] double_3);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr SHPCreateSimpleObject(ShapeLib.ShapeType shapeType_0, int int_0, double[] double_0, double[] double_1, double[] double_2);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr SHPCreateTree(IntPtr intptr_0, int int_0, int int_1, double[] double_0, double[] double_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern void SHPDestroyObject(IntPtr intptr_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern void SHPDestroyTree(IntPtr intptr_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern void SHPGetInfo(IntPtr intptr_0, ref int int_0, ref ShapeLib.ShapeType shapeType_0, double[] double_0, double[] double_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr SHPOpen(string string_0, string string_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern string SHPPartTypeName(ShapeLib.PartType partType_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr SHPReadObject(IntPtr intptr_0, int int_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int SHPTreeAddShapeId(IntPtr intptr_0, IntPtr intptr_1);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern IntPtr SHPTreeFindLikelyShapes(IntPtr intptr_0, double[] double_0, double[] double_1, ref int int_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern void SHPTreeTrimExtraNodes(IntPtr intptr_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern string SHPTypeName(ShapeLib.ShapeType shapeType_0);

		[DllImport("shapelib.dll", CharSet=CharSet.Ansi, ExactSpelling=false)]
		public static extern int SHPWriteObject(IntPtr intptr_0, int int_0, IntPtr intptr_1);

		public enum DBFFieldType
		{
			FTString,
			FTInteger,
			FTDouble,
			FTLogical,
			FTInvalid,
			FTDate
		}

		public enum PartType
		{
			TriangleStrip,
			TriangleFan,
			OuterRing,
			InnerRing,
			FirstRing,
			Ring
		}

		public enum ShapeType
		{
			NullShape = 0,
			Point = 1,
			PolyLine = 3,
			Polygon = 5,
			MultiPoint = 8,
			PointZ = 11,
			PolyLineZ = 13,
			PolygonZ = 15,
			MultiPointZ = 18,
			PointM = 21,
			PolyLineM = 23,
			PolygonM = 25,
			MultiPointM = 28,
			MultiPatch = 31
		}

		public class SHPObject
		{
			public ShapeLib.ShapeType shpType;

			public int nShapeId;

			public int nParts;

			public IntPtr paPartStart;

			public IntPtr paPartType;

			public int nVertices;

			public IntPtr padfX;

			public IntPtr padfY;

			public IntPtr padfZ;

			public IntPtr padfM;

			public double dfXMin;

			public double dfYMin;

			public double dfZMin;

			public double dfMMin;

			public double dfXMax;

			public double dfYMax;

			public double dfZMax;

			public double dfMMax;

			public SHPObject()
			{
			}
		}
	}
}
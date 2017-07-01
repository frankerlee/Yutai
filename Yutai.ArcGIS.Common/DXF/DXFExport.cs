using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Text;

namespace Yutai.ArcGIS.Common.DXF
{
    public class DXFExport
    {
        public static float accuracy;

        public static bool alternativeBlack;

        public static AutoCADVersion autoCADVer;

        private ArrayList blkRecs;

        private string block;

        private bool blockMode;

        private ArrayList blocks;

        public ArrayList current;

        private DXFLayer currentLayer;

        public static int[] DXFLineWeights;

        public int ellipses;

        private ArrayList entities;

        private ArrayList figuresList;

        public float fOffset;

        private int handle;

        public static bool isParseWhite;

        private ArrayList layers;

        private DXFPoint limMax;

        private DXFPoint limMin;

        private ArrayList lTypes;

        private string nameCurrentLayer;

        public static float offsetX;

        public static float offsetY;

        public float penWidthRatio;

        public static bool use01MM;

        public string Block
        {
            get { return this.block; }
            set { this.BeginBlock(value); }
        }

        public DXFLayer CurrentLayer
        {
            get { return this.currentLayer; }
            set
            {
                this.currentLayer = (DXFLayer) value.Clone();
                this.nameCurrentLayer = this.currentLayer.Name;
                if (this.layers.IndexOf(value) == -1)
                {
                    this.layers.Add(this.currentLayer);
                }
            }
        }

        public float PenWidthRatio
        {
            get { return this.penWidthRatio; }
            set { this.penWidthRatio = value; }
        }

        static DXFExport()
        {
            DXFExport.DXFLineWeights = new int[]
                {0, 5, 9, 13, 15, 18, 20, 25, 30, 35, 40, 50, 53, 60, 70, 80, 90, 100, 106, 120, 140, 158, 200, 211};
            DXFExport.autoCADVer = AutoCADVersion.R2000;
            DXFExport.use01MM = false;
            DXFExport.isParseWhite = false;
            DXFExport.alternativeBlack = true;
            DXFExport.offsetX = 0f;
            DXFExport.offsetY = 0f;
            DXFExport.accuracy = 1E-06f;
        }

        public DXFExport()
        {
            this.current = new ArrayList();
            this.limMin = new DXFPoint();
            this.limMax = new DXFPoint();
            this.entities = new ArrayList();
            this.blocks = new ArrayList();
            this.lTypes = new ArrayList();
            this.blkRecs = new ArrayList();
            this.current = this.entities;
            this.figuresList = new ArrayList();
            this.handle = 32;
            this.layers = new ArrayList();
            this.layers.Add(new DXFLayer("0"));
            this.SetCurrentLayer((DXFLayer) this.layers[0]);
            this.fOffset = 300f;
            this.penWidthRatio = -1f;
        }

        public void Add3DPoint(int code, DXFPoint p)
        {
            this.AddPoint(code, p);
            if (p.Z != 0f)
            {
                this.AddFloat(code + 20, p.Z);
            }
        }

        public void AddArc(DXFData Data)
        {
            DXFArc dXFArc = new DXFArc(Data)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFArc.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFArc);
        }

        public void AddCircle(DXFData Data)
        {
            DXFEllipse dXFEllipse = new DXFEllipse(Data, false)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFEllipse.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFEllipse);
        }

        public void AddColor(DXFData Data)
        {
            if (Data.color != 256 && Data.color != 0)
            {
                this.AddInt(62, Data.color);
            }
        }

        public void AddEllipse(DXFData Data)
        {
            DXFEllipse dXFEllipse = new DXFEllipse(Data, true)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFEllipse.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFEllipse);
        }

        public void AddFloat(int code, float Value)
        {
            string str = this.SpecialFormat(code);
            this.current.Add(str);
            str = string.Concat("", Value);
            this.current.Add(str.Replace(',', '.'));
        }

        public void AddHandle()
        {
            string str = string.Format("{0:X}", this.handle);
            this.AddString(5, str);
            DXFExport dXFExport = this;
            dXFExport.handle = dXFExport.handle + 1;
        }

        public void AddHatch(DXFData Data)
        {
            DXFHatch dXFHatch = new DXFHatch(Data)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFHatch.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFHatch);
        }

        public void AddInt(int code, int Value)
        {
            string str = this.SpecialFormat(code);
            this.current.Add(str);
            this.current.Add(string.Concat("", Value));
        }

        public void AddLine(DXFData Data)
        {
            DXFLine dXFLine = new DXFLine(Data)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFLine.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFLine);
        }

        public void AddLType(string aName, float[] aParts)
        {
            int i;
            float single = 0f;
            ArrayList arrayLists = new ArrayList();
            for (i = 0; i < (int) aParts.Length; i++)
            {
                single = single + Math.Abs(aParts[i]);
            }
            arrayLists = this.current;
            this.current = this.lTypes;
            try
            {
                this.AddName("LTYPE", "AcDbLinetypeTableRecord");
                this.lTypes.Add("  2");
                this.lTypes.Add(aName);
                this.lTypes.Add("  3");
                this.lTypes.Add("");
                this.lTypes.Add(" 70");
                this.lTypes.Add("0");
                this.lTypes.Add(" 72");
                this.lTypes.Add("65");
                this.lTypes.Add(" 73");
                this.lTypes.Add(string.Concat("", (int) aParts.Length));
                this.lTypes.Add(" 40");
                this.lTypes.Add(string.Concat("", single));
                for (i = 0; i < (int) aParts.Length; i++)
                {
                    this.lTypes.Add(" 49");
                    this.lTypes.Add(string.Concat("", aParts[i]));
                    this.lTypes.Add(" 74");
                    this.lTypes.Add("0");
                }
            }
            finally
            {
                this.current = arrayLists;
            }
        }

        public void AddMText(DXFData Data)
        {
            DXFMText dXFMText = new DXFMText(Data)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFMText.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFMText);
        }

        public void AddName(string aName, string aSub)
        {
            this.current.Add("  0");
            this.current.Add(aName);
            this.AddHandle();
            if (aName == DXFTables.sHatchEntity)
            {
                this.current.Add("330");
                this.current.Add("1F");
            }
            this.current.Add("100");
            if (this.current == this.lTypes || this.current == this.blkRecs)
            {
                this.current.Add("AcDbSymbolTableRecord");
            }
            else
            {
                this.current.Add("AcDbEntity");
                this.current.Add("  8");
                this.current.Add(this.nameCurrentLayer);
            }
            this.current.Add("100");
            this.current.Add(aSub);
        }

        public void AddPixel(DXFData Data)
        {
            DXFPixel dXFPixel = new DXFPixel(Data)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFPixel.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFPixel);
        }

        public void AddPoint(int code, DXFPoint P)
        {
            this.AddFloat(code, this.MM(P.X));
            this.AddFloat(code + 10, this.MM(P.Y));
            if (this.current == this.entities)
            {
                if (this.limMin.X > P.X)
                {
                    this.limMin.X = P.X;
                }
                if (this.limMin.Y > P.Y)
                {
                    this.limMin.Y = P.Y;
                }
                if (this.limMax.X < P.X)
                {
                    this.limMax.X = P.X;
                }
                if (this.limMax.Y < P.Y)
                {
                    this.limMax.Y = P.Y;
                }
            }
        }

        public void AddPolyBezier(DXFData Data, int aIndex)
        {
            DXFPolyBezier dXFPolyBezier = new DXFPolyBezier(Data, aIndex)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFPolyBezier.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFPolyBezier);
        }

        public void AddPolyLine(DXFData Data, int aIndex)
        {
            DXFPolyline dXFPolyline = new DXFPolyline(Data, aIndex)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFPolyline.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFPolyline);
        }

        public void AddRectangle(DXFData Data)
        {
            DXFRectangle dXFRectangle = new DXFRectangle(Data)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFRectangle.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFRectangle);
        }

        protected void AddS(string[] S)
        {
            for (int i = 0; i < (int) S.Length; i++)
            {
                this.current.Add(S[i]);
            }
        }

        public void AddSolid(DXFData Data)
        {
            DXFSolid dXFSolid = new DXFSolid(Data)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFSolid.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFSolid);
        }

        public void AddString(int code, string str)
        {
            string str1 = this.SpecialFormat(code);
            this.current.Add(str1);
            this.current.Add(str);
        }

        public void AddText(DXFData Data)
        {
            DXFText dXFText = new DXFText(Data)
            {
                Layer = this.nameCurrentLayer
            };
            if (this.blockMode)
            {
                dXFText.ExportAsDXF(this);
                return;
            }
            this.figuresList.Add(dXFText);
        }

        public void AddThickness(DXFData Data)
        {
            int dXFLineWeights;
            if (DXFExport.autoCADVer == AutoCADVersion.R2000)
            {
                if (Data.text != "")
                {
                    this.current.Add("  6");
                    this.current.Add(Data.text);
                }
                try
                {
                    if (!DXFExport.use01MM)
                    {
                        dXFLineWeights = (int) Math.Round((double) Data.thickness)*10;
                        if (this.penWidthRatio > 0f)
                        {
                            dXFLineWeights = (int) Math.Round((double) (Data.thickness*100f*this.penWidthRatio));
                        }
                    }
                    else
                    {
                        dXFLineWeights = (int) Math.Round((double) Data.thickness);
                    }
                }
                catch
                {
                    dXFLineWeights = 100;
                }
                if (dXFLineWeights >= 5)
                {
                    if (dXFLineWeights >= 211)
                    {
                        dXFLineWeights = 211;
                        return;
                    }
                    int num = 0;
                    while (num < (int) DXFExport.DXFLineWeights.Length - 1)
                    {
                        if (dXFLineWeights >= DXFExport.DXFLineWeights[num])
                        {
                            num++;
                        }
                        else
                        {
                            dXFLineWeights = DXFExport.DXFLineWeights[num - 1];
                            break;
                        }
                    }
                    this.AddInt(370, dXFLineWeights);
                }
            }
        }

        public void AddVertex(DXFPoint P)
        {
            this.Add3DPoint(10, P);
        }

        public void BeginBlock(string aName)
        {
            this.blockMode = true;
            this.block = aName;
            if (aName == "")
            {
                this.current = this.entities;
                return;
            }
            int num = this.blocks.IndexOf(aName);
            if (num >= 0)
            {
                this.current = (ArrayList) this.blocks[num];
                return;
            }
            this.current = this.blkRecs;
            this.AddName("BLOCK_RECORD", "AcDbBlockTableRecord");
            this.blkRecs.Add("  2");
            this.blkRecs.Add(aName);
            this.current = new ArrayList();
            this.blocks.Add(this.current);
            this.AddName("BLOCK", "AcDbBlockBegin");
            this.current.Add("  2");
            this.current.Add(aName);
            this.current.Add(" 10");
            this.current.Add("0");
            this.current.Add(" 20");
            this.current.Add("0");
            this.current.Add(" 70");
            this.current.Add("0");
        }

        public void BeginPoly(DXFData Data, int aCount)
        {
            this.AddName("LWPOLYLINE", "AcDbPolyline");
            this.AddColor(Data);
            this.AddThickness(Data);
            this.AddInt(90, aCount);
            this.AddInt(70, Data.flags);
            if (Data.globalWidth != 0f)
            {
                this.AddFloat(43, Data.globalWidth);
            }
        }

        public void Clear()
        {
            this.entities.Clear();
            this.blocks.Clear();
            this.lTypes.Clear();
            this.blkRecs.Clear();
            this.current = this.entities;
            this.handle = 32;
        }

        public static int ColorToDXF(Color Value)
        {
            byte[] numArray = new byte[]
            {
                0, 1, 3, 2, 5, 6, 4, 0, 1, 251, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 33, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 102, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 50, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 251, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 252, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 253, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 171, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 254
            };
            string name = Value.Name;
            string str = name;
            if (name != null)
            {
                switch (str)
                {
                    case "Black":
                    case "ff000000":
                    {
                        if (DXFExport.alternativeBlack)
                        {
                            return 250;
                        }
                        return 7;
                    }
                    case "Maroon":
                    case "ff800000":
                    {
                        return 14;
                    }
                    case "Green":
                    case "ff008000":
                    {
                        return 74;
                    }
                    case "Olive":
                    case "ff808000":
                    {
                        return 44;
                    }
                    case "Navy":
                    case "ff000080":
                    {
                        return 154;
                    }
                    case "Purple":
                    case "ff800080":
                    {
                        return 224;
                    }
                    case "Teal":
                    case "ff008080":
                    {
                        return 12;
                    }
                    case "Gray":
                    case "ff808080":
                    {
                        return 8;
                    }
                    case "Silver":
                    case "ffc0c0c0":
                    {
                        return 9;
                    }
                    case "Red":
                    case "ffff0000":
                    {
                        return 1;
                    }
                    case "Lime":
                    case "ff00ff00":
                    {
                        return 3;
                    }
                    case "Yellow":
                    case "ffffff00":
                    {
                        return 2;
                    }
                    case "Blue":
                    case "ff0000ff":
                    {
                        return 5;
                    }
                    case "Fuchsia":
                    case "ffff00ff":
                    {
                        return 6;
                    }
                    case "Aquamarine":
                    case "ff7fffd4":
                    {
                        return 4;
                    }
                    case "White":
                    case "ffffffff":
                    {
                        return 255;
                    }
                    case "0":
                    {
                        return 0;
                    }
                }
            }
            int r = Value.R + (Value.G << 8) + (Value.B << 16);
            return numArray[((r & 224) >> 5) + ((r & 57344) >> 10) + ((r & 12582912) >> 16)];
        }

        public void EndBlock()
        {
            this.blockMode = false;
            this.BeginBlock("");
        }

        public void EndDraw()
        {
        }

        public static string FontToStr(Font FontV, string S)
        {
            string str = string.Concat(";", S);
            if (FontV.Style == FontStyle.Italic)
            {
                str = string.Concat("|i1", str);
            }
            if (FontV.Style == FontStyle.Bold)
            {
                str = string.Concat("|b1", str);
            }
            return string.Concat("\\f", FontV.Name, str);
        }

        public static string GetPourStr(string S)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(S.ToCharArray());
            stringBuilder.Remove(0, S.IndexOf(";") + 1);
            if (stringBuilder.ToString().IndexOf("\\L") == 0)
            {
                stringBuilder.Remove(0, 2);
            }
            return stringBuilder.ToString();
        }

        public string GetStrAutoCADVer()
        {
            if (DXFExport.autoCADVer == AutoCADVersion.R14)
            {
                return "AC1014";
            }
            if (DXFExport.autoCADVer == AutoCADVersion.R2000)
            {
                return "AC1015";
            }
            return "Err";
        }

        public void Insert(DXFData aData)
        {
            this.AddName("INSERT", "AcDbBlockReference");
            this.current.Add("  2");
            this.current.Add(aData.text);
            this.Add3DPoint(10, aData.point);
            this.AddColor(aData);
            if (aData.scale.X != 1f)
            {
                this.AddFloat(41, aData.scale.X);
            }
            if (aData.scale.Y != 1f)
            {
                this.AddFloat(42, aData.scale.Y);
            }
            if (aData.rotation != 0f)
            {
                this.AddFloat(50, aData.rotation);
            }
        }

        public float MM(float Value)
        {
            if (!DXFExport.use01MM)
            {
                return Value;
            }
            return Value/100f;
        }

        protected bool ParseRects(DXFRectangle aRect, DXFFigure aFigure, ArrayList NewElems)
        {
            Line line = new Line();
            Rect x1 = new Rect();
            ArrayList arrayLists = new ArrayList();
            DXFLine dXFLine = new DXFLine();
            bool flag = false;
            DXFData dXFDatum = new DXFData();
            x1.X1 = (int) Math.Round((double) aRect.LeftTop.X);
            x1.Y1 = (int) Math.Round((double) aRect.LeftTop.Y);
            x1.X2 = (int) Math.Round((double) aRect.RightBottom.X);
            x1.Y2 = (int) Math.Round((double) aRect.RightBottom.Y);
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
            if (aFigure is DXFMText)
            {
                if (!((DXFMText) aFigure).IntersecRect(x1))
                {
                    NewElems.Add(aFigure);
                    return flag;
                }
                ((DXFMText) aFigure).ParseToTexts(x1, NewElems);
                return true;
            }
            if (aFigure is DXFEllipse || aFigure is DXFArc || aFigure is DXFRectangle || aFigure is DXFPolyline)
            {
                if (!((DXFRectangle) aFigure).IntersecRect(x1))
                {
                    NewElems.Add(aFigure);
                    return flag;
                }
                ((DXFRectangle) aFigure).ParseToLines(NewElems);
                for (int i = NewElems.Count - 1; i > 0; i--)
                {
                    this.ParseRects(aRect, (DXFFigure) NewElems[i], NewElems);
                }
                NewElems.Clear();
                return true;
            }
            if (!(aFigure is DXFLine))
            {
                NewElems.Add(aFigure);
                return flag;
            }
            dXFLine = (DXFLine) aFigure;
            line.x1 = (int) Math.Round((double) dXFLine.StartPoint.X);
            line.y1 = (int) Math.Round((double) dXFLine.StartPoint.Y);
            line.x2 = (int) Math.Round((double) dXFLine.EndPoint.X);
            line.y2 = (int) Math.Round((double) dXFLine.EndPoint.Y);
            for (int j = 0; j < arrayLists.Count; j++)
            {
                dXFDatum = dXFLine.data;
                Line item = (Line) arrayLists[j];
                dXFDatum.point.X = (float) item.x1;
                item = (Line) arrayLists[j];
                dXFDatum.point.Y = (float) item.y1;
                item = (Line) arrayLists[j];
                dXFDatum.point1.X = (float) item.x2;
                item = (Line) arrayLists[j];
                dXFDatum.point1.Y = (float) item.y2;
                dXFLine = new DXFLine(dXFDatum)
                {
                    Layer = aFigure.Layer
                };
                NewElems.Add(dXFLine);
            }
            arrayLists.Clear();
            return true;
        }

        public static void PtToDXF(Point P, DXFPoint DXF)
        {
            DXF.X = (float) P.X + DXFExport.offsetX;
            DXF.Y = -((float) P.Y + DXFExport.offsetY);
        }

        public bool SaveToFile(string fileName)
        {
            bool flag;
            StreamWriter streamWriter = File.CreateText(fileName);
            try
            {
                this.SaveToStream(streamWriter, null);
                streamWriter.Close();
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private void SaveToFileStream(StreamWriter aS, ArrayList aDt)
        {
            for (int i = 0; i < aDt.Count; i++)
            {
                if (aDt[i] is string)
                {
                    aS.WriteLine(aDt[i]);
                }
                if (aDt[i] is string[])
                {
                    for (int j = 0; j < (int) ((string[]) aDt[i]).Length; j++)
                    {
                        aS.WriteLine(((string[]) aDt[i])[j]);
                    }
                }
                if (aDt[i] is ArrayList)
                {
                    this.SaveToFileStream(aS, (ArrayList) aDt[i]);
                }
            }
        }

        public void SaveToStream(StringWriter St)
        {
            this.SaveToStream(null, St);
        }

        private void SaveToStream(StreamWriter aS, StringWriter aSt)
        {
            ArrayList arrayLists = new ArrayList();
            ArrayList arrayLists1 = new ArrayList();
            ArrayList arrayLists2 = new ArrayList();
            DXFRectangle dXFRectangle = new DXFRectangle();
            DXFFigure dXFFigure = new DXFFigure();
            int k = 0;
            while (k < this.figuresList.Count)
            {
                if (DXFExport.isParseWhite && this.figuresList[k] is DXFHatch &&
                    ((DXFHatch) this.figuresList[k]).Style == HatchStyle.hsSolid &&
                    ((DXFHatch) this.figuresList[k]).Color == 255)
                {
                    if (((DXFHatch) this.figuresList[k]).BndAmount != 1 ||
                        ((DXFHatch) this.figuresList[k]).GetPointsNumber(0) != 4)
                    {
                        k++;
                        continue;
                    }
                    else
                    {
                        dXFFigure = new DXFRectangle(((DXFHatch) this.figuresList[k]).data);
                        dXFFigure.data.point = ((DXFHatch) this.figuresList[k]).GetPoint(0, 0);
                        dXFFigure.data.point1 = ((DXFHatch) this.figuresList[k]).GetPoint(0, 2);
                        this.figuresList[k] = dXFFigure;
                        arrayLists1.Add(this.figuresList[k]);
                        dXFRectangle = (DXFRectangle) this.figuresList[k];
                        for (int i = k - 1; i > 0; i--)
                        {
                            if (this.ParseRects(dXFRectangle, (DXFFigure) this.figuresList[i], arrayLists2))
                            {
                                this.figuresList.RemoveAt(i);
                                for (int j = 0; j < arrayLists2.Count; j++)
                                {
                                    this.figuresList.Insert(i, arrayLists2[j]);
                                }
                            }
                        }
                        arrayLists2.Clear();
                        this.figuresList.Remove(dXFFigure);
                    }
                }
                k++;
            }
            for (k = 0; k < this.figuresList.Count; k++)
            {
                dXFFigure = (DXFFigure) this.figuresList[k];
                this.nameCurrentLayer = dXFFigure.Layer;
                if (dXFFigure != null)
                {
                    dXFFigure.ExportAsDXF(this);
                }
            }
            ArrayList arrayLists3 = new ArrayList();
            arrayLists3 = this.current;
            this.current = arrayLists;
            this.AddS(DXFTables.SHeader);
            this.AddString(9, "$ACADVER");
            this.AddString(1, this.GetStrAutoCADVer());
            this.AddString(9, "$HANDSEED");
            string str = string.Format("{0:X}", this.handle + this.layers.Count + this.blocks.Count + 1);
            this.AddString(5, str);
            this.AddString(9, "$LIMMIN");
            this.AddPoint(10, this.limMin);
            this.AddString(9, "$LIMMAX");
            this.AddPoint(10, this.limMax);
            this.AddS(DXFTables.STablesLTYPE);
            this.current.Add(this.lTypes);
            this.AddS(DXFTables.STablesLAYER);
            for (k = 0; k < this.layers.Count; k++)
            {
                ((DXFLayer) this.layers[k]).ExportAsDXF(this);
            }
            this.AddS(DXFTables.STablesSTYLE);
            this.AddS(DXFTables.STablesDIMSTYLE);
            if (DXFExport.autoCADVer == AutoCADVersion.R2000)
            {
                this.AddS(DXFTables.STablesDIMSTYLE_R2000);
            }
            this.AddS(DXFTables.STablesBLOCK_RECORD);
            this.current.Add(this.blkRecs);
            this.AddS(DXFTables.SBlocks);
            for (k = 0; k < this.blocks.Count; k++)
            {
                this.current.Add((ArrayList) this.blocks[k]);
                this.AddName("ENDBLK", "AcDbBlockEnd");
            }
            string[] strArrays = new string[] {"  0", "ENDSEC", "  0", "SECTION", "  2", "ENTITIES"};
            this.AddS(strArrays);
            DXFData dXFDatum = new DXFData()
            {
                height = 50f,
                rotation = 0f
            };
            dXFDatum.point.X = 57f;
            dXFDatum.point.Y = 0f;
            dXFDatum.color = 1;
            dXFDatum.text = "Trial version";
            this.AddText(dXFDatum);
            this.current.Add(this.entities);
            if (DXFExport.autoCADVer != AutoCADVersion.R2000)
            {
                this.AddS(DXFTables.SObjects_R14);
            }
            else
            {
                this.AddS(DXFTables.SObjects_R2000);
            }
            this.AddS(DXFTables.SEndOfDXF);
            if (aSt != null)
            {
                this.SaveToStStream(aSt, this.current);
            }
            else
            {
                this.SaveToFileStream(aS, this.current);
            }
            this.current = arrayLists3;
        }

        private void SaveToStStream(StringWriter aS, ArrayList aDt)
        {
            for (int i = 0; i < aDt.Count; i++)
            {
                if (aDt[i] is string)
                {
                    aS.WriteLine(aDt[i]);
                }
                if (aDt[i] is string[])
                {
                    for (int j = 0; j < (int) ((string[]) aDt[i]).Length; j++)
                    {
                        aS.WriteLine(((string[]) aDt[i])[j]);
                    }
                }
                if (aDt[i] is ArrayList)
                {
                    this.SaveToStStream(aS, (ArrayList) aDt[i]);
                }
            }
        }

        private void SetCurrentLayer(DXFLayer Value)
        {
            this.currentLayer = Value;
            this.nameCurrentLayer = this.currentLayer.Name;
            if (this.layers.IndexOf(Value) == -1)
            {
                this.layers.Add(this.currentLayer);
            }
        }

        public void SetLayerByString(string aName)
        {
            for (int i = 0; i < this.layers.Count; i++)
            {
                if (((DXFLayer) this.layers[i]).Name == aName)
                {
                    this.currentLayer = (DXFLayer) this.layers[i];
                }
            }
        }

        private string SpecialFormat(int aDt)
        {
            string str = "";
            if (aDt < 100)
            {
                str = (aDt >= 10 ? " " : "  ");
            }
            return string.Concat(str, aDt);
        }
    }
}
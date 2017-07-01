namespace Yutai.ArcGIS.Common.DXF
{
    public struct DXFTables
    {
        public readonly static byte cnstAmount;

        public readonly static string sModel;

        public readonly static string sLayout;

        public readonly static string sACADXDataAppName;

        public readonly static string sActiveVPort;

        public readonly static string sModelSpace;

        public readonly static string sPaperSpace;

        public readonly static string sPolyline;

        public readonly static string sCircle;

        public readonly static string sEllipse;

        public readonly static string sHatchEntity;

        public readonly static string sPatternSOLID;

        public readonly static string sPatternANSI31;

        public readonly static string sPatternANSI37;

        public readonly static string sPatternNET;

        public readonly static string sPatternLINE;

        public readonly static string[] STables2begin;

        public readonly static string[] SHeader;

        public readonly static string[] STablesLTYPE;

        public readonly static string[] STablesLAYER;

        public readonly static string[] STablesSTYLE;

        public readonly static string[] STablesDIMSTYLE;

        public readonly static string[] STablesDIMSTYLE_R2000;

        public readonly static string[] STablesBLOCK_RECORD;

        public readonly static string[] SBlocks;

        public readonly static string[] SObjects_R14;

        public readonly static string[] SObjects_R2000;

        public readonly static string[] SEndOfDXF;

        static DXFTables()
        {
            DXFTables.cnstAmount = 4;
            DXFTables.sModel = "Model";
            DXFTables.sLayout = "Layout1";
            DXFTables.sACADXDataAppName = "ACAD";
            DXFTables.sActiveVPort = "*ACTIVE";
            DXFTables.sModelSpace = "*MODEL_SPACE";
            DXFTables.sPaperSpace = "*PAPER_SPACE";
            DXFTables.sPolyline = "Polyline";
            DXFTables.sCircle = "Circle";
            DXFTables.sEllipse = "Ellipse";
            DXFTables.sHatchEntity = "HATCH";
            DXFTables.sPatternSOLID = "SOLID";
            DXFTables.sPatternANSI31 = "ANSI31";
            DXFTables.sPatternANSI37 = "ANSI37";
            DXFTables.sPatternNET = "NET";
            DXFTables.sPatternLINE = "LINE";
            string[] strArrays = new string[]
            {
                "  0", "ENDTAB", "  0", "TABLE", "  2", "LAYER", "  5", "104", "100", "AcDbSymbolTable", "  0", "LAYER",
                "  5", "105", "100", "AcDbSymbolTableRecord", "100", "AcDbLayerTableRecord", "  2", "0", " 70", "     0"
            };
            DXFTables.STables2begin = strArrays;
            string[] strArrays1 = new string[] {"  0", "SECTION", "  2", "HEADER"};
            DXFTables.SHeader = strArrays1;
            string[] strArrays2 = new string[]
            {
                "  0", "ENDSEC", "  0", "SECTION", "  2", "TABLES", "  0", "TABLE", "  2", "VPORT", "  5", "  1", "100",
                "AcDbSymbolTable", "  0", "ENDTAB", "  0", "TABLE", "  2", "LTYPE", "  5", "  2", "100",
                "AcDbSymbolTable", "  0", "LTYPE", "  5", "  3", "100", "AcDbSymbolTableRecord", "100",
                "AcDbLinetypeTableRecord", "  2", "BYBLOCK", " 70", "     0", "  0", "LTYPE", "  5", "  4", "100",
                "AcDbSymbolTableRecord", "100", "AcDbLinetypeTableRecord", "  2", "BYLAYER", " 70", "     0"
            };
            DXFTables.STablesLTYPE = strArrays2;
            string[] strArrays3 = new string[]
                {"  0", "ENDTAB", "  0", "TABLE", "  2", "LAYER", "  5", " 1A", "330", "  0", "100", "AcDbSymbolTable"};
            DXFTables.STablesLAYER = strArrays3;
            string[] strArrays4 = new string[]
            {
                "  0", "ENDTAB", "  0", "TABLE", "  2", "STYLE", "  5", "  5", "100", "AcDbSymbolTable", "  0", "STYLE",
                "  5", "  6", "100", "AcDbSymbolTableRecord", "100", "AcDbTextStyleTableRecord", "  2", "STANDARD",
                " 70", "     0", " 40", "0.0", " 41", "1.0", " 50", "0.0", " 71", "     0", " 42", "10.0", "  3", "txt",
                "  4", "bigfont", "  0", "ENDTAB", "  0", "TABLE", "  2", "VIEW", "  5", "  7", "100", "AcDbSymbolTable",
                "  0", "ENDTAB", "  0", "TABLE", "  2", "UCS", "  5", "  8", "100", "AcDbSymbolTable", "  0", "ENDTAB",
                "  0", "TABLE", "  2", "APPID", "  5", "  9", "100", "AcDbSymbolTable", "  0", "APPID", "  5", "  A",
                "100", "AcDbSymbolTableRecord", "100", "AcDbRegAppTableRecord", "  2", "ACAD", " 70", "0"
            };
            DXFTables.STablesSTYLE = strArrays4;
            string[] strArrays5 = new string[]
                {"  0", "ENDTAB", "  0", "TABLE", "  2", "DIMSTYLE", "  5", "  B", "100", "AcDbSymbolTable"};
            DXFTables.STablesDIMSTYLE = strArrays5;
            string[] strArrays6 = new string[] {" 70", "     1", "100", "AcDbDimStyleTable", " 71", "     0"};
            DXFTables.STablesDIMSTYLE_R2000 = strArrays6;
            string[] strArrays7 = new string[]
            {
                "  0", "ENDTAB", "  0", "TABLE", "  2", "BLOCK_RECORD", "  5", "  C", "100", "AcDbSymbolTable", "  0",
                "BLOCK_RECORD", "  5", "  D", "100", "AcDbSymbolTableRecord", "100", "AcDbBlockTableRecord", "  2",
                "*MODEL_SPACE", "  0", "BLOCK_RECORD", "  5", "  E", "100", "AcDbSymbolTableRecord", "100",
                "AcDbBlockTableRecord", "  2", "*PAPER_SPACE"
            };
            DXFTables.STablesBLOCK_RECORD = strArrays7;
            string[] strArrays8 = new string[]
            {
                "  0", "ENDTAB", "  0", "ENDSEC", "  0", "SECTION", "  2", "BLOCKS", "  0", "BLOCK", "  5", "  F",
                "330", " D", "100", "AcDbEntity", "  8", "0", "100", "AcDbBlockBegin", "  2", "*MODEL_SPACE", " 70",
                "     0", "  0", "ENDBLK", "  5", " 10", "100", "AcDbEntity", "  8", "0", "100", "AcDbBlockEnd", "  0",
                "BLOCK", "  5", " 11", "330", " E", "100", "AcDbEntity", "  8", "0", "100", "AcDbBlockBegin", "  2",
                "*PAPER_SPACE", " 70", "     0", "  0", "ENDBLK", "  5", " 12", "100", "AcDbEntity", "  8", "0", "100",
                "AcDbBlockEnd"
            };
            DXFTables.SBlocks = strArrays8;
            string[] strArrays9 = new string[]
            {
                "  0", "ENDSEC", "  0", "SECTION", "  2", "OBJECTS", "  0", "DICTIONARY", "  5", "13", "100",
                "AcDbDictionary", "  3", "ACAD_GROUP", "350", "14", "  3", "ACAD_MLINESTYLE", "350", "15", "  0",
                "DICTIONARY", "  5", "14", "102", "{ACAD_REACTORS", "330", "13", "102", "}", "100", "AcDbDictionary",
                "  0", "DICTIONARY", "  5", "15", "102", "{ACAD_REACTORS", "330", "13", "102", "}", "100",
                "AcDbDictionary", " 3", "STANDARD", "350", "16", "  0", "MLINESTYLE", "  5", "16", "102",
                "{ACAD_REACTORS", "330", "15", "102", "}", "100", "AcDbMlineStyle", "  2", "STANDARD", " 70", "     0",
                "  3", " ", " 62", "   256", " 51", "90.0", " 52", "90.0", " 71", "     2", " 49", "0.5", " 62",
                "   256", "  6", "BYLAYER", " 49", "-0.5", " 62", "   256", "  6", "BYLAYER"
            };
            DXFTables.SObjects_R14 = strArrays9;
            string[] strArrays10 = new string[]
            {
                "  0", "ENDSEC", "  0", "SECTION", "  2", "OBJECTS", "  0", "DICTIONARY", "  5", " 13", "330", "0",
                "100", "AcDbDictionary", "281", "     1", "  3", "ACAD_GROUP", "350", " 14", " 3", "ACAD_MLINESTYLE",
                "350", " 16", "  3", "ACAD_PLOTSETTINGS", "350", " 17", "  3", "ACAD_PLOTSTYLENAME", "350", " 18", "  0",
                "DICTIONARY", "  5", " 14", "330", " 13", "100", "AcDbDictionary", "281", "     1", "  0", "DICTIONARY",
                "  5", " 16", "330", " 13", "100", "AcDbDictionary", "281", "     1", "  0", "DICTIONARY", "  5", " 17",
                "102", "{ACAD_REACTORS", "330", " 13", "102", "}", "330", " 13", "100", "AcDbDictionary", "281",
                "     1", "  0", "ACDBDICTIONARYWDFLT", "  5", " 18", "102", "{ACAD_REACTORS", "330", " 13", "102", "}",
                "330", " 13", "100", "AcDbDictionary", "281", "     1", "  3", "Normal", "350", " 19", "100",
                "AcDbDictionaryWithDefault", "340", " 19", "  0", "ACDBPLACEHOLDER", "  5", " 19", "102",
                "{ACAD_REACTORS", "330", " 18", "102", "}", "330", " 18"
            };
            DXFTables.SObjects_R2000 = strArrays10;
            string[] strArrays11 = new string[] {"  0", "ENDSEC", "  0", "EOF"};
            DXFTables.SEndOfDXF = strArrays11;
        }
    }
}
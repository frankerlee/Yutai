using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Pipeline.Config.Helpers
{
    public class PipeConfigWordHelper
    {
        public static PipePointConfigWords PointWords =new PipePointConfigWords();
        public static PipeLineConfigWords LineWords = new PipeLineConfigWords();
        public static AssPointConfigWords AssPointWords = new AssPointConfigWords();
        public static AssLineConfigWords AssLineWords=new AssLineConfigWords();
        public static GouPointConfigWords GouPointWords=new GouPointConfigWords();
        public static GouLineConfigWords GouLineWords=new GouLineConfigWords();
        public static AnnoConfigWords AnnoWords=new AnnoConfigWords();
        public static Point3DConfigWords Point3DWords = new Point3DConfigWords();
        public static Line3DConfigWords Line3DWords = new Line3DConfigWords();
    }
    
    public class PipePointConfigWords
    {
        
        public string GDBH
        {
            get { return "GDBH"; }
        }

        public  string YSDM {get{return  "YSDM";}}
        public  string SZTF {get{return  "SZTF";}}
        public  string XZB {get{return  "XZB";}}
        public  string YZB {get{return  "YZB";}}
        public  string DMGC {get{return  "DMGC";}}
        public  string TZW {get{return  "TZW";}}
        public  string FSW {get{return  "FSW";}}
        public  string JGCZ {get{return  "JGCZ";}}
        public  string SYZT {get{return  "SYZT";}}
        public  string PXJH {get{return  "PXJH";}}
        public  string QSDW {get{return  "QSDW";}}
        public  string FHJD {get{return  "FHJD";}}
        public  string JCSBLX {get{return  "JCSBLX";}}
        public  string JCSBBH {get{return  "JCSBBH";}}
        public  string SGYHLX {get{return  "SGYHLX";}}
        public  string SGYHDJ {get{return  "SGYHDJ";}}
        public  string SZDL {get{return  "SZDL";}}
        public  string TCDW {get{return  "TCDW";}}
        public  string TCRQ {get{return  "TCRQ";}}
        public  string RKRQ {get{return  "RKRQ";}}
        public  string BZ {get{return  "BZ";}}
        public  string BZ2 {get{return  "BZ2";}}

        public string MSRQ
        {
            get { return "MSRQ"; }
          
        }
    }

    public class PipeLineConfigWords
    {
        public  string QDBH {get{return "QDBH";}}
        public  string ZDBH {get{return "ZDBH";}}
        public  string QDMS {get{return "QDMS";}}
        public  string ZDMS {get{return "ZDMS";}}
        public  string QDGC {get{return "QDGC";}}
        public  string ZDGC {get{return "ZDGC";}}
        public  string GXCZ {get{return "GXCZ";}}
        public  string MSFS {get{return "MSFS";}}

        public string GJ { get { return "GJ"; } }
        public  string DMCC {get{return "DMCC";}}
        public  string MSRQ {get{return "MSRQ";}}
        public  string QSDW {get{return "QSDW";}}
        public  string YSDM {get{return "YSDM";}}
        public  string GDXZ {get{return "GDXZ";}}
        public  string DLTS {get{return "DLTS";}}
        public  string DY {get{return "DY";}}
        public  string YL {get{return "YL";}}
        public  string ZKS {get{return "ZKS";}}
        public  string YYKS {get{return "YYKS";}}
        public  string TGXH {get{return "TGXH";}}
        public  string LX {get{return "LX";}}
        public  string JCSBLX {get{return "JCSBLX";}}
        public  string JCSBBH {get{return "JCSBBH";}}
        public  string SGYHLX {get{return "SGYHLX";}}
        public  string SGYHDJ {get{return "SGYHDJ";}}
        public  string SZDL {get{return "SZDL";}}
        public  string TCDW {get{return "TCDW";}}
        public  string TCRQ {get{return "TCRQ";}}
        public  string RKRQ {get{return "RKRQ";}}
        public  string BZ {get{return "BZ";}}
        public  string BZ2 {get{return "BZ2";}}
    }

    public class AssPointConfigWords
    {
        public  string FZDH {get{return "FZDH";}}
        public  string YSDM {get{return "YSDM";}}
        public  string XZB {get{return "XZB";}}
        public  string YZB {get{return "YZB";}}
        public  string DMGC {get{return "DMGC";}}
        public  string FHJD {get{return "FHJD";}}
        public  string SZDL {get{return "SZDL";}}
        public  string TCDW {get{return "TCDW";}}
        public  string TCRQ {get{return "TCRQ";}}
        public  string RKRQ {get{return "RKRQ";}}
        public  string BZ {get{return "BZ";}}
        public  string BZ2 {get{return "BZ2";}}
    }

    public class AssLineConfigWords
    {
        public  string QDBH {get{return "QDBH";}}
        public  string ZDBH {get{return "ZDBH";}}
        public  string GXLX {get{return "GXLX";}}
        public  string YSDM {get{return "YSDM";}}
        public  string SSLX {get{return "SSLX";}}
        public  string XXDM {get{return "XXDM";}}
        public  string SZDL {get{return "SZDL";}}
        public  string TCDW {get{return "TCDW";}}
        public  string TCRQ {get{return "TCRQ";}}
        public  string RKRQ {get{return "RKRQ";}}
        public  string BZ {get{return "BZ";}}
        public  string BZ2 {get{return "BZ2";}}
    }


    public class GouPointConfigWords
    {
        public  string GGDH {get{return "GGDH";}}
        public  string YSDM {get{return "YSDM";}}
        public  string XZB {get{return "XZB";}}
        public  string YZB {get{return "YZB";}}
        public  string DMGC {get{return "DMGC";}}
        public  string JCSBLX {get{return "JCSBLX";}}
        public  string JCSBBH {get{return "JCSBBH";}}
        public  string SGYHLX {get{return "SGYHLX";}}
        public  string SGYHDJ {get{return "SGYHDJ";}}
        public  string SZDL {get{return "SZDL";}}
        public  string TCDW {get{return "TCDW";}}
        public  string TCRQ {get{return "TCRQ";}}
        public  string RKRQ {get{return "RKRQ";}}
        public  string BZ {get{return "BZ";}}
        public  string BZ2 {get{return "BZ2";}}
    }


    public class GouLineConfigWords
    {
        public  string QDBH {get{return "QDBH";}}
        public  string ZDBH {get{return "ZDBH";}}
        public  string QDMS {get{return "QDMS";}}
        public  string ZDMS {get{return "ZDMS";}}
        public  string GXCZ {get{return "GXCZ";}}
        public  string DMCC {get{return "DMCC";}}
        public  string YSDM {get{return "YSDM";}}
        public  string GXLX {get{return "GXLX";}}
        public  string JCSBLX {get{return "JCSBLX";}}
        public  string JCSBBH {get{return "JCSBBH";}}
        public  string SGYHLX {get{return "SGYHLX";}}
        public  string SGYHDJ {get{return "SGYHDJ";}}
        public  string SZDL {get{return "SZDL";}}
        public  string TCDW {get{return "TCDW";}}
        public  string TCRQ {get{return "TCRQ";}}
        public  string RKRQ {get{return "RKRQ";}}
        public  string BZ {get{return "BZ";}}
        public  string BZ2 {get{return "BZ2";}}
    }

    public class AnnoConfigWords
    {
        public  string BZDH {get{return "BZDH";}}
        public  string YSDM {get{return "YSDM";}}
        public  string XZB {get{return "XZB";}}
        public  string YZB {get{return "YZB";}}
        public  string XAJD {get{return "XAJD";}}
        public  string WZ {get{return "WZ";}}
        public  string SZDL {get{return "SZDL";}}
        public  string RKRQ {get{return "RKRQ";}}
        public  string BZ {get{return "BZ";}}
        public  string BZ2 {get{return "BZ2";}}
    }

    public class Point3DConfigWords
    {
        public  string GDBH {get{return "GDBH";}}
        public  string RKRQ {get{return "RKRQ";}}
    }

    public class Line3DConfigWords
    {
        public  string GDBH {get{return "GDBH";}}
        public  string RKRQ {get{return "RKRQ";}}
    }
}

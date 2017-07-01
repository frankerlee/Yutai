using System;

namespace Yutai.ArcGIS.Common.Excel
{
    internal class TimeDef
    {
        private static DateTime dt1;
        private static DateTime dt2;

        public static void End()
        {
            dt2 = DateTime.Now;
            Console.WriteLine("结束：" + dt2.ToString() + dt2.Millisecond.ToString());
            TimeSpan span = (TimeSpan) (dt2 - dt1);
            Console.WriteLine("用时：" + span.TotalSeconds.ToString() + "秒");
            Console.WriteLine("用时：" + span.TotalMilliseconds.ToString() + "毫秒");
        }

        public static void Start()
        {
            dt1 = DateTime.Now;
            Console.WriteLine("开始：" + dt1.ToString() + dt1.Millisecond.ToString());
        }
    }
}
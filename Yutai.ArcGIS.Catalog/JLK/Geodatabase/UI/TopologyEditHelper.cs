namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class TopologyEditHelper
    {
        internal static IArray m_pList;

        internal static  event DeleteAllTopolyClassHandler DeleteAllTopolyClass;

        internal static  event DeleteTopolyClassHandler DeleteTopolyClass;

        static TopologyEditHelper()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            m_pList = new ArrayClass();
        }

        internal static void OnDeleteAllTopolyClass()
        {
            if (DeleteAllTopolyClass != null)
            {
                DeleteAllTopolyClass();
            }
        }

        internal static void OnDeleteTopolyClass(int int_0)
        {
            if (DeleteTopolyClass != null)
            {
                DeleteTopolyClass(int_0);
            }
        }

        internal delegate void DeleteAllTopolyClassHandler();

        internal delegate void DeleteTopolyClassHandler(int int_0);
    }
}


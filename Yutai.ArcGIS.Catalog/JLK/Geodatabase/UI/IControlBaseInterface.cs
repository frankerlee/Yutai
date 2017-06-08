namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using System;

    internal interface IControlBaseInterface
    {
        void Init();

        IField Filed { set; }

        bool IsEdit { set; }

        IWorkspace Workspace { set; }
    }
}


using System;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    internal interface IFloaty
    {
        event EventHandler Docking;

        void Dock();
        void Float();
        void Hide();
        void Show();

        bool DockOnHostOnly { get; set; }

        bool DockOnInside { get; set; }

        string Text { get; set; }
    }
}
using System;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Events
{
    public class MapIdentifyArgs : EventArgs
    {
        private IEnvelope _envelope;

        public MapIdentifyArgs(IEnvelope envelope)
        {
            _envelope = envelope;
        }

       public IEnvelope Envelope { get { return _envelope; } set { _envelope = value; } }
    }
}
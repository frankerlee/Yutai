using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;

namespace Yutai.Plugins.Identifer.Common
{
    public class LayerObject
    {
        private ILayer _layer = null;

        public ILayer Layer
        {
            get { return this._layer; }
            set { this._layer = null; }
        }

        public LayerObject(ILayer layer)
        {
            this._layer = layer;
        }

        public override string ToString()
        {
            return this._layer.Name;
        }
    }
}
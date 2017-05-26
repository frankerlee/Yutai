using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Services
{
    public interface IFileDialogService
    {
        bool SaveFile(string filter, ref string filename);
        bool Open(string filter, out string filename, int filterIndex = -1);
        bool OpenFile(DataSourceType layerType, out string filename);
        bool OpenFiles(DataSourceType layerType, out string[] filenames);
        bool ChooseFolder(string initialPath, out string chosenPath);
        string Title { get; set; }
        string GetLayerFilter(LayerType layerType);
    }
}

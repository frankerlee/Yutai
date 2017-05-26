using System.IO;
using System.Runtime.Serialization;

namespace Yutai.Services.Serialization
{
    [DataContract]
    public class XmlProjectSettings
    {
        [DataMember]
        public string SavedAsFilename { get; set; }

        public string LoadAsFilename { get; set; }

        /// <summary>
        /// Calculates new path absolute layer path (assuming that relative path is the same) in case project location has changed.
        /// </summary>
        public string UpdateLayerPath(string layerFilename)
        {
            if (string.IsNullOrWhiteSpace(layerFilename))
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(SavedAsFilename) || string.IsNullOrWhiteSpace(LoadAsFilename))
            {
                return string.Empty;
            }

            string newProjectPath = Path.GetDirectoryName(LoadAsFilename) + Path.DirectorySeparatorChar;

            if (string.IsNullOrWhiteSpace(newProjectPath))
            {
                return string.Empty;
            }

            string relativePath = Shared.PathHelper.GetRelativePath(SavedAsFilename, layerFilename);

            return Path.Combine(newProjectPath, relativePath);
        }
    }
}
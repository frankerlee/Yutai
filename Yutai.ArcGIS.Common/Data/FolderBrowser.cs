using System.Windows.Forms.Design;

namespace Yutai.ArcGIS.Common.Data
{
    public class FolderBrowser : FolderNameEditor
    {
        public enum fbStyles
        {
            BrowseForComputer = 4096,
            BrowseForEverything = 16384,
            BrowseForPrinter = 8192,
            RestrictToDomain = 2,
            RestrictToFilesystem = 1,
            RestrictToSubfolders = 8,
            ShowTextBox = 16
        }

        public enum fbFolder
        {
            Desktop,
            Favorites = 6,
            MyComputer = 17,
            MyDocuments = 5,
            MyPictures = 39,
            NetAndDialUpConnections = 49,
            NetworkNeighborhood = 18,
            Printers = 4,
            Recent = 8,
            SendTo,
            StartMenu = 11,
            Templates = 21
        }

        private FolderNameEditor.FolderBrowser folderBrowser_0 = null;

        public Data.FolderBrowser.fbFolder StartLocation
        {
            get
            {
                return (Data.FolderBrowser.fbFolder)this.folderBrowser_0.StartLocation;
            }
            set
            {
                if (value <= Data.FolderBrowser.fbFolder.Templates)
                {
                    switch (value)
                    {
                        case Data.FolderBrowser.fbFolder.Desktop:
                            this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.Desktop;
                            break;
                        case (Data.FolderBrowser.fbFolder)1:
                        case (Data.FolderBrowser.fbFolder)2:
                        case (Data.FolderBrowser.fbFolder)3:
                        case (Data.FolderBrowser.fbFolder)7:
                        case (Data.FolderBrowser.fbFolder)10:
                            break;
                        case Data.FolderBrowser.fbFolder.Printers:
                            this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.Printers;
                            break;
                        case Data.FolderBrowser.fbFolder.MyDocuments:
                            this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.MyDocuments;
                            break;
                        case Data.FolderBrowser.fbFolder.Favorites:
                            this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.Favorites;
                            break;
                        case Data.FolderBrowser.fbFolder.Recent:
                            this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.Recent;
                            break;
                        case Data.FolderBrowser.fbFolder.SendTo:
                            this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.SendTo;
                            break;
                        case Data.FolderBrowser.fbFolder.StartMenu:
                            this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.StartMenu;
                            break;
                        default:
                            switch (value)
                            {
                                case Data.FolderBrowser.fbFolder.MyComputer:
                                    this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.MyComputer;
                                    break;
                                case Data.FolderBrowser.fbFolder.NetworkNeighborhood:
                                    this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.NetworkNeighborhood;
                                    break;
                                case Data.FolderBrowser.fbFolder.Templates:
                                    this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.Templates;
                                    break;
                            }
                            break;
                    }
                }
                else if (value != Data.FolderBrowser.fbFolder.MyPictures)
                {
                    if (value == Data.FolderBrowser.fbFolder.NetAndDialUpConnections)
                    {
                        this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.NetAndDialUpConnections;
                    }
                }
                else
                {
                    this.folderBrowser_0.StartLocation = FolderNameEditor.FolderBrowserFolder.MyPictures;
                }
            }
        }

        public Data.FolderBrowser.fbStyles Style
        {
            get
            {
                return (Data.FolderBrowser.fbStyles)this.folderBrowser_0.Style;
            }
            set
            {
                if (value <= Data.FolderBrowser.fbStyles.ShowTextBox)
                {
                    switch (value)
                    {
                        case Data.FolderBrowser.fbStyles.RestrictToFilesystem:
                            this.folderBrowser_0.Style = FolderNameEditor.FolderBrowserStyles.RestrictToFilesystem;
                            break;
                        case Data.FolderBrowser.fbStyles.RestrictToDomain:
                            this.folderBrowser_0.Style = FolderNameEditor.FolderBrowserStyles.RestrictToDomain;
                            break;
                        default:
                            if (value != Data.FolderBrowser.fbStyles.RestrictToSubfolders)
                            {
                                if (value == Data.FolderBrowser.fbStyles.ShowTextBox)
                                {
                                    this.folderBrowser_0.Style = FolderNameEditor.FolderBrowserStyles.ShowTextBox;
                                }
                            }
                            else
                            {
                                this.folderBrowser_0.Style = FolderNameEditor.FolderBrowserStyles.RestrictToSubfolders;
                            }
                            break;
                    }
                }
                else if (value != Data.FolderBrowser.fbStyles.BrowseForComputer)
                {
                    if (value != Data.FolderBrowser.fbStyles.BrowseForPrinter)
                    {
                        if (value == Data.FolderBrowser.fbStyles.BrowseForEverything)
                        {
                            this.folderBrowser_0.Style = FolderNameEditor.FolderBrowserStyles.BrowseForEverything;
                        }
                    }
                    else
                    {
                        this.folderBrowser_0.Style = FolderNameEditor.FolderBrowserStyles.BrowseForPrinter;
                    }
                }
                else
                {
                    this.folderBrowser_0.Style = FolderNameEditor.FolderBrowserStyles.BrowseForComputer;
                }
            }
        }

        public string Description
        {
            get
            {
                return this.folderBrowser_0.Description;
            }
            set
            {
                this.folderBrowser_0.Description = value;
            }
        }

        public string DirectoryPath
        {
            get
            {
                string result;
                try
                {
                    result = this.folderBrowser_0.DirectoryPath;
                }
                catch
                {
                    result = null;
                }
                return result;
            }
        }

        public FolderBrowser()
        {
            this.folderBrowser_0 = new FolderNameEditor.FolderBrowser();
        }

        public System.Windows.Forms.DialogResult ShowDialog()
        {
            return this.folderBrowser_0.ShowDialog();
        }
    }
}

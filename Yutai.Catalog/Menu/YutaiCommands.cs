using System;
using System.Collections.Generic;
using Yutai.Plugins.Catalog.Commands;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Catalog.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        private List<YutaiCommand> _commands;
        private CatalogPlugin _plugin;
        private List<string> _commandKeys;

        public YutaiCommands(IAppContext context, PluginIdentity identity)
            : base(context, identity)
        {
        }

        public CatalogPlugin Plugin
        {
            get { return _plugin; }
            set { _plugin = value; }
        }

        public List<string> GetKeys()
        {
            return _commandKeys;
        }

        public override IEnumerable<YutaiCommand> GetCommands()
        {
            //第一次被初始化的时候Plugin为空，出现错误，所以在创建菜单的时候重新进行了初始化。

            try
            {
                _commands = new List<YutaiCommand>()
                {
                    new CmdShowCatalogView(_context),
                    new CmdCopyItem(_context),
                    new CmdPasteItem(_context),
                    new CmdDeleteItem(_context),
                    new CmdCreateFeatureClass(_context),
                    new CmdRefreshItem(_context),
                    new CmdRenameItem(_context),
                    new CmdRegisterAsVersion(_context),
                    new CmdUnregisterVersion(_context),
                    new CmdEnableArchiving(_context),
                    new CmdDisableArchiving(_context),
                    new CmdCreateVersionView(_context),
                    new CmdCreateRelationClass(_context),
                    new CmdCreateNetworkDataset(_context),
                    new CmdCreateTopology(_context),
                    new CmdCreateGeometryNetwork(_context),
                    new CmdImportSingleFeatureClass(_context),
                    new CmdImportMultiFeatureClasses(_context),
                    new CmdImportXY(_context),
                    new CmdGxObjectProperty(_context),
                    new CmdCreateAttachments(_context),
                    new CmdDeleteAttachments(_context),
                    new CmdRegisterAsObjectClass(_context),
                    new CmdDataLoader(_context),
                    new CmdExportMultiFeatureClasses(_context),
                    new CmdExportSingleToGDB(_context),
                    new CmdExportShapefile(_context),
                    new CmdCreateFeatureDataset(_context),
                    new CmdCreateTable(_context),
                    new CmdCreateRasterFolder(_context),
                    new CmdCreateRasterDataset(_context),
                    new CmdImportTable(_context),
                    new CmdImportTables(_context),
                    new CmdRasterToGDB(_context),
                    new CmdEGDBStart(_context),
                    new CmdVersionInfo(_context),
                    new CmdConnection(_context),
                    new CmdDisconnection(_context),
                    new CmdConnectionProperty(_context),
                    new CmdNewFGDB(_context),
                    new CmdNewFolder(_context),
                    new CmdNewPGDB(_context),
                    new CmdNewShapefile(_context),
                    new CmdStartServer(_context),
                    new CmdStoptServer(_context),
                    new CmdServiceProperty(_context),
                };
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }


            return _commands;
        }
    }
}
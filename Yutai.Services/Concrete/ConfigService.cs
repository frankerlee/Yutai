using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Helpers;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Yutai.Shared;

namespace Yutai.Services.Concrete
{
    internal class ConfigService : IConfigService
    {
        private readonly IPluginManager _manager;
        //  private readonly IRepository _repository;

        public ConfigService(IPluginManager manager) //, IRepository repository)
        {
            Logger.Current.Trace("In ConfigService");
            if (manager == null) throw new ArgumentNullException("manager");
            //   if (repository == null) throw new ArgumentNullException("repository");

            _manager = manager;
            //    _repository = repository;

            AppConfig.Instance = new AppConfig();
        }

        public AppConfig Config
        {
            get { return AppConfig.Instance; }
        }

        public string ConfigPath
        {
            get { return ConfigPathHelper.GetConfigPath(); }
        }

        public void SaveAll()
        {
            SaveConfig();
            // SaveRepository();
        }

        public void LoadAll()
        {
            //LoadConfig();
            // LoadRepository();
        }

        public bool SaveConfig()
        {
            try
            {
                using (var stream = new StreamWriter(ConfigPathHelper.GetConfigFilePath(), false))
                {
                    string state = GetXmlConfig().Serialize(false);
                    stream.Write(state);
                    stream.Flush();
                    stream.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                const string msg = "设置保存失败。";
                Logger.Current.Error(msg, ex);
                MessageService.Current.Info(msg);
            }
            return false;
        }

        public bool LoadConfig()
        {
            var path = ConfigPathHelper.GetConfigFilePath();
            Logger.Current.Trace("开始引导配置文件 " + path);
            if (!File.Exists(path))
            {
                Logger.Current.Debug("配置文件 {0} 不存在", path);
                return false;
            }

            try
            {
                XmlConfig xmlConfig;
                using (var stream = new StreamReader(path, Encoding.UTF8))
                {
                    string state = stream.ReadToEnd();
                    xmlConfig = state.Deserialize<XmlConfig>();
                    stream.Close();
                }

                ApplyXmlConfig(xmlConfig);
                Logger.Current.Trace("配置文件引导结束");
                return true;
            }
            catch (Exception ex)
            {
                MessageService.Current.Info("配置引导失败，原因为: " + ex.Message);
            }
            return false;
        }

        /*  private void ApplyRepositoryConfig(XmlRepository xmlRepository)
          {
              if (xmlRepository == null) return;
  
              if (xmlRepository.Folders != null)
              {
                  foreach (var item in xmlRepository.Folders)
                  {
                      _repository.AddFolderLink(item);
                  }
              }
  
              if (xmlRepository.Connections != null)
              {
                  foreach (var item in xmlRepository.Connections)
                  {
                      _repository.AddConnection(item);
                  }
              }
  
              if (xmlRepository.WmsServers != null)
              {
                  _repository.ClearWmsServers();
  
                  foreach (var item in xmlRepository.WmsServers)
                  {
                      _repository.AddWmsServer(item);
                  }
              }
  
              if (xmlRepository.TmsProviders != null)
              {
                  _repository.TmsProviders.Clear();
  
                  _repository.TmsProviders.AddRange(xmlRepository.TmsProviders);
              }
  
              if (xmlRepository.TmsGroups != null)
              {
                  _repository.TmsGroups.Clear();
  
                  _repository.TmsGroups.AddRange(xmlRepository.TmsGroups);
              }
  
              if (xmlRepository.ExpandedFolders != null)
              {
                  _repository.ExpandedFolders.Clear();
                  _repository.ExpandedFolders.AddRange(xmlRepository.ExpandedFolders);
              }
          }*/

        private void ApplyXmlConfig(XmlConfig xmlConfig)
        {
            AppConfig.Instance = xmlConfig.Settings;
            AppConfig.Instance.ApplicationPlugins = xmlConfig.ApplicationPlugins.Select(p => p.Guid).ToList();
        }

        private XmlConfig GetXmlConfig()
        {
            var xmlConfig = new XmlConfig {Settings = AppConfig.Instance};

            var plugins =
                _manager.ApplicationPlugins.Select(p => new XmlPlugin {Guid = p.Identity.Guid, Name = p.Identity.Name});

            xmlConfig.ApplicationPlugins = plugins.ToList();

            return xmlConfig;
        }

        /* private bool LoadRepository()
         {
             var path = ConfigPathHelper.GetRepositoryConfigPath();
             Logger.Current.Trace("Start LoadRepository. Config file: " + path);
 
             if (!File.Exists(path))
             {
                 Logger.Current.Debug("Repository config file {0} does not exist", path);
                 return false;
             }
 
             try
             {
                 XmlRepository xmlRepository;
                 using (var stream = new StreamReader(path, Encoding.UTF8))
                 {
                     string state = stream.ReadToEnd();
                     xmlRepository = state.Deserialize<XmlRepository>();
                     stream.Close();
                 }
 
                 ApplyRepositoryConfig(xmlRepository);
                 Logger.Current.Trace("End LoadRepository");
                 return true;
             }
             catch (Exception ex)
             {
                 MessageService.Current.Info("Failed to load the state of the repository: " + ex.Message);
             }
             return false;
         }
 
         private bool SaveRepository()
         {
             _repository.PrepareToSave();
 
             try
             {
                 using (var stream = new StreamWriter(ConfigPathHelper.GetRepositoryConfigPath(), false))
                 {
                     var state = new XmlRepository(_repository).Serialize(false);
                     stream.Write(state);
                     stream.Flush();
                     stream.Close();
                     return true;
                 }
             }
             catch (Exception ex)
             {
                 const string msg = "Failed to save the state of the repository.";
                 Logger.Current.Error(msg, ex);
                 MessageService.Current.Info(msg);
             }
             return false;
         }*/
    }
}
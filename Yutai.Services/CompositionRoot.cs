using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;
using Yutai.Services.Concrete;
using Yutai.Services.Serialization;
using Yutai.Shared;
using Yutai.Shared.Log;

namespace Yutai.Services
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterService<IFileDialogService, FileDialogService>()
                .RegisterService<IMessageService, MessageService>() // FlexibleMessageService
                .RegisterSingleton<ILoggingService, LoggingService>()
                .RegisterSingleton<IProjectService, ProjectService>()
                .RegisterSingleton<IConfigService, ConfigService>()
                .RegisterService<ProjectLoaderLegacy>();
               // .RegisterSingleton<ILayerService, LayerService>()
               // .RegisterSingleton<IGeoLocationService, NominatimGeoLocationService>()               
                //.RegisterSingleton<ISelectLayerService, SelectLayerService>()                
                //.RegisterService<ICreateLayerView, CreateLayerView>()
                //.RegisterService<ImageSerializationService>()                
                //.RegisterSingleton<ITempFileService, TempFileService>()               
                //.RegisterSingleton<IProjectLoader, ProjectLoader>()
                //.RegisterView<IMissingLayersView, MissingLayersView>()
                //.RegisterView<ISelectLayerView, SelectLayerView>();

          /*  EnumHelper.RegisterConverter(new SelectionOperationConverter());
            EnumHelper.RegisterConverter(new AreaUnitsConverter());
            EnumHelper.RegisterConverter(new LogLevelConverter());
            EnumHelper.RegisterConverter(new GeometryTypeConverter());
            EnumHelper.RegisterConverter(new SaveResultConverter());
            EnumHelper.RegisterConverter(new TileProviderConverter());
            EnumHelper.RegisterConverter(new InterpolationTypeConverter());
            EnumHelper.RegisterConverter(new RasterOverviewSamplingConverter());
            EnumHelper.RegisterConverter(new RasterOverviewTypeConverter());
            EnumHelper.RegisterConverter(new DynamicVisiblityModeConverter());*/
        }
    }
}

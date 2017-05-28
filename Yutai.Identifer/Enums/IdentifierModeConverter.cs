using System;
using Yutai.Api.Enums;
using Yutai.Shared;

namespace Yutai.Plugins.Identifer.Enums
{
    internal class IdentifierModeConverter : IEnumConverter<IdentifierMode>
    {
        public string GetString(IdentifierMode value)
        {
            switch (value)
            {
                case IdentifierMode.CurrentLayer:
                    return "当前图层";
                case IdentifierMode.TopLayer:
                    return "顶层图层";
                case IdentifierMode.AllLayer:
                    return "所有图层";
                case IdentifierMode.VisibleLayer:
                    return "可见图层";
                case IdentifierMode.SelectableLayer:
                    return "可选图层";
            }
            throw new ApplicationException("错误的信息查询模式");
        }
    }
}
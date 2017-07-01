using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Yutai.ArcGIS.Common.ControlExtendEx
{
    public sealed class ImageComboItemConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext itypeDescriptorContext_0, Type type_0)
        {
            return ((type_0 == typeof(InstanceDescriptor)) || base.CanConvertTo(itypeDescriptorContext_0, type_0));
        }

        public override object ConvertTo(ITypeDescriptorContext itypeDescriptorContext_0, CultureInfo cultureInfo_0,
            object object_0, Type type_0)
        {
            if (type_0 == typeof(InstanceDescriptor))
            {
                return new InstanceDescriptor(object_0.GetType().GetConstructor(Type.EmptyTypes), null, false);
            }
            return base.ConvertTo(itypeDescriptorContext_0, cultureInfo_0, object_0, type_0);
        }
    }
}
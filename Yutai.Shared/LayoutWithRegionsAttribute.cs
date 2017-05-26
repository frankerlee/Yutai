// -------------------------------------------------------------------------------------------
// <copyright file="LayoutWithRegionsAttribute.cs" company="MapWindow OSS Team - www.mapwindow.org">
//  MapWindow OSS Team - 2015
// </copyright>
// -------------------------------------------------------------------------------------------

using System;

namespace Yutai.Shared
{
    /// <summary>
    /// Specifies that R# cleanup should use layout with region when reordering members of the class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class LayoutWithRegionsAttribute : Attribute
    {
    }
}
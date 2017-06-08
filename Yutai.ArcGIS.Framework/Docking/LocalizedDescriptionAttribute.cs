﻿using System;
using System.ComponentModel;

namespace Yutai.ArcGIS.Framework.Docking
{
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private bool m_initialized;

        public LocalizedDescriptionAttribute(string key) : base(key)
        {
            this.m_initialized = false;
        }

        public override string Description
        {
            get
            {
                if (!this.m_initialized)
                {
                    string description = base.Description;
                    base.DescriptionValue = ResourceHelper.GetString(description);
                    if (base.DescriptionValue == null)
                    {
                        base.DescriptionValue = string.Empty;
                    }
                    this.m_initialized = true;
                }
                return base.DescriptionValue;
            }
        }
    }
}


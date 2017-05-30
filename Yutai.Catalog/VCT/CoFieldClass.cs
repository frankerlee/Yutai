namespace Yutai.Catalog.VCT
{
    using System;
    using System.ComponentModel;

    public class CoFieldClass : IDisposable, ICoClone, ICoField
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private CoFieldType coFieldType_0 = CoFieldType.字符型;
        private int int_0 = 0;
        private int int_1 = 0;
        private string string_0 = string.Empty;
        private string string_1 = string.Empty;
        private string string_2 = string.Empty;
        private string string_3 = Guid.NewGuid().ToString();

        public virtual object Clone()
        {
            return new CoFieldClass { AliasName = this.string_0, DefaultValue = this.string_1, Enable = this.bool_1, ID = this.string_3, Length = this.int_0, Name = this.string_2, Precision = this.int_1, Required = this.bool_0, Type = this.coFieldType_0 };
        }

        public void Dispose()
        {
        }

        ~CoFieldClass()
        {
        }

        public override string ToString()
        {
            return this.string_2;
        }

        [Description("字段别名"), DisplayName("字段别名")]
        public string AliasName
        {
            get
            {
                return this.string_0;
            }
            set
            {
                if (this.string_0 != value)
                {
                    this.string_0 = value;
                }
            }
        }

        [Description("默认值"), DisplayName("默认值")]
        public string DefaultValue
        {
            get
            {
                return this.string_1;
            }
            set
            {
                if (this.string_1 != value)
                {
                    this.string_1 = value;
                }
            }
        }

        [Description("设置此字段是否参与检查"), DisplayName("是否检查")]
        public bool Enable
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                if (this.bool_1 != value)
                {
                    this.bool_1 = value;
                }
            }
        }

        [Browsable(false)]
        public string ID
        {
            get
            {
                return this.string_3;
            }
            set
            {
                if (this.string_3 != value)
                {
                    this.string_3 = value;
                }
            }
        }

        [Description("字段长度"), DisplayName("字段长度")]
        public int Length
        {
            get
            {
                return this.int_0;
            }
            set
            {
                if (this.int_0 != value)
                {
                    this.int_0 = value;
                }
            }
        }

        [Description("字段名称"), DisplayName("字段名称")]
        public string Name
        {
            get
            {
                return this.string_2;
            }
            set
            {
                if (this.string_2 != value)
                {
                    this.string_2 = value;
                }
            }
        }

        [DisplayName("精度"), Description("精度")]
        public int Precision
        {
            get
            {
                return this.int_1;
            }
            set
            {
                if (this.int_1 != value)
                {
                    this.int_1 = value;
                }
            }
        }

        [DisplayName("必要的"), Description("必要的")]
        public bool Required
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                if (this.bool_0 != value)
                {
                    this.bool_0 = value;
                }
            }
        }

        [Description("字段类型"), DisplayName("字段类型")]
        public CoFieldType Type
        {
            get
            {
                return this.coFieldType_0;
            }
            set
            {
                if (this.coFieldType_0 != value)
                {
                    this.coFieldType_0 = value;
                }
            }
        }
    }
}


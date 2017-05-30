namespace Yutai.Catalog.VCT
{
    using System;
    using System.ComponentModel;

    public class CoRuleClass
    {
        private string string_0;
        private string string_1;
        private string string_2;
        private uint uint_0;

        public CoRuleClass()
        {
            this.string_0 = Guid.NewGuid().ToString();
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.uint_0 = 0;
        }

        public CoRuleClass(string string_3, string string_4)
        {
            this.string_0 = Guid.NewGuid().ToString();
            this.string_1 = string.Empty;
            this.string_2 = string.Empty;
            this.uint_0 = 0;
            this.string_0 = string_3;
            this.string_1 = string_4;
        }

        [Browsable(true), Description("规则的中文说明"), DisplayName("描述")]
        public string Description
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        [Description("唯一标识码"), Browsable(false), DisplayName("标识码")]
        public string ID
        {
            get
            {
                return this.string_0;
            }
        }

        [Description("以数字0-100来描述错误级别，数字越大，错误越严重"), DisplayName("分级")]
        public uint Level
        {
            get
            {
                return this.uint_0;
            }
            set
            {
                if (value > 100)
                {
                    throw new Exception("属性值超出范围[0,100]");
                }
                this.uint_0 = value;
            }
        }

        [Browsable(true), Description("以条件表达式为规则文本，参考SQL标准语法，根据具体使用对象语法有所不同"), DisplayName("规则文本")]
        public string Rule
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }
    }
}


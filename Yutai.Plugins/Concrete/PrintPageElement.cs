using System.ComponentModel;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Concrete
{
    [DefaultProperty("Value")]
    public class PrintPageElement : IPrintPageElement
    {
        private string _name;
        private string _value;
        private string _aliasName;

        public PrintPageElement(string name, string aliasName, string value)
        {
            _name = name;
            _value = value;
            _aliasName = aliasName;
        }
        [DisplayName("字段名称")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DisplayName("字段别名")]
        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }
        [DisplayName("字段值"), Description("系统将依据字段名称、别名和值自动替换模板中的标注内容")]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
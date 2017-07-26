using Yutai.Plugins.Template.Interfaces;

namespace Yutai.Plugins.Template.Concretes
{
    public class YTDomainValue 
    {
        private object _value;
        private string _description;

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
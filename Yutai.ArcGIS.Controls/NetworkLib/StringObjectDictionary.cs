using System.Collections;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class StringObjectDictionary : DictionaryBase
    {
        public void Add(string key, object value)
        {
            base.Dictionary.Add(key, value);
        }

        public bool Contains(string key)
        {
            return base.Dictionary.Contains(key);
        }

        public void Remove(string key)
        {
            base.Dictionary.Remove(key);
        }

        public object this[string key]
        {
            get { return base.Dictionary[key]; }
            set { base.Dictionary[key] = value; }
        }

        public ICollection Keys
        {
            get { return base.Dictionary.Keys; }
        }

        public ICollection Values
        {
            get { return base.Dictionary.Values; }
        }
    }
}
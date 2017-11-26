using System.Configuration;

namespace ViewModel.Configs.Size
{
    [ConfigurationCollection(typeof(SizeElement))]
    public class SizeCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new SizeElement();
        protected override object GetElementKey(ConfigurationElement element) => ((SizeElement) element).SizeType;
        public SizeElement this[int key] => (SizeElement) BaseGet(key);
        public new SizeElement this[string key] => (SizeElement)BaseGet(key);
    }
}

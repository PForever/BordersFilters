using System.Configuration;

namespace ViewModel.Configs.Path
{
    [ConfigurationCollection(typeof(PathElement))]
    public class PathCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new PathElement();
        protected override object GetElementKey(ConfigurationElement element) => ((PathElement) element).PathType;
        public PathElement this[int key] => (PathElement) BaseGet(key);
        public new PathElement this[string key] => (PathElement)BaseGet(key);
    }
}

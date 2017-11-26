using System.Configuration;

namespace ViewModel.Configs.Path
{
    public class PathConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty(nameof(PathCollection))]
        public PathCollection PathItems => (PathCollection) base[nameof(PathCollection)];
    }
}

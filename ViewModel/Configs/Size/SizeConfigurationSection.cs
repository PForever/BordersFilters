using System.Configuration;

namespace ViewModel.Configs.Size
{
    public class SizeConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty(nameof(SizeCollection))]
        public SizeCollection SizeItems => (SizeCollection) base[nameof(SizeCollection)];
    }
}

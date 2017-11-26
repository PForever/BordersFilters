using System.Configuration;

namespace ViewModel.Configs.Logic
{
    public class LogicConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty(nameof(LogicElement))]
        public LogicElement LogicElement => (LogicElement) base[nameof(LogicElement)];
    }
}

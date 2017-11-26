using System.Configuration;
using ViewModel.Configs.Logic;
using ViewModel.Configs.Path;
using ViewModel.Configs.Size;

namespace ViewModel.Configs
{
    public static class Configurator
    {
        private static readonly Configuration _configurator;

        public static SizeConfigurationSection Size { get; }
        public static PathConfigurationSection Path { get; }
        public static LogicConfigurationSection Logic { get; }
        public static void Save() => _configurator.Save();
        static Configurator()
        {
            _configurator = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Size = (SizeConfigurationSection)_configurator.Sections[nameof(SizeConfigurationSection)];
            Path = (PathConfigurationSection)_configurator.Sections[nameof(PathConfigurationSection)];
            Logic = (LogicConfigurationSection)_configurator.Sections[nameof(LogicConfigurationSection)];
        }
        
            
        //public static Configurator CreateConfigurator() => new Configurator();
    }
}

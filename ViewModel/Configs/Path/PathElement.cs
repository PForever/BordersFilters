using System.Configuration;

namespace ViewModel.Configs.Path

{
    public class PathElement : ConfigurationElement
    {
        //private const string PathTypeDefault = "";
        //private const string PathDefault = @"C:\";
        [ConfigurationProperty(nameof(PathType), IsKey = true)]
        public string PathType
        {
            get => (string)base[nameof(PathType)];
            set => base[nameof(PathType)] = value;
        }
        [ConfigurationProperty(nameof(Path), IsRequired = true)]
        public string Path
        {
            get => (string) base[nameof(Path)];
            set => base[nameof(Path)] = value;
        }
    }
}

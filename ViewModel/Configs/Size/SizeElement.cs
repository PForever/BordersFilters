using System;
using System.Configuration;

namespace ViewModel.Configs.Size
{
    public class SizeElement : ConfigurationElement
    {
        //private const string SizeTypeDefault = "";
        //private const string WidthDefault = "300";
        //private const string HeightDefault = "400";
        [ConfigurationProperty(nameof(SizeType), IsKey = true)]
        public string SizeType
        {
            get => (string)base[nameof(SizeType)];
            set => base[nameof(SizeType)] = value;
        }
        [ConfigurationProperty(nameof(Width), IsRequired = true)]
        public string Width
        {
            get => (string) base[nameof(Width)];
            set => base[nameof(Width)] = value;
        }
        [ConfigurationProperty(nameof(Height), IsRequired = true)]
        public string Height
        {
            get => (string)base[nameof(Height)];
            set => base[nameof(Height)] = value;
        }
        [ConfigurationProperty(nameof(Top), IsRequired = true)]
        public string Top
        {
            get => (string)base[nameof(Top)];
            set => base[nameof(Top)] = value;
        }
        [ConfigurationProperty(nameof(Left), IsRequired = true)]
        public string Left
        {
            get => (string)base[nameof(Left)];
            set => base[nameof(Left)] = value;
        }
        
        [ConfigurationProperty(nameof(WindowState), IsRequired = true)]
        public int WindowState
        {
            get => Convert.ToInt32(base[nameof(WindowState)]);
            set => base[nameof(WindowState)] = value;
        }
    }
}

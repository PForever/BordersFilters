using System;
using System.Configuration;

namespace ViewModel.Configs.Logic
{
    public class LogicElement : ConfigurationElement
    {
        [ConfigurationProperty(nameof(Sigma), IsRequired = true)]
        public double Sigma
        {
            get => (double)base[nameof(Sigma)];
            set => base[nameof(Sigma)] = value;
        }
        [ConfigurationProperty(nameof(Matrix), IsRequired = true)]
        public int Matrix
        {
            get => (int) base[nameof(Matrix)];
            set => base[nameof(Matrix)] = value;
        }
        [ConfigurationProperty(nameof(RGB), IsRequired = true)]
        public Boolean RGB
        {
            get => (Boolean)base[nameof(RGB)];
            set => base[nameof(RGB)] = value;
        }
    }
}

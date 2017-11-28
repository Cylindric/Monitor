using System.Configuration;

namespace Agent
{

    public class PluginSettings : ConfigurationSection
    {
        public const string SectionName = "PluginSettings";
        private const string PluginCollectionName = "Plugins";

        [ConfigurationProperty(PluginCollectionName)]
        [ConfigurationCollection(typeof(PluginCollection), AddItemName="add")]
        public PluginCollection Plugins { get { return (PluginCollection)base[PluginCollectionName]; } }

        public static PluginSettings GetConfig()
        {
            var s = ConfigurationManager.GetSection(SectionName) as PluginSettings;
            return s;
//            return (PluginSettings)System.Configuration.ConfigurationManager.GetSection(SectionName) ?? new PluginSettings();
        }

    }

    public class PluginCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PluginSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PluginSetting)element).Code;
        }
    }

    public class PluginSetting : ConfigurationElement
    {
        [ConfigurationProperty("code", IsRequired = true)]
        public string Code
        {
            get { return (string)this["code"]; }
            set { this["code"] = value; }
        }

        [ConfigurationProperty("interval", IsRequired = true)]
        public int Interval
        {
            get { return (int)this["interval"]; }
            set { this["interval"] = value; }
        }
    }

        //public Plugins Plugins
        //{
        //    get
        //    {
        //        object o = this["Plugins"];
        //        return o as Plugins;
        //    }
        //}

        //public static PluginSettings GetConfig()
        //{
        //    return (PluginSettings)System.Configuration.ConfigurationManager.GetSection("Plugins") ?? new PluginSettings();
        //}    //public class Plugins : ConfigurationElementCollection
    //{
    //    public Plugin this[int index]
    //    {
    //        get
    //        {
    //            return base.BaseGet(index) as Plugin;
    //        }
    //        set
    //        {
    //            if (base.BaseGet(index) != null)
    //            {
    //                base.BaseRemoveAt(index);
    //            }
    //            this.BaseAdd(index, value);
    //        }
    //    }


    //    protected override System.Configuration.ConfigurationElement CreateNewElement()
    //    {
    //        return new Plugin();
    //    }

    //    protected override object GetElementKey(System.Configuration.ConfigurationElement element)
    //    {
    //        return ((Plugin)element).Name;
    //    }
    //}

    //public class Plugin : ConfigurationElement
    //{
    //    [ConfigurationProperty("code", IsRequired = true)]
    //    public string Code
    //    {
    //        get
    //        {
    //            return this["code"] as string;
    //        }
    //    }

    //    [ConfigurationProperty("name", IsRequired = true)]
    //    public string Name
    //    {
    //        get
    //        {
    //            return this["name"] as string;
    //        }
    //    }

    //}


}

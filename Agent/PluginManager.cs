using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Utils;

namespace Agent
{
    public struct AvailablePlugin
    {
        public string AssemblyPath;
        public string ClassName;
        public IAgentPlugin Plugin;
    }

    public class PluginManager
    {

        private List<AvailablePlugin> mPlugins = new List<AvailablePlugin>();

        public List<AvailablePlugin> Plugins { get { return mPlugins; } }

        public void LoadPlugins(string code)
        {
            string assemblyPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            DirectoryInfo pluginRoot = new DirectoryInfo(assemblyPath);

            if (pluginRoot.Exists)
            {
                foreach (FileInfo files in pluginRoot.GetFiles("*.dll"))
                {
                    try
                    {
                        Assembly testAssembly = Assembly.LoadFile(files.FullName);
                        ExamineAssembly(testAssembly, code);
                    }
                    catch
                    {
                    }
                }
            }
        }


        public void InitialisePlugins(IAgentPluginHost host)
        {
            foreach (var p in mPlugins)
            {
                p.Plugin.Initialise(host);
            }
        }


        private void ExamineAssembly(Assembly assembly, string code)
        {
            // Loop through all the types in the DLL, looking for a valid plugin
            foreach (var t in assembly.GetTypes())
            {
                // Only interested in Public types
                if (t.IsPublic)
                {
                    // And not interested in Abstract types
                    if (!t.Attributes.HasFlag(TypeAttributes.Abstract))
                    {
                        // Now check if it the type implements our interface
                        Type iface = t.GetInterface("IAgentPlugin", true);

                        if (iface != null)
                        {
                            // It does
                            AvailablePlugin p = new AvailablePlugin();
                            p.AssemblyPath = assembly.Location;
                            p.ClassName = t.FullName;
                            p.Plugin = CreateInstance(p);
                            if (p.Plugin.Code == code)
                            {
                                mPlugins.Add(p);
                            }
                        }
                    }
                }
            }
        }


        private IAgentPlugin CreateInstance(AvailablePlugin availablePlugin)
        {
            IAgentPlugin plugin;
            try
            {
                Assembly dll = Assembly.LoadFrom(availablePlugin.AssemblyPath);
                plugin = (IAgentPlugin)dll.CreateInstance(availablePlugin.ClassName);
            }
            catch
            {
                return null;
            }
            return plugin;
        }

    }
}

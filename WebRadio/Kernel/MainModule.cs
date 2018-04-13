using Ninject.Modules;
using Persistence.Mapper;
using PlayerInterface.Interfaces;
using RadioPlayerMock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebRadio.Mapper;
using WebRadio.ViewModel;
using WmpRadioPlayer;

namespace WebRadio.Kernel
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlayer>().To<RadioMock>();

            Bind<IRadioChannelListViewModel>().To<RadioChannelListViewModel>();

            LoadStatics();
            LoadPlayerPlugins();
        }

        private void LoadStatics()
        {
            Bind<ISettingsDao>().ToConstant(new SettingsFileDao("Settings.sts"));
        }

        private void LoadPlayerPlugins()
        {
            var uri = new Uri(new Uri(System.Reflection.Assembly.GetEntryAssembly().Location), ".").AbsolutePath + "Plugins";
            

            if (Directory.Exists(uri))
            {
                var assemblies = LoadAssemblies(uri);
                var types = FindImplementation(assemblies, typeof(IPlayer));

                foreach(var type in types)
                {
                    Bind<IPlayer>().To(type);
                }
            } else
            {
                Directory.CreateDirectory(uri);
            }

            //new Uri(System.Reflection.Assembly.GetEntryAssembly().Location).
            //IEnumerable<Assembly> playerAssemblies = 
        }

        private IEnumerable<Assembly> LoadAssemblies(string folder)
        {
            IEnumerable<string> dlls =
                from file in new DirectoryInfo(folder).GetFiles()
                where file.Extension == ".dll"
                select file.FullName;

            IList<Assembly> assemblies = new List<Assembly>();

            foreach (string dll in dlls)
            {
                try
                {
                    assemblies.Add(Assembly.LoadFile(dll));
                }
                catch
                {
                }
            }

            return assemblies;
        }

        private IEnumerable<Type> FindImplementation(
            IEnumerable<Assembly> assemblies,
            Type serviceType)
        {
            var implementationType = (
                from dll in assemblies
                from type in dll.GetExportedTypes()
                where serviceType.IsAssignableFrom(type)
                where !type.IsAbstract
                where !type.IsGenericTypeDefinition
                select type);

            return implementationType;
        }
    }
}

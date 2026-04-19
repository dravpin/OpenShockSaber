using IPA;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace OpenShockSaber
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger, IPA.Config.Config conf, Zenjector zenjector)
        {
            Log = logger;
            
            OpenShockSaberConfig.Instance = conf.Generated<OpenShockSaberConfig>();

            zenjector.Install(Location.App, container => {
                container.Bind<OpenShockManager>().AsSingle();
            });
            
            zenjector.Install(Location.Player, container => {
                container.BindInterfacesAndSelfTo<NoteEventHandler>().AsSingle();
            });
        }
    }
}

using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace OpenShockSaber
{
    internal class OpenShockSaberConfig
    {
        public static OpenShockSaberConfig Instance { get; set; }
        
        public virtual string APIEndpoint { get; set; } = "https://api.openshock.com/v1/shock";
        public virtual string APIKey { get; set; } = "your-api-key-here";
        public virtual string DeviceID { get; set; } = "your-device-id";
        
        public virtual int MinIntensity { get; set; } = 1;
        public virtual int MaxIntensity { get; set; } = 16;
        public virtual int DurationMs { get; set; } = 300;
        
        public virtual float CooldownSeconds { get; set; } = 10.0f;
        public virtual int MinScoreThreshold { get; set; } = 100;
        public virtual bool ShockOnBadCut { get; set; } = true;
    }
}
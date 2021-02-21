using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;

namespace TranslucentPlugin
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class TranslucentPlugin : BasePlugin
    {
        public const string Id = "net.kunmc.lab";

        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> Name { get; private set; }

        public override void Load()
        {
            Name = Config.Bind("Fake", "Name", ":>");

            Harmony.PatchAll();
        }

        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
        public static class ExamplePatch
        {
            public static void Postfix(PlayerControl __instance)
            {
                __instance.nameText.Text = PluginSingleton<TranslucentPlugin>.Instance.Name.Value;
            }
        }

        private void vanish(PlayerControl playerControl)
        {
            playerControl.Visible = false;
        }
        
        private void unvanish(PlayerControl playerControl)
        {
            playerControl.Visible = true;
        }
    }
}

using BepInEx;
using UnityEngine;
using HarmonyLib;

[BepInPlugin("me.fastshotgun", "Fast Shotgun", "1.0")]
public class Plugin : BaseUnityPlugin
{
    public static ConfigEntry<bool> FastShotgunEnabled;
    void Awake()
    {
        FastShotgunEnabled=Config.Bind(
            "General", 
            "Enabled", true, 
            "Whether the mod is enabled or not."
        );
        Logger.LogInfo("Mod loaded!");
        var harmony = new Harmony("me.fastshotgun");
        harmony.PatchAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Logger.LogInfo("Pressed F1");
        }
    }
};

[HarmonyPatch(typeof(Shotgun), "Update")]
class Shotgun_Patch
{
    static void Prefix(Shotgun __instance)
    {
        Traverse.Create(__instance)
            .Field("gunReady")
            .SetValue(true);
    }
}
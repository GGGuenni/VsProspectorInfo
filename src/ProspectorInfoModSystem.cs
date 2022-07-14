using Foundation.ModConfig;
using HarmonyLib;
using ProspectorInfo.Map;
using System.IO;
using System.Reflection;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

namespace ProspectorInfo
{
    public class ProspectorInfoModSystem : ModSystem
    {
        private const string MapLayerName = "prospectorInfo";

        public const string DATAFILE = "vsprospectorinfo.data.json";
        public ModConfig Config;

        public override void Start(ICoreAPI api)
        {
            Foundation.Extensions.ApiExtensions.MigrateOldDataIfExists(Path.Combine(GamePaths.DataPath, "ModData", api.World.Seed.ToString(),
                "PospectorInfo.prospectorMessages.json"), DATAFILE, api);

            this.Config = api.LoadOrCreateConfig<ModConfig>(this);

            var mapManager = api.ModLoader.GetModSystem<WorldMapManager>();
            mapManager.RegisterMapLayer<ProspectorOverlayLayer>(MapLayerName);

            var prospectorInfoPatches = new Harmony("vsprospectorinfo.patches");
            prospectorInfoPatches.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
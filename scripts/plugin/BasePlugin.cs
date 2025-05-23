using System;
using System.Collections;
using System.IO;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.ObjectCreation;
using MTM101BaldAPI.Registers;
using MTM101BaldAPI.SaveSystem;
using UnityEngine;

namespace MoreYTPs
{
	// Token: 0x02000002 RID: 2
	[BepInPlugin("heptatonic.bbplus.extraytps", "More YTPs", "1.0.0.0")]
	internal class BasePlugin : BaseUnityPlugin
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002174 File Offset: 0x00000374
		private void Awake()
		{
			BasePlugin.plugin = this;
			Harmony harmony = new Harmony("heptatonic.bbplus.extraytps");
			BasePlugin.modPath = AssetLoader.GetModPath(this);
			MTM101BaldiDevAPI.AddWarningScreen("Test_Warning_Screen2.", false);
			GeneratorManagement.Register(this, GenerationModType.Addend, delegate(string levelName, int levelNum, SceneObject obj)
			{
				foreach (CustomLevelObject customLevelObject in obj.GetCustomLevelObjects())
				{
					bool flag = obj.levelTitle == "F1" || obj.levelTitle == "F2" || obj.levelTitle == "F3" || obj.levelTitle == "F4";
					bool flag2 = flag;
					if (flag2)
					{
						WeightedItemObject[] items = new WeightedItemObject[]
						{
							new WeightedItemObject
							{
								weight = 16000,
								selection = this.alert_ytp
							},
							new WeightedItemObject
							{
								weight = 7000,
								selection = this.times_ytp
							},
							new WeightedItemObject
							{
								weight = 16500,
								selection = this.purple_ytp
							},
							new WeightedItemObject
							{
								weight = 15000,
								selection = this.plusOrminus_ytp
							},
							new WeightedItemObject
							{
								weight = 11000,
								selection = this.diamond_ytp
							}
						};
						customLevelObject.potentialItems = customLevelObject.potentialItems.AddRangeToArray(items);
					}
				}
			});
			LoadingEvents.RegisterOnAssetsLoaded(base.Info, this.GetAllAssets(), false);
			ModdedSaveGame.AddSaveHandler(base.Info);
			BasePlugin.Instance = this;
			harmony.PatchAllConditionals();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		private void Update()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000205D File Offset: 0x0000025D
		static BasePlugin()
		{
			BasePlugin.assetManager = new AssetManager();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002085 File Offset: 0x00000285
		internal static BasePlugin Instance { get; private set; }

		// Token: 0x06000008 RID: 8 RVA: 0x0000208D File Offset: 0x0000028D
		private IEnumerator GetAllAssets()
		{
			yield return 4;
			yield return "Loading sprites...";
			BasePlugin.assetManager.Add<Sprite>("spr_timesytp", AssetLoader.SpriteFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.subDirectory, "Times_YTP.png"), new Vector2(0.5f, 0.5f), 40f));
			BasePlugin.assetManager.Add<Sprite>("spr_diamondytp", AssetLoader.SpriteFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.subDirectory, "Diamond_YTP.png"), new Vector2(0.5f, 0.5f), 40f));
			BasePlugin.assetManager.Add<Sprite>("spr_purpleytp", AssetLoader.SpriteFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.subDirectory, "Purple_YTP.png"), new Vector2(0.5f, 0.5f), 40f));
			BasePlugin.assetManager.Add<Sprite>("spr_alertytp", AssetLoader.SpriteFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.subDirectory, "Alert_YTP.png"), new Vector2(0.5f, 0.5f), 40f));
			BasePlugin.assetManager.Add<Sprite>("spr_plusorminusytp", AssetLoader.SpriteFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.subDirectory, "Random_YTP.png"), new Vector2(0.5f, 0.5f), 40f));
			yield return "Loading audio...";
			BasePlugin.assetManager.Add<AudioClip>("Sfx_timespickup", AssetLoader.AudioClipFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.sfxSub, "times_collect.wav")));
			BasePlugin.assetManager.Add<AudioClip>("Sfx_diamondpickup", AssetLoader.AudioClipFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.sfxSub, "diamond_collect.wav")));
			BasePlugin.assetManager.Add<AudioClip>("Sfx_purplepickup", AssetLoader.AudioClipFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.sfxSub, "purple_collect.wav")));
			BasePlugin.assetManager.Add<AudioClip>("Sfx_alertpickup", AssetLoader.AudioClipFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.sfxSub, "alert_collect.wav")));
			BasePlugin.assetManager.Add<AudioClip>("Sfx_plusorminuspickup", AssetLoader.AudioClipFromFile(Path.Combine(BasePlugin.modPath, BasePlugin.sfxSub, "random_collect.wav")));
			yield return "Creating SoundObjects...";
			BasePlugin.assetManager.Add<SoundObject>("so_timesytp_pickup", ObjectCreators.CreateSoundObject(BasePlugin.assetManager.Get<AudioClip>("Sfx_timespickup"), "Sfx_TimesYTP_Pickup", SoundType.Effect, Color.white, 0.6f));
			BasePlugin.assetManager.Add<SoundObject>("so_diamond_pickup", ObjectCreators.CreateSoundObject(BasePlugin.assetManager.Get<AudioClip>("Sfx_diamondpickup"), "Sfx_DiamondYTP_Pickup", SoundType.Effect, Color.white, 0.6f));
			BasePlugin.assetManager.Add<SoundObject>("so_purple_pickup", ObjectCreators.CreateSoundObject(BasePlugin.assetManager.Get<AudioClip>("Sfx_purplepickup"), "Sfx_PurpleYTP_Pickup", SoundType.Effect, Color.white, 0.6f));
			BasePlugin.assetManager.Add<SoundObject>("so_alert_pickup", ObjectCreators.CreateSoundObject(BasePlugin.assetManager.Get<AudioClip>("Sfx_alertpickup"), "Sfx_AlertYTP_Pickup", SoundType.Effect, Color.white, 0.6f));
			BasePlugin.assetManager.Add<SoundObject>("so_plusorminus_pickup", ObjectCreators.CreateSoundObject(BasePlugin.assetManager.Get<AudioClip>("Sfx_plusorminuspickup"), "Sfx_PlusOrMinusYTP_Pickup", SoundType.Effect, Color.white, 0.6f));
			this.so_timesytp_pickup = BasePlugin.assetManager.Get<SoundObject>("so_timesytp_pickup");
			this.so_timesytp_pickup.subtitle = false;
			this.so_diamond_pickup = BasePlugin.assetManager.Get<SoundObject>("so_diamond_pickup");
			this.so_diamond_pickup.subtitle = false;
			this.so_purpleytp_pickup = BasePlugin.assetManager.Get<SoundObject>("so_purple_pickup");
			this.so_purpleytp_pickup.subtitle = false;
			this.so_alertytp_pickup = BasePlugin.assetManager.Get<SoundObject>("so_alert_pickup");
			this.so_alertytp_pickup.subtitle = false;
			this.so_plusOrminus_pickup = BasePlugin.assetManager.Get<SoundObject>("so_plusorminus_pickup");
			this.so_plusOrminus_pickup.subtitle = false;
			yield return "Finally building all custom YTPs...";
			this.times_ytp = new ItemBuilder(base.Info).SetEnum(Items.Points).SetItemComponent<ITM_TimesYTP>().SetGeneratorCost(18).SetAsInstantUse().SetSprites(BasePlugin.assetManager.Get<Sprite>("spr_timesytp"), BasePlugin.assetManager.Get<Sprite>("spr_timesytp")).SetPickupSound(BasePlugin.assetManager.Get<SoundObject>("so_timesytp_pickup")).Build();
			this.diamond_ytp = new ItemBuilder(base.Info).SetEnum(Items.Points).SetItemComponent<ITM_DiamondYTP>().SetGeneratorCost(14).SetAsInstantUse().SetSprites(BasePlugin.assetManager.Get<Sprite>("spr_diamondytp"), BasePlugin.assetManager.Get<Sprite>("spr_diamondytp")).SetPickupSound(BasePlugin.assetManager.Get<SoundObject>("so_diamond_pickup")).Build();
			this.purple_ytp = new ItemBuilder(base.Info).SetEnum(Items.Points).SetItemComponent<ITM_PurpleYTP>().SetGeneratorCost(11).SetAsInstantUse().SetSprites(BasePlugin.assetManager.Get<Sprite>("spr_purpleytp"), BasePlugin.assetManager.Get<Sprite>("spr_purpleytp")).SetPickupSound(BasePlugin.assetManager.Get<SoundObject>("so_purple_pickup")).Build();
			this.alert_ytp = new ItemBuilder(base.Info).SetEnum(Items.Points).SetItemComponent<ITM_AlertYTP>().SetGeneratorCost(9).SetAsInstantUse().SetSprites(BasePlugin.assetManager.Get<Sprite>("spr_alertytp"), BasePlugin.assetManager.Get<Sprite>("spr_alertytp")).SetPickupSound(BasePlugin.assetManager.Get<SoundObject>("so_alert_pickup")).Build();
			this.plusOrminus_ytp = new ItemBuilder(base.Info).SetEnum(Items.Points).SetItemComponent<ITM_PlusOrMinusYTP>().SetGeneratorCost(8).SetAsInstantUse().SetSprites(BasePlugin.assetManager.Get<Sprite>("spr_plusorminusytp"), BasePlugin.assetManager.Get<Sprite>("spr_plusorminusytp")).SetPickupSound(BasePlugin.assetManager.Get<SoundObject>("so_plusorminus_pickup")).Build();
			yield break;
		}

		// Token: 0x04000001 RID: 1
		public AssetManager asm;

		// Token: 0x04000002 RID: 2
		private static string subDirectory = "Sprites";

		// Token: 0x04000003 RID: 3
		public static string modPath;

		// Token: 0x04000005 RID: 5
		internal static AssetManager assetManager;

		// Token: 0x04000006 RID: 6
		public static BasePlugin plugin;

		// Token: 0x04000007 RID: 7
		private static string sfxSub = "Sounds";

		// Token: 0x04000008 RID: 8
		internal SoundObject so_timesytp_pickup;

		// Token: 0x04000009 RID: 9
		internal ItemObject times_ytp;

		// Token: 0x0400000A RID: 10
		internal ItemObject diamond_ytp;

		// Token: 0x0400000B RID: 11
		internal SoundObject so_diamond_pickup;

		// Token: 0x0400000C RID: 12
		internal SoundObject so_purpleytp_pickup;

		// Token: 0x0400000D RID: 13
		internal ItemObject purple_ytp;

		// Token: 0x0400000E RID: 14
		internal SoundObject so_alertytp_pickup;

		// Token: 0x0400000F RID: 15
		internal ItemObject alert_ytp;

		// Token: 0x04000010 RID: 16
		internal SoundObject so_plusOrminus_pickup;

		// Token: 0x04000011 RID: 17
		internal ItemObject plusOrminus_ytp;
	}
}

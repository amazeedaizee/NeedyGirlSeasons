using BepInEx;
using HarmonyLib;
using ngov3;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Seasons
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    [BepInProcess("Windose.exe")]
    public class MyPatches : BaseUnityPlugin
    {
        public const string pluginGuid = "needy.girl.seasons";
        public const string pluginName = "Seasons";
        public const string pluginVersion = "1.0.0.0";

        public static PluginInfo PInfo { get; private set; }
        public void Awake()
        {
            PInfo = Info;

            Logger.LogInfo("Seasons loaded.");
            Harmony harmony = new Harmony(pluginGuid);
            MethodInfo originalWebcam = AccessTools.Method(typeof(App_Webcam), "bgView");
            MethodInfo originalStream = AccessTools.Method(typeof(Live), "bgView");
            MethodInfo patchWebcam = AccessTools.Method(typeof(Seasons), "ReplaceCamBack");
            MethodInfo patchStream = AccessTools.Method(typeof(Seasons), "ReplaceStreamBack");

            harmony.Patch(originalWebcam, new HarmonyMethod(patchWebcam));
            harmony.Patch(originalStream, new HarmonyMethod(patchStream));
        }
    }
    public class BuildAssets
    {
        //Building assets.

        public static AssetBundle assets = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(MyPatches.PInfo.Location), "bg_seasons.bundle"));
        public static Sprite bgNormal = assets.LoadAsset<Sprite>("bg_stream");
        public static Sprite bgSpring = assets.LoadAsset<Sprite>("bg_stream_2");
        public static Sprite bgAutumn = assets.LoadAsset<Sprite>("bg_stream_3");
        public static Sprite bgWinter = assets.LoadAsset<Sprite>("bg_stream_4");
        public static Sprite bgSilverNormal = assets.LoadAsset<Sprite>("bg_stream_shield_silver");
        public static Sprite bgSilverSpring = assets.LoadAsset<Sprite>("bg_stream_shield_silver_2");
        public static Sprite bgSilverAutumn = assets.LoadAsset<Sprite>("bg_stream_shield_silver_3");
        public static Sprite bgSilverWinter = assets.LoadAsset<Sprite>("bg_stream_shield_silver_4");
        public static Sprite bgGoldNormal = assets.LoadAsset<Sprite>("bg_stream_shield_gold");
        public static Sprite bgGoldSpring = assets.LoadAsset<Sprite>("bg_stream_shield_gold_2");
        public static Sprite bgGoldAutumn = assets.LoadAsset<Sprite>("bg_stream_shield_gold_3");
        public static Sprite bgGoldWinter = assets.LoadAsset<Sprite>("bg_stream_shield_gold_4");
    }
    public class Seasons
    {
        //Replaces background on Ame's webcam depending on the month.
        public static void ReplaceCamBack(ref Sprite ___background_gold_shield, ref Sprite ___background_no_shield, ref Sprite ___background_silver_shield, ref Image ____backGround)
        {
            DateTime time = DateTime.Now;

            //Winter
            if (time.Month == 12 || time.Month == 1 || time.Month == 2)
            {
                ___background_gold_shield = BuildAssets.bgGoldWinter;
                ___background_silver_shield = BuildAssets.bgSilverWinter;
                ____backGround.sprite = BuildAssets.bgWinter;

            }
            //Spring
            else if (time.Month == 3 || time.Month == 4 || time.Month == 5)
            {
                ___background_gold_shield = BuildAssets.bgGoldSpring;
                ___background_silver_shield = BuildAssets.bgSilverSpring;
                ____backGround.sprite = BuildAssets.bgSpring;

            }
            //Summer (this uses the default game background)
            else if (time.Month == 6 || time.Month == 7 || time.Month == 8)
            {
                ___background_gold_shield = BuildAssets.bgGoldNormal;
                ___background_silver_shield = BuildAssets.bgSilverNormal;
                ____backGround.sprite = BuildAssets.bgNormal;
            }
            //Autumn
            else if (time.Month == 9 || time.Month == 10 || time.Month == 11)
            {
                ___background_gold_shield = BuildAssets.bgGoldAutumn;
                ___background_silver_shield = BuildAssets.bgSilverAutumn;
                ____backGround.sprite = BuildAssets.bgAutumn;
            }
        }

        //Replaces background on KAngels stream depending on the month. Does not include Milestone Streams.
        public static void ReplaceStreamBack(Live __instance)
        {
            DateTime time = DateTime.Now;
            //Winter
            if (time.Month == 12 || time.Month == 1 || time.Month == 2)
            {
                __instance.Tenchan.background_gold_shield = BuildAssets.bgGoldWinter;
                __instance.Tenchan.background_silver_shield = BuildAssets.bgSilverWinter;
                __instance.Tenchan._backGround.sprite = BuildAssets.bgWinter;

            }
            //Spring
            else if (time.Month == 3 || time.Month == 4 || time.Month == 5)
            {
                __instance.Tenchan.background_gold_shield = BuildAssets.bgGoldSpring;
                __instance.Tenchan.background_silver_shield = BuildAssets.bgSilverSpring;
                __instance.Tenchan._backGround.sprite = BuildAssets.bgSpring;

            }
            //Summer (this uses the default game background)
            else if (time.Month == 6 || time.Month == 7 || time.Month == 8)
            {
                __instance.Tenchan.background_gold_shield = BuildAssets.bgGoldNormal;
                __instance.Tenchan.background_silver_shield = BuildAssets.bgSilverNormal;
                __instance.Tenchan._backGround.sprite = BuildAssets.bgNormal;
            }
            //Autumn
            else if (time.Month == 9 || time.Month == 10 || time.Month == 11)
            {
                __instance.Tenchan.background_gold_shield = BuildAssets.bgGoldAutumn;
                __instance.Tenchan.background_silver_shield = BuildAssets.bgSilverAutumn;
                __instance.Tenchan._backGround.sprite = BuildAssets.bgAutumn;
            }
        }
    }
}


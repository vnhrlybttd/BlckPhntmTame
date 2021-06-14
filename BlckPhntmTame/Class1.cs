using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using BepInEx;
using BepInEx.Configuration;

namespace FollowDisabled
{
	[BepInPlugin("FollowDisabled", "FollowDisabled", "1.0.0")]
	[BepInProcess("valheim.exe")]
	public class FollowDisabled : BaseUnityPlugin
    {

		private readonly Harmony harmony = new Harmony("FollowDisabled");

		

	    void Awake()
		{
			Debug.Log("FollowDisabled Loaded");
			this.harmony.PatchAll();
			
		}

		void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
		
		[HarmonyPatch(typeof(ZNetScene), "Awake")]
        public static class ZNetScene_Awake_Patch
        {
            public static void Postfix(ZNetScene __instance)
            {
                if (__instance == null)
                {
                    return;
                }

                var lox = __instance.GetPrefab("Lox");
                if (lox == null)
                {
                    Debug.Log("Lox not loaded.");
                    return;
                }

				var wolf = __instance.GetPrefab("Wolf");
                if (wolf == null)
                {
                    Debug.Log("Wolf not loaded.");
                    return;
                }

				var boar = __instance.GetPrefab("Boar");
                if (boar == null)
                {
                    Debug.Log("Boar not loaded.");
                    return;
                }

                Debug.Log("Removing tameable script from lox.");
				Debug.Log("Removing tameable script from wolf.");
				Debug.Log("Removing tameable script from boar.");
                Destroy(lox.GetComponent<Tameable>());
				Destroy(wolf.GetComponent<Tameable>());
				Destroy(wolf.GetComponent<Procreation>());
				Destroy(boar.GetComponent<Tameable>());
				Destroy(boar.GetComponent<Procreation>());

            }
        }

		[HarmonyPatch(typeof(Tameable), "Interact")]
		private class Patch_Wolf
		{
			private static void Postfix()
			{
                foreach (BaseAI baseAI in BaseAI.GetAllInstances())
                { 
					bool flag5 = baseAI.name == "RRRM_GoldenLox(Clone)";
					bool flag6 = flag5;
					if (flag6)
					{
						Tameable component3 = baseAI.GetComponent<Tameable>();
						component3.m_commandable = true;
					}
					bool flag7 = baseAI.name == "RRRM_BlackLox(Clone)";
					bool flag8 = flag7;
					if (flag8)
					{
						Tameable component4 = baseAI.GetComponent<Tameable>();
						component4.m_commandable = true;
					}
					bool flag9 = baseAI.name == "RRRM_SnowLox(Clone)";
					bool flag10 = flag9;
					if (flag10)
					{
						Tameable component5 = baseAI.GetComponent<Tameable>();
						component5.m_commandable = true;
					}
					bool flag11 = baseAI.name == "RRRM_BabyLox(Clone)";
					bool flag12 = flag11;
					if (flag12)
					{
						Tameable component6 = baseAI.GetComponent<Tameable>();
						component6.m_commandable = true;
					}

                }
			}
		}
	
		

		
	}
}

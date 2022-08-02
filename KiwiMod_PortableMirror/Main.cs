using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABI_RC.Core.Player;
using MelonLoader;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using ABI.CCK.Components;
using ChilloutButtonAPI.UI;
using ChilloutButtonAPI;

namespace KiwiMod
{
    public class PortableMirror : MelonMod
    {
        public static GameObject _mirror;
        public static float _distance;

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("OwO notices your mods.");

            ChilloutButtonAPIMain.OnInit += () =>
            {
                SubMenu menu = ChilloutButtonAPIMain.MainPage.AddSubMenu("Portable Mirror");
                menu.AddToggle("Mirror", "Mirror Toggle", ToggleMirror, false);
                menu.AddSlider("Distance", "Distance", (v) =>
                {
                    _distance = v;
                }, 0.5f, -1f, 5f);
            };
        }

        public static void ToggleMirror(bool state)
        {
            if (state)
            {
                _mirror = GameObject.CreatePrimitive(PrimitiveType.Plane);
                _mirror.transform.localScale = new Vector3(2, 1, 1);

                Renderer rend = _mirror.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("FX/MirrorReflection");

                CVRMirror mirror = _mirror.AddComponent<CVRMirror>();
                mirror.m_TextureSize = 4096;
                mirror.m_ReflectLayers = 3607;

                Collider col = _mirror.GetComponent<Collider>();
                col.enabled = false;

                GameObject camera = Camera.main.gameObject;
                _mirror.transform.position = camera.transform.position + camera.transform.forward + (camera.transform.forward * _distance);
                _mirror.transform.rotation = camera.transform.rotation;
                _mirror.transform.rotation *= Quaternion.Euler(-90f, 0f, 0f);
            } 
            else
            {
                UnityEngine.Object.Destroy(_mirror);
            }
        }
    }
}

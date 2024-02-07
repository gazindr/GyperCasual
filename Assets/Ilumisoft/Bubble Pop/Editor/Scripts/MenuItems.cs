using UnityEditor;
using UnityEngine;

namespace Ilumisoft.BubblePop.Editor.GameTemplate
{
    public static class MenuItems
    {
        [MenuItem("Game Template/Bubble Pop/Welcome")]
        static void OpenPackageUtility()
        {
            PackageUtilityWindow.Init();
        }

        [MenuItem("Game Template/Bubble Pop/Rate")]
        static void Rate()
        {
            var bundleInfo = ScriptableObjectUtility.Find<PackageInfo>();

            if (bundleInfo != null)
            {
                Application.OpenURL(bundleInfo.PackageURL);
            }
        }
    }
}
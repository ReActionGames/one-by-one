using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Continuous
{
    public class ProceduralPathEditorWindow : OdinMenuEditorWindow
    {
        private static string ZoneDirectory = "Assets/Continuous Mode/Data/Path Zones";
        private static ProceduralPathSettings PathSettings;

        [MenuItem("Tools/Procedural Path Editor")]
        private static void Open()
        {
            ProceduralPathEditorWindow window = GetWindow<ProceduralPathEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree
            {
                { "Path Settings", FindPathSettings() }
            };
            tree.AddAllAssetsAtPath("Path Settings", "Assets/Continuous Mode/Data/Path Zones", typeof(ProceduralZone));
            //tree.AddRange(ProceduralPathSettings.Instance.Zones, (obj) => ("Path Settings/Zone " + ProceduralPathSettings.Instance.Zones.Count));
            tree.SortMenuItemsByName();
            //tree.
            return tree;
        }

        private ProceduralPathSettings FindPathSettings()
        {
            ProceduralPathSettings settings = AssetDatabase.FindAssets("t:ProceduralPathSettings")
                .Select(guid => AssetDatabase.LoadAssetAtPath<ProceduralPathSettings>(AssetDatabase.GUIDToAssetPath(guid)))
                .First();

            PathSettings = settings;
            return settings;
        }

        protected override void OnBeginDrawEditors()
        {
            OdinMenuItem selected = this.MenuTree.Selection.FirstOrDefault();
            int toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Add Zone")))
                {
                    PathSettings.AddZone(ProceduralZone.Default);

                    //base.TrySelectMenuItemWithObject(zone);
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Delete Zone")) &&
                    PathSettings.Zones.Count > 0)
                {
                    //ProceduralZone zone = selected.Value as ProceduralZone;

                    //PathSettings.RemoveZone(zone);
                    //AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(zone));

                    //base.TrySelectMenuItemWithObject(PathSettings);
                    //if (PathSettings.Zones.Count > 0)
                    //    base.TrySelectMenuItemWithObject(PathSettings.Zones.Last());

                    //base.MenuTree.MarkDirty();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}
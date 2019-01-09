using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Continuous
{
    public class ProceduralPathEditorWindow : OdinMenuEditorWindow
    {
        private static string ZoneDirectory = "Assets/Continuous Mode/Data/Path Zones";

        [MenuItem("Tools/Procedural Path Editor")]
        private static void Open()
        {
            var window = GetWindow<ProceduralPathEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree
            {
                { "Path Settings", ProceduralPathSettings.Instance }
            };
            tree.AddAllAssetsAtPath("Path Settings", "Assets/Continuous Mode/Data/Path Zones", typeof(ProceduralZone), true, true);
            //tree.
            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Add Zone")))
                {
                    var zone = ScriptableObject.CreateInstance(typeof(ProceduralZone));

                    if (!Directory.Exists(ZoneDirectory))
                    {
                        Directory.CreateDirectory(ZoneDirectory);
                        AssetDatabase.Refresh();
                    }

                    AssetDatabase.CreateAsset(zone, ZoneDirectory);
                    AssetDatabase.Refresh();

                    base.TrySelectMenuItemWithObject(zone);
                }

                //if (SirenixEditorGUI.ToolbarButton(new GUIContent("Delete Zone")))
                //{
                //    ScriptableObjectCreator.ShowDialog<Character>("Assets/Plugins/Sirenix/Demos/Sample - RPG Editor/Character", obj =>
                //    {
                //        obj.Name = obj.name;
                //        base.TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                //    });
                //}
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}
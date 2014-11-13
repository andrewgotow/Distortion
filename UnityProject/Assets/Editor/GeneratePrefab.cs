using UnityEditor;
using UnityEngine;
using System.Collections;

class CreatePrefabFromSelected : ScriptableObject
{
	const string menuTitle = "GameObject/Create Prefab From Selected";
	
	/// <summary>
	/// Creates a prefab from the selected game object.
	/// </summary>
	[MenuItem(menuTitle)]
	static void CreatePrefab()
	{
		GameObject[] obj = Selection.gameObjects;
		
		foreach (GameObject go in obj)
		{
			string name = go.name;
			string localPath = "Assets/" + name + ".prefab";
			
			if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
			{
				if (EditorUtility.DisplayDialog("Are you sure?", "The prefab already exists. Do you want to overwrite it?", "Yes", "No"))
				{
					createNew(go, localPath);
				}
			}
			else
			{
				createNew(go, localPath);
			}
			
		}
	}
	
	static void createNew(GameObject obj, string localPath)
	{
		Object prefab = PrefabUtility.CreatePrefab (localPath, obj);
		EditorUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
		AssetDatabase.Refresh();
	}
	
	/// <summary>
	/// Validates the menu.
	/// </summary>
	/// <remarks>The item will be disabled if no game object is selected.</remarks>
	[MenuItem(menuTitle, true)]
	static bool ValidateCreatePrefab()
	{
		return Selection.activeGameObject != null;
	}
}
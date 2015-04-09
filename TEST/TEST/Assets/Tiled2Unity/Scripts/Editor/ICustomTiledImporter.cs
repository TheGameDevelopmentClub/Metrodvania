using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using UnityEditor;
using UnityEngine;

namespace Tiled2Unity
{
    interface ICustomTiledImporter
    {
        // A game object within the prefab has some custom properites assigned through Tiled that are not consumed by Tiled2Unity
        // This callback gives customized importers a chance to react to such properites.
        void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties);

        // Called just before the prefab is saved to the asset database
        // A last chance opporunity to modify it through script
        void CustomizePrefab(GameObject prefab);
    }
}

[Tiled2Unity.CustomTiledImporter]
class CustomTiledImporterAddComp : Tiled2Unity.ICustomTiledImporter
{
	
	public void HandleCustomProperties(UnityEngine.GameObject gameObject,
	                                   IDictionary<string, string> props)
	{
		// Does this game object have a spawn property?
		if (!props.ContainsKey("spawn"))
			return;
		
		// Are we spawning an Appearing Block?
		if (props ["spawn"] != "WalkingEnemy") {
			//Debug.Log ("spawn");
			return;
		}
		//Debug.Log ("prefab");
		// Load the prefab assest and Instantiate it
		string prefabPath = "Assets/Prefabs/AppearingBlock.prefab";
		UnityEngine.Object spawn = 
			AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
		if (spawn != null)
		{
			GameObject spawnInstance = 
				(GameObject)GameObject.Instantiate(spawn);
			spawnInstance.name = spawn.name;
			
			// Use the position of the game object we're attached to
			spawnInstance.transform.parent = gameObject.transform;
			spawnInstance.transform.localPosition = Vector3.zero;
		}
	}
	
	public void CustomizePrefab(UnityEngine.GameObject prefab)
	{
		// Do nothing
	}
}
// Examples
/*
[Tiled2Unity.CustomTiledImporter]
class CustomImporterAddComponent : Tiled2Unity.ICustomTiledImporter
{
    public void HandleCustomProperties(UnityEngine.GameObject gameObject,
        IDictionary<string, string> props)
    {
        // Simply add a component to our GameObject
        if (props.ContainsKey("AddComp"))
        {
            gameObject.AddComponent(props["AddComp"]);
        }
    }


    public void CustomizePrefab(GameObject prefab)
    {
        // Do nothing
    }
}
*/

using System;
using UnityEngine;

//The main class for our Mod
public class Mod_ModdingAPI : Mod
{
	//The start function; we should handle loading all files in here
	//Not in the constructor
	private void Start()
	{
		//Load the asset bundle
		AssetBundle assetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/raftmod_items");
		if (assetBundle == null)
		{
			RConsole.Log("Couldn't find the asset bundle");
			return;
		}
		
		//Load the sprite
		Sprite icon = assetBundle.LoadAsset<Sprite>("garbage");
		
		//Add a new item to the game
		this.garbage = new ModItem(ItemType.Inventory, "Garbage", "It's just garbage");
		this.garbage.SetIcon(icon);
		ModHelper.AddItem(this, this.garbage);
	}

	//Per tick update
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			ModHelper.GiveItem(this.garbage.itemInstance, 1, null);
		}
	}

	//Initialize the mod context
	//Please don't initialize items or anything else here
	public Mod_ModdingAPI() : base("Modding API", "The main API for modding", "v0.0.1", "1.01b")
	{
	}

	//Store our item, so we can use it later (for recipes, or giving it to the player)
	private ModItem garbage;
}

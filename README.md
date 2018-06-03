# RaftModdingAPI
This is a modding API for Raft, which will support at least Items and Blocks. For now, this is a tool to help you create everything more easily and at some point this might be merged into the [https://github.com/TeKGameR950/RaftModLoader](Core ModLoader).
## Dependencies
Before you can use this, you have to [https://store.steampowered.com/app/648800/Raft/](buy Raft) and look through their [http://raft-game.com/modding-guidelines.html](modding guidelines) and make sure you know at least a basic programming language, so you can start working on your mod. You also need to have [https://github.com/0xd4d/dnSpy](dnSpy), so you can inspect the C# assembly from the dll. Other than that; assets are exported through [https://unity3d.com/get-unity/download](Unity), so if you want any sprites, models or audio, you'll have to install that as well.
## Setting up a modding folder
First, I suggest you set up a modding folder, with all Raft's game files (taken from steamapps/common/Raft in the Steam programs root). This is because you have to work into the Assembly-CSharp.dll file inside Raft_Data/Managed; meaning that if you make (really bad) mistakes, you can mess up your entire game (But don't worry, it should be fine, as long as you stay to the modding API).
## Setting up a Mod class
If you open up dnSpy, you should be able to go to Edit / Create Assembly in the toolbar and you can configure that as following:  
![Screenshot](https://i.imgur.com/gCX0O1Y.png)
Turn the .NET version to 2.0-3.5, set version to 1.0.0.0 and the name to your mod name.
Now you need to import the modloader, which you can do by getting the [https://github.com/TeKGameR950/RaftModLoader](modding dll) and replacing your Assembly-CSharp.dll with it. This modloader provides you a console (to view all messages and to use commands) which you can enable by pressing F10. This is the core of the modding API; it will load the assembly files you create from C# and import it and load it as a mod.
### Example Code
```cs
using System;
using UnityEngine;

public class Main : Mod
{

  //Set up the Mod base class
  //Prefer to use Start() over the constructor
  //Only use constructor for setting up basic values
	public Main(): base("Modding API", "An API for raft modding"){

	}

  //Initialize all mod items, blocks, etc.
	private void Start()
	{
		RConsole.Log("Mod loaded!");
		AssetBundle items = AssetBundle.LoadFromFile("Assets/AssetBundles/raftmod_items");
		if (items == null)
		{
			RConsole.Log("Couldn't find the asset bundle");
			return;
		}
		Sprite garbageIcon = items.LoadAsset<Sprite>("garbage");
		this.garbage = new ModItem(ItemType.Inventory, "Garbage", "It's just garbage");
		this.garbage.SetIcon(garbageIcon);
		ModHelper.AddItem(this, this.garbage);
	}

	//Allow the player to give itself garbage
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			ModHelper.GiveItem(this.garbage.itemInstance, 1, null);
		}
	}
	
  //Register the mod (will probably be removed in the future)
	public static Main loadMod(GameObject host)
	{
		return host.AddComponent<Main>();
	}

	private ModItem garbage;
}
```
As you can see from the source, we are getting the AssetBundle 'raftmod_items' and using the 'garbage' sprite from it. This 'garbage' sprite is then used in the ModItem garbage, which we register after setting it up.

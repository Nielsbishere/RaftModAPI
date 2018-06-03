# RaftModdingAPI
This is a modding API for Raft, which will support at least Items and Blocks. For now, this is a tool to help you create everything more easily and at some point this might be merged into the [Core ModLoader](https://github.com/TeKGameR950/RaftModLoader).
## Dependencies
Before you can use this, you have to [buy Raft](https://store.steampowered.com/app/648800/Raft/) and look through their [modding guidelines](http://raft-game.com/modding-guidelines.html) and make sure you know at least a basic programming language, so you can start working on your mod. You also need to have [dnSpy](https://github.com/0xd4d/dnSpy), so you can inspect the C# assembly from the dll. Other than that; assets are exported through [Unity](https://unity3d.com/get-unity/download), so if you want any sprites, models or audio, you'll have to install that as well.
## Setting up a modding folder
First, I suggest you set up a modding folder, with all Raft's game files (taken from steamapps/common/Raft in the Steam programs root). This is because your game could be updated and/or changed while you are modding, which could remove the modding API and because having a backup for modding is good practice (for if it crashes).
## Setting up a Mod class
If you open up dnSpy, you should be able to go to Edit / Create Assembly in the toolbar and you can configure that as following:  
![Screenshot](https://i.imgur.com/gCX0O1Y.png)  
Turn the .NET version to 2.0-3.5, set version to 1.0.0.0 and the name to your mod name.
Now you need to import the modloader, which you can do by getting the [modding dll](https://github.com/TeKGameR950/RaftModLoader) and replacing your Assembly-CSharp.dll with it. This modloader provides you a console (to view all messages and to use commands) which you can enable by pressing F10. This is the core of the modding API; it will load the assembly files you create from C# and import it and load it as a mod.
### Example Code
```cs
using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class Mod_ModdingAPI : Mod
{
	// Token: 0x06000089 RID: 137
	private void Start()
	{
		AssetBundle assetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/raftmod_items");
		if (assetBundle == null)
		{
			RConsole.Log("Couldn't find the asset bundle");
			return;
		}
		Sprite icon = assetBundle.LoadAsset<Sprite>("garbage");
		this.garbage = new ModItem(ItemType.Inventory, "Garbage", "It's just garbage");
		this.garbage.SetIcon(icon);
		ModHelper.AddItem(this, this.garbage);
	}

	// Token: 0x0600008A RID: 138
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			ModHelper.GiveItem(this.garbage.itemInstance, 1, null);
		}
	}

	// Token: 0x0600008B RID: 139
	public Mod_ModdingAPI() : base("Modding API", "The main API for modding", "v0.0.1", "1.01b")
	{
	}

	// Token: 0x0400002C RID: 44
	private ModItem garbage;
}
```
As you can see from the source, we are getting the AssetBundle 'raftmod_items' and using the 'garbage' sprite from it. This 'garbage' sprite is then used in the ModItem garbage, which we register after setting it up.
### Exporting assets
First, you should create a Unity project and go to the assets folder in which you put the sprites, models, etc. you need.
#### Sprites
![Assets](https://i.imgur.com/7V2oYBW.png)  
Go to Texture Type and change it to 'Sprite (2D and UI)'.  
![TextureType](https://i.imgur.com/6dACy7y.png)  
Then go to the bottom left and select 'AssetBundle'. I already have an asset bundle, but you should create a custom one. This asset bundle is what you're going to load into your mod, to support assets.  
![AssetBundle](https://i.imgur.com/qEfopkw.png)  
Then, head to the toolbar and select 'Assets', go down to Build AssetBundles and click it.  
![BuildAssetBundles](https://i.imgur.com/ywNlKfo.png)  
Now, there should be a file with the name you entered as the AssetBundle in the Assets\AssetBundles folder. This file you need to put into the Assets\AssetBundles folder next to the Raft.exe  
![The two directories](https://i.imgur.com/5wpJgOY.png)  
Above you can see the Unity project's Assets and the Assets next to raft. You'll need to copy them in order to use them.
### Importing assets
```cs
	AssetBundle items = AssetBundle.LoadFromFile("Assets/AssetBundles/raftmod_items");
	if (items == null)
	{
		RConsole.Log("Couldn't find the asset bundle");
		return;
	}
	Sprite garbageIcon = items.LoadAsset<Sprite>("garbage");
```
That's how you import an AssetBundle and load a Sprite from it.

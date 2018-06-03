using System;
using UnityEngine;

//Wrapper for Item_Base
public class ModItem
{
	
	public bool isUnlocked { get; private set; }
	public string name { get; private set; }
	public string displayName { get; private set; }
	public string description { get; private set; }
	public int stackSize { get; set; }
	public int durability { get; set; }
	
	public void SetIcon(Sprite sprite)
	{
		if (this.inventoryIcon == null)
		{
			this.inventoryIcon = sprite;
		}
	}

	public Sprite inventoryIcon { get; private set; }
	
	public Item_Base itemInstance { get; private set; }
	public bool isInitialized { get; private set; }

	public void Initialize(Item_Base itemInstance)
	{
		if (this.isInitialized)
		{
			return;
		}
		this.itemInstance = itemInstance;
		this.isInitialized = true;
	}

	public ItemType type { get; private set; }
	public CraftingCategory craftingCategory { get; private set; }

	public ModItem(ItemType type, string name, string description)
	{
		this.type = type;
		this.name = name;
		this.displayName = name;
		this.description = description;
		this.stackSize = this.stackSize;
		this.inventoryIcon = null;
		this.craftingCategory = CraftingCategory.Nothing;
		this.durability = 0;
		this.stackSize = 20;
	}
}

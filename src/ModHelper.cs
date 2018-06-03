using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class ModHelper
{
	// Token: 0x0600001C RID: 28
	public ModHelper()
	{
	}

	// Token: 0x0600004D RID: 77
	static ModHelper()
	{
	}

	// Token: 0x06000074 RID: 116
	public static void AddItem(Mod mod, ModItem item)
	{
		ItemType itemType = (ItemType) 1;
		CraftingCategory craftingCategory = (CraftingCategory) 2;
		int id = 399 + ModHelper.modItems.Count;
		Item_Base item_Base = new Item_Base();
		item_Base.Initialize(id, item.name, itemType, item.durability);
		item_Base.name = item.name;
		item_Base.settings_Inventory = new ItemInstance_Inventory(item.inventoryIcon, "Item/" + item.name, item.stackSize);
		item_Base.settings_Inventory.DisplayName = item.displayName;
		item_Base.settings_Inventory.Description = item.description;
		item_Base.settings_usable = new ItemInstance_Usable("LMD", 0f, 0, false, false, 0, 0, false, false, false, "LMD");
		item_Base.settings_recipe = new ItemInstance_Recipe(craftingCategory, item.isUnlocked, item.isUnlocked, "", 0);
		item_Base.settings_recipe.NewCost = new CostMultiple[]
		{
			new CostMultiple(new Item_Base[]
			{
				ItemManager.GetItemByName("Scrap")
			}, 1)
		};
		item_Base.settings_equipment = new ItemInstance_Equipment(0);
		item_Base.settings_cookable = new ItemInstance_Cookable(0, 0f, null);
		item_Base.settings_consumeable = new ItemInstance_Consumeable(0f, 0f, false, null, 0);
		item_Base.settings_buildable = new ItemInstance_Buildable(null, false, false);
		try
		{
			BindingFlags accessPrivateStatic = (BindingFlags) 40;
			FieldInfo field = typeof(ItemManager).GetField("allAvailableItems", accessPrivateStatic);
			List<Item_Base> list = (List<Item_Base>)field.GetValue(null);
			list.Add(item_Base);
			field.SetValue(null, list);
			item.Initialize(item_Base);
			if(!ModHelper.modItems.ContainsKey(mod))
				ModHelper.modItems[mod] = new List<ModItem>();
			ModHelper.modItems[mod].Add(item);
			RConsole.Log(string.Concat(new object[]
			{
				"Adding item at id ",
				id,
				" ",
				item.displayName,
				" (",
				item.description,
				")"
			}));
		}
		catch (Exception ex)
		{
			RConsole.Log(string.Concat(new object[]
			{
				"Failed to add item at id ",
				id,
				" and displayName ",
				item.displayName,
				": ",
				ex.ToString()
			}));
		}
	}

	// Token: 0x060000DD RID: 221
	public static void GiveItem(Item_Base item, int amount, Player p = null)
	{
		if (p == null)
		{
			p = UnityEngine.Object.FindObjectOfType<Player>();
		}
		FieldInfo field = Enumerable.FirstOrDefault<FieldInfo>(typeof(Player).GetFields((BindingFlags)36), (FieldInfo x) => x.Name == "playerInventory");
		if (field == null)
		{
			RConsole.Log("Couldn't find playerInventory");
			return;
		}
		PlayerInventory pi = (PlayerInventory)field.GetValue(p);
		if (pi == null)
		{
			RConsole.Log("Couldn't get playerInventory");
			return;
		}
		try
		{
			pi.AddItem(item.name, 1);
		}
		catch (Exception ex)
		{
			RConsole.Log(ex.ToString());
		}
	}

	// Token: 0x0400002D RID: 45
	private static Dictionary<Mod, List<ModItem>> modItems = new Dictionary<Mod, List<ModItem>>();
}

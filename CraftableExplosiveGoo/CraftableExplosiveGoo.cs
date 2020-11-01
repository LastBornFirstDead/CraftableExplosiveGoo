using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class CraftableExplosiveGoo : Mod
{
    public void Start()
    {
        CreateRecipe(ItemManager.GetItemByName("ExplosiveGoo"), 2);
        Debug.Log("Mod Craftable Explosive Goo has been loaded!");
        allowItems("Scrap", "Nail");
    }
    private void allowItems(params string[] validItemNames)
    {
        var sandItem = ItemManager.GetItemByName("Sand");
        var sandIngredient = new CostMultiple(new Item_Base[] { sandItem }, 4);

        var clayItem = ItemManager.GetItemByName("Clay");
        var clayIngredient = new CostMultiple(new Item_Base[] { clayItem }, 4);

        var gooItem = ItemManager.GetItemByName("VineGoo");
        var gooIngredient = new CostMultiple(new Item_Base[] { gooItem }, 3);

        var dyeItem = ItemManager.GetItemByName("Flower_Blue");
        var dyeIngredient = new CostMultiple(new Item_Base[] { dyeItem }, 4);

        var validItems = validItemNames.Select(name => ItemManager.GetItemByName(name)).ToArray();
        var validIngredients = new CostMultiple(validItems, 2);

        var goo = ItemManager.GetItemByName("ExplosiveGoo");
        goo.settings_recipe.NewCost = new CostMultiple[] { sandIngredient, clayIngredient, gooIngredient, dyeIngredient, validIngredients };
    }

    /// <param name="pResultItem">Item resulting from the crafting.</param>
    public static void CreateRecipe(Item_Base pResultItem, int pAmount)
    {
        Traverse.Create(pResultItem.settings_recipe).Field("craftingCategory").SetValue(CraftingCategory.Resources);
        Traverse.Create(pResultItem.settings_recipe).Field("amountToCraft").SetValue(pAmount);
    }

    public void OnModUnload()
    {
        Debug.Log("Mod Craftable Explosive Goo has been unloaded!");
    }
}
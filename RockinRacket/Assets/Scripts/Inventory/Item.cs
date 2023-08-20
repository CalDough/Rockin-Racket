using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Item class which can be created in code or based off the ItemData scriptable object
    Items really only need a unique ID, to function, all else can be empty for testing purposes
    Items will affect things such as BandManager and each Band Role such as skill/mood/instrument, StoryManager for owning Key Items for progression, Upgrades, and Money
*/
[System.Serializable]
public class Item
{
    public int ID;
    public string ItemName;
    public Sprite ItemSprite;
    public string ImagePath;
    public string Description;
    public int Value;
    public int Amount;
    public int Durability;
    public int MoodBonus;
    public bool IsKeyItem; //Prevents selling or removal
    public bool IsConsumable; //Can be activated before concert or during
    
    public List<Attribute> ItemTypes;
    public List<Skill> SkillBonus;

    public Item(ItemData itemData, int amount)
    {
        this.ID = itemData.ID;
        this.ItemName = itemData.ItemName;
        this.ImagePath = itemData.ImagePath;
        this.Description = itemData.Description;
        this.Value = itemData.Value;
        this.Amount = amount;
        this.Durability = itemData.Durability;
        this.MoodBonus = itemData.MoodBonus;
        
        this.IsKeyItem = itemData.IsKeyItem;
        this.IsConsumable = itemData.IsConsumable;
        
        this.ItemTypes = itemData.ItemTypes;
        this.SkillBonus = itemData.SkillBonus;

        if(!string.IsNullOrEmpty(ImagePath))
        {
            LoadSprite();
        }
    }

    public bool HasItemType(Attribute typeToCheck)
    {
        return ItemTypes.Contains(typeToCheck);
    }

    //Save the sprite to the Assets/Resources folder
    //Example:
    //Assets/Resources/Sprites/Items/Anime.png
    //
    //the imagePath will have to be Sprites/Items/Anime 
    public void LoadSprite()
    {
        ItemSprite = Resources.Load<Sprite>(ImagePath);
    }
}

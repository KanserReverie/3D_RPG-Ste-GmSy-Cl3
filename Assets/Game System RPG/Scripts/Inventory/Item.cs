using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plr;

[System.Serializable]
public class Item
{
    public Player.EquipmentSlot slot = Player.EquipmentSlot.Booties;
    public enum ItemType
    {
        Food,
        Weapon,
        Apparel,
        Crafting,
        Ingredients,
        Potions,
        Scrolls,
        Quest,
        Money
    }

    #region Private variables.
    [SerializeField] private string name; // Item's ID.
    [SerializeField] private string description;
    [SerializeField] private int value;
    [SerializeField] private int amount;
    [SerializeField] private Texture2D icon;
    [SerializeField] private GameObject mesh;
    [SerializeField] private ItemType type;
    [SerializeField] private int damage;
    [SerializeField] private int armour;
    [SerializeField] private int heal;
    #endregion

    #region Public properties.

    // Same as :
    //      [SerializeField] private string Name { get; set; }  <--- Won't get shown in editor.
    // But will get shown in editor this way

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }
    public Texture2D Icon
    {
        get { return icon; }
        set { icon = value; }
    }
    public GameObject Mesh
    {
        get { return mesh; }
        set { mesh = value; }
    }
    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int Armour
    {
        get { return armour; }
        set { armour = value; }
    }
    public int Heal
    {
        get { return heal; }
        set { heal = value; }
    }
    #endregion

    public Item()
    {

    }

    public Item (Item copyItem, int copyAmount)
    {
        Name        = copyItem.Name;
        Description = copyItem.Description;
        Value       = copyItem.Value;
        Amount      = copyAmount;
        Icon        = copyItem.Icon;
        Mesh        = copyItem.Mesh;
        Type        = copyItem.Type;
        Damage      = copyItem.Damage;
        Armour      = copyItem.Armour;
        Heal        = copyItem.Heal;

    }

    public virtual void OnClicked() => Debug.Log($"Item pressed was: {name}!");
}

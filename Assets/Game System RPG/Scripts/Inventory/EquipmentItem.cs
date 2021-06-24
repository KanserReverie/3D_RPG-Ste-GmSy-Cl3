using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Plr;
using System;

[System.Serializable]
public class EquipmentItem : Item
{
    public Player.EquipmentSlot slot = Player.EquipmentSlot.Booties;
    public bool isEquipped = false;

    public override void OnClicked()
    {
        base.OnClicked();

        Player player = GameObject.FindObjectOfType<Player>();
        EquipmentItem oldItem = player.EquipItem(this);
            Inventory inventory = GameObject.FindObjectOfType<Inventory>();
        if(oldItem != null)
        {
            inventory.AddItem(oldItem);
        }
        inventory.RemoveItem(this);
    }
}
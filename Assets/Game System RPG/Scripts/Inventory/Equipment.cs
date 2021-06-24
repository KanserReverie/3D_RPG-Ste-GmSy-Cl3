using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EquipSlot
{
    [SerializeField] private Item item;
    public Item EquipedItem
    {
        get
        {
            return item;
        }
        set 
        {
            item = value;
            itemEquiped.Invoke(this); // change to this
        }
    }
    public Transform visualLocation;
    public Vector3 offset;

    public delegate void ItemEquiped(EquipSlot item);
    public event ItemEquiped itemEquiped;
}

// Equipment equipment = Getcompent<>;
// equipment.primatry.EquipedIem = itemIWantToEquip;



public class Equipment : MonoBehaviour
{
    public EquipSlot primary;
    public EquipSlot secondary;
    public EquipSlot defensive;

    private void Awake()
    {
        primary.itemEquiped += EquipItem;
        secondary.itemEquiped += EquipItem;
        defensive.itemEquiped += EquipItem;
    }



    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        EquipItem(primary);
        EquipItem(secondary);
        EquipItem(defensive);
    }

    public void EquipItem(EquipSlot item) //Item item, Transform visualLocation).
    {
        if(item.visualLocation == null)
        {
            return;
        }
        
        foreach (Transform child in item.visualLocation)
        {
            GameObject.Destroy(child.gameObject);
        }

        if(item.EquipedItem.Mesh == null)
        {
            return;
        }

        GameObject meshInstance = Instantiate(item.EquipedItem.Mesh, item.visualLocation);
        meshInstance.transform.localPosition = item.offset;
        OffsetLocation offset = meshInstance.GetComponent<OffsetLocation>();

        if (offset != null)
        {
            meshInstance.transform.localPosition += offset.postionOffset;
            meshInstance.transform.localRotation = Quaternion.Euler(offset.rotationOffset);
            meshInstance.transform.localScale = offset.scaleOffset;
        }
    }
}

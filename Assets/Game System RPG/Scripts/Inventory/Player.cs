using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Plr
{
    public class Player : MonoBehaviour
    {
        public enum EquipmentSlot
        {
            Helmet, Chestplate, Pantaloon, Booties, StabbyThings, ProtectyThing,
            Consumables, Miscellaneous
        }
        private Dictionary<EquipmentSlot, EquipmentItem> slots = new Dictionary<EquipmentSlot, EquipmentItem>();

        // Start is called before the first frame update
        void Start()
        {
            // Auto generate the slots in the dictionary.
            foreach (EquipmentSlot slot in System.Enum.GetValues(typeof(EquipmentSlot)))
            {
                slots.Add(slot, null);
            }
        }

        public EquipmentItem EquipItem(EquipmentItem _toEquip)
        {
            if(_toEquip == null)
            {
                Debug.LogError("WHY WOULD YOU PASS NULL INTO THIS. YOU WERE WARNED.");
                return null;
            }

            // Attempt to get ANYTHING out of the slot, be it null or not
            if (slots.TryGetValue(_toEquip.slot, out EquipmentItem item))
            {
                // Create a copy of the original, set the slot item to the passed value
                EquipmentItem original = item;
                slots[_toEquip.slot] = _toEquip;
                
                // Return what was originally in the slot  to prevent loosing items when equipping
                return original;
            }

            // SOMEHOW (shouldn't happen but a fail safe) the slot didn't exist, so lets create it and
            // reurn null as no item would be in the slot anyway.
            slots.Add(_toEquip.slot, _toEquip);
            return null;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
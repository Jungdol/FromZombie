using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject slotItem;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == ("Player"))
        {
            Inventory inven = collision.gameObject.GetComponent<Inventory>();
            for (int i = 0; i < inven.slots.Count; i++)
            {
                if (inven.slots[i].isEmpty)
                {
                    GameObject ItemImage = Instantiate(slotItem, inven.slots[i].slotObj.transform, false);
                    
                    ItemImage.transform.SetSiblingIndex(ItemImage.transform.GetSiblingIndex() - 1);
                    inven.slots[i].isEmpty = false;
                    Destroy(this.gameObject);
                    break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}

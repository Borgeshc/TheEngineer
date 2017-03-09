using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    public List<GameObject> itemsInInventory;
    public int[] itemStacking;
    Dictionary<string, GameObject> inventoryItems;
	ItemEffects itemEffects;

    void Start()
    {
		itemEffects = GameObject.FindGameObjectWithTag ("Player").GetComponent<ItemEffects>();
        itemStacking = new int[slots.Length];
        itemsInInventory = new List<GameObject>(slots.Length);
        inventoryItems = new Dictionary<string, GameObject>();
    }

    public void AddItem(GameObject itemToAdd)
    {
        string itemName = itemToAdd.name;
        for (int i = 0; i < slots.Length; i++)
        {
            if (!inventoryItems.ContainsKey(itemName))
            {
                if (slots[i].transform.FindChild("HoldItem").transform.childCount > 0)
                    continue;
                else
                {
                    GameObject clone = Instantiate(itemToAdd, slots[i].transform.position, slots[i].transform.rotation, slots[i].transform.FindChild("HoldItem").transform) as GameObject;
                    foreach (Transform child in clone.GetComponentInChildren<Transform>())
                    {
                        Destroy(child.gameObject);
                    }
					clone.name = itemName;
                    clone.GetComponent<RectTransform>().sizeDelta = new Vector2(90, 90);
					clone.GetComponent<Image> ().raycastTarget = false;
					Destroy(clone.GetComponent<Button>());
                    inventoryItems.Add(itemName, clone);
                    itemStacking[i]++;
                    slots[i].transform.FindChild("StackCount").GetComponent<Text>().text = itemStacking[i].ToString();
                    return;
                }
            }
            else if(inventoryItems.ContainsKey(itemName))
            {
                int index = Convert.ToInt32(inventoryItems[itemName].transform.parent.transform.parent.name.Substring(13));
                itemStacking[index - 1]++;
                slots[index - 1].transform.FindChild("StackCount").GetComponent<Text>().text = itemStacking[index -1].ToString();
                return;
            }
        }
    }

    public void RemoveItem(GameObject itemToRemove)
    {
		string itemName = itemToRemove.name;
		itemEffects.UsedItem (itemName);
		for (int i = 0; i < slots.Length; i++) 
		{
			if (inventoryItems.ContainsKey (itemName)) 
			{
				int index = Convert.ToInt32(inventoryItems[itemName].transform.parent.transform.parent.name.Substring(13));
				if (itemStacking [index - 1] > 1) 
				{
					itemStacking [index - 1]--;
					slots [index - 1].transform.FindChild ("StackCount").GetComponent<Text> ().text = itemStacking [index - 1].ToString ();
					return;
				} 
				else 
				{
					itemStacking[index - 1]--;
					slots[index - 1].transform.FindChild("StackCount").GetComponent<Text>().text = "";
					inventoryItems.Remove (itemName);
					Destroy (itemToRemove);
					return;
				}
			}
		}
    }
}
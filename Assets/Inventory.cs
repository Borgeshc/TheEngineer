using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    public List<GameObject> itemsInInventory;
    public int[] itemStacking;

    void Start()
    {
        itemStacking = new int[slots.Length];
        itemsInInventory = new List<GameObject>(slots.Length);
    }

    public void AddItem(GameObject itemToAdd)
    {
        print("Add Item called" + itemToAdd.name + " Passed");
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == itemToAdd)
            {
                itemStacking[i]++;
                slots[i].transform.FindChild("StackCount").GetComponent<Text>().text = itemStacking[i].ToString();
            }
            else
            {
                GameObject clone = Instantiate(itemToAdd, slots[i].transform.position, slots[i].transform.rotation, slots[i].transform.FindChild("HoldItem").transform) as GameObject;
                foreach(Transform child in clone.GetComponentInChildren<Transform>())
                {
                    Destroy(child.gameObject);
                }
                clone.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                itemsInInventory.Add(clone);
                itemStacking[i]++;
                slots[i].transform.FindChild("StackCount").GetComponent<Text>().text = itemStacking[i].ToString();
                return;
            }
        }
    }

    public void RemoveItem(GameObject itemToRemove)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == itemToRemove)
            {
                itemStacking[i]--;
                if(itemStacking[i] < 0)
                {
                    Destroy(itemToRemove);
                }
                else
                    slots[i].transform.FindChild("StackCount").GetComponent<Text>().text = itemStacking[i].ToString();
            }
        }
    }
}

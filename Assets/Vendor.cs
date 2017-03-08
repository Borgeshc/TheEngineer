using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour 
{
	public GameObject vendor;
    public GameObject inventory;
	public GameObject player;
    public GameObject[] vendorSlots;
    public GameObject[] vendorItems;

    Inventory myInventory;

    void Start()
    {
        myInventory = inventory.GetComponent<Inventory>();
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			vendor.SetActive (true);
            inventory.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			vendor.SetActive (false);
            inventory.SetActive(false);
		}
	}

    public void VendorSlot(int item)
    {
        myInventory.AddItem(vendorItems[item]);
    }
}

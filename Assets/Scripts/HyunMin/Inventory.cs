using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] itemSlot;
    private int invenCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        itemSlot = GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invenCount == 0)
        {
            int a = 0;
            itemSlot[a].AddItem();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            PlayerInfo.SaveItemList("Banana", 13, PlayerInfo.ItemType.Food);
            invenCount++;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayerInfo.SaveItemList("Apple", 7, PlayerInfo.ItemType.Food);
            invenCount++;
        }
        if(invenCount >0 && invenCount <= 20)
        { 
            itemSlot[invenCount - 1].AddItem();
        }
    }
}

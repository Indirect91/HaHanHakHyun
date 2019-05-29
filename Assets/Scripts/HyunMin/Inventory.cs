using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] itemSlot;
    public int invenCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        itemSlot = GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
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
        //인벤토리에 아이템이 비어있을 경우
        if (invenCount == 0)
        {
            int empty = 0;
            itemSlot[empty].AddItem();
        }
        //아이템이 들어가있을 경우
        if (invenCount > 0 && invenCount <= 20)
        {
            itemSlot[invenCount - 1].AddItem();
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            for (int i = 0; i < itemSlot.Length; i++)
            {
                
                itemSlot[i].RemoveItem();
            }
        }
    }

    void UseItem()
    {
        if(Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < itemSlot.Length - 1; i++)
            {
                itemSlot[i].RemoveItem();
            }
        }
    }
}

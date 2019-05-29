using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //인벤토리 아이콘
    private Image icon;
    private void Start()
    {
        icon = GetComponent<Image>();
    }

    public void AddItem()
    {
        if (PlayerInfo.itemList.Count == 0)
        {
            icon.sprite = null;
            //Debug.Log("비어있음");
        }
        else
        {
            //List에서 아이템을 찾을 foreach문
            foreach (PlayerInfo.TagItemInfo toExamine in PlayerInfo.itemList)
            {
                //이름이 Apple일 경우 이미지 변경
                if (toExamine.Name == "Apple")
                {
                    //Resources폴더에 있는 sprite타입의 이미지를 가져와 캐스팅 한다....
                    icon.sprite = Resources.Load("Apple", typeof(Sprite)) as Sprite;
                }
                //이름이 Banana일 경우 이미지 변경
                if (toExamine.Name == "Banana")
                {
                    icon.sprite = Resources.Load("Banana", typeof(Sprite)) as Sprite;
                }
                //이름이 DrinkCan일 경우 이미지 변경
                if (toExamine.Name == "DrinkCan")
                {
                    icon.sprite = Resources.Load("button6", typeof(Sprite)) as Sprite;
                }
                //이름이 RoastMeat일 경우 이미지 변경
                if(toExamine.Name == "RoastMeat")
                {
                    icon.sprite = Resources.Load("button3", typeof(Sprite)) as Sprite;
                }

            }
        }
    }

    public void RemoveItem()
    {
        icon.sprite = null;
        Debug.Log("아이템 사용");
    }

}

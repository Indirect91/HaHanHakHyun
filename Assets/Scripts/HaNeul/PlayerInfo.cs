using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    //퍼블릭 스태틱을 이용하면 타 클래스에서 건드릴 수 있게된다
    public static GameObject clickTarget = null;

    //플레이어 정보
    float playerHp = 25.0f;
    float playerAtt1 = 4.0f; //스타볼
    float playerAtt2 = 8.0f; //썬더볼
    float playerDef = 4.0f;

    //아이템 타입
    public enum ItemType
    {
        food,
        key,
        gem,
        pet
    }

    //아이템 정보
    struct TagItemInfo
    {
        public string Name;
        public float Value;

        public ItemType itemType;
    }

    //플레이어가 소환 후 저장할 소모품 정보
    List<TagItemInfo> itemList = new List<TagItemInfo>();

    public void SaveFoodItemList(string itemName, float itemValue, ItemType itemType)
    {
        TagItemInfo itemInfo;
        itemInfo.Name = itemName;
        itemInfo.Value = itemValue;
        itemInfo.itemType = itemType;

        
    }
}

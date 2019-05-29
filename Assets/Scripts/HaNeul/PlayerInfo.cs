using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    //퍼블릭 스태틱을 이용하면 타 클래스에서 건드릴 수 있게된다
    public static GameObject clickTarget = null;

    //플레이어 정보
    public static float playerHp = 25.0f;
    public static float playerAtt1 = 4.0f;
    public static float playerDef = 4.0f;

    //플레이어 고양이 소유 정보
    public static bool isCat1 = false; //고양이1 얻었는가?
    public static bool isCat2 = false; //고양이2 얻었는가?
    public static bool isCat3 = false; //고양이3 얻었는가?
    public static bool isCat4 = false; //고양이4 얻었는가?

    //아이템 타입
    public enum ItemType
    {
        Food,
        Key
    }

    //아이템 정보
    public struct TagItemInfo
    {
        public string Name;
        public float Value;

        public ItemType itemType;
    }

    //플레이어가 소환 후 저장할 소모품 정보
    public static List<TagItemInfo> itemList = new List<TagItemInfo>();

    //소지하고있는 아이템 개수 확인용
    public static int ItemListCount() { return itemList.Count; }

    //소환시 나타난 아이템 저장용
    public static void SaveItemList(string itemName, float itemValue, ItemType itemType)
    {
        TagItemInfo itemInfo;

        itemInfo.Name = itemName; //이름
        itemInfo.Value = itemValue; //값
        itemInfo.itemType = itemType; //타입(음식인지, 열쇠인지)

        //아이템 리스트에 담는다
        itemList.Add(itemInfo);
    }
}

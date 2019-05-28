using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public List<Item> equipItem = new List<Item>();     //얻은 아이템

    }

    [System.Serializable]
    public class Item
    {
        public enum ItemType { POSITIONSAMLL, POSITIONMEDIUM, POSITIONLARGE, MEAT, JUICE }  //아이템 종류 선언
        public enum ItemCalc { INC_VALUE, PERCENT } //계산 방식 선언
        public ItemType itemType;   //아이템 종류
        public ItemCalc itemCalc;   //계산 방식
        public string itemName;     //아이템 이름
        public string desc;         //아이템 소개
        public float value;          //계산 값
    }

    [System.Serializable]
    public class Animals
    {

    }
}

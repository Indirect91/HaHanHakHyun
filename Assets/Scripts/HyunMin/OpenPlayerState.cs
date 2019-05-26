using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPlayerState : MonoBehaviour
{
    //플레이어 스테이터스 CanvasGroup 컴포넌트 저장할 변수
    [SerializeField] private CanvasGroup playerStateGroup;
    //동물 스테이터스 CanvasGroup 컴포넌트 저장할 변수
    [SerializeField] private CanvasGroup animalsStatueGroup;
    //인벤토리 CanvasGroup 컴포넌트 저장할 변수
    [SerializeField] private CanvasGroup itemInventroyGroup;
    //셋팅 창 CansvasGroup 컴포넌트 저장 변수
    [SerializeField] private CanvasGroup settingGroup;
    //버튼 컴포넌트
    [SerializeField] private Button playerButton;
    [SerializeField] private Button animalsInventorytButton;
    [SerializeField] private Button itemInventroyButton;
    [SerializeField] private Button settingButton;

    // Start is called before the first frame update
    void Start()
    {
        //처음 시작할때 플레이어 스테이터스 창 비활성화
        this.OnPlayerStateOpen(false);
        this.OnAnimalsStatusOpen(false);
        this.OnInventroyOpen(false);
        this.OnSettingOpen(false);
    }

    //플레이어 스테이터스 창 오픈
    public void OnPlayerStateOpen(bool isOpened)
    {
        //만약 isOpened가 true면 스테이터스 알파값을 1로 변경  false면 0
        playerStateGroup.alpha = (isOpened) ? 1.0f : 0.0f;
        //boolean값이 true면 canvas그룹이랑 상호작용 가능하도록 false면 비활성화
        playerStateGroup.interactable = isOpened;
        //boolean값이 true면 레이케스트 활성화 false면 비활성화
        playerStateGroup.blocksRaycasts = isOpened;
        //플레이어 스테이터스창이 열려있을때 다른 창 못열게 
        animalsInventorytButton.enabled = (isOpened) ? false : true;
        itemInventroyButton.enabled = (isOpened) ? false : true;
        settingButton.enabled = (isOpened) ? false : true;
    }

    //동물들 스테이터스 창 오픈
    public void OnAnimalsStatusOpen(bool isOpened)
    {
        //만약 isOpened가 true면 스테이터스 알파값을 1로 변경  false면 0
        animalsStatueGroup.alpha = (isOpened) ? 1.0f : 0.0f;
        //boolean값이 true면 canvas그룹이랑 상호작용 가능하도록 false면 비활성화
        animalsStatueGroup.interactable = isOpened;
        //boolean값이 true면 레이케스트 활성화 false면 비활성화
        animalsStatueGroup.blocksRaycasts = isOpened;
        //동물 스테이터스창이 열려있을때 다른 창 못열게 
        playerButton.enabled = (isOpened) ? false : true;
        itemInventroyButton.enabled = (isOpened) ? false : true;
        settingButton.enabled = (isOpened) ? false : true;
    }

    public void OnInventroyOpen(bool isOpened)
    {
        //만약 isOpened가 true면 스테이터스 알파값을 1로 변경  false면 0
        itemInventroyGroup.alpha = (isOpened) ? 1.0f : 0.0f;
        //boolean값이 true면 canvas그룹이랑 상호작용 가능하도록 false면 비활성화
        itemInventroyGroup.interactable = isOpened;
        //boolean값이 true면 레이케스트 활성화 false면 비활성화
        itemInventroyGroup.blocksRaycasts = isOpened;
        //인벤토리가 열려있을때 다른 창 못열게 
        playerButton.enabled = (isOpened) ? false : true;
        animalsInventorytButton.enabled = (isOpened) ? false : true;
        settingButton.enabled = (isOpened) ? false : true;
    }

    public void OnSettingOpen(bool isOpened)
    {
        //만약 isOpened가 true면 스테이터스 알파값을 1로 변경  false면 0
        settingGroup.alpha = (isOpened) ? 1.0f : 0.0f;
        //boolean값이 true면 canvas그룹이랑 상호작용 가능하도록 false면 비활성화
        settingGroup.interactable = isOpened;
        //boolean값이 true면 레이케스트 활성화 false면 비활성화
        settingGroup.blocksRaycasts = isOpened;
        //셋팅 창이 열려있을때 다른 창 못열게 
        playerButton.enabled = (isOpened) ? false : true;
        animalsInventorytButton.enabled = (isOpened) ? false : true;
        itemInventroyButton.enabled = (isOpened) ? false : true;
    }
}

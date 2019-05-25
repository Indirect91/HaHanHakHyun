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
    //인벤토리 스테이터스 CanvasGroup 컴포넌트 저장할 변수
    [SerializeField] private CanvasGroup inventroyGroup;

    // Start is called before the first frame update
    void Start()
    {
        //처음 시작할때 플레이어 스테이터스 창 비활성화
        this.OnPlayerStateOpen(false);
        this.OnAnimalsStatusOpen(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void OnInventroyOpen(bool isOpened)
    {

    }
}

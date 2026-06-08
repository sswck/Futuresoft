using UnityEngine;
using DG.Tweening;

public class PhoneUIManager : MonoBehaviour
{
    [Header("UI 연결")]
    public CanvasGroup phonePopupGroup;

    [Header("애니메이션 설정")]
    public float animDuration = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        phonePopupGroup.alpha = 0f;
        phonePopupGroup.interactable = false;
        phonePopupGroup.blocksRaycasts = false;
        phonePopupGroup.transform.localScale = Vector3.one * 0.8f;
        phonePopupGroup.gameObject.SetActive(false);
    }

    /// <summary>
    /// 스마트폰 오브젝트를 클릭했을 때 팝업을 여는 함수입니다.
    /// </summary>
    public void OpenPhoneMenu()
    {
        phonePopupGroup.gameObject.SetActive(true);
        phonePopupGroup.DOFade(1f, animDuration);
        phonePopupGroup.transform.DOScale(1f, animDuration).SetEase(Ease.OutBack).OnComplete(() =>
        {
            phonePopupGroup.interactable = true;
            phonePopupGroup.blocksRaycasts = true;
        });
    }

    /// <summary>
    /// 팝업 내의 [닫기] 버튼을 눌렀을 때 팝업을 닫는 함수입니다.
    /// </summary>
    public void ClosePhoneMenu()
    {
        phonePopupGroup.interactable = false;
        phonePopupGroup.blocksRaycasts = false;
        phonePopupGroup.DOFade(0f, animDuration);
        phonePopupGroup.transform.DOScale(0.8f, animDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            phonePopupGroup.gameObject.SetActive(false);
        });
    }

    public void OnClickPartTime()
    {
        Debug.Log("알바시작: 추후 알바 진행 예정입니다.");
        // (임시) 버튼을 누르면 팝업이 자동으로 닫히게 설정
        ClosePhoneMenu(); 
    }

    public void OnClickMarket()
    {
        Debug.Log("중고거래시작: 추후 남주인공과의 만남 스토리 씬으로 이동할 예정입니다.");
        ClosePhoneMenu();
    }
}

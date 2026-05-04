using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("화면 플레이스홀더 (CanvasGroup 연결)")]
    [Tooltip("UI/UX 담당자가 디자인할 타이틀 화면 최상위 객체")]
    public CanvasGroup titleScreen;

    [Tooltip("UI/UX 담당자가 디자인할 메인 허브 화면 최상위 객체")]
    public CanvasGroup mainHubScreen;

    [Header("애니메이션 설정")]
    public float fadeDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ShowScreen(titleScreen);
        HideScreen(mainHubScreen, 0f);
    }

    /// <summary>
    /// [새로 시작] 버튼을 눌렀을 때 호출될 함수입니다.
    /// UI의 OnClick 이벤트에 이 함수를 연결하면 됩니다.
    /// </summary>
    public void OnClickStartGame()
    {
        Debug.Log("게임 시작! 메인 허브로 부드럽게 이동합니다.");
        
        FadeOut(titleScreen);
        FadeIn(mainHubScreen);
    }

    // ==========================================
    // 🎨 UI 전환 애니메이션 함수 (DOTween 활용)
    // ==========================================

    private void FadeIn(CanvasGroup cg)
    {
        cg.gameObject.SetActive(true);
        
        cg.DOFade(1f, fadeDuration).OnComplete(() => 
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        });
    }

    private void FadeOut(CanvasGroup cg)
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
        
        cg.DOFade(0f, fadeDuration).OnComplete(() => 
        {
            cg.gameObject.SetActive(false);
        });
    }

    private void ShowScreen(CanvasGroup cg)
    {
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        cg.gameObject.SetActive(true);
    }

    private void HideScreen(CanvasGroup cg, float duration)
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        cg.gameObject.SetActive(false);
    }
}

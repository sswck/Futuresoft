using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("화면 플레이스홀더")]
    public CanvasGroup titleScreen;

    [Header("애니메이션 설정")]
    public float fadeDuration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (titleScreen != null)
        {
            titleScreen.alpha = 1f;
            titleScreen.interactable = true;
            titleScreen.blocksRaycasts = true;
        }
    }

    /// <summary>
    /// 게임 시작 버튼 클릭 시 호출되는 함수입니다.
    /// </summary>
    public void OnClickStartGame()
    {
        Debug.Log("게임 시작! MainHub 씬으로 이동합니다.");
        
        titleScreen.interactable = false;
        titleScreen.blocksRaycasts = false;

        titleScreen.DOFade(0f, fadeDuration).OnComplete(() => { SceneManager.LoadScene("MainHub"); });
    }
}

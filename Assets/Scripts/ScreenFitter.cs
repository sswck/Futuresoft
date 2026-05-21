using UnityEngine;

public class ScreenFitter : MonoBehaviour
{
    [Header("기준 배경 스프라이트")]
    [Tooltip("화면 꽉 채우기의 기준이 될 메인 방 배경 오브젝트의 SpriteRenderer를 연결하세요.")]
    public SpriteRenderer backgroundRenderer;

    [Header("스케일 옵션")]
    [Tooltip("true: 이미지 비율을 유지하며 꽉 채움 (추천, 미세한 외곽 잘림 있음)\nfalse: 화면에 맞춰 강제로 늘림 (약간 찌그러질 수 있음)")]
    public bool keepAspectRatio = true;

    // Start is called before the first frame update
    void Start()
    {
        AdjustScale();
    }

    /// <summary>
    /// 현재 카메라 해상도와 배경 크기를 비교하여 부모 오브젝트의 스케일을 동적으로 조절합니다.
    /// </summary>
    public void AdjustScale()
    {
        if (backgroundRenderer == null)
        {
            Debug.LogWarning("기준 배경 SpriteRenderer가 연결되지 않았습니다.");
            return;
        }

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Camera.main.aspect;

        float spriteWidth = backgroundRenderer.sprite.bounds.size.x;
        float spriteHeight = backgroundRenderer.sprite.bounds.size.y;

        float scaleX = worldScreenWidth / spriteWidth;
        float scaleY = worldScreenHeight / spriteHeight;

        if (keepAspectRatio)
        {
            float targetScale = Mathf.Max(scaleX, scaleY);
            transform.localScale = new Vector3(targetScale, targetScale, 1f);
        }
        else
        {
            transform.localScale = new Vector3(scaleX, scaleY, 1f);
        }

        Debug.Log($"해상도 대응 완료! 현재 화면 비율: {Camera.main.aspect}, 적용된 스케일: {transform.localScale.x}");
    }
}

using UnityEngine;

public class SceneBGMPlayer : MonoBehaviour
{
    [Header("씬 배경음악")]
    public AudioClip sceneBGM;

    private void Start()
    {
        if (sceneBGM != null && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBGM(sceneBGM);
        }
        else
        {
            Debug.LogWarning("BGM 클립이 비어있거나 SoundManager가 존재하지 않습니다.");
        }
    }
}
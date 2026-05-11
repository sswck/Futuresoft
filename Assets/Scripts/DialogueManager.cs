using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI 연결")]
    [Tooltip("텍스트가 출력될 TextMeshPro 컴포넌트")]
    public TextMeshProUGUI dialogueText;

    [Header("타이핑 설정")]
    [Tooltip("글자가 하나씩 출력되는 속도 (초)")]
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartDialogue("Hi! This is a sample dialogue. Press Space to see this text appear with a typing effect.");
        }
    }

    /// <summary>
    /// 외부에서 대화를 전달받아 출력을 시작하는 함수
    /// </summary>
    public void StartDialogue(string sentence)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// 글자가 하나씩 출력되는 타이핑 효과를 구현하는 코루틴
    /// </summary>
    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}

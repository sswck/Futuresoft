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
    private string currentSentence;
    private bool isTyping = false;
    
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
            if (isTyping)
            {
                SkipTyping();
            }
            else
            {
                StartDialogue("Hi! This is a sample dialogue. Press Space to see this text appear with a typing effect.");
            }
        }
    }

    /// <summary>
    /// 외부에서 대화를 전달받아 출력을 시작하는 함수
    /// </summary>
    public void StartDialogue(string sentence)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        currentSentence = sentence;
        typingCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// 글자가 하나씩 출력되는 타이핑 효과를 구현하는 코루틴
    /// </summary>
    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        isTyping = false;
    }

    /// <summary>
    /// 진행 중인 타이핑을 멈추고 즉시 전체 문장을 보여주는 함수
    /// </summary>
    private void SkipTyping()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        dialogueText.text = currentSentence;
        isTyping = false;
    }
}

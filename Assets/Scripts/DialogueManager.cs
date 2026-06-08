using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueData
{
    public int id;
    public string speaker;
    public string expression;
    public string sentence;
}

[System.Serializable]
public struct CharacterSprite
{
    public string expressionName;
    public Sprite sprite;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI 연결")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerText;
    public Image characterImage;

    [Header("데이터 연결")]
    public TextAsset dialogueCSV;
    public List<CharacterSprite> characterSprites;

    [Header("타이핑 설정")]
    public float typingSpeed = 0.05f;

    public List<DialogueData> dialogueList = new List<DialogueData>();
    private int currentDialogueIndex = 0;
    private Coroutine typingCoroutine;
    private string currentSentence;
    private bool isTyping = false;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadDialogueDataFromCSV();
        if (characterImage != null) characterImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping) SkipTyping();
            else PlayNextDialogue();
        }
    }

    /// <summary>
    /// 데이터 로드 (CSV 파싱) 함수
    /// </summary>
    public void LoadDialogueDataFromCSV()
    {
        if (dialogueCSV == null) return;

        string[] lines = dialogueCSV.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] row = lines[i].Split(',', 4);

            DialogueData data = new DialogueData();
            data.id = int.Parse(row[0]);
            data.speaker = row[1];
            data.expression = row[2].Replace("\r", "");
            data.sentence = row[3].Replace("\r", "");

            dialogueList.Add(data);
        }

        Debug.Log($"총 {dialogueList.Count}개의 대화를 성공적으로 불러왔습니다!");
    }

    /// <summary>
    /// 다음 대화를 출력하는 함수
    /// </summary>
    public void PlayNextDialogue()
    {
        if (currentDialogueIndex >= dialogueList.Count)
        {
            dialogueText.text = "";
            if (speakerText != null) speakerText.text = "";
            if (characterImage != null) characterImage.gameObject.SetActive(false);
            return;
        }

        DialogueData currentData = dialogueList[currentDialogueIndex];

        if (speakerText != null) speakerText.text = currentData.speaker;

        UpdateCharacterExpression(currentData.expression);
        StartDialogue(currentData.sentence);
        currentDialogueIndex++;
    }

    private void UpdateCharacterExpression(string exp)
    {
        if (characterImage == null) return;

        if (string.IsNullOrEmpty(exp) || exp.Trim() == "None")
        {
            characterImage.gameObject.SetActive(false);
            return;
        }

        foreach (var charSprite in characterSprites)
        {
            if (charSprite.expressionName == exp.Trim())
            {
                characterImage.sprite = charSprite.sprite;
                characterImage.gameObject.SetActive(true);
                return;
            }
        }

        Debug.LogWarning($"⚠️ '{exp}' 이름과 일치하는 캐릭터 이미지를 찾을 수 없습니다.");
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

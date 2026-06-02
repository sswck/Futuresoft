using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [Header("스프라이트 설정")]
    public Sprite defaultSprite;
    public Sprite hoverSprite;

    [Header("클릭 이벤트")]
    public UnityEvent onClickAction;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (defaultSprite != null) spriteRenderer.sprite = defaultSprite;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (hoverSprite != null) spriteRenderer.sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        if (defaultSprite != null) spriteRenderer.sprite = defaultSprite;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Debug.Log($"{gameObject.name}이(가) 클릭되었습니다.");
        onClickAction?.Invoke();
    }
}

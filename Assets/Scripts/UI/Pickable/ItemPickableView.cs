using System.Linq;
using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ItemPickableView : MonoBehaviour
{
    [SerializeField] private Image itemSpriteRenderer;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float nearDistance = 5f;
    private CharacterCenter _characterCenter;
    private PickableItem _pickable;

    private void Start()
    {
        _pickable = GetComponentInParent<PickableItem>();
        SetView(_pickable.item.image);
        _characterCenter = FindObjectsOfType<CharacterCenter>()
            .First(center => center.characterName.Equals(LevelManager.instance.mainPlayerName));
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _characterCenter.transform.position);
        if (distance <= nearDistance && _characterCenter.effectState != CharacterEffectState.Death)
        {
            ScaleUp();
        }
        else
        {
            if (transform.localScale.x <= 0.2f)
            {
                transform.localScale = Vector3.zero;
                return;
            }
            ScaleDown();
        }
    }

    public void SetView(Sprite itemSprite)
    {
        itemSpriteRenderer.sprite = itemSprite;
    }

    private void ScaleUp()
    {
        transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.Flash);
    }

    private void ScaleDown()
    {
        transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.Flash);
    }
}
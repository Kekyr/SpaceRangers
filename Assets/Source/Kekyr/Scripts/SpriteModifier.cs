using DG.Tweening;
using UnityEngine;

public class SpriteModifier : MonoBehaviour
{
    [SerializeField] private float _duration;
    
    public void ChangeColor(SpriteRenderer spriteRenderer, Color color)
    {
        spriteRenderer.DOColor(color, _duration)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
        {
            spriteRenderer.DOColor(Color.white, _duration);
        });
    }
}
using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace Game
{
    public class SpriteModifier : MonoBehaviour
    {
        public Sequence ChangeColor(SpriteRenderer spriteRenderer, Color color, float duration)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(spriteRenderer.DOColor(color, duration)
                .SetEase(Ease.OutBounce)
                .OnComplete<Tween>(() => { spriteRenderer.DOColor(Color.white, duration); }));

            return sequence;
        }
    }
}
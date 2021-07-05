using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace NWR.Modules
{
    public class LoadingScreenAnimations : IAppearAnimation, IHideAnimation
    {
        private Vector2 positionOnScreen = new Vector2(0f, 0f);
        private Vector2 positionOutOfScreen = new Vector2(2350f, 0f);

        private Sequence _sequence;
        public IEnumerator AppearAnimation(GameObject obj)
        {
            _sequence = DOTween.Sequence();
            RectTransform objRectTransform = obj.GetComponent<RectTransform>();

            _sequence.Append(objRectTransform.DOAnchorPos(positionOnScreen, 1.5f));
            yield return new WaitForSeconds(1.55f);

            _sequence.AppendCallback(() => { KillSequence(); });
        }


        public IEnumerator HideAnimation(GameObject obj)
        {
            _sequence = DOTween.Sequence();
            RectTransform objRectTransform = obj.GetComponent<RectTransform>();

            _sequence.Append(objRectTransform.DOAnchorPos(positionOutOfScreen, 1.5f));
            yield return new WaitForSeconds(1.55f);

            _sequence.AppendCallback(() => { KillSequence(); });
        }

        private void KillSequence()
        {
            _sequence.Kill();
        }
    }
}

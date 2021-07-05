using System.Collections;
using UnityEngine;
using DG.Tweening;
using NWR.Modules;

namespace NWR.MainMenu
{
    public class Menu_Animations : IAppearAnimation, IHideAnimation
    {
        private Sequence _sequence;

        [SerializeField] private Vector2 positionOnScreenForMenu = new Vector2(-350f, 0f);
        [SerializeField] private Vector2 positionOutOfScreenForMenu = new Vector2(350f, 0f);



        public IEnumerator AppearAnimation(GameObject obj)
        {
            _sequence = DOTween.Sequence();
            RectTransform objRectTransform = obj.GetComponent<RectTransform>();
            obj.GetComponent<CanvasGroup>().interactable = false;

            _sequence.Append(objRectTransform.DOAnchorPos(positionOnScreenForMenu - new Vector2(40f, 0f), 0.8f));
            _sequence.Append(objRectTransform.DOAnchorPos(positionOnScreenForMenu, 0.25f));
            yield return new WaitForSeconds(1.05f);

            obj.GetComponent<CanvasGroup>().interactable = true;
            _sequence.AppendCallback(() => { KillSequence(); });
        }


        public IEnumerator HideAnimation(GameObject obj)
        {
            _sequence = DOTween.Sequence();
            RectTransform objRectTransform = obj.GetComponent<RectTransform>();
            obj.GetComponent<CanvasGroup>().interactable = false;

            _sequence.Append(objRectTransform.DOAnchorPos(positionOnScreenForMenu - new Vector2(40f, 0f), 0.25f));
            _sequence.Append(objRectTransform.DOAnchorPos(positionOutOfScreenForMenu, 0.8f));
            yield return new WaitForSeconds(1.05f);

            obj.GetComponent<CanvasGroup>().interactable = true;
            _sequence.AppendCallback(() => { KillSequence(); });
        }



        private void KillSequence(GameObject obj = null)
        {
            _sequence.Kill();
        }
    }
}

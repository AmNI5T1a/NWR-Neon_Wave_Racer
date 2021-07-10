using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace NWR.Lobby
{
    public class UI_List : MonoBehaviour, IShowOrHideList
    {
        [Header("Stats: ")]
        [SerializeField] private Vector2 position_ON_Screen;
        [SerializeField] private Vector2 position_OUT_ofScreen;

        [Space(10)]
        [SerializeField] private Vector2 leap;

        [Header("Play mode stats:")]
        [SerializeField] private bool isActive = false;


        private Sequence _sequence;
        public void ShowOrHide()
        {
            if (isActive)
            {
                StartCoroutine(Hide());
                isActive = !isActive;
            }
            else
            {
                StartCoroutine(Show());
                isActive = !isActive;
            }
        }
        public IEnumerator Hide()
        {
            _sequence = DOTween.Sequence();
            this.gameObject.GetComponent<CanvasGroup>().interactable = false;
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

            _sequence.Append(rectTransform.DOAnchorPos(position_ON_Screen - leap, 0.15f));
            _sequence.Append(rectTransform.DOAnchorPos(position_OUT_ofScreen, 0.35f));
            yield return new WaitForSeconds(1.05f);

            _sequence.AppendCallback(() => { KillSequence(); });
        }

        public IEnumerator Show()
        {
            _sequence = DOTween.Sequence();
            this.gameObject.GetComponent<CanvasGroup>().interactable = true;
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

            _sequence.Append(rectTransform.DOAnchorPos(position_ON_Screen - leap, 0.35f));
            _sequence.Append(rectTransform.DOAnchorPos(position_ON_Screen, 0.15f));
            yield return new WaitForSeconds(1.05f);

            _sequence.AppendCallback(() => { KillSequence(); });
        }

        private void KillSequence()
        {
            _sequence.Kill();
        }
    }
}

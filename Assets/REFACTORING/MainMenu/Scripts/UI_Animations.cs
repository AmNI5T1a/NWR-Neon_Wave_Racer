using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace NWR.MainMenu
{
    public class UI_Animations : Singleton<UI_Animations>
    {
        private Sequence _sequence;

        [SerializeField] private Vector2 positionOnScreenForMenu = new Vector2(-350f, 0f);
        [SerializeField] private Vector2 positionOutOfScreenForMenu = new Vector2(350f, 0f);



        void Awake()
        {
            DOTween.Init();
        }



        public void Appear(GameObject obj)
        {
            StartCoroutine(AppearAnimation(obj));
        }
        private IEnumerator AppearAnimation(GameObject obj)
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



        public void Hide(GameObject obj)
        {
            StartCoroutine(HideAnimation(obj));
        }
        private IEnumerator HideAnimation(GameObject obj)
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

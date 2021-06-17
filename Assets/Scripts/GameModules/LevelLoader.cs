using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NWR
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("In game stats: ")]
        [SerializeField] public List<GameObject> listOFObjectsNotDestroyOnLoad;
        [SerializeField] public List<Canvas> listOfCanvasObjectsToHideBeforeLoad;
        [HideInInspector] public bool gameIsLoading;
        private ushort currentSceneId;
        private ushort needToLoadSceneId;

        [Space(2)]

        [SerializeField] private AsyncOperation sceneAsync;
        public void LoadScene(ushort currentSceneId, ushort needToLoadSceneId)
        {
            this.currentSceneId = currentSceneId;
            this.needToLoadSceneId = needToLoadSceneId;

            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return null;

            HideALLUIElementsOnScene();

            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Start");
            yield return new WaitForSeconds(1f);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex: needToLoadSceneId);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                Debug.Log("Current progress: " + (asyncOperation.progress * 100) + "%");
                Scene loadedScene = SceneManager.GetSceneByBuildIndex(needToLoadSceneId);

                foreach (GameObject obj in listOFObjectsNotDestroyOnLoad)
                {
                    DontDestroyOnLoad(obj);
                }

                if (asyncOperation.progress >= 0.9f)
                {
                    Debug.LogWarning("Game is loaded");
                    this.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        asyncOperation.allowSceneActivation = true;
                    }
                }
                yield return null;
            }
        }

        private void HideALLUIElementsOnScene()
        {
            if (listOfCanvasObjectsToHideBeforeLoad.Count != 0)
            {
                List<GameObject> listOFUIObjectsToHide = new List<GameObject>();
                foreach (Canvas can in listOfCanvasObjectsToHideBeforeLoad)
                {
                    for (byte c = 0; c < can.gameObject.transform.childCount; c++)
                    {
                        can.gameObject.transform.GetChild(c).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.LogWarning("Didn't contains any canvas to hide UI elements");
                Debug.LogWarning("To hide UI elements u need to add canvas to list with name {listOfCanvasObjectsToHideBeforeLoad}");
            }
        }
    }
}
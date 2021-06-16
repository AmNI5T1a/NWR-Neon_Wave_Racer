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
    }
}
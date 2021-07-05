using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NWR.Modules
{
    public class LevelLoader : Singleton<LevelLoader>
    {
        private static bool sceneIsLoading;
        private AsyncOperation operation;

        void Awake()
        {
            InstanciateLoadingScreenCanvasFromRes();



            DontDestroyOnLoad(this.gameObject);
        }

        private void InstanciateLoadingScreenCanvasFromRes()
        {
            var loadingScreen = Resources.Load("Loading Screen (Canvas)");
            var obj = Instantiate(loadingScreen) as GameObject;
            obj.transform.SetParent(this.gameObject.transform);
        }

        public void LoadScene(int sceneIdToLoad)
        {
            if (!sceneIsLoading)
                StartCoroutine(Load(sceneIdToLoad));
            else
                Debug.LogWarning("Scene is already loading!");
        }

        private IEnumerator Load(int sceneIdToLoad)
        {
            sceneIsLoading = true;

            IAppearAnimation appearAnimation = new LoadingScreenAnimations();
            StartCoroutine(appearAnimation.AppearAnimation(this.gameObject.transform.GetChild(0).GetChild(0).gameObject));
            yield return new WaitForSeconds(1.55f);

            operation = SceneManager.LoadSceneAsync(sceneBuildIndex: sceneIdToLoad);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                Debug.Log("Current loading progress: " + (operation.progress * 100) + "%");
                Scene loadedScene = SceneManager.GetSceneByBuildIndex(sceneIdToLoad);

                if (operation.progress >= 0.9f)
                {
                    Debug.LogWarning("Scene is loaded!");
                    this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        operation.allowSceneActivation = true;
                        IHideAnimation hideAnimation = new LoadingScreenAnimations();
                        StartCoroutine(hideAnimation.HideAnimation(this.gameObject.transform.GetChild(0).GetChild(0).gameObject));
                        yield return new WaitForSeconds(1.55f);
                    }
                }
                yield return null;
            }

            sceneIsLoading = false;
            Destroy(this.gameObject);
        }
    }
}

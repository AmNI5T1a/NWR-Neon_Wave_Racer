using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NWR
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] public List<GameObject> listOfGameObjectsNeedToLoad;

        [Header("In game stats: ")]
        [SerializeField] public bool gameIsLoading;

        [Space(2)]

        [SerializeField] private ushort currentSceneId;
        [SerializeField] private ushort needToLoadSceneId;

        [Space(2)]

        [SerializeField] private AsyncOperation sceneAsync;
        public void StartTransition()
        {
            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Start");
        }

        public void EndTransition()
        {
            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("End");
        }

        public void LoadScene(ushort currentSceneId, ushort needToLoadSceneId)
        {
            this.currentSceneId = currentSceneId;
            this.needToLoadSceneId = needToLoadSceneId;

            StartCoroutine(LoadASyncScene());
        }

        private IEnumerator LoadASyncScene()
        {
            if (!gameIsLoading)
            {
                gameIsLoading = true;

                AsyncOperation scene = SceneManager.LoadSceneAsync(needToLoadSceneId, LoadSceneMode.Additive);
                scene.allowSceneActivation = false;

                sceneAsync = scene;

                this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Start");
                yield return new WaitForSeconds(1f);

                while (scene.progress < 0.9f)
                {
                    Debug.Log("Loading scene progress: " + scene.progress);
                    yield return null;
                }

                enableNewLoadedScene();
            }
        }

        private void enableNewLoadedScene()
        {

            Scene loadedScene = SceneManager.GetSceneByBuildIndex(needToLoadSceneId);

            if (loadedScene.IsValid())
            {
                Debug.Log("Scene is Valid");

                foreach (GameObject obj in listOfGameObjectsNeedToLoad)
                {
                    SceneManager.MoveGameObjectToScene(obj, loadedScene);
                }

                //sceneAsync.allowSceneActivation = true;
                SceneManager.SetActiveScene(loadedScene);
                SceneManager.UnloadSceneAsync(currentSceneId);
            }
        }
    }
}
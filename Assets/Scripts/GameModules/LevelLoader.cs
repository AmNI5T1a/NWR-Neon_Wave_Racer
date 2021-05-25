using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NWR
{
    public class LevelLoader : Singleton<LevelLoader>
    {
        [Header("In game stats: ")]
        [SerializeField] public bool gameIsLoading;
        public void StartTransition()
        {
            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Start");
        }

        public void EndTransition()
        {
            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("End");
        }

        public void LoadScene(byte sceneNumber)
        {
            StartCoroutine(LoadASyncScene(sceneNumber));
        }

        private IEnumerator LoadASyncScene(byte sceneNumber)
        {
            if (!gameIsLoading)
            {
                gameIsLoading = true;

                Scene currentScene = SceneManager.GetActiveScene();

                StartTransition();

                yield return new WaitForSeconds(1);

                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Additive);

                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                SceneManager.UnloadSceneAsync(currentScene);
            }
        }
    }
}
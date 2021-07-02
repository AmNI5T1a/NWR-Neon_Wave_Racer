using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR.Modules
{
    public class LevelLoader : Singleton<LevelLoader>
    {
        [SerializeField] private bool sceneIsLoading;

        private byte currentSceneId;

        [Space(2)]
        [SerializeField] private AsyncOperation operation;

        void Awake()
        {
            var loadingScreen = Resources.Load("loadingScreen");
            var obj = Instantiate(loadingScreen) as GameObject;
            obj.transform.SetParent(this.gameObject.transform);
        }

        public void LoadScene(int sceneIdToLoad)
        {
            StartCoroutine(Load(sceneIdToLoad));
        }

        private IEnumerator Load(int sceneIdToLoad)
        {
            sceneIsLoading = true;
            yield return null;
            Debug.Log("Yeah everything works fine");
        }
    }
}

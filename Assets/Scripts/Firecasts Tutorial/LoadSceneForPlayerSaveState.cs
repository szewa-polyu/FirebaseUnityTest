using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FirecastsTutorial
{
    public class LoadSceneForPlayerSaveState : MonoBehaviour
    {
        [SerializeField] private PlayerSaveManager _playerSaveManager;
        [SerializeField] private string _sceneForSaveExists;
        [SerializeField] private string _sceneForNoSave;

        private Coroutine _coroutine;

        public void Trigger()
        {
            //if (_playerSaveManager.SaveExists())
            //{
            //    SceneManager.LoadScene(_sceneForSaveExists);
            //}
            //else
            //{
            //    SceneManager.LoadScene(_sceneForNoSave);
            //}

            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(LoadSceneCoroutine());
            }
        }

        private IEnumerator LoadSceneCoroutine()
        {
            var saveExistsTask = _playerSaveManager.SaveExistsAsync();
            yield return new WaitUntil(() => saveExistsTask.IsCompleted);
            if (saveExistsTask.Result)
            {
                SceneManager.LoadScene(_sceneForSaveExists);
            }
            else
            {
                SceneManager.LoadScene(_sceneForNoSave);
            }

            _coroutine = null;
        }
    }
}
using UnityEngine;

namespace FirecastsTutorial
{
    public class SyncPlayerToSave : MonoBehaviour
    {
        [SerializeField] private PlayerSaveManager _playerSaveManager;
        [SerializeField] private PlayerBehaviour _player;

        private void Reset()
        {
            _playerSaveManager = FindObjectOfType<PlayerSaveManager>();
        }

        //private void Start()
        //{
        //    var playerData = _playerSaveManager.LoadPlayer();
        //    if (playerData.HasValue)
        //    {
        //        _player.UpdatePlayer(playerData.Value);
        //    }
        //    _player.OnPlayerUpdated.AddListener(HandlePlayerUpdated);
        //}

        private void Start()
        {
            //var playerDataTask = _playerSaveManager.LoadPlayerAsync();
            //yield return new WaitUntil(() => playerDataTask.IsCompleted);
            //var playerData = playerDataTask.Result;
            //if (playerData.HasValue)
            //{
            //    _player.UpdatePlayer(playerData.Value);
            //}
            _player.OnPlayerUpdated.AddListener(HandlePlayerUpdated);
            _playerSaveManager.OnPlayerUpdated.AddListener(HandlePlayerSaveUpdated);
            _player.UpdatePlayer(_playerSaveManager.LastPlayerData);
        }

        private void OnDestroy()
        {
            _player.OnPlayerUpdated.RemoveListener(HandlePlayerUpdated);
            _playerSaveManager.OnPlayerUpdated.RemoveListener(HandlePlayerSaveUpdated);
        }

        private void HandlePlayerUpdated()
        {
            _playerSaveManager.SavePlayerAsync(_player.PlayerData);
        }

        private void HandlePlayerSaveUpdated(PlayerData playerData)
        {
            _player.UpdatePlayer(playerData);
        }
    }
}

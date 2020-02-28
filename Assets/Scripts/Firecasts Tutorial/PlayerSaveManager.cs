using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace FirecastsTutorial
{
    public class PlayerSaveManager : MonoBehaviour
    {
        private const string PLAYER_KEY = "PLAYER_KEY";
        private FirebaseDatabase _database;
        private DatabaseReference _ref;

        public PlayerData LastPlayerData { get; private set; }
        public PlayerUpdatedEvent OnPlayerUpdated = new PlayerUpdatedEvent();


        private void Start()
        {
            _database = FirebaseDatabase.DefaultInstance;
            _ref = _database.GetReference(PLAYER_KEY);
            _ref.ValueChanged += HandleValueChanged;
        }

        private void OnDestroy()
        {
            _ref.ValueChanged -= HandleValueChanged;
            _ref = null;
            _database = null;
        }


        public async Task SavePlayerAsync(PlayerData player)
        {
            string playerJson = JsonUtility.ToJson(player);

            //PlayerPrefs.SetString(PLAYER_KEY, playerJson);

            if (!player.Equals(LastPlayerData))
            {
                await _database.GetReference(PLAYER_KEY).SetRawJsonValueAsync(playerJson);
            }
        }

        public async Task<PlayerData?> LoadPlayerAsync()
        {
            //if (await SaveExistsAsync())
            //{
            //    return JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PLAYER_KEY));
            //}

            //return null;

            var databaseSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();

            if (!databaseSnapshot.Exists)
            {
                return null;
            }

            return JsonUtility.FromJson<PlayerData>(databaseSnapshot.GetRawJsonValue());
        }

        public async Task<bool> SaveExistsAsync()
        {
            //return PlayerPrefs.HasKey(PLAYER_KEY);

            var databaseSnapshot = await _database.GetReference(PLAYER_KEY).GetValueAsync();
            return databaseSnapshot.Exists;
        }

        public void EraseSave()
        {
            //PlayerPrefs.DeleteKey(PLAYER_KEY);

            _database.GetReference(PLAYER_KEY).RemoveValueAsync();
        }

        private void HandleValueChanged(object sender, ValueChangedEventArgs e)
        {
            var json = e.Snapshot.GetRawJsonValue();
            if (!string.IsNullOrEmpty(json))
            {
                var playerData = JsonUtility.FromJson<PlayerData>(json);
                LastPlayerData = playerData;
                OnPlayerUpdated?.Invoke(playerData);
            }
        }

        [System.Serializable]
        public class PlayerUpdatedEvent : UnityEvent<PlayerData>
        {

        }
    }
}

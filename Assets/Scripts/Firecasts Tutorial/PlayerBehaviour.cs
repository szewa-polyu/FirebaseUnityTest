using UnityEngine;
using UnityEngine.Events;

namespace FirecastsTutorial
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private PlayerData _playerData;

        public PlayerData PlayerData => _playerData;
        public string Name => _playerData.Name;
        public Color Color => _playerData.Color;

        public UnityEvent OnPlayerUpdated = new UnityEvent();

        public void SetColor(Color color)
        {
            _playerData.Color = color;
            OnPlayerUpdated?.Invoke();
        }

        public void UpdatePlayer(PlayerData playerData)
        {
            if (!playerData.Equals(_playerData))
            {
                _playerData = playerData;
                OnPlayerUpdated?.Invoke();
            }
        }
    }
}

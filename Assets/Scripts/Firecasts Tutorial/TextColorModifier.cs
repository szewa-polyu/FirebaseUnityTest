using UnityEngine;
using UnityEngine.UI;

namespace FirecastsTutorial
{
    // https://www.youtube.com/watch?v=Uw6XcLImDVk
    public class TextColorModifier : MonoBehaviour
    {
        public PlayerBehaviour playerBehaviour;

        public Text text;
        public Slider red;
        public Slider green;
        public Slider blue;

        private void Start()
        {
            playerBehaviour.OnPlayerUpdated.AddListener(HandlePlayerUpdated);
            SetSliders(playerBehaviour.Color);            
        }

        private void OnDestroy()
        {
            playerBehaviour.OnPlayerUpdated.RemoveListener(HandlePlayerUpdated);
        }


        private void SetSliders(Color color)
        {
            red.value = color.r;
            green.value = color.g;
            blue.value = color.b;
        }

        
        public void OnEdit()
        {
            Color color = new Color
            {
                r = red.value,
                g = green.value,
                b = blue.value,
                a = text.color.a
            };
            text.color = color;
            playerBehaviour.SetColor(color);
        }

        private void HandlePlayerUpdated()
        {
            SetSliders(playerBehaviour.Color);
        }
    }
}

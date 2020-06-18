using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersLeoEcs.AppData
{
    public class SceneData : MonoBehaviour
    {
        public Camera Camera = default;
        public Canvas Canvas = default;

        public Canvas SplashScreen = default;
        public Text SplashScreenScore = default;

        public Text ScoreText = default;
        
        public Transform Player1 = null;
        public Transform Player2 = null;
    }
}
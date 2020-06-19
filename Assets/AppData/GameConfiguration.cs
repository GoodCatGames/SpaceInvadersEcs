using SpaceInvadersLeoEcs.Blueprints;
using UnityEngine;

namespace SpaceInvadersLeoEcs.AppData
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "SpaceInvadersLeoEcs/GameConfiguration", order = 0)]
    public class GameConfiguration : ScriptableObject
    {
        public GunBlueprint Player1Gun = default;
        public float Player1Speed = default;
        
        public GunBlueprint Player2Gun = default;
        public float Player2Speed = default;
        
        public GameObject GunUndicatorPrefab = default;

        public string mobBlueprintsPath = "Blueprints/Mobs/";
    }
}
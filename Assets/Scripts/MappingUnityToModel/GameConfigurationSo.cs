using Model.AppData;
using UnityEngine;

namespace SpaceInvadersLeoEcs.MappingUnityToModel
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "SpaceInvadersLeoEcs/GameConfiguration", order = 0)]
    public class GameConfigurationSo : ScriptableObject
    {
        public GameConfiguration GameConfiguration;
    }
}
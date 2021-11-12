using System;
using SpaceInvadersLeoEcs.MappingUnityToModel.EntityFactoriesFromSo;
using UnityEngine;

namespace SpaceInvadersLeoEcs.MappingUnityToModel
{
    [Serializable]
    public class AppConfiguration
    {
        public GunEntityFactoryFromSo Player1Gun = default;
        public GunEntityFactoryFromSo Player2Gun = default;

        public GameObject GunIndicatorPrefab = default;

        public string mobBlueprintsPath = "ScriptableObjects/Mobs/";
    }
}
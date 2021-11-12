using Leopotam.Ecs;
using Model.Extensions.EntityFactories;
using UnityEngine;

namespace SpaceInvadersLeoEcs.Extensions
{
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public abstract class EntityFactoryFromSo : ScriptableObject, IEntityFactory
    {
        public abstract EcsEntity CreateEntity(EcsWorld world);
    }
}
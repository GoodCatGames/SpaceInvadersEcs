using Leopotam.Ecs;

namespace Model.Extensions.EntityFactories
{
    public interface IEntityFactory
    {
        EcsEntity CreateEntity(EcsWorld world);
    }
}
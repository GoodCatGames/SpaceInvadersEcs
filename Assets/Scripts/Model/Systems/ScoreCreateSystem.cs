using Leopotam.Ecs;
using Model.Components.Body;
using Model.Components.Requests;

namespace Model.Systems
{
    public sealed class ScoreCreateSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;

        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<Score>();
            entity.Get<ViewCreateRequest>();
        }
    }
}
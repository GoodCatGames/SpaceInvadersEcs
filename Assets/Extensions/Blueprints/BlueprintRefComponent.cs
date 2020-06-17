namespace SpaceInvadersLeoEcs.Extensions.Blueprints
{
    internal struct BlueprintRefComponent<T>
        where T : Blueprint
    {
        public T Value;
    }
}
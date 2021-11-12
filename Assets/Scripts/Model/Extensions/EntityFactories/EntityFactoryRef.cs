namespace Model.Extensions.EntityFactories
{
    public struct EntityFactoryRef<T>
        where T : IEntityFactory
    {
        public T Value;
    }
}
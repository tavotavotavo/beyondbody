namespace Domain
{
    public abstract class BaseEntity
    {
        public abstract bool IsEmpty { get; }

        protected T GetValueOrDefault<T>(T entity) where T : new()
        {
            return entity == null ? (entity = new T()) : entity;
        }
    }
}
namespace Neureveal.Data.Repositories.GenericRepo
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        TEntity GetById(Guid id);

        void Add(TEntity entity);
        void Update(TEntity entity);

        void Delete(Guid id);

        bool SaveChanges();

    }
}

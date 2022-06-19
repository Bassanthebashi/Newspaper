using Neureveal.Data.Context;

namespace Neureveal.Data.Repositories.GenericRepo
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        private readonly NewspaperContext context;

        public GenericRepo(NewspaperContext _context)
        {
            context = _context;
        }
        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }
        public void Delete(Guid id)
        {
            TEntity entityToDelete = GetById(id);
            if (entityToDelete != null)
                context.Remove(entityToDelete);

        }

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void Update(TEntity entity)
        {

        }
    }
}

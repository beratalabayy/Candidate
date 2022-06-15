using Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly DbContext _context;
        #endregion

        #region Ctor

        public EfRepositoryBase(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _context.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }

        #endregion

        #region Methods

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().AsNoTracking()
                      .FirstOrDefault(e => e.Id == id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().AsNoTracking()
                     .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                entity.IsDelete = false;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = null;
                _context.Set<TEntity>().Add(entity);
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                entity.Id = 0;
                entity.IsDelete = false;
                entity.CreatedDate = DateTime.Now;
                entity.UpdatedDate = null;
                await _context.Set<TEntity>().AddAsync(entity);
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                entity.UpdatedDate = DateTime.Now;
                _context.Set<TEntity>().Update(entity);
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                entity.UpdatedDate = DateTime.Now;
                entity.IsDelete = true;
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.Now;
                _context.Set<TEntity>().Update(entity);
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10)
        {
            if (condition == null)
                return _context.Set<TEntity>().ToList();
            else
                return _context.Set<TEntity>().Where(condition).ToList();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10)
        {
            int skip = page == 0 ? 0 : (page - 1) * pageSize;

            if (condition == null)
                return await _context.Set<TEntity>().Skip(skip).Take(pageSize).ToListAsync();
            else
                return await _context.Set<TEntity>().Where(condition).Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null)
        {
            if (condition == null)
                return await _context.Set<TEntity>().ToListAsync();
            else
                return await _context.Set<TEntity>().Where(condition).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          Expression<Func<TEntity, TEntity>> selectExpression = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);
            if (selectExpression != null) query = query.Select(selectExpression);
            return await query.ToListAsync();
        }
        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Count(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10)
        {
            if (condition == null)
                return _context.Set<TEntity>().Count();
            else
                return _context.Set<TEntity>().Where(condition).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10)
        {
            if (condition == null)
                return await _context.Set<TEntity>().CountAsync();
            else
                return await _context.Set<TEntity>().Where(condition).CountAsync();
        }

        #endregion

        #region Properties

        public virtual IQueryable<TEntity> Table => _context.Set<TEntity>();

        public virtual IQueryable<TEntity> TableNoTracking => _context.Set<TEntity>().AsNoTracking();

        #endregion
    }
}

using Core.Entites;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public partial interface IEntityRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="selectExpression"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate = null,
             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
             Expression<Func<TEntity, TEntity>> selectExpression = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> condition = null, int page = 0, int pageSize = 10);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();
        #endregion

        #region Properties

        IQueryable<TEntity> Table { get; }


        IQueryable<TEntity> TableNoTracking { get; }

        #endregion
    }
}

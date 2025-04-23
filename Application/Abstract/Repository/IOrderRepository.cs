using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IOrderRepository : IGeneralRepository<Order>
    {
        IQueryable<Order> GetAll();
        Task<Order?> GetByIdAsync(Guid id);
        //Task<Order> GetOrderByUserIdAsync(Guid userId);
        Task<List<Order>> GetByUserIdAsync(Guid userId);
        Task<List<Order>> GetOrdersFromDateAsync(DateTime fromDate);
    }
}

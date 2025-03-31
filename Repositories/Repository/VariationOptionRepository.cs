using Domain.Entities;
using Persistence;
using Repositories.Repository.GeneralRepository;

namespace Repositories.Repository
{
    public class VariationOptionRepository : GeneralRepository<VariationOption>
    {
        public VariationOptionRepository(ApplicationDbContext context) : base(context)
        { }
    }
}

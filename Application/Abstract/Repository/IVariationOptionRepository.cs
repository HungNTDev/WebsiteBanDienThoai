﻿using Application.Abstract.Repository.Base;
using Domain.Entities;

namespace Application.Abstract.Repository
{
    public interface IVariationOptionRepository : IGeneralRepository<VariationOption>
    {
        Task<VariationOption> GetByIdAsync(Guid id);
    }
}

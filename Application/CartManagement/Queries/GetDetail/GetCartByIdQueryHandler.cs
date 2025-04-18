using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using Application.CategoryManagement.Queries.GetDetail;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using OneOf;

namespace Application.CartManagement.Queries.GetCartById
{
    public class GetCartByIdQueryHandler :
        IQueryHandler<GetCartByIdQuery, OneOf<GetCartByIdDto, ApiResponse<GetCartByIdDto>>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<GetCartByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetCartByIdQueryHandler(IMapper mapper,
                                       ILogger<GetCartByIdQueryHandler> logger,
                                       ICartRepository cartRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cartRepository = cartRepository;
        }

        public async Task<OneOf<GetCartByIdDto, ApiResponse<GetCartByIdDto>>>
            Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Cart cart = await _cartRepository.GetCartByIdAsync(request.id);
                if (cart == null)
                {
                    return ApiResponseBuilder.Error<GetCartByIdDto>($"Không tìm thấy giỏ hàng ",
                        statusCode: 404);
                }
                GetCartByIdDto item = _mapper.Map<GetCartByIdDto>(cart);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

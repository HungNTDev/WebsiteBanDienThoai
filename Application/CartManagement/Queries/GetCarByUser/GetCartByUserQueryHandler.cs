using Application.Abstract.BaseClass;
using Application.Abstract.CQRS;
using Application.Abstract.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.CartManagement.Queries.GetCarByUser
{
    public class GetCartByUserQueryHandler : IQueryHandler<GetCartByUserQuery, ApiResponse<GetCartByUserDto>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCartByUserQueryHandler> _logger;

        public GetCartByUserQueryHandler(IMapper mapper, ICartRepository cartRepository, ILogger<GetCartByUserQueryHandler> logger)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<GetCartByUserDto>> Handle(
            GetCartByUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);
                if (cart == null)
                {
                    return ApiResponseBuilder.Error<GetCartByUserDto>("Không tìm thấy giỏ hàng");
                }
                var dto = _mapper.Map<GetCartByUserDto>(cart);
                return ApiResponseBuilder.Success<GetCartByUserDto>(dto, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new NullReferenceException(nameof(Handle));
            }
        }
    }
}

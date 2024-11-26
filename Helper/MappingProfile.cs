namespace Store.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Item mappings
            CreateMap<PostItemCommand, Item>();
            CreateMap<PutItemDto, Item>();
            CreateMap<Item, GetAllItemDto>();
            CreateMap<Item, GetItemByIdDto>();

            // UOM mappings
            CreateMap<PostUOMsCommand, UOM>(); 
            CreateMap<PutUOMsDto, UOM>();       
            CreateMap<UOM, GetAllUOMsDto>();

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<RegisterCommand, User>();
            CreateMap<User, GetAllUsersDTO>();

            // Order mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
                .ForMember(dest => dest.CloseDate, opt => opt.MapFrom(src => src.CloseDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.CurrencyCode))
                .ForMember(dest => dest.ExchangeRate, opt => opt.MapFrom(src => src.ExchangeRate))
                .ForMember(dest => dest.ForeignPrice, opt => opt.MapFrom(src => src.ForeignPrice))
                .ForMember(dest => dest.IsClosed, opt => opt.MapFrom(src => src.IsClosed))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            // OrderDetail mappings
            CreateMap<OrderDetail, GetOrderDetailDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.ItemPrice, opt => opt.MapFrom(src => src.ItemPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<GetOrderByIdQuery, Order>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId));
            CreateMap<Order, GetOrderByIdDto>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        }
    }
}

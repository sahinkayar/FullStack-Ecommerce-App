using api.Dtos.Order;

namespace api.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto toDtoFromDb(this api.models.Order order)
        {

            return new OrderDto()
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                Items = order.OrderItems.Select((o) => new OrderItemDto
                {
                    ProductId = o.ProductId,
                    ProductName = o.Product!.Name,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    LineTotal = o.Price * o.Quantity
                }).ToList()

            };
        }

    }
}

using api.Dtos.Order;
using api.models;

namespace api.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto toDtoFromDb(this Order order)
        {

             order.OrderItems.Select((c) => new OrderItemDto
            {
                
               
            });

            return new OrderDto()
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                Items = order.OrderItems.Select((o) => new OrderItemDto
                {
                    ProductId = o.ProductId,
                    ProductName = o.Product.Name,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    LineTotal = o.Price * o.Quantity
                }).ToList()

            };
        }

    }
}

﻿using FinalTouch.Api.Extensions;
using FinalTouch.Core.Entities.OrderAggregate;
using FinalTouch.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalTouch.Core.Interfaces;
using FinalTouch.Api.Dtos;
using FinalTouch.Core.Specifications;

namespace FinalTouch.Api.Controllers
{
    [Authorize]
    public class ordersController(ICartService cartService, IUnitOfWork unit) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDto orderDto)
        {
            var email = User.GetEmail();

            var cart = await cartService.GetCartAsync(orderDto.CartId);

            if (cart == null) return BadRequest("Cart not found");

            if (cart.PaymentIntentId == null) return BadRequest("No payment intent for this order");

            var items = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                var productItem = await unit.QueryRepository<Product>().GetByIdAsync(item.ProductId);

                if (productItem == null) return BadRequest("Problem with the order");

                var itemOrdered = new ProductItemOrdered
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    PictureUrl = item.PictureUrl
                };

                var orderItem = new OrderItem
                {
                    ItemOrdered = itemOrdered,
                    Price = productItem.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderItem);
            }

            var deliveryMethod = await unit.QueryRepository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliveryMethod == null) return BadRequest("No delivery method selected");

            var order = new Order
            {
                OrderItems = items,
                DeliveryMethod = deliveryMethod,
                ShippingAddress = orderDto.ShippingAddress,
                Subtotal = items.Sum(x => x.Price * x.Quantity),
                Discount = orderDto.Discount,
                PaymentSummary = orderDto.PaymentSummary,
                PaymentIntentId = cart.PaymentIntentId,
                BuyerEmail = email
            };

            unit.CommandRepository<Order>().Add(order);

            if (await unit.Complete())
            {
                return order;
            }

            return BadRequest("Problem creating order");
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var spec = new OrderSpecification(User.GetEmail());

            var orders = await unit.QueryRepository<Order>().ListAsync(spec);

            var ordersToReturn = orders.Select(o => o.ToDto()).ToList();

            return Ok(ordersToReturn);
        }

[HttpGet("{id:int}")]
public async Task<ActionResult<OrderDto>> GetOrderById(int id)
{
    var email = User.GetEmail();
    var isAdmin = User.IsInRole("Admin");

    Order? order;

    if (isAdmin)
    {
        // Allow admin to get any order
        var spec = new OrderSpecification(id);
        order = await unit.QueryRepository<Order>().GetEntityWithSpec(spec);
    }
    else
    {
        // Normal user can only get their own orders
        var spec = new OrderSpecification(email, id);
        order = await unit.QueryRepository<Order>().GetEntityWithSpec(spec);
    }

    if (order == null) return NotFound();

    return order.ToDto();
}

    }
}
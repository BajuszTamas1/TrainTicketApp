using System.Collections.Generic;
using TrainTicketApp.Models;

namespace TrainTicketApp.Services;

public interface IOrderService
{
    void CreateOrder(Order order);
    IEnumerable<Order> GetOrdersByUser(string email);
    Order GetOrderById(int id);
}
using System.Collections.Generic;
using System.Linq;
using TrainTicketApp.Models;
using TrainTicketApp.Data;

namespace TrainTicketApp.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public IEnumerable<Order> GetOrdersByUser(string email)
    {
        return _context.Orders.Where(o => o.UserEmail == email).ToList();
    }

    public Order GetOrderById(int id)
    {
        return _context.Orders.Find(id);
    }
}
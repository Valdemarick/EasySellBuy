using Bag.Domain.Entities;
using Bag.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bag.Persistence;

public class DataSeeder
{
    private readonly ApplicationDbContext _context;

    public DataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InitializeDatabaseAsync()
    {
        if (!await _context.Sellers.AnyAsync())
        {
            await _context.AddRangeAsync(GetSellers());
            await _context.SaveChangesAsync();
        }
        if (!await _context.Customers.AnyAsync())
        {
            await _context.AddRangeAsync(GetCustomers());
            await _context.SaveChangesAsync();
        }
        if (!await _context.Orders.AnyAsync())
        {
            await _context.AddRangeAsync(GetOrders());
            await _context.SaveChangesAsync();
        }
    }

    private static IEnumerable<Customer> GetCustomers() => new List<Customer>
    {
        new()
        {
            UserName = "first customer",
            PhoneNumber = "+375 29 341 42 32"
        },
        new()
        {
            UserName = "second customer",
            PhoneNumber = "+375 33 432 12 53"
        }
    };

    private static IEnumerable<Seller> GetSellers() => new List<Seller>
    {
        new()
        {
            UserName = "first seller",
            PhoneNumber = "+375 44 213 65 32"
        },
        new()
        {
            UserName = "second seller",
            PhoneNumber = "+375 29 312 00 11"
        }
    };

    private static IEnumerable<Order> GetOrders() => new List<Order>
    {
        new()
        {
            CustomerId = 1,
            SellerId = 1,
            TotalAmount = 345.43m,
        },
        new()
        {
            CustomerId = 2,
            SellerId = 2,
            TotalAmount = 1435.32m
        }
    };
}
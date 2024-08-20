using Microsoft.EntityFrameworkCore;
using WarehouseManager.API.Models;

namespace WarehouseManager.API.Data;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}

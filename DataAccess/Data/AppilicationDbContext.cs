using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Data
{
    public class AppilicationDbContext : DbContext
    {
        public AppilicationDbContext(DbContextOptions<AppilicationDbContext> options) : base(options)
        { }  
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
    }
}

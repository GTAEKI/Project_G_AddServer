﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameDB
{
    public class GameDbContext : DbContext
    {
        public DbSet<PlayerDb> Players { get; set; }

        static readonly ILoggerFactory _logger = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public static string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GameDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public GameDbContext() 
        {
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                .UseLoggerFactory(_logger)
                .UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayerDb>()
                .HasIndex(t => t.UserId)
                .IsUnique();
        }
    }
}

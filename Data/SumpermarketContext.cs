﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Models;

namespace SupermarketWEB.Data
{
	public class SupermarketContext : DbContext
	{
		public SupermarketContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
        public DbSet<PayModeModels> PayModes { get; set; }
        public DbSet<Providers> Provider { get; set; }

    }
}
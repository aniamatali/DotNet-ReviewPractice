﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gummi.Models
{
	public interface IProductRepository
	{
    IQueryable<Category> Categories { get; }
		IQueryable<Product> Products { get; }
		Product Save(Product product);
		Product Edit(Product product);
		void Remove(Product product);
	}
}

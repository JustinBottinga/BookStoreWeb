using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ApplicationDbContext _context {  get; set; }
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product obj)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (product != null)
            {
                product.Title = obj.Title;
                product.Description = obj.Description;
                product.Price = obj.Price;
                product.CategoryId = obj.CategoryId;
                product.Price50 = obj.Price50;
                product.Price100 = obj.Price100;
                product.CoverTypeId = obj.CoverTypeId;
                product.ISBN = obj.ISBN;
                product.Author = obj.Author;
                product.ListPrice = obj.ListPrice;
                if (obj.ImageUrl != null)
                {
                    product.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}

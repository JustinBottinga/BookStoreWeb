using BookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Catergory { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Catergory = new CategoryRepository(context);
            CoverType = new CoverTypeRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

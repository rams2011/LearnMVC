using Learn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.DataAccess.Repository
{
   internal class CompanyRepository : Repository<Company>, ICompanyRepository
   {
      private ApplicationDbContext _db;
      public CompanyRepository(ApplicationDbContext db) : base(db)
      {
         _db = db;
      }
      public void Update(Company company)
      {
         _db.Companies.Update(company);
      }
   }
}

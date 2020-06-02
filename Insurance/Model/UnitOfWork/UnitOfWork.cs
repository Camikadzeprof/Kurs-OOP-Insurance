using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceComp
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseDbContext _dbContext;

        public IRepository<User> UserRepository => new GenericRepository<User>(_dbContext);
        public IRepository<InsType> InsTypeRepository => new GenericRepository<InsType>(_dbContext);
        public IRepository<Insurance> InsuranceRepository => new GenericRepository<Insurance>(_dbContext);
        public IRepository<Incident> IncidentRepository => new GenericRepository<Incident>(_dbContext);
        public IRepository<Payout> PayoutRepository => new GenericRepository<Payout>(_dbContext);


        public UnitOfWork(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

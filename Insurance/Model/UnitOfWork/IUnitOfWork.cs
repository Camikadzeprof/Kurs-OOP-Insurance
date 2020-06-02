using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceComp
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<InsType> InsTypeRepository { get; }
        IRepository<Insurance> InsuranceRepository { get; }
        IRepository<Incident> IncidentRepository { get; }
        IRepository<Payout> PayoutRepository { get; }

        void Commit();

        void Dispose();
    }
}

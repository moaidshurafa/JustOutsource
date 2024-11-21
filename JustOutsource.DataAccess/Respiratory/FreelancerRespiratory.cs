using JustOutsource.DataAccess.Data;
using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustOutsource.DataAccess.Respiratory
{
    public class FreelancerRespiratory : Respiratory<Freelancer>, IFreelancerRespiratory
    {
        private ApplicationDbContext _db;
        public FreelancerRespiratory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Freelancer obj)
        {
            _db.Freelancers.Update(obj);
        }
    }
}

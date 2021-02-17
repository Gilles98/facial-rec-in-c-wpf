using FacialRec_DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialRec_DAL
{
    public class Context:DbContext
    {
        public Context(): base ("name=FacialRec")
        {

        }

        public DbSet<Persoon> Personen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FaceRec");
        }
    }
}

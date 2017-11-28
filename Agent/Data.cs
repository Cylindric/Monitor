using System.Data.Entity;
using Utils;

namespace Agent
{

    class Data : DbContext
    {
        public Data()
            : base()
        {
        }

        public DbSet<CounterValue> CounterValues { get; set; }
    }


    class DataInitialiser : CreateDatabaseIfNotExists<Data>
    {
        protected override void Seed(Data context)
        {
            base.Seed(context);
        }
    }

}

using PubliSub.Transfer.Domain.Models;

namespace PubliSub.Transfer.Data.Context
{ 
    public static class Seeder
    {
        public static void Seed(this TransferDbContext transferDbContext)
        {
            if (!transferDbContext.TransferLogs.Any())
            {
                //Fixture fixture = new Fixture();
                //fixture.Customize<Product>(product => product.Without(p => p.ProductId));
                ////--- The next two lines add 100 rows to your database
                //List<Product> products = fixture.CreateMany<Product>(100).ToList();
                //bankingDbContext.AddRange(products);
                //bankingDbContext.SaveChanges();

                var transfer1 = new TransferLog { Amount = 5.10M, FromAccount = 1, ToAccount = 2 };
                var transfer2 = new TransferLog { Amount = 15.10M, FromAccount = 1, ToAccount = 2 };
                var transfers = new List<TransferLog> { transfer1, transfer2 };
                transferDbContext.AddRange(transfers);
                transferDbContext.SaveChanges();
            }
        }
    }
}

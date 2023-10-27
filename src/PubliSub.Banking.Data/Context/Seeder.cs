namespace PubliSub.Banking.Data.Context
{
    public static class Seeder
    {
        public static void Seed(this BankingDbContext bankingDbContext)
        {
            if (!bankingDbContext.Accounts.Any())
            {
                //Fixture fixture = new Fixture();
                //fixture.Customize<Product>(product => product.Without(p => p.ProductId));
                ////--- The next two lines add 100 rows to your database
                //List<Product> products = fixture.CreateMany<Product>(100).ToList();
                //bankingDbContext.AddRange(products);
                //bankingDbContext.SaveChanges();
            }
        }
    }
}

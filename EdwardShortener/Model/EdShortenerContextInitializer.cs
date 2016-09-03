using System.Data.Entity;

namespace EdwardShortener.Model
{
    internal class EdShortenerContextInitializer : CreateDatabaseIfNotExists<edShortenerModel>
    {
        protected override void Seed(edShortenerModel context)
        {
           
            context.Users.Add(new User()
            {
                idUser = new System.Guid(),
                userName = "dummyDefault",
                userPass = "sdfgsdfg"
            });

            context.SaveChanges();
        }
    }
}
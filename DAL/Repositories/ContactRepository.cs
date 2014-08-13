using DAL.DBInteractions;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ContactRepository : EntityRepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(IDBFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public Contact GetContactById(long id)
        {
            return DataContext.Contact.Find(id);
        }
    }
}

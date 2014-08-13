using DAL.DBInteractions;
using DAL.Entities;

namespace DAL.Repositories
{
    public interface IContactRepository : IEntityRepository<Contact>
    {
        Contact GetContactById(long Id);
    }
}

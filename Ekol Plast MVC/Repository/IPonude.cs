using Ekol_Plast_MVC.Models;

namespace Ekol_Plast_MVC.Repository
{
    public interface IPonude
    {
        Task<List<Contact>> GetAll();
        Task<Contact> Add(Contact ponuda);
    }
}

using Ekol_Plast_MVC.Data;
using Ekol_Plast_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Ekol_Plast_MVC.Repository
{
    public class PonudeRepository :IPonude
    {
        private readonly ApplicationDbContext _db;
        public PonudeRepository(ApplicationDbContext db) 
        {
            _db = db;
        }
        public List<Contact> Ponude = new List<Contact>();

        public async Task<Contact> Add(Contact ponuda)
        {
           await _db.Ponude.AddAsync(ponuda);
           await _db.SaveChangesAsync();
           return ponuda;
        }

        public async Task<List<Contact>> GetAll()
        {
            var ponude = await _db.Ponude.ToListAsync();
            ponude.Reverse();
            return ponude;
        }
    }
}

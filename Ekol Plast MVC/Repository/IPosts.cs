using Ekol_Plast_MVC.Models;

namespace Ekol_Plast_MVC.Repository
{
    public interface IPosts
    {
        Task<List<Posts>> GetProducts();
       Task<Posts> GetPostById(int Id);
       Task DeletePost(int Id);
       Task<Posts> UpdatePost(Posts post);
        Task<Posts> AddPost(Posts post);
    }
}

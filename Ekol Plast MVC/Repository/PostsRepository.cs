using Ekol_Plast_MVC.Data;
using Ekol_Plast_MVC.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ekol_Plast_MVC.Repository
{
    public class PostsRepository : IPosts
    {
        private readonly ApplicationDbContext _db;

        public PostsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Posts> Posts = new List<Posts>();


        public async Task DeletePost(int Id)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == Id);
            if (post != null)
            {
                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();
            }
            
        }

        public async Task<Posts> GetPostById(int Id)
        {
            return await _db.Posts.FirstOrDefaultAsync(p => p.Id == Id);
        }


        public async Task<Posts> UpdatePost(Posts post)
        {
            _db.Entry(post).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return post;
        }

        public async Task<Posts> AddPost(Posts post)
        {     
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();

            return post;
        }

        public async Task<List<Posts>> GetProducts()
        {
            var posts = await _db.Posts.OrderBy(p => p.Id).ToListAsync();
            posts.Reverse();
            return posts;
        }
    }
}

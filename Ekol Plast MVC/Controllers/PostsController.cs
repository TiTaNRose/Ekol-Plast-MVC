using Ekol_Plast_MVC.Data;
using Ekol_Plast_MVC.Models;
using Ekol_Plast_MVC.Repository;
using Microsoft.AspNetCore.Authorization; // Add this for [Authorize]
using Microsoft.AspNetCore.Mvc;

namespace Ekol_Plast_MVC.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostsRepository _postsRepo;
        private readonly ApplicationDbContext _db;

        public PostsController(PostsRepository postsRepo, ApplicationDbContext db)
        {
            _postsRepo = postsRepo;
            _db = db;
        }

        // GET: PostsController
        public async Task<IActionResult> Index()
        {  
            var posts = await _postsRepo.GetProducts();
            if(posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }


        // GET: PostsController/Details/5
        public async Task<IActionResult> Details(int id, string? title)
        {
            var post = await _postsRepo.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Views ??= 0;
            post.Views++;
            await _postsRepo.UpdatePost(post);

            return View(post);
        }

        // GET: PostsController/Create
        [Authorize(Roles = "Admin")] // Only Admins can access this
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only Admins can perform this action
        public async Task<IActionResult> Create(Posts post)
        {
            if (ModelState.IsValid)
            {
                await _postsRepo.AddPost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: PostsController/Edit/5
        [Authorize(Roles = "Admin")] // Only Admins can access this
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postsRepo.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only Admins can perform this action
        public async Task<IActionResult> Edit(int id, Posts post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _postsRepo.UpdatePost(post);
                }
                catch
                {
                    return View(post);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: PostsController/Delete/5
        [Authorize(Roles = "Admin")] // Only Admins can access this
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postsRepo.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            await _postsRepo.DeletePost(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: PostsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only Admins can perform this action
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postsRepo.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            await _postsRepo.DeletePost(id);
            return RedirectToAction(nameof(Index));
        }
       
    }
}

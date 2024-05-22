using Blogger.Business.Exceptions;
using Blogger.Business.Services.Abstract;
using Blogger.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
    public class BlogController : Controller
    {
		private readonly IBlogService _blogService;
		public BlogController(IBlogService blogService)
		{
			_blogService = blogService;
		}

		public IActionResult Index()
        {
            var blogs = _blogService.GetAllBlogs();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Create(Blog blog)
		{
			if (!ModelState.IsValid)
				return View();

			try
			{
				await _blogService.AddBlog(blog);
			}
			catch (ImageContentException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (ImageSizeException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (FileNullReferenceException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return RedirectToAction("Index");
		}
		public IActionResult Update(int id)
		{
			var existBlog = _blogService.GetBlog(x => x.Id == id);
			if (existBlog == null) return NotFound();
			return View(existBlog);
		}
		[HttpPost]
		public IActionResult Update(Blog blog)
		{
			if (!ModelState.IsValid)
				return View();

			try
			{
				_blogService.UpdateBlog(blog.Id, blog);
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound();
			}
			catch (ImageContentException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (ImageSizeException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (Blogger.Business.Exceptions.FileNotFoundException ex)
			{
				ModelState.AddModelError("ImageFile", ex.Message);
				return View();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}


			return RedirectToAction("Index");
		}
		public IActionResult Delete(int id)
		{
			var existBlog = _blogService.GetBlog(x => x.Id == id);
			if (existBlog == null) return NotFound();
			return View(existBlog);
		}

		[HttpPost]
		public IActionResult DeletePost(int id)
		{

			try
			{
				_blogService.DeleteBlog(id);
			}
			catch (EntityNotFoundException ex)
			{
				return NotFound();
			}
			catch (Blogger.Business.Exceptions.FileNotFoundException ex)
			{
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return RedirectToAction("Index");
		}
	}
}

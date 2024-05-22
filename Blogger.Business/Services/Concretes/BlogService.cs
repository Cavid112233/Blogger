
using Blogger.Business.Exceptions;
using Blogger.Business.Extensions;
using Blogger.Business.Services.Abstract;
using Blogger.Core.Models;
using Blogger.Core.RepositoryAbstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileNotFoundException = Blogger.Business.Exceptions.FileNotFoundException;

namespace Blogger.Business.Services.Concretes
{
	public class BlogService : IBlogService
	{
		private readonly IBlogRepository _blogRepository;
		private readonly IWebHostEnvironment _env;

		public BlogService(IBlogRepository blogRepository, IWebHostEnvironment env)
		{
			_blogRepository = blogRepository;
			_env = env;
		}

		public async Task AddBlog(Blog blog)
		{
			if(blog.ImageFile == null)
				throw new FileNotFoundException("File bos ola bilmez!");

			blog.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads/blogs", blog.ImageFile);
			await _blogRepository.AddAsync(blog);
			await _blogRepository.CommitAsync();
		}

		public void DeleteBlog(int id)
		{
			var existBlog = _blogRepository.Get(x => x.Id == id);
			if (existBlog == null)
				throw new EntityNotFoundException("Blog tapilmadi!");
			Helper.DeleteFile(_env.WebRootPath, @"uploads\blogs", existBlog.ImageUrl);

			 _blogRepository.Delete(existBlog);
			 _blogRepository.Commit();
		}

		public List<Blog> GetAllBlogs(Func<Blog, bool>? predicate = null)
		{
			return _blogRepository.GetAll(predicate);
		}

		public Blog GetBlog(Func<Blog, bool>? predicate = null)
		{
			return _blogRepository.Get(predicate);	
		}

		public void UpdateBlog(int id, Blog newBlog)
		{
			var oldBlog = _blogRepository.Get(x=>x.Id == id);

			if (oldBlog == null)
				throw new EntityNotFoundException("Blog tapilmadi!");

			if(newBlog.ImageFile != null)
			{
				Helper.DeleteFile(_env.WebRootPath,@"uploads\blogs", oldBlog.ImageUrl);
				oldBlog.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\blogs", newBlog.ImageFile);
			}
			oldBlog.Title = newBlog.Title;
			oldBlog.Description = newBlog.Description;
			oldBlog.Name = newBlog.Name;


			_blogRepository.Commit();
		}
	}
}

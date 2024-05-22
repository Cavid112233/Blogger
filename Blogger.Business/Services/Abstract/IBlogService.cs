using Blogger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Business.Services.Abstract
{
	public interface IBlogService
	{
		Task AddBlog(Blog blog);
		void DeleteBlog(int id);
		void UpdateBlog(int id, Blog newBlog);
		Blog GetBlog(Func<Blog,bool>? predicate = null);
		List<Blog> GetAllBlogs(Func<Blog, bool>? predicate = null);
	}
}

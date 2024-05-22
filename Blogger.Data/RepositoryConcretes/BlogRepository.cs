using Blogger.Core.Models;
using Blogger.Core.RepositoryAbstract;
using Blogger.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Data.RepositoryConcretes
{
	public class BlogRepository : GenericRepository<Blog>, IBlogRepository
	{
		public BlogRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}
	}
}

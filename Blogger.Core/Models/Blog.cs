using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Core.Models
{
	public class Blog : BaseEntity
	{
		[Required]
		[StringLength(50)]
        public string Name { get; set; }
		[Required]
		[StringLength(50)]
		public string Title { get; set; }
		[Required]
		[MaxLength(100)]
		public string Description { get; set; }
		[StringLength(50)]
        public string? ImageUrl { get; set; }
		[NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}

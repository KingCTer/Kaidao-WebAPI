using Kaidao.Application.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaidao.Application.ViewModels
{
    public class BookUpdateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập tên truyện")]
        [DisplayName("Tên truyện")]
        public string Name { get; set; }
        public string Status { get; set; }
        public string Intro { get; set; }
        public string AuthorName { get; set; }
        public Guid CategoryId { get; set; }

        public BookResponse Book { get; set; }

        public List<CategoryResponse> CategoryList { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}

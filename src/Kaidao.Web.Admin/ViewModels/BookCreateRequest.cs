using Kaidao.Application.Responses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kaidao.Web.Admin.ViewModels
{
    public class BookCreateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập tên truyện")]
        [DisplayName("Tên truyện")]
        public string Name { get; set; }
        public string Status { get; set; }
        public string Intro { get; set; }
        public string AuthorName { get; set; }
        public string CategoryId { get; set; }

        public List<CategoryResponse> CategoryList { get; set; }

        public IFormFile ThumbnailImage { get; set; }   
    }
}

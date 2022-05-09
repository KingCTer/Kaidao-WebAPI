using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kaidao.Application.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel(string name)
        {
            Name = name;
        }

        public CategoryViewModel()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}
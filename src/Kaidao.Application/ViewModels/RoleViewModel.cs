using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kaidao.Application.ViewModels
{
    public class RoleViewModel
    {

        public RoleViewModel()
        {
        }

        public RoleViewModel(string id, string name, string normalizedName, bool isSystemRole)
        {
            Id = id;
            Name = name;
            NormalizedName = normalizedName;
            IsSystemRole = isSystemRole;
        }

        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [DisplayName("Name")]
        public string Name { get; set; }

        public string NormalizedName { get; protected set; }

        public bool IsSystemRole { get; set; }
    }
}
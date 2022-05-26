using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kaidao.Application.ViewModels
{
    public class FunctionViewModel
    {

        public FunctionViewModel()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

    }
}
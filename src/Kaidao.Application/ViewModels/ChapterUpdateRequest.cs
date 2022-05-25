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
    public class ChapterUpdateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập tên chương")]
        [DisplayName("Tên chương")]
        public string Name { get; set; }

        public int Order { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }

        public BookResponse Book { get; set; }
        public ChapterResponse Chapter { get; set; }

    }
}

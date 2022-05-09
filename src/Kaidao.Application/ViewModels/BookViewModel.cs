using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaidao.Application.ViewModels
{
    public class BookViewModel
    {
        public BookViewModel(string name, string key, string cover, string status, int view, int like, Guid authorId, Guid categoryId)
        {
            Name = name;
            Key = key;
            Cover = cover;
            Status = status;
            View = view;
            Like = like;
            AuthorId = authorId;
            CategoryId = categoryId;
        }

        public BookViewModel(Guid id,
                             string name,
                             string key,
                             string cover,
                             string status,
                             int view,
                             int like,
                             Guid authorId,
                             Guid categoryId,
                             int chapterTotal,
                             string intro
            )
        {
            Id = id;
            Name = name;
            Key = key;
            Cover = cover;
            Status = status;
            View = view;
            Like = like;
            AuthorId = authorId;
            CategoryId = categoryId;
            ChapterTotal = chapterTotal;
            Intro = intro;
        }

        public BookViewModel()
        {
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [DisplayName("Name")]
        public string Name { get; set; }

        public string Key { get; set; }
        public string Cover { get; set; }
        public string Status { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public int ChapterTotal { get; set; }
        public string Intro { get; set; }


        [ForeignKey("AuthorId")]
        public Guid AuthorId { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
    }
}
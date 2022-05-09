using Kaidao.Domain.Core.Entity;

namespace Kaidao.Domain.AppEntity
{
    public class Book : Entity
    {
        public Book()
        {
        }

        public Book(Guid id,
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

        public string Name { get; set; }
        public string Key { get; set; }
        public string Cover { get; set; }
        public string Status { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public int ChapterTotal { get; set; }
        public string Intro { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Chapter> Chapters { get; set; }
    }
}
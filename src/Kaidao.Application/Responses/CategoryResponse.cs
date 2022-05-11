namespace Kaidao.Application.Responses
{
    public class CategoryResponse
    {
        public CategoryResponse()
        {
        }

        public CategoryResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
       
    }
}
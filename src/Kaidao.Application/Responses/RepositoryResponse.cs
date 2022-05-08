namespace Kaidao.Application.Responses
{
    public class RepositoryResponse<T> where T : class
    {
        public IEnumerable<T> ViewModel { get; set; }
        public int TotalRecords { get; set; }

        public RepositoryResponse(IEnumerable<T> viewModel, int totalRecords)
        {
            ViewModel = viewModel;
            TotalRecords = totalRecords;
        }

        public RepositoryResponse()
        {
        }
    }
}
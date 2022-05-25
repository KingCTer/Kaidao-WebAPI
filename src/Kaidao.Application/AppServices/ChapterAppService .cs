using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.Commands.Book;
using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Interfaces;
using Kaidao.Domain.Specifications;
using Kaidao.Infra.Data.Repository.EventSourcing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;

namespace Kaidao.Application.AppServices
{
    public class ChapterAppService : IChapterAppService
    {
        private readonly IMapper _mapper;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;
        private readonly IChapterRepository _chapterRepository;

        public ChapterAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IChapterRepository chapterRepository
            )
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _chapterRepository = chapterRepository;
        }

		public string CrawlContent(ChapterResponse chapterResponse)
		{
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36");

			try
			{
                // Thực hiện truy vấn GET
                HttpResponseMessage response = httpClient.GetAsync(chapterResponse.Url).Result;

                // Hiện thị thông tin header trả về
                //ShowHeaders(response.Headers);

                // Phát sinh Exception nếu mã trạng thái trả về là lỗi
                response.EnsureSuccessStatusCode();

                string htmlChapter = WebUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
                string chapterContentTemp = Regex.Match(htmlChapter, @"(?=box-chap box-chap).*?(?<=<\/div>)", RegexOptions.Singleline).Value.ToString();

                chapterResponse.Content = Regex.Match(chapterContentTemp, @"(?<=>).*?(?=<\/div>)", RegexOptions.Singleline).Value.ToString();


                var updateCommand = _mapper.Map<UpdateChapterCommand>(chapterResponse);
                Bus.SendCommand(updateCommand);
                

                return chapterResponse.Content;
            }
			catch (Exception)
			{

				throw;
			}

            return "";
		}


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ChapterResponse GetChapterByBookIdAndOrder(Guid bookId, int order)
        {
            var chapter = _chapterRepository.GetChapterByBookIdAndOrder(bookId, order);

            return _mapper.Map<ChapterResponse>(chapter);
        }

        public ChapterResponse GetChapterById(Guid id)
        {
            var chapter = _chapterRepository.GetAll().AsNoTracking().FirstOrDefault(c => c.Id == id);

            return _mapper.Map<ChapterResponse>(chapter);
        }

        public IEnumerable<ChapterResponse> GetChapterListByBookId(Guid bookId)
        {
            return _chapterRepository.GetChapterListByBookId(bookId).ProjectTo<ChapterResponse>(_mapper.ConfigurationProvider);
        }

        public RepositoryResponse<ChapterResponse> GetChapterListByBookId(Guid bookId, int skip, int take, string query)
        {
            var response = _chapterRepository.GetPagination(new ChapterFilterPaginatedSpecification(bookId, skip, take, query));
            var bookResponses = response.Queryable.ProjectTo<ChapterResponse>(_mapper.ConfigurationProvider);

            return new RepositoryResponse<ChapterResponse>(bookResponses, response.TotalRecords);
        }

        public ChapterResponse GetLastChapterByBookId(Guid bookId)
        {
            var chapter = _chapterRepository.GetLastChapterByBookId(bookId);

            return _mapper.Map<ChapterResponse>(chapter);
        }

        public bool Create(ChapterViewModel chapterViewModel)
        {
            var registerChapterCommand = _mapper.Map<RegisterNewChapterCommand>(chapterViewModel);
            return Bus.SendCommand(registerChapterCommand).IsCompleted;
        }

        public bool Remove(Guid chapterId)
        {
            var removeCommand = new RemoveChapterCommand(chapterId);
            return Bus.SendCommand(removeCommand).IsCompleted;
        }

        public bool Update(ChapterViewModel chapterViewModel)
        {
            var updateCommand = _mapper.Map<UpdateChapterCommand>(chapterViewModel);
            return Bus.SendCommand(updateCommand).IsCompleted;
        }
    }
}
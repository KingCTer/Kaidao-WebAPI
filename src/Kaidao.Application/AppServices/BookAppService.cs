using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation.Results;
using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Responses;
using Kaidao.Application.Services;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.Commands.Author;
using Kaidao.Domain.Commands.Book;
using Kaidao.Domain.Commands.Category;
using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Interfaces;
using Kaidao.Domain.Specifications;
using Kaidao.Infra.Data.Repository.EventSourcing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Kaidao.Application.AppServices
{
    public class BookAppService : IBookAppService
    {
        private readonly IMapper _mapper;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IChapterRepository _chapterRepository;
        private readonly IStorageService _storageService;

        private readonly IConfiguration _configuration;

        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public BookAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository,
            IChapterRepository chapterRepository,
            IStorageService storageService,
            IConfiguration configuration
            )
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _chapterRepository = chapterRepository;
            _storageService = storageService;
            _configuration = configuration;
        }

        public void Crawl(string url)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.71 Safari/537.36");

            try
            {
                // Thực hiện truy vấn GET
                HttpResponseMessage response = httpClient.GetAsync(url).Result;

                // Hiện thị thông tin header trả về
                //ShowHeaders(response.Headers);

                // Phát sinh Exception nếu mã trạng thái trả về là lỗi
                response.EnsureSuccessStatusCode();

                // Đọc nội dung content trả về - ĐỌC CHUỖI NỘI DUNG
                string htmltext = WebUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
                var info = Regex.Match(htmltext, @"(?=<div class=""book-information cf"").*?(?<=<div class=""content-nav-wrap cf"">)", RegexOptions.Singleline);

                var book = new BookViewModel();

                book.Intro = Regex.Match(htmltext, @"(?<=<div class=""book-intro"">).*?(?=<\/div>)", RegexOptions.Singleline).Value.ToString().Trim();

                book.Name = Regex.Match(info.Value, @"(?<=<h1>).*?(?=<\/h1>)", RegexOptions.Singleline).Value.ToString();

                string bookAuthor = Regex.Match(info.Value, @"(?<=class=""blue"">).*?(?=<\/a>)").Value.ToString();

                book.Cover = Regex.Match(info.Value, @"(?<=src="").*?(?="")").Value.ToString();

                book.Status = Regex.Match(info.Value, @"(?<=<span class=""blue"">).*?(?=<\/span>)").Value.ToString();

                string bookCategoryTemp = Regex.Match(info.Value, @"(?<=the-loai).*?(?<=<\/a>)", RegexOptions.Singleline).Value.ToString();
                string bookCategory = Regex.Match(bookCategoryTemp, @"(?<="">).*?(?=<\/a>)", RegexOptions.Singleline).Value.ToString();

                book.Like = int.Parse(Regex.Match(info.Value, @"(?<=ULtwOOTH-like"">).*?(?= <\/span>)").Value.ToString());

                book.View = int.Parse(Regex.Match(info.Value, @"(?<=ULtwOOTH-view"">).*?(?=<\/span>)").Value.ToString());

                book.Key = Regex.Match(info.Value, @"(?<=likeStory\(').*?(?=')").Value.ToString();

                response = httpClient.GetAsync("https://truyen.tangthuvien.vn/story/chapters?story_id=" + book.Key).Result;
                string htmlList = WebUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
                var chapterList = Regex.Matches(htmlList, @"(?=<li).*?(?=<\/li>)", RegexOptions.Singleline);

                book.ChapterTotal = chapterList.Count;

                var author = _mapper.Map<AuthorViewModel>(_authorRepository.GetByName(bookAuthor));
                if (author == null)
                {
                    var registerAuthorCommand = _mapper.Map<RegisterNewAuthorCommand>(new AuthorViewModel(bookAuthor));
                    Bus.SendCommand(registerAuthorCommand);
                }
                book.AuthorId = _mapper.Map<AuthorViewModel>(_authorRepository.GetByName(bookAuthor)).Id;

                var category = _mapper.Map<CategoryViewModel>(_categoryRepository.GetByName(bookCategory));
                if (category == null)
                {
                    var registerCategoryCommand = _mapper.Map<RegisterNewCategoryCommand>(new CategoryViewModel(bookCategory));
                    Bus.SendCommand(registerCategoryCommand);
                }
                book.CategoryId = _mapper.Map<CategoryViewModel>(_categoryRepository.GetByName(bookCategory)).Id;

                var bookInDatabase = _mapper.Map<BookViewModel>(_bookRepository.GetByKey(book.Key));
                if (bookInDatabase == null)
                {
                    var registerBookCommand = _mapper.Map<RegisterNewBookCommand>(book);
                    Bus.SendCommand(registerBookCommand);
                    bookInDatabase = _mapper.Map<BookViewModel>(_bookRepository.GetByKey(book.Key));
                }
                else
                {
                    // TODO: Update
                }

                foreach (var chapter in chapterList)
                {
                    var chapterViewModel = new ChapterViewModel();

                    chapterViewModel.BookId = bookInDatabase.Id;

                    chapterViewModel.Order = int.Parse(Regex.Match(chapter.ToString(), @"(?<=title="").*?(?="" ng-chap)").Value.ToString());
                    chapterViewModel.Name = Regex.Match(chapter.ToString(), @"(?<=chapter-text"">).*?(?=<\/span>)").Value.ToString();
                    chapterViewModel.Url = Regex.Match(chapter.ToString(), @"(?<=href="" ).*?(?= "")").Value.ToString();

                    var chapterInDatabase = _mapper.Map<ChapterViewModel>(_chapterRepository.GetChapterByBookIdAndOrder(chapterViewModel.BookId, chapterViewModel.Order));
                    if (chapterInDatabase == null)
                    {
                        //response = httpClient.GetAsync(chapterViewModel.Url).Result;
                        //string htmlChapter = WebUtility.HtmlDecode(response.Content.ReadAsStringAsync().Result);
                        //string chapterContentTemp = Regex.Match(htmlChapter, @"(?=box-chap box-chap).*?(?<=<\/div>)", RegexOptions.Singleline).Value.ToString();
                        //chapterViewModel.Content = Regex.Match(chapterContentTemp, @"(?<=>).*?(?=<\/div>)", RegexOptions.Singleline).Value.ToString();
                        chapterViewModel.Content = "";
                        var registerChapterCommand = _mapper.Map<RegisterNewChapterCommand>(chapterViewModel);
                        Bus.SendCommand(registerChapterCommand);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public BookResponse GetById(Guid id)
        {
            var book = _bookRepository.GetById(id);

            if (book == null) return null;

            book.Author = _authorRepository.GetById(book.AuthorId);
            book.Category = _categoryRepository.GetById(book.CategoryId);
            return _mapper.Map<BookResponse>(book);
        }

        public IEnumerable<BookResponse> GetAll()
        {
            return _bookRepository.GetAll().ProjectTo<BookResponse>(_mapper.ConfigurationProvider);
        }

        public RepositoryResponse<BookResponse> GetAll(int skip, int take, string query)
        {
            var response = _bookRepository.GetAll(new BookFilterPaginatedSpecification(skip, take, query));

            var books = response.Queryable.ProjectTo<BookResponse>(_mapper.ConfigurationProvider);

            return new RepositoryResponse<BookResponse>(books, response.TotalRecords);
            //return _bookRepository.GetAll(new BookFilterPaginatedSpecification(skip, take))
            //    .ProjectTo<BookViewModel>(_mapper.ConfigurationProvider);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return _configuration["ApplicationUrl"] + "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public bool Create(BookCreateRequest bookCreateRequest)
        {
            var cover = "";
            if (bookCreateRequest.ThumbnailImage != null)
            {
                cover = this.SaveFile(bookCreateRequest.ThumbnailImage).Result;
            }

            Guid authorId;
            var author = _mapper.Map<AuthorViewModel>(_authorRepository.GetByName(bookCreateRequest.AuthorName));
            if (author == null)
            {
                var registerAuthorCommand = _mapper.Map<RegisterNewAuthorCommand>(new AuthorViewModel(bookCreateRequest.AuthorName));
                Bus.SendCommand(registerAuthorCommand);
                authorId = _mapper.Map<AuthorViewModel>(_authorRepository.GetByName(bookCreateRequest.AuthorName)).Id;
            }
            else
            {
                authorId = author.Id;
            }

            var bookViewModel = new BookViewModel
            {
                Name = bookCreateRequest.Name,
                Status = bookCreateRequest.Status,
                Intro = bookCreateRequest.Intro,
                CategoryId = bookCreateRequest.CategoryId,
                AuthorId = authorId,
                Key = Guid.NewGuid().ToString(),
                View = 0,
                Like = 0,
                ChapterTotal = 0,
                Cover = cover
            };




            var registerCommand = _mapper.Map<RegisterNewBookCommand>(bookViewModel);
            
            return Bus.SendCommand(registerCommand).IsCompleted;
        }

        public bool Update(BookViewModel bookViewModel)
        {
            var updateCommand = _mapper.Map<UpdateBookCommand>(bookViewModel);
            return Bus.SendCommand(updateCommand).IsCompleted;
        }

        public bool Remove(Guid id)
        {
            var removeCommand = new RemoveBookCommand(id);
            return Bus.SendCommand(removeCommand).IsCompleted;
        }
    }
}
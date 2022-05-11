using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Responses;
using Kaidao.Domain.Core.Bus;
using Kaidao.Domain.Interfaces;
using Kaidao.Domain.Specifications;
using Kaidao.Infra.Data.Repository.EventSourcing;

namespace Kaidao.Application.AppServices
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IMapper _mapper;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            ICategoryRepository categoryRepository
            )
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _categoryRepository = categoryRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<CategoryResponse> GetAll()
        {
            return _categoryRepository.GetAll().ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider); ;
        }
    }
}
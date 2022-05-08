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
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        private readonly IUserRepository _userRepository;

        public UserAppService(
            IMapper mapper,
            IEventStoreRepository eventStoreRepository,
            IMediatorHandler bus,
            IUserRepository userRepository
        )
        {
            _mapper = mapper;
            _eventStoreRepository = eventStoreRepository;
            Bus = bus;
            _userRepository = userRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public RepositoryResponse<UserResponse> GetAll(int skip, int take, string query)
        {
            var response = _userRepository.GetAll(new UserFilterPaginatedSpecification(skip, take, query));

            var users = response.Queryable.ProjectTo<UserResponse>(_mapper.ConfigurationProvider);

            return new RepositoryResponse<UserResponse>(users, response.TotalRecords);
        }

        public IEnumerable<UserResponse> GetAll()
        {
            return _userRepository.GetAll().ProjectTo<UserResponse>(_mapper.ConfigurationProvider);
        }
    }
}
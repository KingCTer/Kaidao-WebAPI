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
    public class RoleAppService : IRoleAppService
    {
        private readonly IMapper _mapper;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;
        private readonly IRoleRepository _roleRepository;
        private readonly IFunctionRepository _functionRepository;
        private readonly ICommandInFunctionRepository _commandInFunctionRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RoleAppService(
            IMapper mapper,
            IMediatorHandler bus,
            IEventStoreRepository eventStoreRepository,
            IRoleRepository roleRepository,
            IFunctionRepository functionRepository,
            ICommandInFunctionRepository commandInFunctionRepository,
            IPermissionRepository permissionRepository
            )
        {
            _mapper = mapper;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
            _roleRepository = roleRepository;
            _functionRepository = functionRepository;
            _commandInFunctionRepository = commandInFunctionRepository;
            _permissionRepository = permissionRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public RoleViewModel GetById(string roleId)
        {
            var role = _roleRepository.GetById(roleId);

            return _mapper.Map<RoleViewModel>(role);
        }

        public RoleViewModel GetByName(string roleName)
        {
            var role = _roleRepository.GetByName(roleName);

            return _mapper.Map<RoleViewModel>(role);
        }

        public IEnumerable<RoleViewModel> GetAll()
        {
            return _roleRepository.GetAll().AsNoTracking().ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider);
        }

        public List<RoleWithPermissionViewModel> GetAllWithPermission()
        {
            var list = new List<RoleWithPermissionViewModel>();

            

            var functionList = _functionRepository.GetAll().ToList();

            var roleList = _roleRepository.GetAll().AsNoTracking().ToList();
            roleList.ForEach(role => {

                var result = new RoleWithPermissionViewModel();
                result.Role = _mapper.Map<RoleViewModel>(role);

                var listCommandInFunction = new List<CommandInFunctionList>();

                var rolePermissions = _permissionRepository.GetByRoleId(role.Id);

                functionList.ForEach(function =>
                {
                    var commandInFunctionList = new CommandInFunctionList();

                    commandInFunctionList.Function = _mapper.Map<FunctionViewModel>(function);

                    var permissions = new List<Tuple<CommandInFunctionViewModel, bool>>();

                    var commandInFunctions = _commandInFunctionRepository.GetByFunctionId(function.Id).ToList();
                    commandInFunctions.ForEach( cif =>
                    {
                        var isEnable = rolePermissions.FirstOrDefault(a => a.FunctionId == cif.FunctionId && a.CommandId == cif.CommandId) != null;
                        permissions.Add(new Tuple<CommandInFunctionViewModel, bool>(_mapper.Map<CommandInFunctionViewModel>(cif), isEnable));
                    });

                    commandInFunctionList.Permissions = permissions;

                    listCommandInFunction.Add(commandInFunctionList);
                });

                result.CommandInFunction = listCommandInFunction;

                list.Add(result);
            });

            return list;
        }

        public UserRolePermissionViewModel GetRoleWithPermission(string roleName)
        {
            var result = new UserRolePermissionViewModel();

            var functionList = _functionRepository.GetAll().ToList();

            var role = _roleRepository.GetByName(roleName);
            
            result.CurrentRole = _mapper.Map<RoleViewModel>(role);

            var listCommandInFunction = new List<CustomUserPermissions>();

            var rolePermissions = _permissionRepository.GetByRoleId(role.Id);

            functionList.ForEach(function =>
            {
                var commandInFunctionList = new CustomUserPermissions();

                commandInFunctionList.Function = _mapper.Map<FunctionViewModel>(function);

                var permissions = new List<Tuple<CommandInFunctionViewModel, bool, string>>();

                var commandInFunctions = _commandInFunctionRepository.GetByFunctionId(function.Id).ToList();
                commandInFunctions.ForEach(cif =>
                {
                    var isEnable = rolePermissions.FirstOrDefault(a => a.FunctionId == cif.FunctionId && a.CommandId == cif.CommandId) != null;

                    var asignassignBy = "Default";
                    if (isEnable)
                    {
                        asignassignBy = "Role";
                    }

                    permissions.Add(new Tuple<CommandInFunctionViewModel, bool, string>(_mapper.Map<CommandInFunctionViewModel>(cif), isEnable, asignassignBy));
                });

                commandInFunctionList.Permissions = permissions;

                listCommandInFunction.Add(commandInFunctionList);
            });

            result.UserPermission = listCommandInFunction;


            return result;
        }
    }
}
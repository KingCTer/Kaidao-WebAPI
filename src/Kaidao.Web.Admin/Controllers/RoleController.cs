using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.Interfaces;
using Kaidao.Web.Admin.ViewModels;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleAppService _roleAppService;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public RoleController(
            IBaseApiClient baseApiClient,
            IRoleAppService roleAppService,
            IPermissionRepository permissionRepository,
            IUserPermissionRepository userPermissionRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository
        ) : base(baseApiClient)
        {
            _roleAppService = roleAppService;
            _permissionRepository = permissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new RoleGroupViewModel();
            viewModel.Index = _roleAppService.GetAllWithPermission();
            return View(viewModel);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult Edit([FromForm] RoleGroupViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var roleId = viewModel.RoleUpdateId;
            var permissionUpdateList = viewModel.Permission;


            if (viewModel.RoleCurrentName != viewModel.RoleUpdateName)
            {
                var roleUpdate = _roleRepository.GetById(roleId);
                roleUpdate.Name = viewModel.RoleUpdateName;
                roleUpdate.NormalizedName = viewModel.RoleUpdateName.ToUpper();
                _roleRepository.Update(roleUpdate);
                _roleRepository.SaveChanges();
            }
            permissionUpdateList.ForEach(p =>
            {
                var baseEnable = _permissionRepository.GetPermission(roleId, p.FunctionId, p.CommandId);
                if (p.IsEnable)
                {
                    if (baseEnable == null)
                    {
                        _permissionRepository.Add(roleId, p.FunctionId, p.CommandId);
                    }
                } else
                {
                    if (baseEnable != null)
                    {
                        _permissionRepository.Remove(baseEnable);
                    }
                }
                
            });
            _permissionRepository.SaveChanges();



            return RedirectToAction("Index");
        }


        public IActionResult UserPermission(string userId)
        {
            var userPermissions = _userPermissionRepository.GetByUserId(userId);

            if (userPermissions == null)
            {
                return RedirectToAction("Index", "User");
            }

            var userRole = _userRoleRepository.GetByUserId(userId).RoleId;
            var rolePermission = _roleAppService.GetRoleWithPermission(userRole);
            rolePermission.UserId = userId;

            for (int i = 0; i < rolePermission.UserPermission.Count; i++)
            {
                var rp = rolePermission.UserPermission[i];
                for (int j = 0; j < rp.Permissions.Count; j++)
                {
                    var p = rp.Permissions[j];
                    var userPermission = userPermissions.FirstOrDefault(a => a.FunctionId == p.Item1.FunctionId && a.CommandId == p.Item1.CommandId);
                    if (userPermission != null)
                    {
                        var asignassignBy = "Default";
                        if (userPermission.Allow)
                        {
                            if (p.Item2)
                            {
                                asignassignBy = "RoleUser";
                            } else
                            {
                                asignassignBy = "User";
                            }
                        } else
                        {
                            if (p.Item2)
                            {
                                asignassignBy = "User";
                            }
                            else
                            {
                                asignassignBy = "RoleUser";
                            }
                        }
                        rp.Permissions[j] = new Tuple<CommandInFunctionViewModel, bool, string>(p.Item1, userPermission.Allow, asignassignBy);
                    }
                }
            }

            return View(rolePermission);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult EditUserPermission([FromForm] UserRolePermissionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("UserPermission", "Role", new { userId = viewModel .UserId});

            var updateList = viewModel.Permission;
            updateList.ForEach(up =>
            {
                var baseUserPermission = _userPermissionRepository.GetPermission(viewModel.UserId, up.FunctionId, up.CommandId);
                if (baseUserPermission != null)
                {
                    if (up.IsFactory)
                    {
                        _userPermissionRepository.Remove(baseUserPermission);
                    } else
                    {
                        if (baseUserPermission.Allow != up.IsEnable)
                        {
                            _userPermissionRepository.Update(viewModel.UserId, up.FunctionId, up.CommandId, up.IsEnable);
                        }
                    }
                } else
                {
                    if (!up.IsFactory)
                    {
                        _userPermissionRepository.Add(viewModel.UserId, up.FunctionId, up.CommandId, up.IsEnable);
                    }
                }
            });

            _userPermissionRepository.SaveChanges();

            return RedirectToAction("UserPermission", "Role", new { userId = viewModel.UserId });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult Add([FromForm] RoleGroupViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var roleName = viewModel.RoleUpdateName;
            if (_roleAppService.GetByName(roleName) != null)
            {
                return RedirectToAction("Index");
            }

            _roleRepository.Add(roleName);
            _roleRepository.SaveChanges();

            var permissionUpdateList = viewModel.Permission;

            var roleId = roleName;
            permissionUpdateList.ForEach(p =>
            {
                if (p.IsEnable)
                {
                    _permissionRepository.Add(roleId, p.FunctionId, p.CommandId);
                }

            });
            _permissionRepository.SaveChanges();



            return RedirectToAction("Index");
        }

    }
}

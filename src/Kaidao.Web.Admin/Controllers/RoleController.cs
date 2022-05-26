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
        private readonly IUnitOfWork _uow;
        private readonly IRoleAppService _roleAppService;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;

        public RoleController(
            IBaseApiClient baseApiClient,
            IUnitOfWork uow,
            IRoleAppService roleAppService,
            IPermissionRepository permissionRepository,
            IRoleRepository roleRepository
        ) : base(baseApiClient)
        {
            _roleAppService = roleAppService;
            _permissionRepository = permissionRepository;
            _uow = uow;
            _roleRepository = roleRepository;
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


    }
}

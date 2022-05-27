﻿using AutoMapper;
using Kaidao.Application.AppServices.Interfaces;
using Kaidao.Application.Common;
using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.Constants;
using Kaidao.Web.Admin.Authorization;
using Kaidao.Web.Share.ApiClient.Interfaces;
using Kaidao.Web.Share.Query;
using Microsoft.AspNetCore.Mvc;

namespace Kaidao.Web.Admin.Controllers
{
    public class BookController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IBookAppService _bookAppService;
        private readonly ICategoryAppService _categoryAppService;

        public BookController(
            IBaseApiClient baseApiClient,
            IConfiguration configuration,
            IBookAppService bookAppService,
            ICategoryAppService categoryAppService
            ) : base(baseApiClient)
        {
            _configuration = configuration;
            _bookAppService = bookAppService;
            _categoryAppService = categoryAppService;
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.BOOK, CommandCode.LIST)]
        public IActionResult Index(PaginationFilter filter, string queryType = "")
        {
            var query = "";
            if (queryType != "")
            {
                query = queryType + filter.Query;
            }

            var data = _bookAppService.GetAll((filter.PageNumber - 1) * filter.PageSize, filter.PageSize, query);

            var pagedResult = new PagedResult<BookResponse>()
            {
                TotalRecords = data.TotalRecords,
                Items = data.ViewModel.ToList(),
                PageSize = filter.PageSize,
                PageIndex = filter.PageNumber
            };

            ViewBag.Keyword = filter.Query;
            ViewBag.PortalAddress = _configuration["PortalAddress"];

            return View(pagedResult);
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.BOOK, CommandCode.CREATE)]
        public IActionResult Create()
        {
            var viewModel = new BookCreateRequest();
            viewModel.CategoryList = _categoryAppService.GetAll().ToList();
            return View(viewModel);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ClaimRequirement(FunctionCode.BOOK, CommandCode.CREATE)]
        public IActionResult Create([FromForm] BookCreateRequest request)
        {
            request.CategoryList = _categoryAppService.GetAll().ToList();

            if (!ModelState.IsValid)
                return View(request);

            if (_bookAppService.Create(request))
            {
                return RedirectToAction("Index");
            }


            return View(request);

        }

        [HttpPost]
        [ClaimRequirement(FunctionCode.BOOK, CommandCode.DELETE)]
        public IActionResult Delete(Guid id)
        {
            _bookAppService.Remove(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.BOOK, CommandCode.UPDATE)]
        public IActionResult Edit(Guid id)
        {
            var viewModel = new BookUpdateRequest();
            viewModel.CategoryList = _categoryAppService.GetAll().ToList();
            viewModel.Book = _bookAppService.GetById(id);

            return View(viewModel);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ClaimRequirement(FunctionCode.BOOK, CommandCode.UPDATE)]
        public IActionResult Edit([FromForm] BookUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Edit", new {id = request.Book.Id});

            var result = _bookAppService.Update(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            return View(request);
        }

        [HttpGet]
        [ClaimRequirement(FunctionCode.BOOK, CommandCode.CRAWL)]
        public IActionResult Crawl()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ClaimRequirement(FunctionCode.BOOK, CommandCode.CRAWL)]
        public IActionResult Crawl([FromForm] string url)
        {
            if (url == "") return RedirectToAction("Crawl");

            _bookAppService.Crawl(url);


            return RedirectToAction("Index");

        }
    }
}

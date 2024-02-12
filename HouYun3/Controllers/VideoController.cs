﻿using HouYun3.IRepositories;
using HouYun3.ViewModels.forVideo;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;

        public VideoController(IVideoRepository videoRepository, ICategoryRepository categoryRepository)
        {
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(string category)
        {
            var model = new VideoViewModel();

            if (!string.IsNullOrEmpty(category))
            {
                model.Videos = await _videoRepository.GetVideosByCategory(category);
            }
            else
            {
                model.Videos = await _videoRepository.GetAllVideos();
            }

            model.Categories = await _categoryRepository.GetAllCategories();

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var video = await _videoRepository.GetVideoById(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }
    }
}
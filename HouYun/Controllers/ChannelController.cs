﻿using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forVideo;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HouYun.Controllers
{
    public class ChannelController : Controller
    {
        private const long MaxVideoSize = 50L * 1024 * 1024 * 1024;
        private const long MaxPosterSize = 10 * 1024 * 1024;
        private readonly IChannelRepository _channelRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IVideoRepository _videoRepository;

        public ChannelController(IChannelRepository channelRepository, ICategoryRepository categoryRepository, IVideoRepository videoRepository)
        {
            _channelRepository = channelRepository;
            _categoryRepository = categoryRepository;
            _videoRepository = videoRepository;
        }

        [Authorize(Roles = "Admin,User,Author")]
        [HttpGet("Channel/{channelName?}")]
        public async Task<IActionResult> Index(string channelName)
        {
            var channel = await _channelRepository.GetChannelByName(channelName);

            if (channel == null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                channel = await _channelRepository.GetChannelByUserId(userId);

                return RedirectToAction("Index", new { channelName = channel.Name });
            }

            return View(channel);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpGet("Channel/Add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddVideoViewModel
            {
                Categories = await _categoryRepository.GetAllCategories(),
            };

            return PartialView("_AddVideoPartial", model);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(51L * 1024 * 1024 * 1024)]
        public async Task<IActionResult> Add(AddVideoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryRepository.GetAllCategories();
                return PartialView("_AddVideoPartial", model);
            }

            if (!ValidateVideoFile(model) || !ValidatePosterFile(model))
            {
                model.Categories = await _categoryRepository.GetAllCategories();
                return PartialView("_AddVideoPartial", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var video = new Video
            {
                Title = model.Title,
                Description = model.Description,
                CategoryId = (Guid)model.CategoryId,
                ChannelId = channelId
            };

            await _videoRepository.AddVideo(video, model.VideoFile, model.PosterFile);
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpGet("Channel/Delete")]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var videos = await _videoRepository.GetVideosByChannelId(channelId);
            return PartialView("_DeleteVideoPartial", videos);
        }

        [Authorize(Roles = "Admin,Author")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _videoRepository.DeleteVideo(id);
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin,User,Author")]
        [HttpGet("Channel/Update")]
        public async Task<IActionResult> Update()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channel = await _channelRepository.GetChannelByUserId(userId);

            var model = new UpdateChannelViewModel
            {
                ChannelName = channel.Name,
                Description = channel.Description
            };

            return PartialView("_ChannelUpdatePartial", model);
        }

        [Authorize(Roles = "Admin,User,Author")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateChannelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ChannelUpdatePartial", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channel = await _channelRepository.GetChannelByUserId(userId);

            if (model.ChannelName == channel.Name
                && model.Description == channel.Description)
            {
                return Json(new { success = true });
            }

            channel.Name = model.ChannelName ?? channel.Name;
            channel.Description = model.Description ?? channel.Description;

            try
            {
                await _channelRepository.UpdateChannel(channel);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("_ChannelUpdatePartial", model);
            }
        }

        [Authorize(Roles = "Admin,User,Author")]
        [HttpGet("Channel/UpdateBanner")]
        public IActionResult UpdateBanner()
        {
            return PartialView("_UpdateBannerPartial");
        }

        [Authorize(Roles = "Admin,User,Author")]
        [HttpPost]
        public async Task<IActionResult> UpdateBanner(UpdateBannerChannelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_UpdateBannerPartial", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channel = await _channelRepository.GetChannelByUserId(userId);

            if (!ValidateBannerFile(model))
            {
                return PartialView("_AddVideoPartial", model);
            }

            await _channelRepository.UpdateBannerChannel(channel, model.BannerFile);
            return Json(new { success = true });
        }

        private bool ValidateVideoFile(AddVideoViewModel model)
        {
            if (model.VideoFile.Length > MaxVideoSize)
            {
                ModelState.AddModelError("VideoFile", "Размер файла видео превышает максимально допустимый");
                return false;
            }

            var allowedVideoExtensions = new[] { ".mp4" };
            var videoExtension = Path.GetExtension(model.VideoFile.FileName).ToLower();
            if (!allowedVideoExtensions.Contains(videoExtension))
            {
                ModelState.AddModelError("VideoFile", "Формат видео должен быть MP4");
                return false;
            }

            return true;
        }

        private bool ValidatePosterFile(AddVideoViewModel model)
        {
            if (model.PosterFile != null)
            {
                if (model.PosterFile.Length > MaxPosterSize)
                {
                    ModelState.AddModelError("PosterFile", "Размер файла превышает 10МБ");
                    return false;
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(model.PosterFile.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("PosterFile", "Недопустимое расширение файла");
                    return false;
                }
            }

            return true;
        }

        private bool ValidateBannerFile(UpdateBannerChannelViewModel model)
        {
            if (model.BannerFile != null)
            {
                if (model.BannerFile.Length > MaxPosterSize)
                {
                    ModelState.AddModelError("BannerFile", "Размер файла превышает 10МБ");
                    return false;
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(model.BannerFile.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("BannerFile", "Недопустимое расширение файла");
                    return false;
                }
            }

            return true;
        }
    }
}

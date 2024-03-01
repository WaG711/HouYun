using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forVideo;
using HouYun.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ChannelController : Controller
    {
        private const long MaxVideoSize = 50L * 1024 * 1024 * 1024;
        private const long MaxPosterSize = 5 * 1024 * 1024;
        private readonly IChannelRepository _channelRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IVideoRepository _videoRepository;

        public ChannelController(IChannelRepository channelRepository, ICategoryRepository categoryRepository, IVideoRepository videoRepository)
        {
            _channelRepository = channelRepository;
            _categoryRepository = categoryRepository;
            _videoRepository = videoRepository;
        }

        public async Task<IActionResult> Index(string channelName)
        {
            var channel = await _channelRepository.GetChannelByName(channelName);

            if (channel == null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                channel = await _channelRepository.GetChannelByUserId(userId);
            }

            return View(channel);
        }

        public async Task<IActionResult> Add()
        {
            var model = new AddVideoViewModel
            {
                Categories = await _categoryRepository.GetAllCategories(),
            };

            return PartialView("_AddVideoPartial", model);
        }

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

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var videos = await _videoRepository.GetVideosByChannelId(channelId);
            return PartialView("_DeleteVideoPartial", videos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _videoRepository.DeleteVideo(id);
            return Json(new { success = true });
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Update(UpdateChannelViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channel = await _channelRepository.GetChannelByUserId(userId);

            if (model.ChannelName == channel.Name && model.Description == channel.Description)
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

        private bool ValidateVideoFile(AddVideoViewModel model)
        {
            if (model.VideoFile.Length > MaxVideoSize)
            {
                ModelState.AddModelError("VideoFile", "Размер файла видео превышает максимально допустимый.");
                return false;
            }

            var allowedVideoExtensions = new[] { ".mp4" };
            var videoExtension = Path.GetExtension(model.VideoFile.FileName).ToLower();
            if (!allowedVideoExtensions.Contains(videoExtension))
            {
                ModelState.AddModelError("VideoFile", "Формат видео должен быть MP4.");
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
                    ModelState.AddModelError("PosterFile", "Размер файла постера превышает максимально допустимый.");
                    return false;
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(model.PosterFile.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("PosterFile", "Недопустимое расширение файла постера.");
                    return false;
                }
            }

            return true;
        }
    }
}

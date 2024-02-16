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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddVideoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = new AddVideoViewModel
                {
                    Categories = await _categoryRepository.GetAllCategories(),
                };

                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var video = new Video
            {
                Title = model.Title,
                Description = model.Description,
                CategoryId = model.CategoryId,
                ChannelId = channelId
            };

            await _videoRepository.AddVideo(video, model.VideoFile, model.PosterFile);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var videos = await _videoRepository.GetVideosByChannelId(channelId);
            return View(videos);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _videoRepository.DeleteVideo(id);
            return RedirectToAction("Delete");
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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateChannelViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channel = await _channelRepository.GetChannelByUserId(userId);

            if (model.ChannelName == channel.Name && model.Description == channel.Description)
            {
                return RedirectToAction("Channel");
            }

            channel.Name = model.ChannelName ?? channel.Name;
            channel.Description = model.Description ?? channel.Description;

            try
            {
                await _channelRepository.UpdateChannel(channel);
                return RedirectToAction("Channel");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}

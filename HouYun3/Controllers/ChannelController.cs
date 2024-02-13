﻿using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.ViewModels.forVideo;
using HouYun3.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class ChannelController : Controller
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ChannelController(IChannelRepository channelRepository, IVideoRepository videoRepository,
            ICategoryRepository categoryRepository)
        {
            _channelRepository = channelRepository;
            _videoRepository = videoRepository;
            _categoryRepository = categoryRepository;
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

            if(model.ChannelName == channel.Name && model.Description == channel.Description) 
            {
                return RedirectToAction("Index");
            }

            if(model.ChannelName != null) 
            {
                channel.Name = model.ChannelName;
            }

            if (model.Description != null)
            {
                channel.Description = model.Description;
            }

            try
            {
                await _channelRepository.UpdateChannel(channel);
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}

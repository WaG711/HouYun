using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    public class ViewController : Controller
    {
        private readonly IViewRepository _viewRepository;

        public ViewController(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        [HttpPost]
        [Route("ViewController/AddView")]
        public async Task<IActionResult> AddView(Guid videoId, string userId)
        {
            var existingView = await _viewRepository.GetViewByVideoAndUser(videoId, userId);
            if (existingView != null)
            {
                return Ok();
            }

            var view = new View
            {
                VideoId = videoId,
                UserId = userId,
            };

            await _viewRepository.AddView(view);

            return Ok();
        }
    }
}

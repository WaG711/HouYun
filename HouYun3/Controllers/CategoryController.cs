using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allCategories = await _categoryRepository.GetAllCategories();
            return View(allCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.AddCategory(category);
                return RedirectToAction("Index", "Video");
            }

            return View(category);
        }

        public async Task<IActionResult> Delete(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int categoryId)
        {
            await _categoryRepository.DeleteCategory(categoryId);
            return RedirectToAction("Index", "Video");
        }
    }
}

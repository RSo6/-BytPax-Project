// using Microsoft.AspNetCore.Mvc;
// using BytPax.Models;
// using BytPax.Repositories;
//
// [ApiController]
// [Route("api/[controller]")]
// public class PublicArticlesController : ControllerBase
// {
//     private readonly Repository<Article> _repository;
//     private readonly Repository<Category> _categoryRepository;
//
//     public PublicArticlesController(Repository<Article> repository, Repository<Category> categoryRepository)
//     {
//         _repository = repository;
//         _categoryRepository = categoryRepository;
//     }
//
//     [HttpGet("GetArticles")]
//     public IActionResult GetArticles()
//     {
//         var articles = _repository.GetAll()
//             .Select(a => new
//             {
//                 a.Id,
//                 a.Topic,
//                 a.BodyText,
//                 a.ImagePath,
//                 a.CategoryId,
//                 CategoryName = _categoryRepository.GetById(a.CategoryId)?.Name
//             });
//
//         return Ok(articles);
//     }
// }

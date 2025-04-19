// using BytPax.Models;
// using BytPax.Instructions;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Text.Json;
//
// namespace BytPax.Repositories
// {
// public class ArticleRepository : IDataStorage<Article>
// {
// private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "articles.json");
//
// public List<Article> GetAll()
// {
//     if (!File.Exists(_filePath))
//     {
//         return new List<Article>();
//     }
//
//     var json = File.ReadAllText(_filePath);
//     return JsonConvert.DeserializeObject<List<Article>>(json) ?? new List<Article>();
// }
//
// public Article GetById(long id)
// {
//     var articles = GetAll();
//     return articles.FirstOrDefault(x => x.Id == id);
// }
//
// public void Add(Article entity)
// {
//     var articles = GetAll();
//     articles.Add(entity);
//
//     var json = JsonConvert.SerializeObject(articles, Formatting.Indented);
//     File.WriteAllText(_filePath, json);
// }
//
// public void Update(Article entity)
// {
//     var articles = GetAll();
//     var index = articles.FindIndex(x => x.Id == entity.Id);
//     if (index != -1)
//     {
//         articles[index] = entity;
//
//         var json = JsonConvert.SerializeObject(articles, Formatting.Indented);
//         File.WriteAllText(_filePath, json);
//     }
// }
//
// public void Delete(long id)
// {
//     var articles = GetAll();
//     var article = articles.FirstOrDefault(x => x.Id == id);
//     if (article != null)
//     {
//         articles.Remove(article);
//
//         var json = JsonConvert.SerializeObject(articles, Formatting.Indented);
//         File.WriteAllText(_filePath, json);
//     }
// }
// }
//
// }
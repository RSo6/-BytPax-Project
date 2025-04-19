using System.Text.Json;
using BytPax.Instructions;
using BytPax.Models.core;

namespace BytPax.Data;
    public class JsonStorage<T> : IDataStorage<T> where T : BaseEntity
    {
        private readonly string _filePath;
        private readonly ILogger<JsonStorage<T>> _logger;
        private List<T> _items;

        public JsonStorage(string filePath, ILogger<JsonStorage<T>> logger)
        {
            _filePath = filePath;
            _logger = logger;
            _items = LoadFromFile();
        }

        private List<T> LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }
            
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
        
        public List<T> GetAll()
        {
            // if (!File.Exists(_filePath))
            // {
            //     return new List<T>();
            // }
            //
            // var json = File.ReadAllText(_filePath);
            // return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            return LoadFromFile();
        }

        public T? GetById(int id)
        {
            var _items = GetAll();
            return _items.Find(item => item.Id == id);
        }

        public void Add(T entity)
        {
            _items = LoadFromFile();
            _items.Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            _items = LoadFromFile();
            var index = _items.FindIndex(e => e.Id == entity.Id);
            if (index != -1)
            {
                _logger.LogInformation("Update: Оновлення статті з ID = {Id}", entity.Id);

                _items[index] = entity;
                Save();
                _logger.LogInformation("Update: Стаття з ID = {Id} успішно оновлена", entity.Id);
             
            }
            else
            {
                _logger.LogWarning("Update: Статтю з ID = {Id} не знайдено для оновлення", entity.Id);
            }
    }

        public void Delete(int id)
        { 
            _items = LoadFromFile();
            var entity = _items.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                _items.Remove(entity);
                Save();
                _logger.LogInformation("Delete: Статтю з ID = {Id} видалено", id);
            }
            else
            {
                _logger.LogWarning("Delete: Статтю з ID = {Id} не знайдено у файлі", id);
            }
        }

        public void Save()
        { 
            _logger.LogInformation("Save: Збереження {Count} статей у файл {Path}", _items.Count, _filePath);
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_filePath, json);
        }
    }

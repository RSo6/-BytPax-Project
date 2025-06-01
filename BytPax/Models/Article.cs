using System.Text.Json.Serialization;
using BytPax.Models.core;
using Microsoft.AspNetCore.Http;

namespace BytPax.Models
{
    public class Article : BaseEntity
    {
        public string Topic { get; set; }
        public string BodyText { get; set; }
        public int CategoryId { get; set; }
        public string? ImagePath { get; set; }

        public Article() { }

        public Article(string topic, string bodyText, int categoryId, string imagePath)
            : base(0)
        {
            Topic = topic;
            BodyText = bodyText;
            CategoryId = categoryId;
            ImagePath = imagePath;
        }
    }
}
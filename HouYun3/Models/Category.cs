﻿using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Длина 'Названия' не должна превышать 50 символов")]
        public string Name { get; set; }

        public ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}

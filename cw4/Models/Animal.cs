﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Cw4.Models
{
    public class Animal
    {
        [Required]
        public int IdAnimal { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [MaxLength(200)]
        public string Category { get; set; }
        [Required]
        [MaxLength(200)]
        public string Area { get; set; }
    }
}
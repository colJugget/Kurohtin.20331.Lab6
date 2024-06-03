using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurohtin.Domain.Entities
{
    public class Crepezh
    {
        public int Id { get; set; } // id крепежа
        public string Name { get; set; } // название крепежа
        public string Description { get; set; } // описание крепежа
        public int Gramms { get; set; } // кол. грамм веса единицицы
        public string? Image { get; set; } // путь к файлу изображения   

        // Навигационные свойства
        /// <summary>
        /// группа крепежа (например, болты, гайки и т.д.)
        /// </summary>
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

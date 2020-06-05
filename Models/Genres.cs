using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Theater
{
    public partial class Genres
    {
        public Genres()
        {
            PerformanceGenres = new HashSet<PerformanceGenres>();
        }

        public int GnId { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Назва жанру")]
        public string GnName { get; set; }

        public virtual ICollection<PerformanceGenres> PerformanceGenres { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Theater
{
    public partial class PerformanceGenres
    {
        public int Id { get; set; }
        [Display(Name = "Назва вистави")]
        public int? PrId { get; set; }
        [Display(Name = "Назва жанру")]
        public int? GnId { get; set; }
        [Display(Name = "Додатково")]
        public string Info { get; set; }

        public virtual Genres Gn { get; set; }
        public virtual Performance Pr { get; set; }
    }
}

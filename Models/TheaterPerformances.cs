using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Theater
{
    public partial class TheaterPerformances
    {
        public int Id { get; set; }
        [Display(Name = "Назва театру")]
        public int? ThId { get; set; }
        [Display(Name = "Назва вистави")]
        public int? PrId { get; set; }
        [Display(Name = "Інформація")]
        public string Info { get; set; }

        public virtual Performance Pr { get; set; }
        public virtual Theaters Th { get; set; }
    }
}

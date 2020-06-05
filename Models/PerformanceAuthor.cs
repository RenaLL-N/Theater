using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Theater
{
    public partial class PerformanceAuthor
    {
        public int Id { get; set; }
        [Display(Name = "Ім'я автора")]
        public int? AuId { get; set; }
        [Display(Name = "Назва вистави")]
        public int? PrId { get; set; }
        [Display(Name = "Інформациія")]
        public string Info { get; set; }

        public virtual Authors Au { get; set; }
        public virtual Performance Pr { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Theater
{
    public partial class Performance
    {
        public Performance()
        {
            PerformanceAuthor = new HashSet<PerformanceAuthor>();
            PerformanceGenres = new HashSet<PerformanceGenres>();
            TheaterPerformances = new HashSet<TheaterPerformances>();
        }

        public int PrId { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Назва вистави")]
        public string PrName { get; set; }
        [Display(Name = "Рік написання")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string PrYear { get; set; }
        [Display(Name = "Додаткова інформація")]
        public string PrInfo { get; set; }

        public virtual ICollection<PerformanceAuthor> PerformanceAuthor { get; set; }
        public virtual ICollection<PerformanceGenres> PerformanceGenres { get; set; }
        public virtual ICollection<TheaterPerformances> TheaterPerformances { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Theater
{
    public partial class Theaters
    {
        public Theaters()
        {
            TheaterPerformances = new HashSet<TheaterPerformances>();
        }

        public int ThId { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Назва театру")]
        public string ThName { get; set; }
        [Display(Name = "Місто")]
        public int? ThCt { get; set; }
        [Display(Name = "Веб-сторінка")]
        public string ThWebsite { get; set; }
        [Display(Name = "Додаткова інформація")]
        public string ThInfo { get; set; }

        [Display(Name = "Місто")]
        public virtual Cities ThCtNavigation { get; set; }
        public virtual ICollection<TheaterPerformances> TheaterPerformances { get; set; }
    }
}
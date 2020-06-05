using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Theater
{
    public partial class Cities
    {
        public Cities()
        {
            Authors = new HashSet<Authors>();
            Theaters = new HashSet<Theaters>();
        }

        public int CtId { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Назва міста")]
        public string CtName { get; set; }
        [Display(Name = "Назва країни")]
        public int? CoId { get; set; }

        [Display(Name = "Назва країни")]
        public virtual Countries Co { get; set; }
        public virtual ICollection<Authors> Authors { get; set; }
        public virtual ICollection<Theaters> Theaters { get; set; }
    }
}

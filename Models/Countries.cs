using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Theater
{
    public partial class Countries
    {
        public Countries()
        {
            Cities = new HashSet<Cities>();
        }

        public int CoId { get; set; }
        [Required(ErrorMessage = "Заповніть поле")]
        [Display(Name = "Назва країни")]
        public string CoName { get; set; }

        public virtual ICollection<Cities> Cities { get; set; }
    }
}

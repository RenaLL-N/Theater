using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Theater
{
    public partial class Authors
    {
        public Authors()
        {
            PerformanceAuthor = new HashSet<PerformanceAuthor>();
        }

        public int AuId { get; set; }
        [Display(Name = "Ім'я автора")]
        [Required(ErrorMessage = "Заповніть поле")]
        public string AuName { get; set; }
        [Display(Name = "День народження")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AuDateb { get; set; }
        [Display(Name = "День смерті")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AuDated { get; set; }
        [Display(Name = "Місто народження")]
        public int? AuCt { get; set; }
        [Display(Name = "Про автора")]
        public string AuInfo { get; set; }

        [Display(Name = "Місто народження")]
        public virtual Cities AuCtNavigation { get; set; }
        public virtual ICollection<PerformanceAuthor> PerformanceAuthor { get; set; }
    }
}

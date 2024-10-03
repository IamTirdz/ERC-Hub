using System.ComponentModel.DataAnnotations;

namespace ERC.Hub.Common.Enums
{
    public enum EmploymentType
    {
        [Display(Name = "Full-time")]
        FullTime = 1,

        [Display(Name = "Part-time")]
        PartTime = 2,

        [Display(Name = "Contractual")]
        Contractual = 3,

        [Display(Name = "Internship")]
        Internship = 4,
    }
}

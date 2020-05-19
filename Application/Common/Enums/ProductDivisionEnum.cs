using System.ComponentModel.DataAnnotations;

namespace Application.Common.Enums
{
    public enum ProductDivisionEnum
    {
        [Display(Name = "OS")]
        OS = 1,

        [Display(Name = "OP")]
        OP = 2,

        [Display(Name = "CIP")]
        CIP = 3
    }
}

using System.ComponentModel.DataAnnotations;

namespace Application.Common.Enums
{
    public enum ContactTypeEnum
    {
        [Display(Name = "Principal")]
        Main = 1,

        [Display(Name = "Técnico")]
        technician = 2,

        [Display(Name = "Compras")]
        sales = 3,

        [Display(Name = "Financeiro")]
        finacial = 4,

        [Display(Name = "Especial")]
        special = 5
    }
}

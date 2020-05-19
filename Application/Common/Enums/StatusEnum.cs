using System.ComponentModel.DataAnnotations;

namespace Application.Common.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "Ativo")]
        active = 1,

        [Display(Name = "Inativo")]
        inactive = 2,

        [Display(Name = "Bloqueado")]
        blocked = 3
    }
}

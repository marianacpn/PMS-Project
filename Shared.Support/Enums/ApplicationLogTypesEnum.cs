using System.ComponentModel.DataAnnotations;

namespace Shared.Support.Enums
{
    public enum ApplicationLogTypesEnum
    {
        [Display(Name = "Erro")]
        Error = 1,

        [Display(Name = "Aviso")]
        Warning = 2
    }
}

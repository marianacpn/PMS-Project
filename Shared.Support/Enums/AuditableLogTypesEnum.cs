using System.ComponentModel.DataAnnotations;

namespace Shared.Support.Enums
{
    public enum AuditableLogTypesEnum
    {
        [Display(Name = "Criar")]
        Create = 1,

        [Display(Name = "Editar")]
        Update = 2,

        [Display(Name = "Remover")]
        Delete = 3,

        [Display(Name = "Acesso")]
        Read = 4,

        [Display(Name = "Informação")]
        Info = 5
    }
}

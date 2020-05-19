using System.ComponentModel.DataAnnotations;

namespace Application.Common.Enums
{
    public enum ClientProfileEnum
    {
        [Display(Name = "Cliente Final")]
        FinalClient = 1,

        [Display(Name = "Revenda Tier 1")]
        ResaleTier1 = 2,

        [Display(Name = "Revenda Tier 2")]
        ResaleTier2 = 3,

        [Display(Name = "Distribuidor")]
        Distribuidor = 4
    }
}

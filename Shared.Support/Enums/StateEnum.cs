using System.ComponentModel.DataAnnotations;

namespace Shared.Support.Enums
{
    public enum StateEnum
    {
        [Display(Name = "Planned")]
        Planned = 1,

        [Display(Name = "In Progress")]
        InProgress = 2,

        [Display(Name = "Completed")]
        Completed = 3
    }
}

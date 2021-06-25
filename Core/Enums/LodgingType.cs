using System.ComponentModel.DataAnnotations;

namespace Core.Enums
{
    public enum LodgingType
    {
        Individual,
        [Display(Name = "Group Male")]
        GroupMale,
        [Display(Name = "Group Female")]
        GroupFemale
    }
}
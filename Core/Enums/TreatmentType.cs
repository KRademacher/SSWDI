using System;

namespace Core.Enums
{
    [Flags]
    public enum TreatmentType
    {
        Euthanasia = 1 << 0,
        Castration = 1 << 1,
        Chipping = 1 << 2,
        Sterilisation = 1 << 3,
        Surgery = 1 << 4,
        Vaccination = 1 << 5
    }
}
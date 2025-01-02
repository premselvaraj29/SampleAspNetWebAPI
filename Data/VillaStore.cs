using SampleProject.Models.DTO;

namespace SampleProject.Data;

public static class VillaStore
{
    public static List<VillaDTO> VillaList = new List<VillaDTO>()
    {
        new VillaDTO() { Id = 1, Name = "PoolVilla" },
        new VillaDTO() { Id = 2, Name = "BeachView" },
    };
}
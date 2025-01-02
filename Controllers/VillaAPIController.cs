using Microsoft.AspNetCore.Mvc;
using SampleProject.Data;
using SampleProject.Models;
using SampleProject.Models.DTO;

namespace SampleProject.Controllers;
//[Route("api/[controller]")]
[Route("api/VillaAPI")]
[ApiController]
public class VillaAPIController:ControllerBase
{
    [HttpGet]
    public IEnumerable<VillaDTO> GetVillas()
    {
        return VillaStore.VillaList;
    }


    [HttpGet( "{id:int}")]

    public VillaDTO GetVilla(int id)
    {
        return VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
    }
}
using Microsoft.AspNetCore.JsonPatch;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.VillaList);
    }


    [HttpGet( "{id:int}",Name="GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        return Ok(villa);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
    {
        
        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }

        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }
        if (VillaStore.VillaList.FirstOrDefault(x => x.Name.ToUpper() == villaDTO.Name.ToUpper()) != null)
        {
            ModelState.AddModelError("Name","Villa already exists");
            return BadRequest(ModelState);
        }

        if (villaDTO.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villaDTO.Id = VillaStore.VillaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
        VillaStore.VillaList.Add(villaDTO);
        return CreatedAtRoute("GetVilla",new{id=villaDTO.Id},villaDTO);
    }


    [HttpDelete("{id:int}",Name="DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        VillaStore.VillaList.Remove(villa);
        return NoContent();
    }

    [HttpPut("{id:int}",Name="UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateVilla(int id,[FromBody]VillaDTO villaDTO)
    {
        if (villaDTO == null || id != villaDTO.Id)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        villa.Name =villaDTO.Name;
        villa.SqFt = villaDTO.SqFt;
        villa.Occupancy=villaDTO.Occupancy;
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDocument)
    {
        if (patchDocument == null || id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return BadRequest("Villa not found");
        }
        
        patchDocument.ApplyTo(villa, ModelState);//If any errors, error will be stored in the ModelState
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }
}
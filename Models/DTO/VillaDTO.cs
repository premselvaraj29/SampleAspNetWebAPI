using System.ComponentModel.DataAnnotations;

namespace SampleProject.Models.DTO;

public class VillaDTO
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }   
    
    [Required]
    public int SqFt { get; set; }
    
    [Required]
    public int Occupancy { get; set; }
}
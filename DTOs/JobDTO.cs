using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace nombre.DTOs;
public class JobDTO
{   
    [Required(ErrorMessage = "El Nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El Nombre no puede exceder los 100 caracteres.")]
    public required string Name { get; set; }

    [StringLength(500, ErrorMessage = "La Descripción no puede exceder los 500 caracteres.")]
    public string? Description { get; set; }

}
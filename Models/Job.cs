using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace nombre.Models;

[Table("Job")]
public class Job
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int id {get; set; }

    [Column("name")]
    public required string Name {get; set; }

    [Column("description")]
    public string? Description {get; set; }

    [Column("completed")]
    public bool Completed {get; set; } 

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApp.Domain.Entities
{
    [Table("Sample", Schema = "MySchema")]
    public class Sample
    {
        [Column("Id")]
        [Key]
        public Guid SampleId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Created")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset Created { get; set; }

        public IEnumerable<SubSample> SubSamples { get; set; }
    }
}

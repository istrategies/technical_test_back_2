using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApp.Domain.Entities
{
    [Table("SubSample", Schema = "MySchema")]
    public class SubSample
    {
        [Column("SubSampleId")]
        [Key]
        public Guid SubSampleId { get; set; }

        [Column("Info")]
        public string Info { get; set; }

        [Column("Sample1Id")]
        public Guid SampleId { get; set; }

        public Sample SampleRef { get; set; }
    }
}
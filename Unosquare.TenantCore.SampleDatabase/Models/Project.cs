using System;
using System.ComponentModel.DataAnnotations;

namespace Unosquare.TenantCore.SampleDatabase.Models
{
    public class Project
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public long TenantId { get; set; }
    }
}

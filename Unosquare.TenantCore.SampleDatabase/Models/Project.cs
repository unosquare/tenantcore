using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

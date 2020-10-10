using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Data.Entities
{
    public abstract class SoftDeleteBaseEntity : BaseEntity
    {
        public DateTimeOffset? DeletedAt { get; set; }
    }
}

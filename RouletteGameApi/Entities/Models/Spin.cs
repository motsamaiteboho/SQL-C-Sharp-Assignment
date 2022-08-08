﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Spin
    {
        [Column("SpinId")]
        public Guid Id { get; set; }
        public long? SpinResult { get; set; }
        public DateTime TimestampUtc { get; set; }
        public virtual ICollection<Bet>? Bets { get; set; }
    }
}

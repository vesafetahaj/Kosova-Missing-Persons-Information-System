using System;
using System.Collections.Generic;

namespace KosovaMap2.Models
{
    public partial class Qyteti
    {
        public Qyteti()
        {
            TeDhenaVendiZhdukjesNavigations = new HashSet<TeDhena>();
            TeDhenaVendlindjaNavigations = new HashSet<TeDhena>();
        }

        public int QytetiId { get; set; }
        public string? QytetiEmri { get; set; }

        public virtual ICollection<TeDhena> TeDhenaVendiZhdukjesNavigations { get; set; }
        public virtual ICollection<TeDhena> TeDhenaVendlindjaNavigations { get; set; }
    }
}

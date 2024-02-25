using System;
using System.Collections.Generic;

namespace KosovaMap2.Models
{
    public partial class TeDhena
    {
        public int Id { get; set; }
        public string? EmriMbiemri { get; set; }
        public string? DataLindjes { get; set; }
        public int? Vendlindja { get; set; }
        public int? VendiZhdukjes { get; set; }
        public string? DataZhdukjes { get; set; }
        public string? Komente { get; set; }

        public virtual Qyteti? VendiZhdukjesNavigation { get; set; }
        public virtual Qyteti? VendlindjaNavigation { get; set; }
    }
}

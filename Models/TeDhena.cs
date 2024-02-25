using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KosovaMap2.Models
{
    public partial class TeDhena
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Emri dhe Mbiemri jane te detyrueshme.")]
        public string? EmriMbiemri { get; set; }

        [Required(ErrorMessage = "Data e Lindjes është e detyrueshme.")]
        public string? DataLindjes { get; set; }

        [Required(ErrorMessage = "Vendlindja është e detyrueshme.")]
        public int? Vendlindja { get; set; }

        [Required(ErrorMessage = "Vendi i zhdukjes është i detyrueshem.")]
        public int? VendiZhdukjes { get; set; }

        [Required(ErrorMessage = "Data e zhdukjes është e detyrueshme.")]

        public string? DataZhdukjes { get; set; }

        [Required(ErrorMessage = "Komenti është i detyrueshem.")]
        public string? Komente { get; set; }

        public virtual Qyteti? VendiZhdukjesNavigation { get; set; }
        public virtual Qyteti? VendlindjaNavigation { get; set; }
    }
}

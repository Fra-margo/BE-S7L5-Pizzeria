namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordine")]
    public partial class Ordine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordine()
        {
            ProdottiIngredienti = new HashSet<ProdottiIngredienti>();
            DataOrdine = DateTime.Now;
        }

        [Key]
        public int IdOrdine { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        public string Note { get; set; }

        [Required]
        [StringLength(50)]
        public string Stato { get; set; }

        public decimal Totale { get; set; }
        public DateTime DataOrdine { get; set; }

        public virtual Utenti Utenti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdottiIngredienti> ProdottiIngredienti { get; set; }
    }
}

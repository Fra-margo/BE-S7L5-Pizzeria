namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ingredienti")]
    public partial class Ingredienti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingredienti()
        {
            ProdottiIngredienti = new HashSet<ProdottiIngredienti>();
        }

        [Key]
        public int IdIngrediente { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public decimal Prezzo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdottiIngredienti> ProdottiIngredienti { get; set; }
    }
}

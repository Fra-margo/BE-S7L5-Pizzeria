namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProdottiIngredienti")]
    public partial class ProdottiIngredienti
    {
        public int IdProdotto { get; set; }

        public int IdIngrediente { get; set; }

        public int IdOrdine { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        public int Id { get; set; }

        public int Quantita { get; set; }

        [Key]
        public int IdProIng { get; set; }

        public virtual Ingredienti Ingredienti { get; set; }

        public virtual Ordine Ordine { get; set; }

        public virtual Prodotti Prodotti { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}

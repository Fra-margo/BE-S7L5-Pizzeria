using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class OrdineModel
    {
        public string NomeProdotto { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public int Quantita { get; set; }
        public List<Ingredienti> IngredientiSelezionati { get; set; }
    }
}
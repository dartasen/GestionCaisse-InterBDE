using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCaisse_MVVM
{
    /// <summary>
    /// Classe définissant un object de type Produit
    /// </summary>

    [Table("produit")]
    public sealed class Product
    {
        [Column("idProduit"), Key]
        public int IdProduit { get; set; }

        [Column("nom")]
        public string Nom { get; set; }

        [Column("prix")]
        public double Prix { get; set; }

        [Column("prixAchat")]
        public double PrixAchat { get; set; }

        [Column("quantite")]
        public int Quantite { get; set; }

        [Column("categorie")]
        public string Categorie { get; set; }

        [Column("datePeremption")]
        public Nullable<System.DateTime> DatePeremption { get; set; }
    }
}

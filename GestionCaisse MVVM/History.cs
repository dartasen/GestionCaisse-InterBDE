using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCaisse_MVVM
{
    [Table("historique")]
    public class History
    {
        [Column("idVente"), Key]
        public int IdVente { get; set; }

        [Column("idUtilisateur")]
        public Nullable<int> IdUtilisateur { get; set; }

        [Column("idProduit")]
        public int IdProduit { get; set; }

        [Column("quantite")]
        public int Quantite { get; set; }

        [Column("idBDEAcheteur")]
        public int IdBDEAcheteur { get; set; }

        [Column("idClient")]
        public Nullable<int> IdClient { get; set; }

        [Column("dateVente")]
        public System.DateTime DateVente { get; set; }
    }

    [NotMapped]
    public class QueryHistory : History
    {
        [NotMapped]
        public string Username { get; set; }

        [NotMapped]
        public string ProductName { get; set; }

        [NotMapped]
        public string BuyingBDEName { get; set; }

        [NotMapped]
        public string FormatedSaleDate => DateVente.ToString("dd/MM/yyyy HH:MM:ss");
    }
}

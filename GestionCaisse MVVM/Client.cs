using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCaisse_MVVM
{
    [Table("client")]
    public sealed class Client
    {
        [Column("idClient"), Key]
        public int IdClient { get; set; }

        [Column("idCarte")]
        public string IdCarte { get; set; }

        [Column("nom")]
        public string Nom { get; set; }

        [Column("idBde")]
        public Nullable<int> IdBde { get; set; }

        [Column("credit")]
        public double Credit { get; set; }

        [Column("codeSecret")]
        public int CodeSecret { get; set; }
    }
}

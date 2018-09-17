using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCaisse_MVVM
{
    [Table("bde")]
    public sealed class BDE
    {
        [Column("idBde"), Key]
        public int Id { get; set; }

        [Column("nom")]
        public string Nom { get; set; }

        [Column("taux")]
        public double Taux { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionCaisse_MVVM
{
    [Table("utilisateur")]
    public class User
    {
        [Column("idUtilisateur"), Key]
        public int IdUtilisateur { get; set; }

        [Column("nom")]
        public string Nom { get; set; }

        [Column("codePersonnel")]
        public string CodePersonnel { get; set; }

        [Column("codeBadge")]
        public string CodeBadge { get; set; }

        [Column("idBDE")]
        public int IdBDE { get; set; }

        [Column("admin")]
        public bool IsAdmin { get; set; }

        [Column("active")]
        public bool IsActive { get; set; }
    }

    [NotMapped]
    public class QueryUser : User
    {
        [NotMapped]
        public string BDEName { get; set; }

        [NotMapped]
        public string FormatedIsActive => IsActive ? "O" : "X";

        [NotMapped]
        public string FormatedIsAdmin => IsAdmin ? "O" : "X";
    }
}

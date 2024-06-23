using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DachaMentat.Db
{
    public class User
    {
        // Generated Id of User
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Custom login of User
        public string Login { get; set; }   

        // Hashed password of User
        public string PasswordHash { get; set; }        
    }
}

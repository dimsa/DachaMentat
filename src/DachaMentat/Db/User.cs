namespace DachaMentat.Db
{
    public class User
    {
        // Generated Id of User
        public int Id { get; set; }

        // Custom login of User
        public string Login { get; set; }   

        // Hashed password of User
        public string PasswordHash { get; set; }        
    }
}

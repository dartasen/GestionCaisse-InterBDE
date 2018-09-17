namespace GestionCaisse_MVVM.Model.Entities
{
    public class LoginResult
    {
        public ConnectionResult ConnectionResult { get; }
        public User User { get; }

        public LoginResult(ConnectionResult connectionResult, User user)
        {
            ConnectionResult = connectionResult;
            User = user;
        }
    }
}

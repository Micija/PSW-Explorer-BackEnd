using PSW24.BuildingBlocks.Core.Domain;

namespace PSW24.Core.Domain
{
    public class User : Entity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public UserRole Role { get; private set; }
        public bool IsActive { get; set; }
        public string Name {  get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public long? Points { get; private set; }
        public long? Penalty { get; private set; }
        public bool IsBlocked { get; private set; }
        public List<UserInterest> Interests { get; private set; }
        public List<Tour> Tours { get; private set; }       //Za koje je autor
        public ICollection<Cart> Carts { get; } = [];
        public ICollection<Problem> Problems { get; } = []; 
        public ICollection<Report> Reports { get; } = [];

        public User(string username, string password, UserRole role, bool isActive, string name, string surname, string email, long? points, long? penalty) 
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password; 
            Role = role;
            IsActive = isActive;
            Username = username;
            Interests = new();
            Tours = new();
            Points = points;
            Penalty = penalty;
            Validate();
        }

        public User(string username, string password, UserRole role, bool isActive)
        {
            Username = username;
            Password = password;
            Role = role;
            IsActive = isActive;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Username");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Password");
        }

        public string GetPrimaryRoleName()
        {
            return Role.ToString().ToLower();
        }

        public void AddInterest(UserInterest userInterest)
        {
            Interests.Add(userInterest);
        }

        public void IncPoints()
        {
            Points++;
        }

        public void IncPenalty()
        {
            Penalty++;
        }

        public void Block()
        {
            IsBlocked = true;
        }
        public void Unblock()
        {
            IsBlocked = false;
        }

    }
}

public enum UserRole
{
    Tourist,
    Author,
    Admin
}

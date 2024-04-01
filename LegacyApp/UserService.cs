using System;

namespace LegacyApp
{
    public class UserService
    {
        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
        }

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
        }

        private IClientRepository _clientRepository;
        private IUserCreditService _userCreditService;
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsFirstNameCorrect(firstName) || !IsLastNameCorrect(lastName))
            {
                return false;
            }

            if (IsEmailCorrect(email))
            {
                return false;
            }

            var age = CalculateAgeUsingBirthdate(dateOfBirth);

            if (age < 21)
            {
                return false;
            }
            
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else
            {
                int multiplier;
                
                if (client.Type == "ImportantClient")
                    multiplier = 2;
                else
                {
                    multiplier = 1;
                    user.HasCreditLimit = true;
                }

                user.CreditLimit = GetCreditLimit(user) * multiplier;
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private int GetCreditLimit(User user) 
        {
            int creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            return creditLimit;
        }

        private static int CalculateAgeUsingBirthdate(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;
            return age;
        }

        private static bool IsEmailCorrect(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        private static bool IsLastNameCorrect(string lastName)
        {
            return !string.IsNullOrEmpty(lastName);
        }

        private static bool IsFirstNameCorrect(string firstName)
        {
            return !string.IsNullOrEmpty(firstName);
        }
    }
}

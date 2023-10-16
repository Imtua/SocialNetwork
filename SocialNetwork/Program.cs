using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

internal class Program
{
    private static UserService _service = new UserService();
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Добро пожаловать в социальную сеть!");

            Console.WriteLine("Для регистрации нажмите (1).");
            Console.WriteLine("Для авторизации в профиль нажмите (2).");

            switch (Console.ReadLine())
            {
                case "1":

                    Alert.SuccesMessage("РЕГИСТРАЦИЯ");

                    Console.Write("Введите имя для нового пользователя:");
                    var firstName = Console.ReadLine();

                    Console.Write("Введите фамилию для нового пользователя:");
                    var lastName = Console.ReadLine();

                    Console.Write("Введите E-mail для нового пользователя:");
                    var email = Console.ReadLine();

                    Console.Write("Введите пароль для нового пользователя:");
                    var password = Console.ReadLine();

                    UserRegistrationData userRegistrationData = new UserRegistrationData()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Password = password
                    };

                    try
                    {
                        _service.Register(userRegistrationData);
                        Alert.SuccesMessage("Регистрация выполнена.");
                    }
                    catch (Exception ex)
                    {
                        Alert.ErrorMessage($"Произошла ошибка. {Environment.NewLine}[{ex.Message}]");
                    }

                break;

                case "2":
                    {

                        Console.Write("Введите Email пользователя:");
                        var authEmail = Console.ReadLine();

                        Console.Write("Введите пароль:");
                        var authPassword = Console.ReadLine();

                        var userAuthenticationData = new UserAuthenticationData()
                        {
                            Email = authEmail,
                            Password = authPassword
                        };

                        try
                        {
                            var currentSession = _service.Authenticate(userAuthenticationData);
                            Alert.SuccesMessage("Авторизация выполнена. С возвращением!");

                            while (true)
                            {
                                Console.WriteLine("Мой профиль (1)");
                                Console.WriteLine("Редактировать профиль (2)");
                                Console.WriteLine("Поиск друзей (3)");
                                Console.WriteLine("Написать сообщение (4)");
                                Console.WriteLine("Выйти (5)");

                                switch (Console.ReadLine())
                                {

                                    case "1":
                                        {
                                            Alert.SuccesMessage("Информация о профиле");
                                            Alert.SuccesMessage($"Имя: {currentSession.FirstName}");
                                            Alert.SuccesMessage($"Фамилия: {currentSession.LastName}");
                                            Alert.SuccesMessage($"Пароль: {currentSession.Password}");
                                            Alert.SuccesMessage($"Email: {currentSession.Email}");
                                            Alert.SuccesMessage($"Ссылка фото: {currentSession.Photo}");
                                            Alert.SuccesMessage($"Любимый фильм: {currentSession.FavoriteMovie}");
                                            Alert.SuccesMessage($"Любимая книга: {currentSession.FavoriteBook}");

                                            break;
                                        }

                                    case "2":
                                        {
                                            Console.Write("Меня зовут:");
                                            currentSession.FirstName = Console.ReadLine();

                                            Console.Write("Моя фамилия:");
                                            currentSession.LastName = Console.ReadLine();

                                            Console.Write("Ссылка на моё фото:");
                                            currentSession.Photo = Console.ReadLine();

                                            Console.Write("Мой любимый фильм:");
                                            currentSession.FavoriteMovie = Console.ReadLine();

                                            Console.Write("Моя любимая книга:");
                                            currentSession.FavoriteBook = Console.ReadLine();

                                            _service.Update(currentSession);

                                            Alert.SuccesMessage("Профиль обновлён.");

                                            break;
                                        }

                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            Alert.ErrorMessage($"Произошла ошибка. {Environment.NewLine}[{ex.Message}]");
                        }

                break;

                    }
            }
        }
    }
}
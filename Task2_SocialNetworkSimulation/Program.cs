using System.Text;

struct Comment
{
    public string Author;
    public string Text;

    public Comment(string author, string text)
    {
        Author = author;
        Text = text;
    }
}

class Post
{
    public DateTime CreationDate;
    public string Text;
    public int Likes;
    public List<Comment> Comments = new List<Comment>();

    public Post(string text)
    {
        CreationDate = DateTime.Now;
        Text = text;
        Likes = 0;
    }

    public void AddLike()
    {
        Likes++;
    }

    public void AddComment(string author, string text)
    {
        Comments.Add(new Comment(author, text));
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Дата: {CreationDate:dd.MM.yyyy HH:mm}");
        Console.WriteLine($"Текст: {Text}");
        Console.WriteLine($"Лайків: {Likes}");

        Console.WriteLine("Коментарі:");

        if (Comments.Count == 0)
        {
            Console.WriteLine("Немає коментарів.");
        }
        else
        {
            foreach (Comment comment in Comments)
            {
                Console.WriteLine($"{comment.Author}: {comment.Text}");
            }
        }

        Console.WriteLine("-----------------------------");
    }
}

class User
{
    public string Name;
    public string Email;
    public string Nickname;
    public List<Post> Posts = new List<Post>();

    public User(string name, string email, string nickname)
    {
        Name = name;
        Email = email;
        Nickname = nickname;
    }

    public void CreatePost(string text)
    {
        Posts.Add(new Post(text));
    }

    public void DeletePost(Post post)
    {
        Posts.Remove(post);
    }

    public void ShowPosts(int count = int.MaxValue)
    {
        if (Posts.Count == 0)
        {
            Console.WriteLine("У користувача немає постів.");
            return;
        }

        int startIndex = Math.Max(0, Posts.Count - count);

        for (int i = Posts.Count - 1; i >= startIndex; i--)
        {
            Console.WriteLine($"Пост №{i + 1}");
            Posts[i].ShowInfo();
        }
    }
}

class SocialNetwork
{
    public List<User> Users = new List<User>();

    public SocialNetwork()
    {
        User user1 = new User(
            "Олександр",
            "oleksandr@gmail.com",
            "@alex");

        User user2 = new User(
            "Марія",
            "maria@gmail.com",
            "@maria");

        User user3 = new User(
            "Льоха",
            "lyoha@gmail.com",
            "@lyoha");

        user1.CreatePost("Сьогодні бачив пса патрона!");
        user1.CreatePost("Вчу C#, оце прікол.");
        user1.CreatePost("Працюю над новим завданням.");

        user2.CreatePost("Салам, it's me again!");
        user2.CreatePost("Сьогодні жесть як багато роботи...");
        user2.CreatePost("Дивіться, що я знайшла!");
        user2.CreatePost("Ліс такий приємний в цей час.");
        user2.CreatePost("Еххх, сумую за зимою :(");
        user2.CreatePost("Останній пост на сьогодні.");

        user3.CreatePost("Мій перший пост.");
        user3.CreatePost("Я тут щось пишу типу!");

        Users.Add(user1);
        Users.Add(user2);
        Users.Add(user3);
    }

    public void ShowUsers(User? excludedUser = null)
    {
        Console.WriteLine(excludedUser is null
            ? "\nКористувачі:"
            : "\nІнші користувачі:");

        for (int i = 0; i < Users.Count; i++)
        {
            if (Users[i] == excludedUser)
            {
                continue;
            }

            Console.WriteLine(
                $"{i + 1}. {Users[i].Name} " +
                $"({Users[i].Nickname})");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        SocialNetwork network = new SocialNetwork();

        Console.WriteLine("Parodygram");

        network.ShowUsers();

        Console.Write("\nОберіть користувача: ");

        if (!int.TryParse(Console.ReadLine(), out int userNumber) ||
            userNumber < 1 ||
            userNumber > network.Users.Count)
        {
            Console.WriteLine("Неправильний номер користувача.");
            return;
        }

        User currentUser = network.Users[userNumber - 1];

        while (true)
        {
            Console.WriteLine($"\nПоточний користувач: {currentUser.Nickname}");
            Console.WriteLine("1. Переглянути останні 5 постів користувача");
            Console.WriteLine("2. Поставити лайк");
            Console.WriteLine("3. Написати коментар");
            Console.WriteLine("4. Створити новий пост");
            Console.WriteLine("5. Видалити свій пост");
            Console.WriteLine("6. Переглянути свої пости");
            Console.WriteLine("0. Вийти");

            Console.Write("\nОберіть дію: ");
            string? choice = Console.ReadLine();

            if (choice is null)
            {
                return;
            }

            switch (choice)
            {
                case "1":
                    ShowOtherUserPosts(network, currentUser);
                    break;

                case "2":
                    LikePost(network, currentUser);
                    break;

                case "3":
                    CommentPost(network, currentUser);
                    break;

                case "4":
                    CreatePost(currentUser);
                    break;

                case "5":
                    DeletePost(currentUser);
                    break;

                case "6":
                    ShowOwnPosts(currentUser);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невідома команда.");
                    break;
            }
        }
    }

    static User? SelectOtherUser(
        SocialNetwork network,
        User currentUser)
    {
        network.ShowUsers(currentUser);

        Console.Write("Оберіть користувача: ");

        if (!int.TryParse(Console.ReadLine(), out int number) ||
            number < 1 ||
            number > network.Users.Count)
        {
            Console.WriteLine("Неправильний номер.");
            return null;
        }

        User selectedUser = network.Users[number - 1];

        if (selectedUser == currentUser)
        {
            Console.WriteLine("Потрібно обрати іншого користувача.");
            return null;
        }

        return selectedUser;
    }

    static Post? SelectPost(User user, string prompt = "Оберіть пост: ")
    {
        if (user.Posts.Count == 0)
        {
            Console.WriteLine("У користувача немає постів.");
            return null;
        }

        Console.WriteLine($"\nПости {user.Nickname}:");

        for (int i = 0; i < user.Posts.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1}. {user.Posts[i].Text}");
        }

        Console.Write(prompt);

        if (!int.TryParse(Console.ReadLine(), out int postNumber) ||
            postNumber < 1 ||
            postNumber > user.Posts.Count)
        {
            Console.WriteLine("Неправильний номер поста.");
            return null;
        }

        return user.Posts[postNumber - 1];
    }

    static Post? SelectOtherUserPost(
        SocialNetwork network,
        User currentUser)
    {
        User? user = SelectOtherUser(network, currentUser);
        return user is null ? null : SelectPost(user);
    }

    static void ShowOtherUserPosts(
        SocialNetwork network,
        User currentUser)
    {
        User? user = SelectOtherUser(network, currentUser);

        if (user == null)
        {
            return;
        }

        Console.WriteLine(
            $"\nОстанні пости користувача {user.Nickname}:");

        user.ShowPosts(5);
    }

    static void LikePost(
        SocialNetwork network,
        User currentUser)
    {
        Post? post = SelectOtherUserPost(network, currentUser);

        if (post == null)
        {
            return;
        }

        post.AddLike();

        Console.WriteLine("Лайк поставлено.");
    }

    static void CommentPost(
        SocialNetwork network,
        User currentUser)
    {
        Post? post = SelectOtherUserPost(network, currentUser);

        if (post == null)
        {
            return;
        }

        Console.Write("Введіть коментар: ");
        string text = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Коментар не може бути порожнім.");
            return;
        }

        post.AddComment(currentUser.Nickname, text);

        Console.WriteLine("Коментар додано.");
    }

    static void CreatePost(User currentUser)
    {
        Console.Write("Введіть текст нового поста: ");
        string text = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Пост не може бути порожнім.");
            return;
        }

        currentUser.CreatePost(text);

        Console.WriteLine("Пост створено.");
    }

    static void DeletePost(User currentUser)
    {
        if (currentUser.Posts.Count == 0)
        {
            Console.WriteLine("У вас немає постів.");
            return;
        }

        Post? post = SelectPost(currentUser, "Оберіть пост для видалення: ");

        if (post == null)
        {
            return;
        }

        currentUser.DeletePost(post);

        Console.WriteLine("Пост видалено.");
    }

    static void ShowOwnPosts(User currentUser)
    {
        if (currentUser.Posts.Count == 0)
        {
            Console.WriteLine("У вас немає постів.");
            return;
        }

        Console.WriteLine($"\nПости {currentUser.Nickname}:");

        currentUser.ShowPosts();
    }
}

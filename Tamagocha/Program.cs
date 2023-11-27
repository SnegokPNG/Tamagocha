using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

Tamagocha tamagocha = new Tamagocha { Name = "Гаврик" };
tamagocha.HungryChanged += HungryChanged;
tamagocha.ThirstyChanged += ThirstyChanged;
tamagocha.DirtyChanged += DirtyChanged;

ConsoleKeyInfo command;
Random random = new Random();
do
{
    command = Console.ReadKey();
    if (command.Key == ConsoleKey.F)
        tamagocha.Feed();
    else if (command.Key == ConsoleKey.I)
        tamagocha.PrintInfo();
    else if (command.Key == ConsoleKey.D)
        tamagocha.Drink();
    else if (command.Key == ConsoleKey.C)
        tamagocha.Clean();
    else if (command.Key == ConsoleKey.P)
    {
        IPresent present;
        int presentType = random.Next(1, 4);
        switch (presentType)
        {
            case 1:
                present = new Toy();
                break;
            case 2:
                present = new Candy();
                break;
            case 3:
                present = new Book();
                break;
            default:
                present = new Toy();
                break;
        }

        int actionType = random.Next(1, 4);
        switch (actionType)
        {
            case 1:
                present.Open();
                break;
            case 2:
                present.Gnaw();
                break;
            case 3:
                present.Smash();
                break;
            default:
                present.Open();
                break;
        }
    }
}
while (command.Key != ConsoleKey.Escape);
tamagocha.Stop();

void HungryChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 0);
    Console.Write($"{tamagocha.Name} испытывает голод! Параметр голода возврастает: {tamagocha.Hungry}");
    Console.SetCursorPosition(0, 14);
  
}

void ThirstyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 1);
    Console.Write($"{tamagocha.Name} хочет пить! Показатель жажды растет: {tamagocha.Thirsty}");
    
}

void DirtyChanged(object? sender, EventArgs e)
{
    Console.SetCursorPosition(0, 2);
    Console.Write($"{tamagocha.Name} грязный! Показатель грязности растет: {tamagocha.Dirty}");
   
}

class Toy : IPresent
{
    public void Open()
    {
        Console.WriteLine("Игрушка открыта!");
    }

    public void Gnaw()
    {
        Console.WriteLine("Игрушка погрызана!");
    }

    public void Smash()
    {
        Console.WriteLine("Игрушка разбита!");
    }
}

class Candy : IPresent
{
    public void Open()
    {
        Console.WriteLine("Конфеты открыты!");
    }

    public void Gnaw()
    {
        Console.WriteLine("Конфеты погрызаны!");
    }

    public void Smash()
    {
        Console.WriteLine("Конфеты разбиты!");
    }
}

class Book : IPresent
{
    public void Open()
    {
        Console.WriteLine("Книга открыта!");
    }

    public void Gnaw()
    {
        Console.WriteLine("Книга погрызана!");
    }

    public void Smash()
    {
        Console.WriteLine("Книга порвана!");
    }
}

class Tamagocha
{
    public string Name { get; set; }
    public int Health { get; set; } = 100;
    public int Hungry
    {
        get => hungry;
        set
        {
            if (hungry > 260) 
            {
                IsDead = true;
                Console.WriteLine(                   "             Гаврик умер от голода");
            }
            hungry = value;
            HungryChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Dirty
    {
        get => dirty;
        set
        {
            if (Dirty > 260)
            { 
                IsDead = true;
                Console.WriteLine("                     Гаврик покинул вас, ушел за лучшей жизнью ");
            }
            dirty = value;
            DirtyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Thirsty
    {
        get => thirsty; set
        {
            thirsty = value;
            ThirstyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsDead { get => isDead; set => isDead = value; }
    public event EventHandler HungryChanged;
    public event EventHandler DirtyChanged;
    public event EventHandler ThirstyChanged;
    public Tamagocha()
    {
        Thread thread = new Thread(LifeCircle);
        thread.Start();
    }
    Random random = new Random();
    private int hungry = 0;
    private int dirty = 0;
    private int thirsty = 0;
    private bool isDead = false;

    private void LifeCircle(object? obj)
    {
        while (!IsDead)
        {
            Thread.Sleep(500);
            int rnd = random.Next(0, 2);
            switch (rnd)
            {
                case 0: JumpMinute(); break;
                case 1: FallSleep(); break;
                case 2: Swimming(); break;
                case 3: WatchTV(); break;
                case 4: break;
                case 5: break;
                default: break;
            }

        }

    }

    private void FallSleep()
    {
        WriteMessageToConsole($"{Name} внезапно начинает спать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }

    private void JumpMinute()
    {
        WriteMessageToConsole($"{Name} внезапно начинает прыгать как угорелый. Это продолжается целую минуту. Показатели голода, жажды и чистоты повышены!");
        Thirsty += random.Next(5, 10);
        Hungry += random.Next(5, 10);
        Dirty += random.Next(5, 10);
    }
    private void Swimming()
    {
        WriteMessageToConsole($"{Name} пошел тонуть в луже.  Это продолжается не долго, Гаврик напился, помылся, но проголодался");
        Thirsty += random.Next(-3, -1);
        Hungry += random.Next(5, 6);
        Dirty += random.Next(-10, -5);
    }
    private void WatchTV()
    {
        WriteMessageToConsole($"{Name} решил посмотреть Симпсоны. Гаврик отдохнул ");
        Thirsty += random.Next(1, 3);
        Hungry += random.Next(2, 5);
        Dirty += random.Next(1, 3);
    }

    private void WriteMessageToConsole(string message)
    {
        Console.SetCursorPosition(0, 10);
        Console.Write(message);
        Console.SetCursorPosition(0, 5);
    }

    public void PrintInfo()
    {
        Console.SetCursorPosition(0, 8);
        Console.WriteLine($"{Name}: Health:{Health} Hungry:{Hungry} Dirty:{Dirty} Thirsty:{Thirsty} IsDead:{IsDead}");
    }

    public void Stop()
    {
        IsDead = true;
    }

    internal void Feed()
    {
        WriteMessageToConsole($"{Name} начинает ЖРАТЬ как хомяк.");

        Hungry -= random.Next(5, 10);
    }

    internal void Drink()
    {
        WriteMessageToConsole($"{Name} начинает ПИТЬ как водохлеб.");

        Thirsty -= random.Next(5, 10);
    }

    internal void Clean()
    {
        WriteMessageToConsole($"{Name}  начинает МЫТЬСЯ как кот.");

        Dirty -= random.Next(5, 10);
    }

    public void DoSomethingWithPresent(IPresent present)
    {
        present.Open();
        present.Gnaw();
        present.Smash();
    } 
}
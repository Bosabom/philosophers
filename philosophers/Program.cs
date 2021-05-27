using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Dining_Philosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            Fork Plastic = new Fork() { ForkID = "Пластиковая вилка", State = ForkState.OnTheTable };
            Fork Gold = new Fork() { ForkID = "Золотая вилка", State = ForkState.OnTheTable };
            Fork Silver = new Fork() { ForkID = "Серебрянная вилка", State = ForkState.OnTheTable };
            Fork Platinum = new Fork() { ForkID = "Платиновая вилка", State = ForkState.OnTheTable };
            Fork Wood = new Fork() { ForkID = "Деревянная вилка", State = ForkState.OnTheTable };
           

            Philosopher aristotle = new Philosopher(Plastic, Wood, "Аристотель", 7);
            Philosopher plato = new Philosopher(Wood, Silver, "Платон", 4);
            Philosopher kant = new Philosopher(Silver, Gold, "Иммануил Кант", 5);
            Philosopher socrates = new Philosopher(Gold, Platinum, "Сократ", 6);
            Philosopher marx = new Philosopher(Platinum, Plastic, "Карл Маркс", 5);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task1 = new Task(() => aristotle.Think(token));
            Task task2 = new Task(() => plato.Think(token));
            Task task3 = new Task(() => kant.Think(token));
            Task task4 = new Task(() => socrates.Think(token));
            Task task5 = new Task(() => marx.Think(token));

            Task[] all_tasks = new Task[5] { task1,task2,task3,task4,task5};

            foreach(var task in all_tasks)
            {
                task.Start();
            }
           
            Console.WriteLine("Нажмите любую кнопку для выхода.");
            string input = Console.ReadLine();
            if (input != null )
                cancellationTokenSource.Cancel();
            Console.ReadKey();
        }
    }
}

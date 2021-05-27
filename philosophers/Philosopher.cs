using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Dining_Philosophers
{
    enum PhilosopherState { Eating, Thinking }
    class Philosopher
    {
        public string Name { get; set; }

        public PhilosopherState State { get; set; }

        // пик уровня голода

        readonly int StarvationLevel;

        // правая и левая вилки
        public readonly Fork RightFork;
        public readonly Fork LeftFork;

        Random rand = new Random();

        int ThinkingCounter = 0;

        public Philosopher(Fork leftFork, Fork rightFork, string name, int starvation)
        {
            RightFork = rightFork;
            LeftFork = leftFork;
            Name = name;
            State = PhilosopherState.Thinking;
            StarvationLevel = starvation;
        }

        public void Eat(CancellationToken _token)
        {
            if (_token.IsCancellationRequested)
                return;

            // если можно взять вилку в левую руку,то берем
            if (TakeForkInLeftHand())
            {
                Thread.Sleep(2000);//ждем 2 секунды
                // пытаемся взять вилку в правую руку
                if (TakeForkInRightHand())
                {
                    // если в руках обе вилки, то философ кушает
                    this.State = PhilosopherState.Eating;
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("{0} кушает с : {1} и {2}", Name, LeftFork.ForkID, RightFork.ForkID);
                    Thread.Sleep(rand.Next(5000, 10000));

                    ThinkingCounter = 0;

                    // ложим сначала правую,потом левую вилку на стол
                    RightFork.Put();
                    LeftFork.Put();
                }
                // в случае, если не получилось взять вилку в правую руку
                else
                {
                    // Ждем 3 с и пытаемся взять опять
                     Thread.Sleep(3000);
                    if (TakeForkInRightHand())
                    {
                        // если все же взял вилку в правую руку, то кушаем
                        this.State = PhilosopherState.Eating;
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine("{0} кушает с : {1} и {2}", Name, LeftFork.ForkID, RightFork.ForkID);
                        Thread.Sleep(rand.Next(5000, 10000));

                        ThinkingCounter = 0;

                        RightFork.Put();
                        LeftFork.Put();
                    }
                    // в случае, если не получилось взять вилку в правую руку все равно, после ожидания
                    else
                    {
                        LeftFork.Put();//ложим левую вилку
                    }
                }
            }

            Think(_token);
        }

        public void Think(CancellationToken _token)
        {
            if (_token.IsCancellationRequested)
            {
                return;
            }
            this.State = PhilosopherState.Thinking;
            //Console.WriteLine("------------------------------------------------");
            Console.WriteLine("{0} думает...", Name);
         
            Thread.Sleep(rand.Next(2500, 20000));
            ThinkingCounter++;

            if (ThinkingCounter > StarvationLevel)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("{0} голоден !!!", Name);
            }

            Eat(_token);
        }

        private bool TakeForkInLeftHand()
        {
            return LeftFork.Take(Name);
        }

        private bool TakeForkInRightHand()
        {
            return RightFork.Take(Name);
        }

    }
}

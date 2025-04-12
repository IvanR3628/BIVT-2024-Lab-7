using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltytimes;

            public string Name => _name;

            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    if (_penaltytimes == null) return null;
                    int[] copy = new int[_penaltytimes.Length];
                    Array.Copy(_penaltytimes, copy, _penaltytimes.Length);
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    if (_penaltytimes == null) return 0;
                    int time = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++) time += _penaltytimes[i];
                    return time;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null) return false;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10) return true;
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltytimes = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                if (_penaltytimes == null) return;
                Array.Resize(ref _penaltytimes, _penaltytimes.Length + 1);
                _penaltytimes[_penaltytimes.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public static Participant[] Filter(Participant[] array)
            {
                Participant[] filtered = new Participant[0];
                if (array == null) return null;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].IsExpelled) continue;
                    Array.Resize(ref filtered, filtered.Length + 1);
                    filtered[filtered.Length - 1] = array[i];
                }
                return filtered;
            }

            public void Print()
            {
                Console.WriteLine("{0} {1}     \t{2} {3}", _name, _surname, Total, IsExpelled);
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override void PlayMatch(int foul)
            {
                if (_penaltytimes == null || foul < 0 || foul > 5) return;
                base.PlayMatch(foul);
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null) return false;
                    int count = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++) if (_penaltytimes[i] >= 5) count++;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if ((count > 0.1 * _penaltytimes.Length) || (Total > 2 * _penaltytimes.Length))
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int _count = 0;
            private static int _minutes = 0;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _count++;
            }

            public override void PlayMatch(int time)
            {
                base.PlayMatch(time);
                _minutes += time;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null) return false;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10) return true;
                    }
                    if (Total >  0.1 * _minutes / _count) return true;
                    return false;
                }
            }
        }
    }
}

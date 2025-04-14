using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;

            public string Name => _name;

            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] copy = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int score = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++) for (int j = 0; j < _marks.GetLength(1); j++) score += _marks[i, j];
                    return score;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
            }

            public void Jump(int[] result)
            {
                if ((_marks == null) || (result == null)) return;
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    bool skip = false;
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        if (_marks[i, j] != 0)
                        {
                            skip = true;
                            break;
                        }
                    }
                    if (skip) continue;
                    for (int j = 0; j < 5; j++)
                    {
                        _marks[i, j] = result[j];
                    }
                    break;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine("{0} {1}    \t{2}", _name, _surname, TotalScore);
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;
            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
                
            }

            public void Add(Participant[] participants)
            {
                if ((_participants == null) || (participants == null)) return;
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
        }
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if ((Participants.Length < 3) || (Participants == null)) return default;
                    double[] prizes = new double[3];
                    prizes[0] = 0.5 * Bank;
                    prizes[1] = 0.3 * Bank;
                    prizes[2] = 0.2 * Bank;
                    return prizes;
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if ((Participants.Length < 3) || (Participants == null)) return default;
                    int half = Participants.Length / 2;
                    if (half < 3) half = 3;
                    if (half > 10) half = 10;
                    double[] prizes = new double[half];
                    prizes[0] = 0.4 * Bank;
                    prizes[1] = 0.25 * Bank;
                    prizes[2] = 0.15 * Bank;
                    for (int i = 0; i < half; i++) prizes[i] += Bank * 0.2 / half;
                    return prizes;
                }
            }
        }
    }
}

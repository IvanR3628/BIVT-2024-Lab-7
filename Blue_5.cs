using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _placechanged;

            public string Name => _name;

            public string Surname => _surname;

            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _placechanged = false;
            }

            public void SetPlace(int place)
            {
                if (_placechanged) return;
                _place = place;
                _placechanged = true;
            }

            public void Print()
            {
                Console.WriteLine("{0} {1}\t{2}", _name, _surname, _place);
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _sportsmenID;

            public string Name => _name;

            public Sportsman[] Sportsmen => _sportsmen;

            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int scoresum = 0, score = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        score = 6 - _sportsmen[i].Place;
                        if ((score > 0) && (score < 6))
                        {
                            scoresum += score;
                        }
                    }
                    return scoresum;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int mn = 18;
                    for (int i = 0; i < _sportsmenID; i++) if (_sportsmen[i].Place < mn) mn = _sportsmen[i].Place;
                    return mn;
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _sportsmenID = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if ((_sportsmen == null) || (_sportsmenID >= 6)) return;
                _sportsmen[_sportsmenID] = sportsman;
                _sportsmenID++;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null) return;
                for (int i = 0; i < sportsmen.Length; i++)
                {
                    if (_sportsmenID >= 6) return;
                    _sportsmen[_sportsmenID] = sportsmen[i];
                    _sportsmenID++;
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null) return null;
                Team champion = null;
                double mx = double.MinValue;
                for (int i = 0; i < teams.Length; i++)
                {
                    if (teams[i].GetTeamStrength() > mx)
                    {
                        mx = teams[i].GetTeamStrength();
                        champion = teams[i];
                    }
                }
                return champion;
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if ((teams[j].SummaryScore < teams[j + 1].SummaryScore)
                            || ((teams[j].SummaryScore == teams[j + 1].SummaryScore) && (teams[j]).TopPlace > teams[j + 1].TopPlace))
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine("{0}  \t {1} {2}", _name, SummaryScore, TopPlace);
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null) return 0;
                double sum = 0;
                int count = 0;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    if ((Sportsmen[i] == null) || (Sportsmen[i].Place == 0)) continue;
                    sum += Sportsmen[i].Place;
                    count++;
                }
                if (count == 0) return 0;
                sum = sum / count;
                return 100 / sum;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                if (Sportsmen == null) return 0;
                double sum = 0;
                int count = 0;
                double multiplier = 1;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    if ((Sportsmen[i] == null) || (Sportsmen[i].Place == 0)) continue;
                    sum += Sportsmen[i].Place;
                    count++;
                    multiplier *= Sportsmen[i].Place;
                }
                if (multiplier == 0) return 0;
                return 100 * sum * count / multiplier;
            }
        }
    }
}

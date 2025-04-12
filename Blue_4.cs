using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Lab_7.Blue_4;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;

            public string Name => _name;

            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] copy = new int[_scores.Length];
                    Array.Copy(_scores, copy, _scores.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int score = 0;
                    for (int i = 0; i < _scores.Length; i++) score += _scores[i];
                    return score;
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.WriteLine("{0}     \t{1}", _name, TotalScore);
            }
        }

        public class ManTeam : Team { public ManTeam(string name) : base(name) { } }

        public class WomanTeam : Team { public WomanTeam(string name) : base(name) { } }

        public class Group
        {
            private string _name;
            private ManTeam[] _manteams;
            private WomanTeam[] _womanteams;
            private int _manteamID;
            private int _womanteamID;

            public string Name => _name;

            public ManTeam[] ManTeams => _manteams;

            public WomanTeam[] WomanTeams => _womanteams;

            public Group(string name)
            {
                _name = name;
                _manteams = new ManTeam[12];
                _womanteams = new WomanTeam[12];
                _manteamID = 0;
                _womanteamID = 0;
            }

            public void Add(Team team)
            {
                if (team == null) return;
                if (team is ManTeam manteam)
                {
                    if ((_manteams == null) || (_manteamID >= 12)) return;
                    _manteams[_manteamID] = manteam;
                    _manteamID++;
                }
                if (team is WomanTeam womanteam)
                {
                    if ((_womanteams == null) || (_womanteamID >= 12)) return;
                    _womanteams[_womanteamID] = womanteam;
                    _womanteamID++;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    Add(teams[i]);
                }
            }

            public void SortTeams(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            public void Sort()
            {
                SortTeams(_manteams);
                SortTeams(_womanteams);
            }

            public static Team[] MergeTeams(Team[] team1, Team[] team2, int size)
            {
                Team[] newTeam = new Team[size];
                for (int i = 0, j = 0; i + j < size;)
                {
                    if (((team1[i].TotalScore >= team2[j].TotalScore) || (j >= size / 2 + size % 2)) && (i < size / 2 + size % 2))
                    {
                        if (i < team1.Length) newTeam[i + j] = team1[i];
                        i++;
                    }
                    else
                    {
                        if (j < team2.Length) newTeam[i + j] = team2[j];
                        j++;
                    }
                }
                return newTeam;
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group finalists = new Group("Финалисты");
                finalists.Add(MergeTeams(group1._manteams, group2._manteams, size));
                finalists.Add(MergeTeams(group1._womanteams, group2._womanteams, size));
                return finalists;
            }

            public void Print(Team[] teams)
            {
                for (int i = 0; i < teams.Length; i++)
                {
                    teams[i].Print();
                }
            }
        }
    }
}

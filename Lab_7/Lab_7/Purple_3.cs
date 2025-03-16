using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _currentJudge;

            public string Name => _name;
            public string Surname => _surname;
            public int Score { get; private set; }
            public double[] Marks
            {
                get 
                {
                    if (_marks == null) return null;

                    double[] marksCopy = new double[_marks.Length];
                    Array.Copy(_marks, marksCopy, _marks.Length);
                    return marksCopy;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return null;

                    int[] placesCopy = new int[_places.Length];
                    Array.Copy(_places, placesCopy, _places.Length);
                    return placesCopy;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _currentJudge = 0;
            }
            public void Evaluate(double result)
            {
                if (_currentJudge >= 7 || _marks == null) return;

                _marks[_currentJudge] = result;
                _currentJudge++;
            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;

                for (int i = 0; i < 7; i++)
                {
                    var sortedParticipants = participants.OrderByDescending(x => x.Marks[i]).ToArray();

                    for (int j = 0; j < participants.Length; j++)
                    {
                        sortedParticipants[j]._places[i] = j + 1;
                        sortedParticipants[j].Score += (j + 1);
                    }

                    Array.Copy(sortedParticipants, participants, participants.Length);
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                var sortedArray = array.OrderBy(x => x.Score).ThenBy(x => x.Places.Min()).ThenByDescending(x => x.Marks.Sum()).ToArray();
                Array.Copy(sortedArray, array, array.Length);
            }
            
            public void Print()
            {
                Console.WriteLine($"{_printItem(Name)} {_printItem(Surname)} {_printItem(Score.ToString())} {_printItem(Places.Min().ToString())} {_printItem(Math.Round(Marks.Sum(), 2).ToString())}");
            }
            private static string _printItem(string item)
            {
                return item + new string(' ', 15 - item.Length);
            }
        }
    }
}
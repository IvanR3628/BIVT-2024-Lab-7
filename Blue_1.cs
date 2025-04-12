using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Lab_7.Blue_1;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            protected int _votes;

            public string Name => _name;

            public int Votes => _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int count = 0;
                for (int i = 0; i < responses.Length; i++) if (responses[i].Name == _name) count++;
                _votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine("{0}    \t{1}", _name, _votes);
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;

            public string Surname => _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null) return 0;
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if ((responses[i] is HumanResponse humanResponse) && (humanResponse.Name == Name) && (humanResponse.Surname == _surname))
                    {
                        count++;
                    }
                }
                _votes = count;
                return count;
            }
            public override void Print()
            {
                Console.WriteLine("{0} {1}     \t{2}", Name, _surname, _votes);
            }
        }
    }
}

using System;
using System.Linq;

namespace Choice {
    class student {
        public string name {
            get;
        }

        public float note {
            get;
        }

        public option[] choices {
            get;
        }

        public student(string na, float no, option[] ch) {
            this.name    = na;
            this.note    = no;
            // no duplicate option allowed
            if (ch.Distinct().Count() != ch.Count()) {
                Console.Error.WriteLine(String.Format("Choices for {0} must be unique", this.name));
                System.Environment.Exit(1);
            }
            this.choices = ch;
        }
    }
}

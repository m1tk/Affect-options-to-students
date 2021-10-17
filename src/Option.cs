using System;
using System.Collections.Generic;

namespace Choice {
    // Represent instances of choices that will be given to students
    class option {
        public int open_seats {
            get;
            private set;
        }

        public string abr {
            get;
        }

        public option(string ch, int op_se) {
            this.open_seats = op_se;
            this.abr        = ch;
        }

        public bool take_seat(student st) {
            if (this.open_seats > 0) {
                --this.open_seats;
                return true;
            } else {
                return false;
            }
        }
    }

    // every instance of result will represant an option
    // and will have list of students that have been accepted
    // with that option
    class result {
        public option opt {
            get;
        }
        private List<student> st;

        public result(option op) {
            this.opt = op;
            this.st  = new List<student>();
        }


        public void add_student(student stu) {
            this.st.Add(stu);
        }

        public override string ToString() {
            string list = String.Format("Option {0}:\n", this.opt.abr);
            foreach (student st in this.st) {
                list += String.Format("- {0} {1}\n", st.name, st.note);
            }
            if (this.opt.open_seats > 0) {
                list += String.Format("And {0} empty seat(s)\n", this.opt.open_seats);
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Choice {
    class Program {
        static Tuple<string, int>[] options = {
            new Tuple<string, int>("ASR", 1),
            new Tuple<string, int>("ABD", 2),
            new Tuple<string, int>("AGL", 1),
        };

        static Tuple<string, float, string[]>[] students = {
            new Tuple<string, float, string[]>("Gyer re", 15.5f, new string[]{"ASR", "AGL", "ABD"}),
            new Tuple<string, float, string[]>("dhar a", 13.3f, new string[]{"ABD", "ASR", "AGL"}),
            new Tuple<string, float, string[]>("dhar 2", 15.4f, new string[]{"ABD", "ASR", "AGL"}),
            new Tuple<string, float, string[]>("dhar 3", 12.3f, new string[]{"ABD", "ASR", "AGL"}),
            new Tuple<string, float, string[]>("hhe er", 14.4f, new string[]{"AGL", "ASR", "ABD"})
        };

        static void sort(student[] list) {
            Array.Sort(list, delegate(student y, student x) { return x.note.CompareTo(y.note); });
        }

        static int find_choice(List<option> opts, List<result> res, student[] list) {
            // find best choice for every student according to notes and options order
            // and assign every student to the result he got
            // return number of people that were given a choice in the end
            int count = 0;
            for (int i = 0; i < list.Length; i++) {
                for (int j = 0; j < opts.Count; j++) {
                    if (list[i].choices[j].take_seat(list[i])) {
                        ++count;
                        foreach (result result in res) {
                            if (result.opt == list[i].choices[j]) {
                                result.add_student(list[i]);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return count;
        }

        static void fill_opts(List<option> opts, List<result> res) {
            for (int i = 0; i < options.Length; i++) {
                for (int j = 0; j < options.Length; j++) {
                    if (i == j)
                        continue;
                    if (String.Equals(options[i].Item1, options[j].Item1)) {
                        Console.Error.WriteLine(String.Format("Duplicate option {0} found", options[i].Item1));
                        System.Environment.Exit(1);
                    }
                }
                opts.Add(new option(options[i].Item1, options[i].Item2));
                res.Add (new result(opts[i]));
            }
        }

        static student[] check_student_options(List<option> opts, Tuple<string, float, string[]>[] list) {
            string[] choices = new string[opts.Count];
            student[] sts    = new student[list.Length];

            for (int i = 0; i < opts.Count; i++) {
                choices[i] = opts[i].abr;
            }
            Tuple<string, float, string[]> st;
            for (int i = 0; i < list.Length; i++) {
                st = list[i];
                if (choices.Length != st.Item3.Length) {
                    Console.Error.WriteLine(String.Format("Student {0} choices don't match number of options", st.Item1));
                    System.Environment.Exit(1);
                } else if (choices.Intersect(st.Item3).Count() != choices.Count()) {
                    Console.Error.WriteLine(String.Format("A choice specified by student {0} don't exist", st.Item1));
                    System.Environment.Exit(1);
                }

                option[] st_opts = new option[opts.Count];
                for (int c = 0; c < st_opts.Length; c++) {
                    foreach (option op in opts) {
                        if (String.Equals(st.Item3[c], op.abr)) {
                            st_opts[c] = op;
                            break;
                        }
                    }
                }
                sts[i] = new student(st.Item1, st.Item2, st_opts);
            }
            return sts;
        }

        static void Main(string[] args) {
            // instance options and check there is no duplicate
            List<option> opts = new List<option>();
            List<result> res  = new List<result>();
            fill_opts(opts, res);

            // find if number of options in student wishes match options
            // and get student options as array of option references
            student[] st_list = check_student_options(opts, students);

            // we first need to sort list according to notes
            sort(st_list);

            // giving each student best option
            int count = find_choice(opts, res, st_list);

            // Display results
            foreach (result r in res) {
                Console.WriteLine(r);
            }

            if (count != students.Length) {
                Console.WriteLine(String.Format("Number of people without any accepted choice: {0}", students.Length-count));
            }
        }
    }
}

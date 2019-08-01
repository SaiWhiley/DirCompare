using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCompare {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Script Compare");
            string dirOne = "";
            string dirTwo = "";

            while (!Directory.Exists(dirOne)) {
                Console.WriteLine("Directory1: ");
                dirOne = Console.ReadLine();
            }

            while (!Directory.Exists(dirTwo)) {
                Console.WriteLine("Directory2: ");
                dirTwo = Console.ReadLine();
            }

            List<String> FilesDirOne = Directory.GetFiles(dirOne).ToList();
            List<String> FilesDirTwo = Directory.GetFiles(dirTwo).ToList();
            int file1byte = new int();
            int file2byte = new int();
            List<String> FilesNotMatched = new List<string>();
            List<String> FilesNotExist = new List<string>();
            List<String> FilesMatched = new List<string>();

            foreach (String file in FilesDirOne) {
                string equivalentFile = dirTwo + "\\"+ Path.GetFileName(file);
                //if there is a match
                if (File.Exists(equivalentFile)) {
                    FileStream fs1 = new FileStream(file, FileMode.Open);
                    FileStream fs2 = new FileStream(equivalentFile, FileMode.Open);

                    //check lengths
                    if (fs1.Length != fs2.Length) {
                        fs1.Close();
                        fs2.Close();
                        FilesNotMatched.Add(file + " - lengths not match");
                    }
                    else {
                        //byte compare
                        do {
                            file1byte = fs1.ReadByte();
                            file2byte = fs2.ReadByte();
                        } while ((file2byte == file1byte) && file1byte != -1);
                        fs1.Close();
                        fs2.Close();
                        //file1byte will be equal to file2byte only if files are the same
                        if ((file1byte - file2byte) != 0) {
                            FilesNotMatched.Add(file + " - bytestream not match");
                        }
                        else {
                            FilesMatched.Add(file);
                        }
                    }
                }
                else {
                    FilesNotExist.Add(file);
                }
            }

            Console.WriteLine("Files with Diffs : ");
            foreach (String file in FilesNotMatched) {
                Console.WriteLine(file);
            }
            Console.WriteLine("\nFiles not Matched : ");
            foreach (String file in FilesNotExist) {
                Console.WriteLine(file);
            }
            Console.WriteLine("\nFiles matched : ");
            foreach(String file in FilesMatched) {
                Console.WriteLine(file);
            }
            Console.ReadKey();

        }
    }
}

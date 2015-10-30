using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Depedencies_Resolving
{
    class DependenciesResolving
    {
        // reading and saving the information from the .json files
        // modify file location(the path) for personal use
        public static JObject dependencies = JObject.Parse(File.ReadAllText(@"C:\Users\Nick\Desktop\project\dependencies.json"));
        public static JObject all_packages = JObject.Parse(File.ReadAllText(@"C:\Users\Nick\Desktop\project\all_packages.json"));

        // recursion function
        public static void Install(string package)
        {
            string path = "C:\\Users\\Nick\\Desktop\\project\\installed_modules\\" + package;
            if (Directory.Exists(path))
            {
                Console.WriteLine("{0} is already installed.", package);
            }
            else
            {
                foreach (var current_package in all_packages)
                {
                    if (current_package.Key == package)
                    {
                        Console.WriteLine("Installing {0}.", package);
                        Directory.CreateDirectory(path);

                        int numOfDependentPackages = current_package.Value.Count(); // number of dependent packages
                        if (numOfDependentPackages != 0)
                        {
                            Console.Write("In order to install {0}, we need ", package);
                            for (int i = 0; i < numOfDependentPackages; i++)
                            {
                                if (i != numOfDependentPackages - 1)    // if it's not the last iteration
                                {
                                    Console.Write("{0} ", current_package.Value[i]);
                                }
                                else if (i != 0)
                                {
                                    Console.WriteLine("and {0}.", current_package.Value[i]);
                                }
                                else  // this is the case where there is only one dependent package to be installed
                                {
                                    Console.WriteLine("{0}.", current_package.Value[i]);
                                }
                            }

                            // the actual recursion function:
                            for (int i = 0; i < numOfDependentPackages; i++)
                            {
                                Install(current_package.Value[i].ToString());
                            }

                        }
                    }
                }
            }
        }

        static void Main()
        {
            // from dependencies.json
            string dependencyPackage = "";

            foreach (var item in dependencies)
            {
                if (item.Key == "dependencies")
                {
                    dependencyPackage = item.Value.First.ToString();
                }
            }

            // installing the packages (using recursion function)
            Install(dependencyPackage);
            Console.WriteLine("All done.");
        }
    }
}

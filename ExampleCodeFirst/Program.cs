using ExampleCodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            int action = 0;
            using (EFContext context = new EFContext())
            {
                do
                {
                    Console.WriteLine("0. Exit");
                    Console.WriteLine("1. Add UserProfiles");
                    Console.WriteLine("2. Get all UserProfiles");
                    action = int.Parse(Console.ReadLine());
                    switch (action)
                    {
                        case 1:
                            {
                                UserProfile user = new UserProfile();
                                Console.WriteLine("Name: ");
                                user.Name = Console.ReadLine();
                                Console.WriteLine("Image: ");
                                user.Image = Console.ReadLine();
                                Console.WriteLine("Telephone: ");
                                user.Telephone = Console.ReadLine();
                                context.UserProfiles.Add(user);
                                context.SaveChanges();
                                break;
                            }
                        case 2:
                            {
                                foreach(var user in context.UserProfiles)
                                {
                                    Console.WriteLine($"Id: {user.Id}\t" +
                                        $"Name: {user.Name}\t" +
                                        $"Telephone: {user.Telephone}\t" +
                                        $"Image: {user.Image}");
                                }
                                break;
                            }
                        default:
                            break;
                    }

                } while (action != 0);
            }
        }
    }
}

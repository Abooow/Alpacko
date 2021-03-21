using Alpacko.Client.AdminConsole.Controllers;
using Alpacko.Client.AdminConsole.Models;
using Alpacko.Client.AdminConsole.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpacko.Client.AdminConsole.Views
{
    public class PostOfficeView : IView
    {
        private PostOfficeController postOfficeController;

        public PostOfficeView(PostOfficeController postOfficeController)
        {
            this.postOfficeController = postOfficeController;
        }

        public void Render()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("~~~~~ Post Office ~~~~~~");
                Console.WriteLine("1) Show all Post Offices");
                Console.WriteLine("2) Get Post Offices by Id");
                Console.WriteLine("3) Show all Users connected to a Post Office");
                Console.WriteLine("4) Create new Post Office");
                Console.WriteLine();
                Console.WriteLine("\"back\" to go back");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.Write("> ");
                string input = Console.ReadLine().Trim().ToLower();

                switch (input)
                {
                    case "1":
                        ShowAllPostOffices();
                        break;

                    case "2":
                        GetPostOfficeById();
                        break;

                    case "4":
                        CreatePostOffice();
                        break;

                    case "back":
                        return;
                }
            }
        }

        private void CreatePostOffice()
        {
            Console.Clear();
            Console.WriteLine("~~ Create Post Office ~~");
            PostOfficeModel postOffice = Validate.GetValidInstance<PostOfficeModel>();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");

            Console.Clear();
            Console.WriteLine("~~ Create Post Office ~~");
            ModelPrinter.PrintWithSkip(postOffice);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");

            Console.WriteLine();
            Console.WriteLine("Are you sure you want to save this Post Office? y/n");
            bool save = Input.GetYesOrNoInput();

            if (save)
            {
                postOfficeController.SavePostOffice(postOffice);
                Console.WriteLine("Post office successfully saved");
            }

            Console.WriteLine();
            Input.PressAnyKeyToContinue();
        }

        private void GetPostOfficeById()
        {
            Console.Clear();
            Console.WriteLine("Get Post Offices by Id");
            Console.Write("> ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    PostOfficeModel postOffice = postOfficeController.GetPostOffice(id).GetAwaiter().GetResult();
                    Console.WriteLine();
                    ModelPrinter.Print(postOffice, 4);
                }
                catch (Exception e)
                {
                    Utils.WriteLineInColor(e.Message, Utils.ErrorColor);
                }
            }
            else
            {
                Utils.WriteLineInColor("Not a valid Id, only numbers are allowed", Utils.ErrorColor);
            }

            Console.WriteLine();
            Input.PressAnyKeyToContinue();
        }

        private void ShowAllPostOffices()
        {
            Console.Clear();
            Console.WriteLine("Press Q or ESC to quit.");
            Console.WriteLine();

            int take = 2;
            int i = 0;
            while (true)
            {
                PostOfficeModel[] postOffices = postOfficeController.GetPostOffices(i, take).GetAwaiter().GetResult();

                foreach (PostOfficeModel postOffice in postOffices)
                {
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");
                    ModelPrinter.Print(postOffice, 4);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine();
                }
                i += take;

                if (postOffices.Length < take)
                    break;

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Q || key == ConsoleKey.Escape)
                    break;
            }

            Input.PressAnyKeyToContinue();
        }
    }
}
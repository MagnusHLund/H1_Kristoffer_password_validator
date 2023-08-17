namespace H1_Kristoffer_password_validator
{
    internal class Program
    {
        static void Main()
        {
            // Calls the controller
            Controller();
        }

        #region Controllers

        private static void Controller()
        {
            while (true)
            {
                Console.Clear();

                // Outputs the rules for a good and weak password
                SignInMenu();

                // Lets the user write their password, which becomes a char array
                char[] input = Console.ReadLine().ToCharArray();

                // Creates a string of the input
                string password = string.Join("", input);

                // Checks if the password length is valid
                if (password.Length! < 12 || password.Length! > 64)
                {
                    Errors("Pasword length should be between 12 and 64!");
                    continue;
                }

                // Checks if the password is not both upper and lowercase
                if (!UpperLowercase(input))
                {
                    Errors("Password should be both upper and lowercase!");
                    continue;
                }

                // Checks if the password has numbers
                if (!ContainsNumbers(input))
                {
                    Errors("Password should contain at least 2 numbers!");
                    continue;
                }

                // Check if the password has special characters
                if (!ContainsSpecials(input))
                {
                    Errors("Password should contain special characters!");
                    continue;
                }

                // Password is now valid but could be weak

                // Check if there are 4 identical characters in a row
                if (IdenticalCharacters(input))
                {
                    ValidErrors("You have 4 identical characters next to each other!");
                    continue;
                }

                // Check if there are 4 consecutive characters in a row
                if (IsConsecutive(password, input))
                {
                    ValidErrors("You have 4 consecutive characters!");
                    continue;
                }

                // password if secure
                ValidPassword(password);
            }
        }

        static bool UpperLowercase(char[] input)
        {
            // 2 booleans to keep track of if any character has been upper or lower
            bool upper = false;
            bool lower = false;

            string letters = "abcdefghijklmnopqrstuvwxyzæøå";

            // Runs the same amount of times, as the input length
            for (int i = 0; i < input.Length; i++)
            {
                // 2 if statements that check if the current char is upper or lower
                if (letters.Contains(input[i]))
                {
                    lower = true;
                }

                if (letters.ToUpper().Contains(input[i]))
                {
                    upper = true;
                }
            }

            // Return true if the input contains both upper and lower characters, else return false
            if (upper && lower)
            {
                return true;
            }

            return false;
        }

        static bool ContainsNumbers(char[] input)
        {
            // String with each number
            string numbers = "1234567890";

            // Counter makes sure that there are 2 numbers
            int counter = 0;

            // for loop to run through each character in input
            for (int i = 0; i < input.Length; i++)
            {
                // Checks if the input has any numbers, then adds 1 to the counter
                if (numbers.Contains(input[i]))
                {
                    counter++;
                }

                // Return true, if there are 2 numbers
                if (counter == 2)
                {
                    return true;
                }
            }

            return false;
        }

        static bool ContainsSpecials(char[] input)
        {
            // String containing different special characters
            string specials = "@£$€{[]}|!#¤%&/()=?`";

            // Runs through the length of the input array
            for (int i = 0; i < input.Length; i++)
            {
                // Checks if input[i] is a special character
                if (specials.Contains(input[i]))
                {
                    return true;
                }
            }

            return false;
        }

        static bool IdenticalCharacters(char[] input)
        {
            // Runs through the for loop, until i is length of input minus 4
            for (int i = 0; i < input.Length - 3; i++)
            {
                // Check if 4 chars in a row are the same character.
                if (input[i] == input[i + 1] && input[i + 1] == input[i + 2] && input[i + 2] == input[i + 3])
                {
                    return true;
                }
            }

            return false;
        }

        static bool IsConsecutive(string password, char[] input)
        {
            // string containing every character in order and reverse order
            string letters = "abcdefghijklnmopqrstuvwxyzæøå01234567890987654321åøæzyxwvutsrqpomnlkjihgfedcba";
            
            // Goes through the for loop for each letter in the letters string
            for (int i = 0; i < letters.Length-3; i++)
            {
                // Creates a substring, which starts on the i position and then takes the next 4 characters.
                string trimmed = letters.Substring(i, 4);

                // If password contains the substring, then return true
                if (password.Contains(trimmed))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Views

        static void ValidPassword(string password)
        {
            // Does some output in green text, changes back to white
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Valid password!");
            Console.WriteLine(password);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        static void Errors(string message)
        {
            // Changes the text color and outputs an error
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error:\n{message}\nPress enter to try again");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        static void ValidErrors(string message)
        {
            // Sets color to yellow and writes an error and then back to white
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Your password has passed, however it is weak\n{message}\nPress enter to continue");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        static void SignInMenu()
        {
            // Outputs the rules for good passwords
            Console.WriteLine("How to make a good password:\n" +
                "* Length between 12 and 64\n" +
                "* Password has to be upper and lowercase characters\n" +
                "* Password contains numbers\n" +
                "* Password has special characters");

            // Outputs the rules for weak passwords
            Console.WriteLine("\nTo avoid getting a weaker password, while still upholding the rules above:\n" +
                "* Password cannot have 4 consecutive numbers\n" +
                "* Password cannot have 4 indentical characters in a row");

            // Tells the user to write
            Console.WriteLine("\nWrite your password:");
        }

        #endregion
    }
}

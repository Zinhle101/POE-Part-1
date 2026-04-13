using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace userGreeting // start of namespace
{
    public class Program
    {
        static void Main()
        { // start of main method
            ask_user user_name = new ask_user();
            user_name.PlayWelcomeAudio(); 

            user_name.DisplayWelcomeMessage();
            user_name.prompt_name();
            
            chatty_response chatbot = new chatty_response();
            chatbot.bot_chat(user_name.GetName());
        }  // end of main method
    }

    public class ask_user // class that stores methods for conversation
    { // start of ask_user class
        public void PlayWelcomeAudio() // method for welcome audio
{ // start of welcome audio method
    try
    {
        string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Welcome_audio.wav");
        
        if (File.Exists(audioPath))  // if statement that searches for and plays the audio
        {
            SoundPlayer player = new SoundPlayer(audioPath);
            player.PlaySync();
        }
    }
    catch
    {
        // silently fail if audio doesn't play
    }
} // end of welcome audio method
        private string name = string.Empty;
        
        public void DisplayWelcomeMessage() // method for welcome message and logo
        { // start of welcome message method
            Console.ForegroundColor = ConsoleColor.Magenta; // AI generated logo
            Console.WriteLine(@"
   _____       _               _____        __        
  / ____|     | |             / ____|      / _|     
 | |     _   _| |__   ___ _ _| (___   ___ | |_ ___  
 | |    | | | | '_ \ / _ \ '__\___ \ / _ \|  _/ _ \ 
 | |____| |_| | |_) |  __/ |  ____) | (_) | ||  __/  
  \_____|\__, |_.__/ \___|_| |_____/ \___/|_| \___| 
          __/ |                                    
         |___/                                     

            CYBERSAFE
");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================--------------------------------========================");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                      [ Hello! Welcome to the CyberSafe Chatbot app ]          ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("======================--------------------------------========================");
            
            Console.ResetColor();
        } // end of display welcome message method
        
        public void prompt_name()  // method that prompts user to enter name
        { // start of propmt name method 
        
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Chatty: ");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Please enter your name: ");

            do
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Name : ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                name = Console.ReadLine();

                Console.ResetColor();

            } while (!check_name());
            
            display_welcome_response();
        } // end of prompt name method

        private Boolean check_name() // boolean method to check whether the entered name is valid
        { // start of check name method
            if (name == "" || string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Chatty: ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Oops! Please enter a valid name...");

                Console.ResetColor();

                return false;
            }
            
            return true;
        } // end of check name method
        
        private void display_welcome_response() // method for welcome response from chatty
        { // start of welcome response method 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Chatty: ");
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Welcome to the CyberSafe app, {name}! My name is Chatty, how can Ihelp you stay safe online!");
            
            Console.ResetColor();
        } // end of welcome response method 
        
        public string GetName() // getter to get name from other class
        {
            return name;
        }
    }

    public class chatty_response // class containing responses and words to ignore
    { // start of response class
        ArrayList passwordResponses = new ArrayList();
        ArrayList linkResponses = new ArrayList();
        ArrayList phishingResponses = new ArrayList();

        ArrayList ignoring = new ArrayList();

        int passwordIndex = 0; // index to count the responses for passwords
        int linkIndex = 0; // index to count the responses for links
        int phishingIndex = 0; // index to count the responses for phishing

        string lastTopic = "";
        int responsesPerChunk = 3;

        public void bot_chat(string name) // method for chatting between chatty and user 
        { // start of chat method 
            initialize_responses();
            initialize_ignore_words();

            string asking = "";

            do
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(name + " : ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                asking = (Console.ReadLine() ?? string.Empty).ToLower().Trim();

                Console.ResetColor();

            } while (Continue_chat(asking, name));
        } // end of chat method 

        private void initialize_responses() // initialize responses method
        { // start of initializing responses method
            passwordResponses.Add("use a strong password to protect your information");  // responses grouped according to topic
            passwordResponses.Add("make your password at least 8 characters long");
            passwordResponses.Add("use letters numbers and symbols");
            passwordResponses.Add("avoid personal information like your name or birthday");
            passwordResponses.Add("use different passwords for different accounts");
            passwordResponses.Add("change your passwords regularly");

            linkResponses.Add("be careful when clicking links from unknown sources"); // responses to link-related questions
            linkResponses.Add("check the full website address before clicking");
            linkResponses.Add("hover over links to preview them");
            linkResponses.Add("avoid clicking suspicious links");
            linkResponses.Add("shortened links can hide dangerous sites");

            phishingResponses.Add("be cautious of emails asking for personal information"); // responses to phishing-related questions
            phishingResponses.Add("phishing emails often create urgency");
            phishingResponses.Add("check the sender address carefully");
            phishingResponses.Add("do not click suspicious email links");
            phishingResponses.Add("legitimate companies do not ask for passwords via email");
        } // end of initializing responses method

        private void initialize_ignore_words()  // initialize words to be ignored
        { // start of ignored words method 
            ignoring.Add("what");
            ignoring.Add("is");
            ignoring.Add("how");
            ignoring.Add("the");
            ignoring.Add("about");
            ignoring.Add("tell");
            ignoring.Add("me");
            ignoring.Add("more");
            ignoring.Add("and");
            ignoring.Add("or");
        } // end of ignoring method

        private Boolean Continue_chat(string question, string name) // boolean method to check whether the question entered by user is valid 
        { // start of continue chat method 
            if (string.IsNullOrWhiteSpace(question))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Chatty: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Please ask me something!");
                Console.ResetColor();
                return true;
            }

            if (question == "exit") // if statement for the end of the chat when user types "exit"
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Chatty: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Goodbye! Stay safe on the web! " + name);
                Console.ResetColor();
                return false;
            }

            string topic = detect_topic(question); // method that uses keywords to identify the topic of the question

            if (topic == "" && lastTopic != "")
            {
                topic = lastTopic;
            }

            if (topic != "")
            {
                lastTopic = topic;
                show_chunk(topic);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Chatty: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("I'm not sure I understand. Try asking about passwords, phishing, or links.");
                Console.ResetColor();
            }

            Console.WriteLine();

            return true;
        } // end of continue chat method 

        private string detect_topic(string question)
        { // start of detect topic method 
            if (question.Contains("password"))
                return "password";

            if (question.Contains("link") || question.Contains("website") || question.Contains("url"))
                return "link";

            if (question.Contains("phishing") || question.Contains("email"))
                return "phishing";

            return "";
        } // end of detection topic method 

        private void show_chunk(string topic) // method that displays responses in smaller chunks 
        { // start of show chunk method 
            ArrayList list = null;
            int index = 0;

            if (topic == "password")
            {
                list = passwordResponses;
                index = passwordIndex;
            }
            else if (topic == "link")
            {
                list = linkResponses;
                index = linkIndex;
            }
            else if (topic == "phishing")
            {
                list = phishingResponses;
                index = phishingIndex;
            }

            if (list == null || list.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Chatty: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("No information available on this topic.");
                Console.ResetColor();
                return;
            }

            int count = 0;

            while (index < list.Count && count < responsesPerChunk)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Chatty: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine((count + 1) + ". " + list[index]);
                Console.ResetColor();
                
                index++;
                count++;
            }

            if (topic == "password")
                passwordIndex = index;
            else if (topic == "link")
                linkIndex = index;
            else if (topic == "phishing")
                phishingIndex = index;

            int remaining = list.Count - index;
            if (remaining > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[" + remaining + " more tips available on " + topic + "]");
                Console.ResetColor();
            }
            else if (index >= list.Count)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Chatty: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("That's all I have on this topic.");
                Console.ResetColor();
            }
        } // end of show chunk method 
    } // end of response class
} // end of namespace
using System.Linq;
using System.Collections.Generic;

namespace War_Game
{
    public class Main_Game
    {
        static void Main(string[] args)
        {
            string prompt = @"

 .------..------..------.     .------..------..------..------.
 |W.--. ||A.--. ||R.--. |.-.  |G.--. ||A.--. ||M.--. ||E.--. |
 | :/\: || (\/) || :(): ((5)) | :/\: || (\/) || (\/) || (\/) |
 | :\/: || :\/: || ()() |'-.-.| :\/: || :\/: || :\/: || :\/: |
 | '--'W|| '--'A|| '--'R| ((1)) '--'G|| '--'A|| '--'M|| '--'E|
 `------'`------'`------'  '-'`------'`------'`------'`------'
 
 Welcome to War Game! What would you like to do?
(Use the arrow keys to cycle through the options and press enter to select an option.)";

            string[] options = {"Play", "About", "Exit"};
            Menu mainManu = new Menu(prompt, options);
            int selectedIndex = mainManu.Run();

            switch(selectedIndex)
            {
                case 0:
                    //play
                    break;

                case 1:
                    Menu.DisplayAboutInfo();
                    break;

                case 2:
                    Menu.ExitGame();
                    break;
            }

            ConsoleApp.PrintAnimation("Insert nick name Player 1: ", 70);  // (C-APK)
            ConsoleApp.PrintAnimation("(no more than 12 characters)", 70);  // (C-APK)
            string nickNamePlayer1 = Console.ReadLine();           //Console APK (C-APK)

            ConsoleApp.PrintAnimation("Insert nick name Player 2: ", 70);  // (C-APK)
            ConsoleApp.PrintAnimation("(no more than 12 characters)", 70);  // (C-APK)
            string nickNamePlayer2 = Console.ReadLine();           //Console APK (C-APK)

            //ConsoleApp.Loading();

            #region Deck Creation

            Effect effect = new Effect();
            Deck deck1 = new Deck();
            Card card1 = new Card("Hermes", 1, 2, effect);
            deck1.cards.Add(card1);
            Card card2 = new Card("Hera", 3, 4, effect);
            deck1.cards.Add(card2);
            Card card3 = new Card("Hercules", 3, 5, effect);
            deck1.cards.Add(card3);
            Card card4 = new Card("Demeter", 2, 3, effect);
            deck1.cards.Add(card4);
            Card card5 = new Card("Afrodita", 1, 3, effect);
            deck1.cards.Add(card5);
            Card card6 = new Card("Apolo", 2, 3, effect);
            deck1.cards.Add(card6);
            Card card7 = new Card("Medusa", 4, 6, effect);
            deck1.cards.Add(card7);
            Card card8 = new Card("Poseidon", 3, 5, effect);
            deck1.cards.Add(card8);
            Card card9 = new Card("Hefesto", 4, 6, effect);
            deck1.cards.Add(card9);
            Card card10 = new Card("Atenea", 5, 9, effect);
            deck1.cards.Add(card10);
            Card card11 = new Card("Zeus", 6, 12, effect);
            deck1.cards.Add(card11);
            Card card12 = new Card("Ares!", 6, 12, effect);
            deck1.cards.Add(card12);
            #endregion          

            Random rand = new Random();
            var shuffled = deck1.cards.OrderBy(_ => rand.Next()).ToList();
            Player P1 = new Player(nickNamePlayer1, deck1);

            shuffled = deck1.cards.OrderBy(_ => rand.Next()).ToList();
            Player P2 = new Player(nickNamePlayer2, deck1);

            int turn = 1;
            string cont1nue = "";

            while (turn <= 6)
            {
                Console.Clear();
                ConsoleApp.PrintBoard(P1, P2);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Write something to continue: ");
                cont1nue = Console.ReadLine();
                Console.Clear();

                while (!P1.EndTurn)
                {
                    ConsoleApp.ItsYourTurn(P1);
                }
                while (!P2.EndTurn)
                {
                    ConsoleApp.ItsYourTurn(P2);
                }

                P1.EndTurn = false;
                P2.EndTurn = false;

                turn++;
            }

            Console.Clear();
            Console.WriteLine(WhoWon(P1, P2));

            void PlayACard(Player player, Card cardPlayed, int indexTerrain)
            {
                player.Terrains[indexTerrain].CardsPlayed.Add(cardPlayed);
                player.Terrains[indexTerrain].Conquest += cardPlayed.Conquest;
                player.CardsInHand.Remove(cardPlayed);
            }

            string WhoWon(Player one, Player two)
            {
                int onePlayerConquestTerrain = 0;
                int twoPlayerConquestTerrain = 0;

                for (int i = 0; i < 3; i++)
                {
                    if (one.Terrains[i].Conquest < two.Terrains[i].Conquest)
                    {
                        twoPlayerConquestTerrain++;
                    }
                    else onePlayerConquestTerrain++;
                }

                if (onePlayerConquestTerrain == twoPlayerConquestTerrain)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        onePlayerConquestTerrain += one.Terrains[i].Conquest;
                        twoPlayerConquestTerrain += two.Terrains[i].Conquest;
                    }

                    if (onePlayerConquestTerrain == twoPlayerConquestTerrain)
                    {
                        return "TIE!!!";
                    }

                    if (onePlayerConquestTerrain < twoPlayerConquestTerrain)
                    {
                        return two.NickName;
                    }
                    else return one.NickName;
                }

                if (onePlayerConquestTerrain < twoPlayerConquestTerrain)
                {
                    return two.NickName;
                }
                else return one.NickName;
            }
        }

    }
}
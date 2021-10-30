using System;
using static System.Console;

namespace x_403_lovink_mark_Projet
{
    class Program
    {

        /// <summary>
        /// Lovink Mark
        /// 08.09.2021
        /// ETML
        /// Le problème du cavalier
        /// </summary>


        static void Main(string[] args)

        {

            const int topCursor = 10; // Constante de la position de la table de jeu depuis le haut.
            const int leftCursor = 5; // Constante de la position de la table de jeu depuis la gauche.
            int posLeftLetNum = leftCursor; // Placer les lettres avec une varibale modifiable.
            int posTopLetNum = topCursor; // Placer les chiffres avec une varibale modifiable.
            int row = 8; // Nombres de lignes pour la table de jeu.
            int column = 8; // Nombres de colonnes pour la table de jeu.
            int x = leftCursor + 2;
            int y = topCursor + 1;
            string horse = "■"; // Le cheval

            int score = -100; // Les points du joueur.

            int possibilities = 0; // Nombre de cases non possible à jouer.

            char gameOn; // Valeur pour savoir si le joeur veut rejouer ou pas.
            bool gameStart = true; // Les valeurs du tableau de jeu sont correctes? 
            bool lost = false; // Valeur pour savoir si je joueur à perdu.
            bool win = false; // Valeur pour savoir si je joueur à gagné.
            bool gameOver = true; // Valeur pour savoir si je joueur continue de jouer.

            int intPosCol = 0; // Valeur pour savoir dans quelle colonne le curseur se trouve.
            int intPosRow = 0; // Valeur pour savoir dans quelle ligne le curseur se trouve.
            int[,] tabsGames = new int[row, column]; // Le tableau à 2 dimensions.

            ConsoleKeyInfo ckyKeyPress; // Valeur pour savoir sur quelle touche le joueur appuie.


            Title = "Le problème du cavalier"; // Titre de la console.
            do
            {


                while (gameOver) // Pour afficher une fois la table de jeu quand il lance ou relance le jeu.
                {

                    for (int i = 0; i < column; i++) // Attribuer une valeur de "3" dans tout le tableau à 2 dimensionss pour commencer ou l'on veut.
                    {
                        for (int j = 0; j < row; j++)
                        {
                            tabsGames[j, i] = 3;
                        }

                    }

                    GameTitle(); // Affichage du titre.

                    Rules(leftCursor, topCursor, row); // Affichage des règles de jeu.


                    GameField(row, column, topCursor, leftCursor); // Affichage du tableau de jeu.


                    SetCursorPosition(posLeftLetNum + 2, posTopLetNum - 1);


                    Alphabet(posLeftLetNum, posTopLetNum, column, row, leftCursor, topCursor); // affichage des chiffres et lettres autour du tableau de jeu.


                    SetCursorPosition(x, y);
                    gameOver = false; // Pour coupé la boucle d'affichage.
                }
                SetCursorPosition(intPosCol * 5 + x, intPosRow * 3 + y);

                if (row < 8 || row > 20 || column < 8 || column > 26)// Si les valeurs du tableau de jeu n'est pas adéquat le jeu ne s'affiche pas.
                {
                    Clear();
                    WriteLine("Merci de contrôler les valeurs du tableau de jeu!");
                    break;
                }

                ckyKeyPress = ReadKey(); // Connaître la touche joué par le joueur.



                if (ckyKeyPress.Key == ConsoleKey.RightArrow) // Pour pouvoir se déplacer dans la table de jeu sans y sortir.
                {

                    if (intPosCol < column - 1)
                    {
                        intPosCol++;
                    }



                }
                else if (ckyKeyPress.Key == ConsoleKey.LeftArrow)
                {
                    if (intPosCol > 0)
                    {
                        intPosCol--;
                    }

                }
                else if (ckyKeyPress.Key == ConsoleKey.UpArrow)
                {
                    if (intPosRow > 0)
                    {
                        intPosRow--;
                    }

                }
                else if (ckyKeyPress.Key == ConsoleKey.DownArrow)
                {
                    if (intPosRow < row - 1)
                    {
                        intPosRow++;
                    }

                }
                SetCursorPosition(intPosCol * 5 + x, intPosRow * 3 + y); // Pour placer le curseur dans la table de jeu et ce déplacer du bon nombre de cases.
                if (ckyKeyPress.Key == ConsoleKey.Enter) // Rentre dans la boucle si la touche "entrer" à été joué. 
                {
                    possibilities = 0; // A chauqe coup remettre les possibilités à zéro.

                    for (int i = 0; i < column; i++) // Parcour le tableau à 2 dimensions pour pouvoir y placer des valeurs afin de pouvoir jouer au jeu.
                    {
                        for (int j = 0; j < row; j++)
                        {


                            if (tabsGames[intPosRow, intPosCol] == 2 || tabsGames[intPosRow, intPosCol] == 3) // Si les conditions sont vrais le joueur peut effectuer le déplacment et affiché le cheval, son score augmente.
                            {

                                score += 100;
                                tabsGames[intPosRow, intPosCol] = 1;
                            }

                            if (tabsGames[j, i] == 3) // Une fois le cheval placé pour la première fois le tableau se remet à zéro.
                                tabsGames[j, i] = 0;


                            if (tabsGames[intPosRow, intPosCol] == 1 && tabsGames[j, i] == 2) // Regarde si toutes les options sont vrais pour effacé les choix "bleu" pour laisser la place aux nouvelles possibilités.
                            {
                                tabsGames[j, i] = 0;
                            }

                            if (tabsGames[j, i] == 0) // Si la valeur dans le tableau à 2 dimensions est de "0" les cases sont vides.
                            {
                                SetCursorPosition(i * 5 + x, j * 3 + y);
                                Write(" ");
                            }

                            if (tabsGames[j, i] == 1) // Si la valeur dans le tableau à 2 dimensions est de "1" il s'affiche en rouge pour montrer le déplacement effectué du joueur.
                            {
                                ForegroundColor = ConsoleColor.Red;
                                SetCursorPosition(i * 5 + x, j * 3 + y);
                                Write(horse);
                                ResetColor();
                            }

                            if (tabsGames[intPosRow, intPosCol] == 1) // Les possibilités vont s'afficher seulement si le joueur joue sur une case rouge. 
                            {
                                if (intPosCol < column - 2 && intPosRow >= 1 && tabsGames[intPosRow - 1, intPosCol + 2] != 4) // Affiche les possibilités seulement si la case est libre et qu'il soit dans la table de jeu.
                                {
                                    tabsGames[intPosRow - 1, intPosCol + 2] = 2;
                                }

                                if (intPosCol < column - 2 && intPosRow < row - 1 && tabsGames[intPosRow + 1, intPosCol + 2] != 4)
                                {
                                    tabsGames[intPosRow + 1, intPosCol + 2] = 2;
                                }
                                if (intPosCol < column - 1 && intPosRow > 1 && tabsGames[intPosRow - 2, intPosCol + 1] != 4)
                                {
                                    tabsGames[intPosRow - 2, intPosCol + 1] = 2;
                                }
                                if (intPosCol >= 1 && intPosRow > 1 && tabsGames[intPosRow - 2, intPosCol - 1] != 4)
                                {
                                    tabsGames[intPosRow - 2, intPosCol - 1] = 2;
                                }
                                if (intPosCol < column - 1 && intPosRow < row - 2 && tabsGames[intPosRow + 2, intPosCol + 1] != 4)
                                {
                                    tabsGames[intPosRow + 2, intPosCol + 1] = 2;
                                }
                                if (intPosCol >= 1 && intPosRow < row - 2 && tabsGames[intPosRow + 2, intPosCol - 1] != 4)
                                {
                                    tabsGames[intPosRow + 2, intPosCol - 1] = 2;
                                }
                                if (intPosCol > 1 && intPosRow < row - 1 && tabsGames[intPosRow + 1, intPosCol - 2] != 4)
                                {
                                    tabsGames[intPosRow + 1, intPosCol - 2] = 2;
                                }
                                if (intPosCol > 1 && intPosRow >= 1 && tabsGames[intPosRow - 1, intPosCol - 2] != 4)
                                {
                                    tabsGames[intPosRow - 1, intPosCol - 2] = 2;
                                }
                            }

                            if (tabsGames[j, i] == 2) // Dans le tableau à 2 dimensions tous les possibilités représenté par une valeur "2" sont affiché en bleu.
                            {
                                ForegroundColor = ConsoleColor.Blue;
                                SetCursorPosition(i * 5 + x, j * 3 + y);
                                Write(horse);
                                ResetColor();
                            }

                            if (tabsGames[j, i] != 2 && tabsGames[intPosRow, intPosCol] != 3 && tabsGames[intPosRow, intPosCol] != 0) // Comptes s'il y a encore des possibilités de jeu.
                                possibilities += 1;
                            if (possibilities == row * column) // Si le joueur n'a plus de possibilités il a perdu.
                                lost = true;
                        }
                    }

                    for (int i = 0; i < column; i++)
                    {
                        for (int j = 0; j < row; j++)
                        {
                            if (tabsGames[intPosRow, intPosCol] == 1) // Transforme la valeur "1" en "4" dans le tableau à 2 dimensions à la fin de l'affichage complet du tableau à 2 dimensions.
                                tabsGames[intPosRow, intPosCol] = 4;
                        }
                    }

                    if (score == row * column * 100 - 100) // Savoir si le joueur à gagner / s'il a le nombre de points max.
                        win = true;

                    SetCursorPosition(leftCursor + 5 * (column + 5), topCursor + 10); // Affiche le score du joueur à chauqe tour.
                    WriteLine("Vous avez : " + score);
                }

                if (win) // Si le joueur gagne le félicité
                {
                    Clear();
                    SetCursorPosition(30, 2);
                    WriteLine("Bravo vous avez gagné avec : " + score + " points");
                    do
                    {

                        Write("Jouer à nouveau ? (o/n) : ");
                        gameOn = char.Parse(ReadLine());
                        if (gameOn == 'o' || gameOn == 'O') // Savoir si le joueur veut recommencer.
                        {
                            lost = false;
                            win = false;
                            gameOver = true;
                            score = -100;
                            Clear();
                        }
                        else if (gameOn == 'n' || gameOn == 'N')
                        {
                            WriteLine("Merci d'avoir joué");
                            ReadLine();
                        }
                        else
                        {
                            WriteLine("Merci de choisir la bonne lettre");
                        }

                    } while (gameOn != 'o' && gameOn != 'O' && gameOn != 'n' && gameOn != 'N'); //Demande si le joeur veut rejouer tant qu'il n'a pas correctement répondu.
                    continue;
                }

                if (lost)
                {
                    Clear();
                    SetCursorPosition(30, 2);
                    WriteLine("Dommage vous avez perdu avec : " + score + " points sur " + (row * column * 100 - 100) + " points");
                    do
                    {

                        Write("Jouer à nouveau ? (o/n) : ");
                        gameOn = char.Parse(ReadLine());



                        if (gameOn == 'o' || gameOn == 'O')
                        {
                            lost = false;
                            win = false;
                            gameOver = true;
                            score = -100;
                            Clear();
                        }
                        else if (gameOn == 'n' || gameOn == 'N')
                        {
                            WriteLine("Merci d'avoir joué");
                            ReadLine();
                        }
                        else
                        {
                            WriteLine("Merci de choisir la bonne lettre");
                            win = true;
                        }
                    } while (gameOn != 'o' && gameOn != 'O' && gameOn != 'n' && gameOn != 'N'); //Demande si le joeur veut rejouer tant qu'il n'a pas correctement répondu.
                }



            } while (ckyKeyPress.Key != ConsoleKey.Escape && lost != true && win != true && gameStart != false); // Faire la boucle du jeu tant que le joueur n'a pas perdu ou veut arrêter. 
        }
        /// <summary>
        /// La méthode doit afficher le tableau de jeu grâce au valeur de row et de column
        /// </summary>
        /// <param name="leftCursor">constante pour commencé d'une valeur à gauche de la console</param>
        /// <param name="topCursor">constante pour commencé d'une valeur depuis le haut de la console</param>
        /// <param name="row">valeur pour le nombre de ligne du design graphique du jeu</param>
        /// <param name="column">valeur pour le nombre de colonne du design graphique du jeu</param>
        static void GameField(int row, int column, int topCursor, int leftCursor)
        {
            for (int j = 0; j < column && column <= 26; j++)
            {
                for (int i = 0; i < row && row <= 20; i++)
                {

                    SetCursorPosition((j * 5) + leftCursor, (i * 3) + topCursor);
                    WriteLine("╔═══╗");
                    SetCursorPosition((j * 5) + leftCursor, (i * 3) + 1 + topCursor);
                    WriteLine("║   ║");
                    SetCursorPosition((j * 5) + leftCursor, (i * 3) + 2 + topCursor);
                    WriteLine("╚═══╝");



                }
            }


        }

        /// <summary>
        /// La méthode doit afficher les lettres et les chiffres autour du jeu par rapport à la valeur de colum et row
        /// </summary>
        /// <param name="leftCursor">constante pour commencé d'une valeur à gauche de la console</param>
        /// <param name="topCursor">constante pour commencé d'une valeur depuis le haut de la console</param>
        /// <param name="row">valeur pour le nombre de ligne du design graphique du jeu</param>
        /// <param name="column">valeur pour le nombre de colonne du design graphique du jeu</param>
        /// <param name="posLeftLetNum">valeur modifiable pour la position des lettres autour de la table de jeu</param>
        /// <param name="posTopLetNum">valeur modifiable pour la position des nombre autour de la table de jeu</param>
        static void Alphabet(int posLeftLetNum, int posTopLetNum, int column, int row, int leftCursor, int topCursor)
        {
            for (int a = 64; a <= column + 64 && column <= 26; a++)
            {

                WriteLine((char)a);
                SetCursorPosition(posLeftLetNum + 2, posTopLetNum - 1);
                posLeftLetNum += 5;

            }

            posLeftLetNum = leftCursor; // Remettre les valeurs à leurs valeurs initiale.
            posTopLetNum = topCursor; // Remettre les valeurs à leurs valeurs initiale.


            SetCursorPosition(posLeftLetNum - 3, posTopLetNum - 2 + row * 3);
            for (int a = 1; a < row + 1 && row <= 20; a++)
            {


                WriteLine(a);
                SetCursorPosition(posLeftLetNum - 3, posTopLetNum - 5 + row * 3);
                posTopLetNum -= 3;


            }
        }

        /// <summary>
        /// La méthode doit afficher le titre du jeu
        /// </summary>
        static void GameTitle()
        {
            SetCursorPosition(30, 2);
            WriteLine("╔══════════════════════════════════════════════════╗");
            SetCursorPosition(30, 3);
            WriteLine("║ETML - Le problème du cavalier - MLK - v1.0 - 2021║");
            SetCursorPosition(30, 4);
            WriteLine("╚══════════════════════════════════════════════════╝");

        }

        /// <summary>
        /// La méthode doit afficher comment jouer et le score
        /// </summary>
        /// <param name="leftCursor">constante pour commencé d'une valeur à gauche de la console</param>
        /// <param name="topCursor">constante pour commencé d'une valeur depuis le haut de la console</param>
        /// <param name="row">valeur pour le nombre de ligne du design graphique du jeu</param>
        static void Rules(int leftCursor, int topCursor, int column)
        {
            SetCursorPosition(leftCursor + 5 * (column + 5), topCursor);
            WriteLine("Mode d'utilisation :");
            SetCursorPosition(leftCursor + 5 * (column + 5), topCursor + 1);
            WriteLine("═══════════════════");
            SetCursorPosition(leftCursor + 5 * (column + 5), topCursor + 2);
            Write("Déplacement");
            SetCursorPosition(leftCursor + 5 * (column + 5) + 19, topCursor + 2);
            WriteLine("Touches directionnelles");
            SetCursorPosition(leftCursor + 5 * (column + 5), topCursor + 3);
            Write("Jouer");
            SetCursorPosition(leftCursor + 5 * (column + 5) + 19, topCursor + 3);
            WriteLine("Enter");
            SetCursorPosition(leftCursor + 5 * (column + 5), topCursor + 4);
            Write("Quitter");
            SetCursorPosition(leftCursor + 5 * (column + 5) + 19, topCursor + 4);
            WriteLine("Escape");
            SetCursorPosition(leftCursor + 5 * (column + 5), topCursor + 8);
            WriteLine("Score :");
            SetCursorPosition(leftCursor + 5 * (column + 5), topCursor + 9);
            WriteLine("═══════════════════");



        }
    }
}                                                                                       //XIX
using Game.Display;
using Game.Engine.CharacterClasses;
using Game.Engine.Items;
using Game.Engine.Items.EpicItems;
using Game.Engine.Skills;
using System.Collections.Generic;
using System;
using Game.Engine.Skills.BasicWeaponMoves;
using Game.Engine.Skills.BasicSkills;
using Game.Engine.Skills.NewWeaponMoves;
using System.Windows.Documents;
using System.Windows.Controls;

namespace Game.Engine
{
    // the "main" class that commands the entire gameflow
    // in this file, only fields and properties are gathered

    [Serializable]
    public partial class GameSession
    {
        private int playerPosTop, playerPosLeft;
        [NonSerialized] private GamePage parentPage;
        private MetaMapMatrix metaMapMatrix;
        private MapMatrix mapMatrix;
        private List<int> itemPositions; // all item positions
        private List<Item> items; // active items only
        [NonSerialized] private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        [NonSerialized] private System.Windows.Forms.Timer timerPlayer = new System.Windows.Forms.Timer();
        public Player currentPlayer { get; set; }
        public bool[] AvailableMoves; // W,S,A,D
        public string CurrentKey { private get; set; }
        public Skill CurrentSelection { private get; set; }
        public bool RemovableItems 
        {
            get { return parentPage.RemovableItems; }
            set { parentPage.RemovableItems = value; }
        }
        public bool ItemSellFlag
        {
            get { return parentPage.ItemSellFlag; }
            set { parentPage.ItemSellFlag = value; }
        }
        public int PlayerPosTop
        {
            get { return playerPosTop; }
            set
            {
                playerPosTop = value;
                UpdateLocations();
            }
        }
        public int PlayerPosLeft
        {
            get { return playerPosLeft; }
            set
            {
                playerPosLeft = value;
                UpdateLocations();
            }
        }
        public GameSession(GamePage parentPage, string playerChoice)
        {
            // core
            this.parentPage = parentPage;
            currentPlayer = new Mage(this);
            if (playerChoice != null) { if (playerChoice.Contains("Warrior")) currentPlayer = new Warrior(this); }
            itemPositions = new List<int>();
            items = new List<Item>();
            parentPage.AddConsoleText("Welcome to the game!");
            RefreshStats();
            // map
            metaMapMatrix = new MetaMapMatrix(this);
            mapMatrix = metaMapMatrix.GetCurrentMatrix(0);
            //mapMatrix.Matrix[15, 15] = 3001;
            AvailableMoves = new bool[4];
            InitializeMapDisplay(0);
            // starting skills and items
            if (playerChoice != null)
            {
                if (playerChoice.Contains("Axe"))
                {
                    ProduceItem("item0003");
                    ProduceItem("item1560");
                    ProduceItem("item1561");
                    currentPlayer.Learn(new Punch());
                    currentPlayer.Learn(new AxeCut());
                }
                else if (playerChoice.Contains("Sword"))
                {
                    ProduceItem("item0004");
                    ProduceItem("item1562");
                    ProduceItem("item1560");
                    ProduceItem("item1561");
                    currentPlayer.Learn(new Punch());
                    currentPlayer.Learn(new SwordSlash());
                }
                else if (playerChoice.Contains("Spear"))
                {
                    ProduceItem("item0002");
                    ProduceItem("item1563");
                    ProduceItem("item1560");
                    ProduceItem("item1561");
                    ProduceItem("item1562");
                    currentPlayer.Learn(new Punch());
                    currentPlayer.Learn(new SpearStab());
                    currentPlayer.Learn(new LightSpear());
                }
                else if (playerChoice.Contains("Fire"))
                {
                    ProduceItem("item0001");
                    ProduceItem("item1560");
                    ProduceItem("item1561");
                    currentPlayer.Learn(new Hadouken());
                    currentPlayer.Learn(new FireArrow());
                }
                else if (playerChoice.Contains("Wind"))
                {
                    ProduceItem("item0001");
                    ProduceItem("item1560");
                    ProduceItem("item1561");
                    currentPlayer.Learn(new Hadouken());
                    currentPlayer.Learn(new WindGust());
                }
            }      
        }

        private void InitializeMapDisplay(int codeNumber)
        {
            parentPage.ClearMap();
            for (int i = 0; i < mapMatrix.Width; i++)
            {
                for (int j = 0; j < mapMatrix.Height; j++)
                {
                    // scan rows first
                    if (mapMatrix.Matrix[j, i] >= 3000 && mapMatrix.Matrix[j, i] < 5000)
                    {
                        parentPage.AddInteraction(i, j, mapMatrix.Matrix[j, i]);
                    }
                    if (mapMatrix.Matrix[j, i] >= 2000 && mapMatrix.Matrix[j, i] < 3000)
                    {
                        parentPage.AddPortal(i, j);
                    }
                    else if (mapMatrix.Matrix[j, i] == 1000)
                    {
                        parentPage.AddMonster(j * mapMatrix.Width + i, mapMatrix.HintMonsterImage(i, j), mapMatrix.Width);
                    }
                    else if (mapMatrix.Matrix[j, i] < 0)
                    {
                        parentPage.AddObstacle(i, j, -1 * mapMatrix.Matrix[j, i]);
                    }
                }
            }
            // move player
            if (codeNumber == 0)
            {
                bool found = false;
                for (int x = mapMatrix.Width - 2; x > 2; x--)
                {
                    for (int y = 2; y < mapMatrix.Height - 2; y++)
                    {
                        if (mapMatrix.Matrix[y, x] == 1)
                        {
                            playerPosLeft = x;
                            playerPosTop = y;
                            Grid.SetColumn(parentPage.Player, x);
                            Grid.SetRow(parentPage.Player, y);
                            break;
                        }
                    }
                    if (found) break;
                }
            }
            else if (codeNumber > 0)
            {
                bool found = false;
                for (int x = mapMatrix.Width - 2; x > 2; x--)
                {
                    for (int y = 2; y < mapMatrix.Height - 2; y++)
                    {
                        if (mapMatrix.Matrix[y, x] == codeNumber)
                        {
                            playerPosLeft = x;
                            playerPosTop = y;
                            Grid.SetColumn(parentPage.Player, x);
                            Grid.SetRow(parentPage.Player, y);
                            break;
                        }
                    }
                    if (found) break;
                }
            }
            Grid.SetColumn(parentPage.Player, playerPosLeft);
            Grid.SetRow(parentPage.Player, playerPosTop);
            AvailableMoves[0] = AvailableMoves[1] = AvailableMoves[2] = AvailableMoves[3] = false;
            if (playerPosLeft > 0 && mapMatrix.Matrix[playerPosTop, playerPosLeft - 1] > 0) AvailableMoves[2] = true;
            if (playerPosLeft < mapMatrix.Width && mapMatrix.Matrix[playerPosTop, playerPosLeft + 1] > 0) AvailableMoves[3] = true;
            if (playerPosTop > 0 && mapMatrix.Matrix[playerPosTop - 1, playerPosLeft] > 0) AvailableMoves[0] = true;
            if (playerPosTop < mapMatrix.Height && mapMatrix.Matrix[playerPosTop + 1, playerPosLeft] > 0) AvailableMoves[1] = true;
        }

    }
}

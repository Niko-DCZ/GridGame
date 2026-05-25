using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace GridGame
{
    public partial class Game : Form
    {
        PictureBox[,] grid = new PictureBox[10, 10];
        BoardPosition[,] board = new BoardPosition[10, 10]; // use this to figure out what is near it for the hover etc.
        Random r = new Random();
        List<Vector2> inUse = new List<Vector2>();
        List<Character> player1Characters = new List<Character>();
        List<Character> player2Characters = new List<Character>();

        List<BoardPosition> bpForP1 = new List<BoardPosition>();
        List<BoardPosition> bpForP2 = new List<BoardPosition>();

        List<Character> characterDidDoMoveList = new List<Character>();
        List<Character> characters = new List<Character>();

        List<BoardPosition> hoverEndList = new List<BoardPosition>();
        List<BoardPosition> hoverListForSplash = new List<BoardPosition>();

        //PictureBox chosen;
        BoardPosition chosen;
        BoardPosition previous;

        string state = "setup";

        GameManagement gm = new GameManagement("John", "Max");

        System.Windows.Forms.Timer debugTimer = new System.Windows.Forms.Timer();

        public Game()
        {
            InitializeComponent();
            GenerateMap();
            SetCharactersIntoList();
            debugTimer.Interval = 1;
            debugTimer.Tick += DebugTick;
            debugTimer.Start();
        }

        void DebugTick(object sender, EventArgs e)
        {
            DebugBox.Items.Clear();
            DebugBox.Items.Add("Current State " + state);
            DebugBox.Items.Add("Chosen " + chosen);
            DebugBox.Items.Add("previous " + previous);
            if (chosen != null)
            {
                DebugBox.Items.Add("ccc " + chosen.character);
            }
            if (previous != null)
            {
                DebugBox.Items.Add("pcc " + previous.character);
            }
            DebugBox.Items.Add("HLFSC " + hoverListForSplash.Count);
            DebugBox.Items.Add("p1 " + gm.player1.ownedCharacters.Count);
            DebugBox.Items.Add("p2 " + gm.player2.ownedCharacters.Count);
            DebugBox.Items.Add("contents in txtbx " + txtCoord.Text);
            DebugBox.Items.Add("it works" + characters.Count);
            DebugBox.Items.Add(gm.currentPlayer.name + " name");
            DebugBox.Items.Add(gm.player1.boardPositions.Count + " playerCountFor1");
            DebugBox.Items.Add(gm.player2.boardPositions.Count + " playerCountFor2");
            DebugBox.Items.Add(gm.currentPlayer.boardPositions.Count + " playerCountForCurrent");
            DebugBox.Items.Add(gm.notCurrentPlayer.boardPositions.Count + " playerCountForNotCurrent");

        }
        int[] GetBoardPositionIndex(BoardPosition bp)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == bp)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }
        void GenerateMap()
        {
            int boxSize = mapPanel.Width / 10;
            int x = 0;
            int y = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    PictureBox p = new PictureBox();
                    p.Size = new Size(boxSize, boxSize);
                    BoardPosition current = new BoardPosition(p, new Vector2(i, j));

                    p.Tag = current;
                    if (i == 0 || j == 0 || i == 9 || j == 9)
                    {
                        p.Image = Image.FromFile("cog.png");
                        board[i, j] = null;
                    }
                    else
                    {
                        p.Image = Image.FromFile("GrassTexture.png");
                        p.ContextMenuStrip = new ContextMenuStrip();

                        board[i, j] = current;
                        p.MouseClick += Player_Click;
                        p.MouseHover += Hover;
                        p.MouseLeave += HoverEnd;
                    }
                    p.SizeMode = PictureBoxSizeMode.Zoom;
                    p.BorderStyle = BorderStyle.FixedSingle;
                    p.Location = new Point(x, y);

                    mapPanel.Controls.Add(p);
                    grid[i, j] = p;

                    x += boxSize;
                }
                x = 0;
                y += boxSize;
            }
            //spawnCharacters();
        }
        void SetCharactersIntoList()
        {
            characters.Clear();

            string connectionString = @"Server=(localdb)\MSSQLLocalDB;   
    Database=GridGameDB;
    Trusted_Connection=True;";

            /* what Server I am connecting to and the name
             * Name of Database
             * Verifying the connection
             */
            SqlConnection con = new SqlConnection(connectionString); // connect with the server

            con.Open(); // allow connection to database

            string query = "SELECT * FROM Characters"; //collecting all the characters to input them here

            SqlCommand command = new SqlCommand(query, con);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Character character = null;
                string CT = reader["CharacterType"].ToString();
                float health = Convert.ToSingle(reader["Health"]);
                float damage = Convert.ToSingle(reader["Damage"]);
                float tR = Convert.ToSingle(reader["TravelRange"]);
                int bV = Convert.ToInt32(reader["BlockVal"]);
                string iP = reader["ImagePath"].ToString();
                Image img = Image.FromFile(iP);
                string name = reader["Name"].ToString();

                if (CT == "Character")
                {
                    character = new Character(health, damage, (int)tR, bV, img, false, false, null, name);

                }
                else if (CT == "Ogre")
                {
                    float specialVal = Convert.ToSingle(reader["SpecialVal"]);
                    character = new Ogre(health, damage, (int)tR, bV, img, false, false, specialVal, null, name);
                }
                else if (CT == "Goblin")
                {
                    float specialVal = Convert.ToSingle(reader["SpecialVal"]);
                    character = new Goblin(health, damage, (int)tR, bV, img, false, false, specialVal, null, name);
                }
                else if (CT == "Gremlin")
                {
                    float specialVal = Convert.ToSingle(reader["SpecialVal"]);
                    character = new Gremlin(health, damage, (int)tR, bV, img, false, false, specialVal, null, name);
                }
                else if (CT == "RangedK_DAWG")
                {
                    float splashVal = Convert.ToSingle(reader["SplashVal"]);
                    int splashRange = Convert.ToInt32(reader["SplashRange"]);
                    character = new RangedK_DAWG(health, damage, (int)tR, bV, img, false, false, splashVal, splashRange, null, name);
                }
                lstCharacters.Items.Add(character);
            }

            reader.Close();
            con.Close();
        }
        void SetupCharacters()
        {
            gm.SetupPlayer(player1Characters, bpForP1, 1);
            gm.SetupPlayer(player2Characters, bpForP2, 2);

            RedrawMap();
        }
        /* legacy
        void spawnCharacters()
        {
            Character ThePlayer = new Character(20f, 6f, 2, 3, Image.FromFile("abraham.jpg"), false, false, gm.player1);
            RangedK_DAWG ThePlayerAlso = new RangedK_DAWG(4f, 50, 3, 1, Image.FromFile("Malcom.jpg"), false, false, 2f, 2, gm.player1);
            Ogre PartOfTheOtherPlayer = new Ogre(10f, 2f, 2, 3, Image.FromFile("kl.jpg"), false, false, 2f, gm.player2);
            Goblin Part0fTheOtherPlayer = new Goblin(4f, 2f, 3, 3, Image.FromFile("hariot.jpg"), false, false, 2, gm.player2);
            Gremlin PartOfThe0therPlayer = new Gremlin(8f, 3f, 3, 4, Image.FromFile("mlk.jpg"), false, false, 2, gm.player2);

       

            //   characters.AddRange(new Character[] { ThePlayer, ThePlayerAlso, Part0fTheOtherPlayer, PartOfTheOtherPlayer, PartOfThe0therPlayer });

            List<Vector2> usedPositions = new List<Vector2>();

            for (int i = 0; i < characters.Count; i++)
            {
                Vector2 location = new Vector2(r.Next(1, 9), r.Next(1, 9));
                if (Vector2.HasUsedPosition(usedPositions, location))
                {
                    i--;
                    continue;
                }
                Character current = characters[i];
                board[location.x, location.y].character = current;
                usedPositions.Add(location);
                if (player1Characters.Contains(current))
                {
                    bpForP1.Add(board[location.x, location.y]);
                }
                if (player2Characters.Contains(current))
                {
                    bpForP2.Add(board[location.x, location.y]);
                }
            }
   
            RedrawMap();

        }
        */
        void Player_Click(object sender, MouseEventArgs e)
        {
            chosen = (sender as PictureBox).Tag as BoardPosition;

            if (state == "setup")
            {
                txtCoord.Text = chosen.location.GetCoord();
                return;
            }

            Debug.WriteLine("Clicking Char");
            if (chosen == null)
            {
                Debug.WriteLine("Place is null");
            }
            Debug.WriteLine(chosen.location.ToString());
            if (e.Button == MouseButtons.Left)
            {
                if (chosen.character != null)
                {
                    ShowCharacterStats(chosen.character);
                }
                return;
            }

            if (state == "Move")
            {
                Move();
                return;
            }
            if (state == "Attack")
            {
                Attack();
                return;
            }

            Debug.WriteLine("FirstClick");

            dropDownMenu(sender as PictureBox);//right click         

        }
        void splashDamage()
        {
            Debug.WriteLine("Number of valid spaces = " + hoverListForSplash.Count);


            foreach (BoardPosition CharacterPosition in hoverListForSplash)
            {
                if (CharacterPosition.character == null)
                {
                    Debug.WriteLine("Character not present");
                    continue;
                }

                if (CharacterPosition.character == previous.character)
                {
                    Debug.WriteLine("Character is the same as previous character " + CharacterPosition.character.name);
                    continue;
                }

                if (CharacterPosition.character.player == gm.currentPlayer)
                {
                    Debug.WriteLine("Character belongs to current player " + CharacterPosition.character.name);
                    continue;
                }

                Debug.WriteLine("Character attacked " + CharacterPosition.character.name);
                hpDropping(previous.character, CharacterPosition.character);

            }
        }
        void Hover(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            BoardPosition bp = p.Tag as BoardPosition;

            if (chosen == null || bp == null)
            {
                return;
            }

            if (chosen.character is not RangedK_DAWG)
            {
                return;
            }

            if (state != "Attack")
            {
                return;
            }

            if (state == "Attack" && chosen.character.specialMoveUsed == true)
            {// make another list and add those positions to then list then use the attack function to give the damage to the existing locations
                hoverEndList.Clear();
                int[] pos = GetBoardPositionIndex(bp);
                int xPos = pos[0];
                int yPos = pos[1];
                if (bp.validPosition == true)
                {
                    //TOP ROW
                    if (xPos - 1 > 0 && yPos - 1 > 0) { hoverEndList.Add(board[xPos - 1, yPos - 1]); }
                    if (xPos - 1 > 0) { hoverEndList.Add(board[xPos - 1, yPos]); }
                    if (xPos - 1 > 0 && yPos + 1 < 10) { hoverEndList.Add(board[xPos - 1, yPos + 1]); }
                    //MIDDLE ROW           
                    if (xPos > 0 && yPos - 1 > 0) { hoverEndList.Add(board[xPos, yPos - 1]); }
                    if (xPos > 0) { hoverEndList.Add(board[xPos, yPos]); }
                    if (xPos > 0 && yPos + 1 < 10) { hoverEndList.Add(board[xPos, yPos + 1]); }
                    //BOTTOM ROW
                    if (xPos + 1 > 0 && yPos - 1 > 0) { hoverEndList.Add(board[xPos + 1, yPos - 1]); }
                    if (xPos + 1 > 0) { hoverEndList.Add(board[xPos + 1, yPos]); }
                    if (xPos + 1 > 0 && yPos + 1 < 10) { hoverEndList.Add(board[xPos + 1, yPos + 1]); }
                }
                else
                {
                    return;
                }


                ShowAttackHover();
            }
        }
        void dropDownMenu(PictureBox pic)
        {
            Debug.WriteLine(gm.currentPlayer.name);

            pic.ContextMenuStrip.Items.Clear();
            Debug.WriteLine("Running dropDown");
            BoardPosition bp = (BoardPosition)pic.Tag;
            //land menu
            if (!gm.currentPlayer.boardPositions.Contains(bp))
            {
                return;

            }
            if (bp.character == null && !bp.validPosition)
            {
                ToolStripMenuItem infoText = new ToolStripMenuItem();
                infoText.Text = "Grass";
                pic.ContextMenuStrip.Items.Add(infoText);
                return;
            }

            if (previous != null)
            {
                return;
            }


            //did move already
            if (bp.character.didDoSomething)
            {
                ToolStripMenuItem warning = new ToolStripMenuItem();
                warning.Text = "Did Move Already";
                pic.ContextMenuStrip.Items.Add(warning);
                return;
            }


            ToolStripMenuItem move = new ToolStripMenuItem();
            ToolStripMenuItem attack = new ToolStripMenuItem();
            ToolStripMenuItem rest = new ToolStripMenuItem();

            move.Text = "Move";
            attack.Text = "Attack";
            rest.Text = "Rest";

            pic.ContextMenuStrip.Items.Add(move);
            pic.ContextMenuStrip.Items.Add(attack);
            pic.ContextMenuStrip.Items.Add(rest);

            attack.Click += whatItem_Click;
            rest.Click += whatItem_Click;
            move.Click += whatItem_Click;

            if (bp.character.GetType().Name != "Character")
            {
                ToolStripMenuItem SM = new ToolStripMenuItem();
                SM.Text = "Special Move";
                pic.ContextMenuStrip.Items.Add(SM);
                SM.Click += whatItem_Click;
            }
        }
        void whatItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Debug.WriteLine(item.Text);

            if (item.Text == "Move")
            {
                Move();
                return;
            }
            if (item.Text == "Attack")
            {
                Attack();
            }
            if (item.Text == "Special Move")
            {
                Special_Move();
            }
            if (item.Text == "Rest")
            {
                Rest();
            }
        }
        new void Move()
        {
            if (state == "None")
            {
                Debug.WriteLine("through");

                Character selected = chosen.character;
                chosen.theBox.ContextMenuStrip.Items.Clear();

                if (selected != null)
                {
                    state = "Move";
                    ShowMovableArea(chosen);
                }
                previous = chosen;
                return;
            }
            if (state == "Move")
            {
                if (chosen.validPosition)
                {
                    chosen.MoveCharacterToLocation(previous.character);
                    gm.currentPlayer.boardPositions.Add(chosen);
                    gm.currentPlayer.boardPositions.Remove(previous);
                    previous.RemoveCharacterFromLocation();
                    MoveComplete();
                }
            }
        }
        void Attack()
        {
            if (state == "None")
            {
                Character selected = chosen.character;
                if (selected != null)
                {
                    state = "Attack";
                    ShowAttackArea(chosen);

                }
                previous = chosen;
                return;
            }
            if (state == "Attack")
            {
                if (previous.character is RangedK_DAWG && previous.character.specialMoveUsed == true)
                {
                    splashDamage();
                }
                else if (previous.character != null)
                {
                    hpDropping();
                }
                chosen = previous;

                MoveComplete();
            }
        }
        /*
        void isCommanderDead(Character chosen)
        {
            if (chosen.commander == true&&chosen.health <=0)
            {
                //chosen.player.ownedCharacters.Remove(chosen.player.ownedCharacters);
                foreach (Character toRemove in chosen.player.ownedCharacters)
                {
                    chosen.player.ownedCharacters.Remove(toRemove);
                    
                }

            }
        }*/
        void Special_Move()
        {
            chosen.character.ApplySpecialMove();
            MoveComplete();
        }
        void Rest()
        {
            chosen.theBox.ContextMenuStrip.Items.Clear();
            MoveComplete();
        }
        void MoveComplete()  // readjust this function so that i can do this program with no issues
        {
            chosen.theBox.ContextMenuStrip.Items.Clear();
            RedrawMap();
            chosen.character.didDoSomething = true;
            previous = null;
            state = "None";
            endGame();
            CheckIfTurnEnds();
        }

        void CheckIfTurnEnds()
        {
            characterDidDoMoveList.Add(chosen.character);
            Debug.WriteLine(characterDidDoMoveList.Count);
            Debug.WriteLine("it goes");
            bool hasAtLeastSome = gm.currentPlayer.ownedCharacters.Count(c => characterDidDoMoveList.Contains(c)) >= gm.currentPlayer.ownedCharacters.Count;
            Debug.WriteLine(hasAtLeastSome);
            if (hasAtLeastSome)
            {
                gm.Swap();
            }

            if (characterDidDoMoveList.Count == characters.Count)
            {
                Debug.WriteLine("removing from list");

                foreach (Character character in characterDidDoMoveList)
                {
                    character.didDoSomething = false;
                }
                characterDidDoMoveList.Clear();
                Debug.WriteLine(characterDidDoMoveList.Count);
                ResetDropDown();
            }
        }
        void ShowMovableArea(BoardPosition bp)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    BoardPosition current = board[i, j];
                    if (current == null || current == bp)
                    {
                        continue;
                    }
                    if (current.IsWithinRange(bp))
                    {
                        if (current.character == null)
                        {
                            current.theBox.ContextMenuStrip.Items.Clear();
                            current.theBox.Image = Image.FromFile("GrassTextureRed.png");
                            // current.theBox.Image.Tag = "Red";
                            current.validPosition = true;
                        }

                    }
                }
            }
        }
        void hpDropping()
        {
            Character attacker = previous.character;
            Character victimer = chosen.character;
            float damage = 0;
            if (victimer != null)
            {
                if (attacker.damage > victimer.block)
                {
                    damage = attacker.damage - victimer.block;
                }
                victimer.health = victimer.health - (int)damage;

                if (victimer.health <= 0)
                {
                    chosen.character = null;
                    characters.Remove(victimer);
                    gm.notCurrentPlayer.ownedCharacters.Remove(victimer);
                }

            }

        }
        void hpDropping(Character attacker, Character victimer)
        {

            Debug.WriteLine("it runs hp(a,v)");
            float damage = 0;
            if (victimer != null)
            {
                if (attacker.damage > victimer.block)
                {
                    damage = attacker.damage - victimer.block;
                }
                victimer.health = victimer.health - (int)damage;
                /*
                if (victimer.health <= 0)
                {
                    Debug.WriteLine("character removed" + victimer.name);
                    chosen.character = null;
                    characters.Remove(victimer);
                    gm.notCurrentPlayer.ownedCharacters.Remove(victimer);
                }*/
            }

        }

        void ResetDropDown()
        {
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    BoardPosition current = board[i, j];
                    current.theBox.ContextMenuStrip.Items.Clear();
                }
            }
        }
        //health 2// damage 5// travelrange 3 //block 1
        //health 20// damage 4// travel range 2/block 5
        void RedrawMap()
        {
            int characterCount = 0;
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    BoardPosition current = board[i, j];
                    current.validPosition = false;

                    if (current.character == null)
                    {
                        if (state == "Attack" && current.theBox.Image == Image.FromFile("GrassTextureYella.png"))
                        {
                            current.theBox.Image = Image.FromFile("GrassTextureYella.png");
                        }
                        else
                        {
                            current.theBox.Image = Image.FromFile("GrassTexture.png");
                        }
                    }
                    else
                    {
                        characterCount++;
                        current.theBox.Image = current.character.theImage;
                        Debug.WriteLine(current.character.name + " has " + current.character.health);
                        if (current.character.health <= 0)
                        {
                            gm.player1.ownedCharacters.Remove(current.character);
                            gm.player2.ownedCharacters.Remove(current.character);
                            current.character = null;
                            current.theBox.Image = Image.FromFile("GrassTexture.png");
                        }
                    }
                }
            }
            Debug.WriteLine("The character count is " + characterCount);
        }
        void ShowAttackArea(BoardPosition bp)
        {
            Character theCharacter = chosen.character;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    BoardPosition current = board[i, j];
                    if (current == null || current == bp)
                    {
                        continue;
                    }
                    if (current.IsInAttackRange(bp))
                    {

                        if (current.character == null)
                        {
                            current.theBox.ContextMenuStrip.Items.Clear();
                            current.theBox.Image = Image.FromFile("GrassTextureYella.png");
                            //   current.theBox.Image.Tag = "Yella";                    
                        }
                        current.validPosition = true;
                    }
                }
            }
        }
        void ShowAttackHover()
        {
            foreach (BoardPosition bp in hoverEndList)
            {
                bp.previousImage = bp.theBox.Image;
                if (bp.character == null)
                {
                    bp.theBox.Image = Image.FromFile("GrassTextureTest2.png");
                }
                if (bp.character != null)
                {
                    if (!hoverListForSplash.Contains(bp))
                    {
                        hoverListForSplash.Add(bp);
                        Debug.WriteLine("position and name is: " + bp.location.y + bp.location.x + "," + bp.character.name);
                    }
                }
            }
        }
        void HideAttackHover()
        {
            foreach (BoardPosition bp in hoverEndList)
            {
                bp.theBox.Image = bp.previousImage;
                hoverListForSplash.Remove(bp);
            }
        }
        void HoverEnd(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            BoardPosition bp = p.Tag as BoardPosition;
            if (state == "Attack")
            {
                HideAttackHover();
            }

        }
        void ShowCharacterStats(Character selected)
        {
            InfoBox.Items.Clear();
            InfoBox.Items.Add("Health-" + selected.health);
            InfoBox.Items.Add("Damage-" + selected.damage);
            InfoBox.Items.Add("Travel Range-" + selected.travelRange);
            InfoBox.Items.Add("Block-" + selected.block);
            InfoBox.Items.Add("Name = " + selected.name.ToString());
            SelectedPic.Image = selected.theImage;
            InfoBox.Items.Add("Did Move? - " + chosen.character.didDoSomething);
            InfoBox.Items.Add(selected.player.name);
            InfoBox.Items.Add(selected);

            Update();
        }

        private void ConfirmSelection(object sender, EventArgs e)
        {
            if (txtCoord.Text == "")
            {
                return;
            }
            else
            {
                Vector2 coord = Vector2.FromString(txtCoord.Text);
                Character selected = lstCharacters.SelectedItem as Character;
                if (selected == null)
                {
                    return;
                }

                else
                {
                    if (coord.x > 0 && coord.y > 0 && coord.x < board.Length && coord.y < board.Length && !PositionUsed(coord))
                    {
                        lstCharactersConfirmed.Items.Add(selected);
                        characters.Add(selected);
                        lstCharacters.Items.Remove(selected);
                        inUse.Add(coord);
                        txtCoord.Text = "";

                        if (gm.currentPlayer == gm.player1)
                        {
                            bpForP1.Add(board[coord.x, coord.y]);
                            player1Characters.Add(selected);
                            board[coord.x, coord.y].character = selected;
                        }
                        else if (gm.currentPlayer == gm.player2)
                        {
                            bpForP2.Add(board[coord.x, coord.y]);
                            player2Characters.Add(selected);
                            board[coord.x, coord.y].character = selected;

                        }
                    }
                    else
                    {
                        return;
                    }
                }
                if (gm.currentPlayer == gm.player1 && player1Characters.Count == 3)
                {
                    gm.Swap();

                }
            }
        }
        bool PositionUsed(Vector2 coord)
        {
            foreach (Vector2 current in inUse)
            {
                if (current.x == coord.x && current.y == coord.y)
                {
                    return true;
                }
            }
            return false;
        }
        private void setupGameButton_Click(object sender, EventArgs e)
        {
            if (lstCharacters.Items.Count > 0)
            {
                return;
            }
            SetupCharacters();
            state = "None";
        }
        void endGame()
        {
            Debug.WriteLine("End Game Running");
            //Debug.WriteLine("")

            bool endGameAchieved = false;
            Player winningPlayer = null;

            if (gm.notCurrentPlayer.ownedCharacters.Count == 0)
            {
                endGameAchieved = true;
                winningPlayer = gm.currentPlayer;
            }
            if (gm.currentPlayer.ownedCharacters.Count == 0)
            {
                endGameAchieved = true;
                winningPlayer = gm.notCurrentPlayer;
            }

            if (endGameAchieved)
            {
                mapPanel.Visible = false;
                panelForWinning.BringToFront();
                panelForWinning.Visible = true;
                whichPlayerWins.Visible = true;
                whichPlayerWins.Text = winningPlayer.name + " wins";
            }
        }
    }
}

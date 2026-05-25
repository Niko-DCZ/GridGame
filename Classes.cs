using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;

namespace GridGame
{
    class Character
    {
        public float health;
        public float damage;
        public float travelRange;
        public int block;
        public Image theImage;
        public bool didDoSomething;
        public bool specialMoveUsed = false;
        public Player player;
        public string name;
       
        
        public Character(float h, float d, float tR, int b, Image img, bool m, bool sMU, Player pl,string n)
        {
            health = h;
            damage = d;
            travelRange = tR;
            block = b;
            theImage = img;
            didDoSomething = m;
            specialMoveUsed = sMU;
            player = pl;
            name = n;

        }

        public virtual void ApplySpecialMove()
        {

        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
    
    class Ogre : Character
    {
        public float damageIncrease;
       
        public Ogre(float h, float d, int tR, int b,Image img ,bool m,bool sMU,float dI,Player pl,string n) : base(h, d, tR, b, img,m,sMU,pl,n)
        {
            damageIncrease = dI;
        }
        public override void ApplySpecialMove()
        {
            if (!specialMoveUsed)
            {
                damage = damage * damageIncrease;
                specialMoveUsed = true;
            }
        }
    }// dmg increasefloat health = reader.GetFloat(...);
    
    class Gremlin : Character
    {
        public float healthIncrease;
        public Gremlin(float h, float d, int tR, int b,Image img, bool m , bool sMU, float hI, Player pl, string n) : base(h, d, tR, b, img, m, sMU, pl,n)
        {
            healthIncrease = hI;
        }
        public override void ApplySpecialMove()
        {
            health=healthIncrease * health;
            specialMoveUsed = true;

        }
    }//  health increase
    class Goblin : Character
    {
        public float travelRangeIncrease;
        public Goblin(float h, float d, float tR, int b,Image img , bool m , bool sMU, float tRI, Player pl, string n) : base(h, d, tR, b, img, m, sMU, pl,n)
        {
            travelRangeIncrease = tRI;
        }
        public override void ApplySpecialMove()
        {
            travelRange=travelRange*travelRangeIncrease;
            specialMoveUsed = true;
        }
    }// travel range increase
   
    class RangedK_DAWG : Character
    {
        public float splashDamage;
        public int splashRange;
        public bool hoverActivated = false;
        public RangedK_DAWG(float h, float d, int tR, int b, Image img, bool m,bool sMU, float sD, int sR,Player pl, string n) : base(h, d, tR, b, img, m, sMU, pl,n)
        {
            splashDamage = sD;
            splashRange = sR;
        }
        public override void ApplySpecialMove()
        {
            hoverActivated = true;
            specialMoveUsed = true;
        }
    }// splashDmg+splash Range

    class Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool HasUsedPosition(List<Vector2> theList, Vector2 theLocation)
        {
            foreach (Vector2 v2 in theList)
            {
                if (v2.x == theLocation.x && v2.y == theLocation.y)
                {
                    return true;
                }
            }
            return false;
        }

        public static Vector2 FromString(string value)
        {
            string[] valAsArray = value.Split(',');
            int xVal = int.Parse(valAsArray[0]);
            int yVal = int.Parse(valAsArray[1]);
            return new Vector2(xVal, yVal);
        }

        public string GetCoord()
        {
            return x + "," + y;
        }

        public override string ToString()
        {
            return "X:" + x + "," + "Y:" + y;
        }

    }
    class BoardPosition
    {
        public PictureBox theBox;
        public Character character = null;
        public bool validPosition = false;
        public Vector2 location;
        public Image previousImage = null;
        public BoardPosition(PictureBox theBox, Vector2 location)
        {
            this.theBox = theBox;
            this.location = location;
        }
        public bool IsWithinRange(BoardPosition bp)
        {
            Vector2 characterLoc = bp.location;
            int distx = characterLoc.x - location.x;
            if (distx < 0)
            {
                distx = distx * -1;
            }
            int disty = characterLoc.y - location.y;
            if (disty < 0)
            {
                disty = disty * -1;
            }
            if (distx < bp.character.travelRange && disty < bp.character.travelRange)
            {
                return true;
            }
            return false;
        }
        public bool IsInAttackRange(BoardPosition bp)
        {
            Character theCharacter = bp.character;
            Vector2 characterLoc = bp.location;
            if (theCharacter is Character)
            {
                int distx = characterLoc.x - location.x;
                if (distx < 0)
                {
                    distx = distx * -1;
                }
                int disty = characterLoc.y - location.y;
                if (disty < 0)
                {
                    disty = disty * -1;
                }
                if (distx == 1 && disty==1 || distx==1 && disty==0 || disty==1 && distx==0)
                {
                    return true;
                }
            }
            return false;
        }

        public void MoveCharacterToLocation(Character toMove)
        {
                 
                character = toMove;
                theBox.Image = toMove.theImage;
                character.didDoSomething = true;    
                
        }
        public void RemoveCharacterFromLocation()
        {
            character = null;
          
        }
    }
    class GameManagement
    {
        public Player player1;
        public Player player2;
        public int currentPlayerValue = 1;
        public Player currentPlayer;
        public Player notCurrentPlayer;

        public GameManagement(string name1, string name2)
        {
            player1 = new Player(name1);
            player2 = new Player(name2);
            currentPlayer = player1;
            notCurrentPlayer = player2;
        }
        public void Swap()
        {
            if (currentPlayer == player1)
            {
                currentPlayer = player2;
                notCurrentPlayer = player1;
            }
            else
            {
                currentPlayer = player1;
                notCurrentPlayer = player2;
            }
        }  
        public void SetupPlayer(List<Character> characters, List<BoardPosition> boardPositions, int playerNumber)
        {
            if (playerNumber == 1)
            {
                player1.ownedCharacters = characters;
                player1.boardPositions = boardPositions;
                foreach(Character c in characters)
                {
                    c.player = player1;
                }
            }
            else
            {

                player2.ownedCharacters = characters;
                player2.boardPositions = boardPositions;
                foreach (Character c in characters)
                {
                    c.player = player2;
                }
            }
        }
    }
    class Player
    {
        public string name;
        public List<Character> ownedCharacters = new List<Character>();
        public List<BoardPosition> boardPositions = new List<BoardPosition>();
        
        public Player(string name)
        {
            this.name = name;
        }
    }
}
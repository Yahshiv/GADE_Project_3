using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Game
{
    class Wizard : Unit
    {
        public Wizard(int x, int y, string team) : base(x, y, 20, 1, 10, 1.9, team, 'W')
        {
            name = "Wizard";
        }

        public override int XPos
        {
            get => xPos;
            set => xPos = value;
        }
        public override int YPos
        {
            get => yPos;
            set => yPos = value;
        }
        public override int Health
        {
            get => health;
            set => health = value;
        }

        public override int MaxHealth { get => maxHealth; }

        public override int Speed { get => speed; }

        public override string Team { get => team; }

        public override char Sym { get => sym; }

        public override bool IsDead { get => isDead; }


        public override void Attack(Unit target)//loops through the area around the Wizard and attacks all units with matching location values
        {
            for (int i = yPos - 1; i < yPos + 2; i++)
            {
                for (int j = xPos - 1; j < xPos + 2; j++)
                {
                    if (i == yPos && j == xPos)
                    {
                        continue;
                    }

                    for (int k = 0; k < Map.units.Length; k++)
                    {
                        if (Map.units[k].XPos == j && Map.units[k].YPos == i)
                        {
                            isBattling = true;
                            Map.units[k].Health -= atk;

                            if (Map.units[k].Health <= 0)
                            {
                                Map.units[k].Die();
                            }
                        }
                    }
                }
            }
        }

        public override void Attack(Building target)//cannot attack buildings
        {

        }

        public override void Die()
        {
            isDead = true;
            isBattling = false;
            sym = '~';
        }

        public override bool IsInRange(Unit target)
        {
            if (Math.Sqrt(Math.Pow(XPos - target.XPos, 2) + Math.Pow(YPos - target.YPos, 2)) <= range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool IsInRange(Building target)//cannot attack buildings
        {
            return false;
        }

        public override void Move(Unit target)
        {
            {
                if (Health >= maxHealth * 0.5)
                {
                    if (Math.Pow(XPos - target.XPos, 2) >= Math.Pow(YPos - target.YPos, 2))
                    {
                        if (XPos - target.XPos > 0)
                        {
                            XPos--;
                        }
                        else
                        {
                            XPos++;
                        }
                    }
                    else
                    {
                        if (YPos - target.YPos > 0)
                        {
                            YPos--;
                        }
                        else
                        {
                            YPos++;
                        }
                    }
                }
                else
                {
                    if (xPos == 0 && yPos == 0) //top left corner
                    {
                        if (rnd.Next(0, 2) == 0)
                        {
                            xPos++;
                        }
                        else
                        {
                            yPos++;
                        }
                    }
                    else if (xPos == 0 && yPos == GADEGame.MAPHEIGHT) //bottom left corner
                    {
                        if (rnd.Next(0, 2) == 0)
                        {
                            xPos++;
                        }
                        else
                        {
                            yPos--;
                        }
                    }
                    else if (xPos == GADEGame.MAPWIDTH && yPos == GADEGame.MAPHEIGHT) //bottom right corner
                    {
                        if (rnd.Next(0, 2) == 0)
                        {
                            xPos--;
                        }
                        else
                        {
                            yPos--;
                        }
                    }
                    else if (xPos == GADEGame.MAPWIDTH && yPos == 0) //top right corner
                    {
                        if (rnd.Next(0, 2) == 0)
                        {
                            xPos--;
                        }
                        else
                        {
                            yPos++;
                        }
                    }
                    else if (xPos == 0) //left edge
                    {
                        if (rnd.Next(0, 3) == 0)
                        {
                            xPos++;
                        }
                        else if (rnd.Next(0, 2) == 0)
                        {
                            yPos++;
                        }
                        else
                        {
                            yPos--;
                        }
                    }
                    else if (yPos == 0) //top edge
                    {
                        if (rnd.Next(0, 3) == 0)
                        {
                            yPos++; ;
                        }
                        else if (rnd.Next(0, 2) == 0)
                        {
                            xPos++;
                        }
                        else
                        {
                            xPos--;
                        }
                    }
                    else if (xPos == GADEGame.MAPWIDTH) //right edge
                    {
                        if (rnd.Next(0, 3) == 0)
                        {
                            xPos--;
                        }
                        else if (rnd.Next(0, 2) == 0)
                        {
                            yPos++;
                        }
                        else
                        {
                            yPos--;
                        }
                    }
                    else if (yPos == GADEGame.MAPHEIGHT) //bottom edge
                    {
                        if (rnd.Next(0, 3) == 0)
                        {
                            yPos--;
                        }
                        else if (rnd.Next(0, 2) == 0)
                        {
                            xPos++;
                        }
                        else
                        {
                            xPos--;
                        }
                    }
                    else
                    {
                        if ((rnd.Next(0, 4) == 0))
                        {
                            yPos++;
                        }
                        else if (rnd.Next(0, 3) == 0)
                        {
                            yPos--;
                        }
                        else if (rnd.Next(0, 2) == 0)
                        {
                            xPos++;
                        }
                        else
                        {
                            xPos--;
                        }
                    }
                }
            }
        }

        public override void Move(Building target)//cannot attack buildings
        {
            
        }

        public override Unit SeekTarget(Unit[] units)
        {
            double dist = int.MaxValue;
            double temp;
            Unit target = null;

            foreach (Unit u in units)
            {
                if (u == this || u.Team == team || u.IsDead)
                {
                    continue;
                }

                temp = Math.Sqrt(Math.Pow(XPos - u.XPos, 2) + Math.Pow(YPos - u.YPos, 2));

                if (temp < dist)
                {
                    dist = temp;
                    target = u;
                }
            }
            return target;
        }

        public override Building SeekTarget(Building[] buildings)//cannot attack buildings
        {
            return null;
        }

        public override string ToString()
        {
            return "Name: " + name + "\nPosition: " + XPos + ", " + YPos + " | Health: " + Health + "/" + maxHealth + " | Speed: " + speed + " | Attack" + atk + " | Range: " + range + " | Team: " + Team;
        }

        public override string Name { get => name; }

        public override string Save()
        {
            return name[0] + "," + xPos + "," + yPos + "," + health + "," + team;
        }
    }
}

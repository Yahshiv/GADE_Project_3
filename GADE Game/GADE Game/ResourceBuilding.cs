using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Game
{
    class ResourceBuilding : Building
    {
        public override int XPos { get => xPos; }

        public override int YPos { get => yPos; }

        public override int Health { get => health; set => health = value; }

        public override int MaxHealth { get => maxHealth; }

        public override string Team { get => team; }

        public override char Sym { get => sym; }

        public override int Speed { get => speed; }

        public override string Name { get => name; }

        string resType = "Cells";
        int resRate=6, resPool=100;
        public static int sharedResPool = 0;
        public static int resBlue = 0;
        public static int resRed = 0;

        public ResourceBuilding(int xPos, int yPos, string team) : base(xPos, yPos, 25, 1, team, 'P')
        {
            name = "Resource";
            sharedResPool += resPool;
        }

        public override void Die()
        {
            sym = 'W';
        }

        public override string ToString()
        {
            if(team == "RED")
            {
                return "Position: " + XPos + ", " + YPos + " | Health: " + Health + "/" + maxHealth + " | Team: " + Team + " | Resources: " + resRed + "/" + (sharedResPool+resRed);
            }
            return "Position: " + XPos + ", " + YPos + " | Health: " + Health + "/" + maxHealth + " | Team: " + Team + " | Resources: " + resBlue + "/" + (sharedResPool+resBlue);
        }

        public void genRes()//updated to work with static resource fields
        {
            if(resRate > sharedResPool)
            {
                if(team=="RED")
                {
                    resRed += resPool;
                    sharedResPool = 0;
                }
                else
                {
                    resBlue += resPool;
                    sharedResPool = 0;
                }
            }
            else
            {
                if (team == "RED")
                {
                    resRed += resRate;
                    sharedResPool -= resRate;
                }
                else
                {
                    resBlue += resRate;
                    sharedResPool -= resRate;
                }
            }
        }

        public override Unit Work()
        {
            genRes();
            return null;
        }

        public override string Save()
        {
            return xPos + "," + yPos + "," + team + "," + null + "," + health;
        }
    }
}

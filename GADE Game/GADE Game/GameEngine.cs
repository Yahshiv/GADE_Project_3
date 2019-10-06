using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE_Game
{
    class GameEngine
    {
        Map map;
        bool end = false;
        string winner = "";
        int round = 0;

        int mapHeight, mapWidth;

        public GameEngine(int numUnits, int numBuildings, int mapHeight, int mapWidth)
        {
            map = new Map(numUnits, numBuildings, mapHeight, mapWidth);
        }

        public bool End
        {
            get { return end; }
        }

        public string Winner
        {
            get { return winner; }
        }

        public int Round
        {
            get { return round; }
            set { round = value; }
        }

        public string GetMapStr
        {
            get { return map.GetMapStr(); }
        }

        public void Reset(int numUnits, int numBuildings, int mapHeight, int mapWidth)
        {
            map = new Map(numUnits, numBuildings, mapHeight, mapWidth);
            end = false;
            round = 0;
        }

        public void GameRound()
        {
            foreach(Unit u in map.Units)
            {
                if(u.IsDead || round%u.Speed != 0)
                {
                    continue;
                }

                Building targetB = u.SeekTarget(map.Buildings);

                if(targetB==null)
                {
                    Unit target = u.SeekTarget(map.Units);

                    if (target == null)
                    {
                        end = true;
                        winner = u.Team;
                        map.UpdateMap();
                        return;
                    }

                    if (u.Health > u.MaxHealth / 4 && u.IsInRange(target))
                    {
                        u.Attack(target);
                    }
                    else
                    {
                        u.Move(target);
                    }
                }
                else
                {
                    if (u.Health > u.MaxHealth / 4 && u.IsInRange(targetB))
                    {
                        u.Attack(targetB);
                    }
                    else
                    {
                        u.Move(targetB);
                    }
                }

            }

            foreach(Building b in map.Buildings)
            {
                if(b.Health<=0 || round%b.Speed != 0)
                {
                    continue;
                }

                Unit temp = b.Work();
                if(temp != null)
                {
                    Array.Resize(ref Map.units, map.Units.Length + 1);
                    Map.units[map.Units.Length - 1] = temp;
                }
            }

            map.UpdateMap();
            round++;
        }

        public RichTextBox GetInfo(RichTextBox tb)
        {
            tb.Text = map.GetUnitInfo(tb).Text + map.GetBuildingInfo(tb).Text;
            return tb;
        }

        public string UnitAtPos(int col, int row)
        {
            return map.UnitAtPos(col, row);
        }

        public void SaveBuildings()
        {
            map.SaveBuildings();
        }

        public void SaveUnits()
        {
            map.SaveUnits();
        }

        public void LoadUnits()
        {
            map.LoadUnits();
        }

        public void LoadBuildings()
        {
            map.LoadBuildings();
        }
    }
}

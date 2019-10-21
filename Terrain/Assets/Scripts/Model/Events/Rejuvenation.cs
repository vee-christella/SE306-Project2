using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rejuvenation : Event
{

    public Rejuvenation(Game game):base(5,5,5)
    {
        this.Type = EventType.Good;
        this.Game = game;
        this.Title = "Rejuvenation";
        this.Description = "Your abundance in green points has caused the earth to heal.";
        this.TileDeltaDesc = "Some desert tiles have been turned to fertile plain tiles.";
    }

    public override void TileDelta(Tile[,] tiles, bool doDestoryBuildings)
    {
        throw new NotImplementedException();
    }
}

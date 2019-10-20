using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rejuevnation : Event
{

    public Rejuevnation(Game game):base(5,5,5)
    {
        this.Type = EventType.Good;
        this.Game = game;
    }

    public override void TileDelta(Tile[,] tiles, bool doDestoryBuildings)
    {
        throw new NotImplementedException();
    }
}

using ASVPack.Models;
using SavegameToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPack.Extensions
{
    public static class GameObjectExtensions
    {

       

        public static ContentWildCreature AsWildCreature(this GameObject gameObject, GameObject statusObject)
        {
            return new ContentWildCreature(gameObject, statusObject);
        }

        public static ContentTamedCreature AsTamedCreature(this GameObject gameObject, GameObject statusObject)
        {
            return new ContentTamedCreature(gameObject, statusObject);
        }

        public static ContentStructure AsStructure(this GameObject gameObject)
        {
            return new ContentStructure(gameObject);

        }

        public static ContentItem AsItem(this GameObject gameObject)
        {
            return new ContentItem(gameObject);

        }

        public static ContentTribe AsTribe(this GameObject gameObject)
        {
            return new ContentTribe(gameObject);
        }

        public static ContentPlayer AsPlayer(this GameObject gameObject, GameObject statusObject)
        {
            return new ContentPlayer(gameObject, statusObject);
        }

        public static ContentDroppedItem AsDroppedItem(this GameObject gameObject)
        {
            return new ContentDroppedItem(gameObject);

        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPlus.AI
{
    public class Favorites<T>
    {
        /// <summary>
        /// Initializes a new instance of the Favorites class.
        /// </summary>
        public Favorites()
        {
        } // end constructor

        private List<T> list = new List<T>();

        public  IEnumerable<T> GetFavorites()
        {
            return this.list.ToArray();
        } // end function

        public void Add(T item) {

            if (this.list.Contains(item))
            {
                this.list.Remove(item);
            }

            this.list.Insert(0, item);
        } // end sub

    } // end class
} // end namespace

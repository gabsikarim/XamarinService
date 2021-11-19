using System;
using System.Collections.Generic;
using System.Text;

namespace Vives.DOMAIN.Helpers
{
    public class GObject
    {
        private bool success = true;

        public bool Successful
        {
            get { return success; }
        }

        private VivesException vex = null;

        public VivesException Vex
        {
            get { return vex; }
            set 
            {
                success = false;
                vex = value; 
            }
        }

    }
}

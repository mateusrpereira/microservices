﻿namespace Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public bool HasGuest
        {
            //Verificar se existem Bookings abertos para esta Room
            get { return true; }
        }
        public bool IsAvailable
        {
            get
            {
                if (this.InMaintenance || this.HasGuest)
                {
                    return false;
                }
                return true;
            }
        }
    }
}

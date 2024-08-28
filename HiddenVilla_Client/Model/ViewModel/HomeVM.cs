using System;

namespace HiddenVilla_Client.Model.ViewModel
{
    public class HomeVM
    {
        public DateTime startDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int NoOFNights { get; set; } = 1;
    }
}

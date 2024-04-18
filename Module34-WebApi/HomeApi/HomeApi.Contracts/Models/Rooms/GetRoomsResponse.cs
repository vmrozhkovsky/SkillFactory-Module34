﻿namespace HomeApi.Contracts.Models.Rooms
{
    public class GetRoomsResponse
    {
        public int RoomAmount { get; set; }
        public RoomView [] Rooms { get; set; }
    }

    public class RoomView
    {
        // public Guid Id { get; set; }
        // public DateTime AddDate { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public bool GasConnected { get; set; }
        public int Voltage { get; set; }
    }
}
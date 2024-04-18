﻿namespace HomeApi.Contracts.Models.Devices
{
    /// <summary>
    /// Запрос для обновления свойств комнаты
    /// </summary>
    public class EditRoomRequest
    {
        public string NewName { get; set; }
        public int NewArea { get; set; }
        public bool NewGasConnected { get; set; }
        public int NewVoltage { get; set; }
    }
}
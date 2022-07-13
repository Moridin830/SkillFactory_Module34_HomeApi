namespace HomeApi.Data.Queries
{
    /// <summary>
    /// Класс для передачи дополнительных параметров при обновлении комнаты
    /// </summary>
    public class UpdateRoomQuery
    {
        public string NewName { get; }
        public int NewArea{ get; }
        public bool NewGasConnected { get; set; }
        public int NewVoltage { get; set; }

        public UpdateRoomQuery(int newArea, bool newGasConnected, int newVotage, string newName = null)
        {
            NewName         = newName;
            NewArea         = newArea;
            NewGasConnected = newGasConnected;
            NewVoltage      = newVotage;
        }
    }
}